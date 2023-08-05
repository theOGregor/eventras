using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursBusiness;
using SchoolToursData.Object;
using System.Data;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SchoolTours.Mytour
{
    public partial class mytour_pmt : System.Web.UI.Page
    {
        //static int tour_id = 0;
        //static int person_id = 0;
        //string PaymentJson = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Obj_global_Mytour_Login.inv_amt > 0)
                    {
                        input_amt.Text = Obj_global_Mytour_Login.inv_amt.ToString();
                        input_amt.ReadOnly = true;
                    }

                    if (Request.Cookies["CustomerFacing_person"].Value != "")
                    {
                        person_id.Value = Convert.ToString(Request.Cookies["CustomerFacing_person"].Value);
                    }
                    if (Request.Cookies["CustomerFacing_tour"].Value != "")
                    {
                        tour_id.Value = Convert.ToString(Request.Cookies["CustomerFacing_tour"].Value);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("../mytour_index.aspx");
                }
            }
        }

        public void goHome(object sender, EventArgs e)
        {
            Response.Redirect("mytour_dashboard");
        }
        public void setPmt(object sender, EventArgs e)
        {
            // Use the Authorize.net api to charge the credit card.If the API returns a success code, proceed to the next steps.If not successful, display error message from API. 
            //● If @ind_ind = 0, execute pr_set_item(‘pmt’, @tour_id, 0, 10, null, current date, @input_amt, ‘Online Payment’) which returns 1 if successful, 0 if failure. 
            //● If @ind_ind = 1, execute pr_set_item(‘pmt_ind’, @tour_id, @person_id, 0, 10, current date, @input_amt, ‘Online Payment’) which returns 1 if successful, 0 if failure.
            //● If successful, display a message “Your payment has been recorded.” and redirect the browser to mytour_dashboard.aspx. 
            //● If not successful, send an eMail to jrbiggs @cmginc.net with a subject line of “Online Payment Not Recorded in Minimizer” 
            //and whose body contains all payment information returned from the API.
            try
            {
                PaymentJson.Value = "";
                string Payresponse = AuthorizeOnlinePayment();

                if (Payresponse == "success")
                {
                    Obj_SET_ITEM obj = new Obj_SET_ITEM();

                    if (Convert.ToInt32(Request.Cookies["CustomerFacing_ind_ind"].Value) == 0)
                    {
                        obj.mode = "pmt";
                        obj.id1 = Convert.ToInt32(tour_id.Value);
                        obj.id2 = 0;
                        obj.id3 = 10;
                        obj.str1 = System.DateTime.Now.ToString();
                        obj.str2 = String.Format("{0:C}", input_amt.Text.Trim());
                        obj.str3 = "Online Payment";
                    }
                    else
                    {
                        obj.mode = "pmt_ind";
                        obj.id1 = Convert.ToInt32(tour_id.Value);
                        obj.id2 = Convert.ToInt32(person_id.Value);
                        obj.id3 = 0;
                        obj.id4 = 10;
                        obj.str1 = System.DateTime.Now.ToString();
                        obj.str2 = String.Format("{0:C}", input_amt.Text.Trim());
                        obj.str3 = "Online Payment";

                    }
                    DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];
                    if (Convert.ToInt32(dt_Result.Rows[0]["rc"].ToString()) > 0)
                    {
                        Response.Redirect("mytour_dashboard");
                    }
                    else
                    {
                        string err_msg = "\"" + dt_Result.Rows[0]["err_msg"].ToString() + "\"";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                    }
                }
                else
                {
                    try
                    {

                        string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
                        int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
                        int portNumber = PORT;
                        bool enableSSL = true;
                        string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                        string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                        string emailTo = "jrbiggs@cmginc.net";
                        string subject = "“Online Payment Not Recorded in Minimizer";
                        string body = Payresponse;
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(emailFrom);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                        //return 0;
                    }
                    catch (Exception ex)
                    {

                    }
                    dynamic stuff = JsonConvert.DeserializeObject(Payresponse);
                    string Error = stuff.errors.errorText;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Error + "')", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                string err_msg = "\"" + ex.Message + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }

        public string AuthorizeOnlinePayment()
        {

            try
            {
                string AuthorizeUrl = WebConfigurationManager.AppSettings["AuthorizeUrl"];
                string AuthorizeLoginId = WebConfigurationManager.AppSettings["AuthorizeLoginId"];
                string AuthorizeTransactionKey = WebConfigurationManager.AppSettings["AuthorizeTransactionKey"];

                #region Json
                StringBuilder SB = new StringBuilder();

                SB.Append("{");
                SB.Append("\"createTransactionRequest\":{");
                SB.Append("\"merchantAuthentication\": {");
                SB.Append("\"name\": \"" + AuthorizeLoginId + "\",");
                SB.Append("\"transactionKey\": \"" + AuthorizeTransactionKey + "\"");
                SB.Append("},");
                SB.Append("\"refId\": 123456,");
                SB.Append("\"transactionRequest\": {");
                SB.Append("\"transactionType\": \"authCaptureTransaction\",");
                SB.Append("\"amount\": \"" + String.Format("{0:C}", input_amt.Text.Trim()) + "\",");
                SB.Append("\"payment\": {");
                SB.Append("\"creditCard\": {");
                SB.Append("\"cardNumber\": \"" + input_card_nr.Text.Trim() + "\",");
                SB.Append("\"expirationDate\": \"" + input_exp.Text + "\",");
                SB.Append("\"cardCode\": \"" + input_cvv.Text + "\",");
                SB.Append("\"isPaymentToken\": true,");
                SB.Append("\"cryptogram\": \"EjRWeJASNFZ4kBI0VniQEjRWeJA=\"");
                SB.Append("}");
                SB.Append("},");

                SB.Append("\"billTo\": {");
                SB.Append("\"firstName\": \"" + input_nm.Text + "\",");
                SB.Append("\"lastName\": \"" + "" + "\", ");
                //SB.Append("\"company\": \"Souveniropolis\", ");
                SB.Append("\"address\": \"" + input_address.Text + "\", ");
                SB.Append("\"city\": \"" + input_city.Text + "\", ");
                SB.Append("\"state\": \"" + input_state.Text + "\", ");
                SB.Append("\"zip\": \"" + input_zip.Text + "\" ");
                //SB.Append("\"country\": \"USA\"");
                SB.Append("}");
                SB.Append("}");
                SB.Append("}");
                SB.Append("}");

                #endregion Json

                PaymentJson.Value = SB.ToString();
                string Result = "";
                HttpWebRequest webRequestA = (HttpWebRequest)System.Net.WebRequest.Create(AuthorizeUrl);
                webRequestA.Method = "POST";
                //webRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(webRequestA.GetRequestStream()))
                {
                    streamWriter.Write(SB.ToString());
                }
                webRequestA.ContentType = "application/json";
                var httpResponseA = (HttpWebResponse)webRequestA.GetResponse();
                using (var streamReader = new StreamReader(httpResponseA.GetResponseStream()))
                {
                    Result = streamReader.ReadToEnd();

                    dynamic stuff = JsonConvert.DeserializeObject(Result);
                    string coreId = stuff.messages.resultCode;
                    if (coreId == "Ok")
                        return "success";
                    else
                    {
                        return Result;
                        // coreId = ;A duplicate transaction has been submitted.
                        //return "error";
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        protected void input_zip_TextChanged(object sender, EventArgs e)
        {
            if (input_zip.Text != "")
            {
                zipLookup(input_zip.Text);
            }
        }
        public void zipLookup(string Zip)
        {
            #region use
            try
            {
                string zipcodeKey = WebConfigurationManager.AppSettings["zipcodeKey"];

                string url = "http://www.zipcodeapi.com/rest/" + zipcodeKey + "/info.json/" + Zip + "/radians";
                string TimeZone = "";
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers["X-Api-Key"] = "key";
                    client.Headers["X-Api-Secret"] = "secret";

                    string s = client.DownloadString(url);

                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic j = jsonSerializer.Deserialize<dynamic>(s);
                    string city = j["city"].ToString();
                    string state = j["state"].ToString();
                    TimeZone = j["timezone"]["timezone_abbr"].ToString();

                    input_city.Text = city;
                    input_state.Text = state;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showTimeZone('" + TimeZone + "');", true);
            }
            catch (WebException ex)
            {

                input_zip.Text = "";
                input_state.Text = "";
                input_city.Text = "";

                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                dynamic obj = JsonConvert.DeserializeObject(resp);
                var messageFromServer = obj.error_msg;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + messageFromServer + "')", true);
                //return messageFromServer;
            }
            #endregion  use
        }

        protected void radio_amt_inv_CheckedChanged(object sender, EventArgs e)
        {
            chgAmt();
        }

        protected void radio_amt_other_CheckedChanged(object sender, EventArgs e)
        {
            chgAmt();
        }
        public void chgAmt()
        {
            if (radio_amt_inv.Checked == true)
            {
                input_amt.Text = Obj_global_Mytour_Login.inv_amt.ToString();
                input_amt.ReadOnly = true;
            }
            else
            {
                input_amt.Text = "";
                input_amt.ReadOnly = false;
                input_amt.Focus();

            }
        }
    }
}