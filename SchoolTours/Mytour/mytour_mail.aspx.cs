using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SchoolToursBusiness;
using SchoolToursData.Object;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SchoolTours.Mytour
{
    public partial class mytour_mail : System.Web.UI.Page
    {
        //static int tour_id = 0;
        //static int person_id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.Cookies["CustomerFacing_person"].Value != "")
                    {
                        person_id.Value = Convert.ToString(Request.Cookies["CustomerFacing_person"].Value);
                    }
                    if (Request.Cookies["CustomerFacing_tour"].Value != "")
                    {
                        tour_id.Value = Convert.ToString(Request.Cookies["CustomerFacing_tour"].Value);
                    }
                    OnLoad();
                }
                catch (Exception ex)
                {
                    Response.Redirect("../mytour_index.aspx");
                }
            }
        }
        public void OnLoad()
        {
            //Execute pr_dtl_item(‘mytour_email’, @tour_id) which returns a single row recordset with the following columns: 1.producer_nm, 2.div_nm, 3.producer_eMail, 4.leader_eMail, 5.operator_eMail.  
            //● Display producer_nm and div_nm in div_to_nm.  ● Store producer_eMail to be used as recipient eMail address in sndMail() function. 
            //● If operator_eMail is not null, store this value to be used as the sender eMail address in the sndMail() function. 
            //● Otherwise store leader_eMail value to be used as the sender email address in the sndMail() function.

            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
            obj.mode = "mytour_email";
            obj.id1 = tour_id.Value.ToString();

            DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
            ViewState["dt"] = dt;
            if (dt.Rows.Count > 0)
            {
                div_to_nm.Text = dt.Rows[0]["producer_nm"].ToString() + ", " + dt.Rows[0]["div_nm"].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record found.')", true);
                return;
            }
        }

        public void goHome(object sender, EventArgs e)
        {
            Response.Redirect("mytour_dashboard");
        }
        public void sndMail(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];

            if (dt.Rows.Count > 0)
            {
                string eMail = dt.Rows[0]["producer_eMail"].ToString();
                string tBody = input_body.Text;
                string Subject = input_subject.Text;
                string BlindCopy = "";
                int response = SendMail_mailer(eMail, tBody, Subject, BlindCopy);
                if (response == 1)
                {
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "funSuccessfullyMailSend();", true);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('the user can make changes to the eMail and submit again')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record found.')", true);
                return;
            }
        }

        public int SendMail_mailer(string eMail, string tBody, string Subject, string BlindCopy)
        {
            try
            {
                string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
                int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
                int portNumber = PORT;
                bool enableSSL = true;
                string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string emailTo = eMail;
                string subject = Subject;
                string body = tBody;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);

                if (file_upload.HasFile)
                {
                    mail.Attachments.Add((new Attachment(file_upload.PostedFile.InputStream, file_upload.FileName)));
                }
                mail.To.Add(emailTo);
                mail.Bcc.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        protected void bttn_Ok_Click(object sender, EventArgs e)
        {
            Response.Redirect("mytour_dashboard");
        }
    }
}