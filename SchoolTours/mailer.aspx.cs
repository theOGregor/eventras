using SchoolToursData.Object;
using SchoolToursBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using DKIM;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SchoolTours
{
    public partial class mailer : System.Web.UI.Page
    {
        public static string prevPage = String.Empty;
        public static int Person_id = 0;
        public static string FilePath = "";
        public static string Mail_result = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FilePath = "";
                prevPage = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();
               
                if (Request.QueryString["type"] != null)
                {
                    Person_id = Convert.ToInt32(Request.QueryString["id"]);
                    pr_mailer(Request.QueryString["type"], Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        public void pr_mailer(string type, int id)
        {
            try
            {
                Obj_PR_MAILER obj = new Obj_PR_MAILER();
                obj.mode = type;
                obj.id1 = id;

                DataTable dt = DTL_ITEM_Business.Get_PR_MAILER(obj).Tables[0];
                ViewState["dt"] = dt;


                Obj_PR_MAILER obj_emp = new Obj_PR_MAILER();
                obj_emp.mode = "emp_emails";
                obj_emp.id1 = Convert.ToInt32(Session["emp_id"].ToString());

                DataTable dt_emp = DTL_ITEM_Business.Get_PR_MAILER(obj_emp).Tables[0];

                sending_eMail.DataSource = dt_emp;
                sending_eMail.DataTextField = "div_nm";
                sending_eMail.DataValueField = "eMail";
                sending_eMail.DataBind();
                sending_eMail.Items.Insert(0, "Select");
                try
                {
                    sending_eMail.SelectedValue = (dt.Rows[0]["sender_eMail"].ToString());
                    //sending_eMail.SelectedValue = ("bert@soundep.com");
                    // dt1.Rows[0]["sender_eMail"].ToString()
                }
                catch (Exception ex)
                {

                }
                if (dt.Rows.Count > 0)
                {
                    lbl_recipient_descr.Text = Convert.ToString(dt.Rows[0]["recipient_nm"].ToString() + "/" + dt.Rows[0]["recipient_entity"].ToString());
                    //txt_recipient_eMail.Text = Convert.ToString(dt.Rows[0]["recipient_eMail"].ToString());
                    input_subject.Text = Convert.ToString(dt.Rows[0]["subject_txt"].ToString());
                    //input_copy.Text = Convert.ToString(dt.Rows[0]["recipient_eMail"].ToString());
                    input_message.Text = Convert.ToString(dt.Rows[0]["body_txt"].ToString());

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record found.')", true);
                    return;
                }

                if (type == "curr_inv" || type == "past_inv")
                {
                    string File_Name = Convert.ToString(dt.Rows[0]["attachment"].ToString());

                    string wordDocName1 = type == "curr_inv" ? "/INVOICES/" : "/pdf/" + File_Name;
                    if (!File.Exists(Server.MapPath(wordDocName1)))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice not found.')", true);

                    }
                    else {
                        file_upload.SaveAs(Server.MapPath("~" + wordDocName1));
                        div_attach.Text = File_Name;
                        //Image1.ImageUrl = "~/images/dp/" + fname;
                        FilePath = wordDocName1;
                    }
                }
            }
            catch (Exception ex)
            {
                string err_msg = "\"" + ex.Message + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }
        protected void sndMail(object sender, EventArgs e)
        {
            //mail send 
            //successfully-  mail was sent successfully
            //fail - the user can make changes to the eMail and submit again



            //string eMail = "acs.sanjay14@gmail.com";
            //string eMail = txt_recipient_eMail.Text;

            string eMail = sending_eMail.SelectedValue;
            if (eMail == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email is not set for this division.')", true);
                return;
            }
            else if (eMail != "Select")
            {
                string tBody = input_message.Text;
                string Subject = input_subject.Text;
                string BlindCopy = input_copy.Text;

                //DataTable dt1 = (DataTable)ViewState["dt"];
                //string emailTo = dt1.Rows[0]["recipient_eMail"].ToString();
                //int Count_int = 0;
                //int count = Convert.ToInt32(Request.QueryString["type"] == "curr_inv" ? 1 : file_upload.PostedFiles.Count);
                //string[] attachment = new string[count];

                //if (Request.QueryString["type"] == "curr_inv")
                //{
                //    if (FilePath != "")
                //    {
                //        attachment[0] = FilePath;
                //    }
                //}
                //else if (file_upload.HasFile)
                //{
                //    foreach (HttpPostedFile file in file_upload.PostedFiles)
                //    {
                //        file_upload.SaveAs(Server.MapPath("~/Doc/") + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName);
                //        attachment[Count_int] = "~/Doc/" + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName;
                //        Count_int++;
                //    }
                //}
                //try
                //{
                //    Mailer(emailTo, eMail, Subject, tBody, attachment).Start();
                //}
                //catch (Exception ex) { }

                int response = SendMail_mailer(eMail, tBody, Subject, BlindCopy);
                if (response == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "funSuccessfullyMailSend();", true);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('the user can make changes to the eMail and submit again')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select sending_eMail   ')", true);
            }
        }
        public int SendMail_mailer(string eMail, string tBody, string Subject, string BlindCopy)
        {
            try
            {
                DataTable dt1 = (DataTable)ViewState["dt"];
                string subject = Subject;
                string body = tBody;
                string sender_eMail = eMail;
                string emailTo = dt1.Rows[0]["recipient_eMail"].ToString();

                int Count_int = 0;
                int count = 0;

                if (Request.QueryString["type"] == "curr_inv") count = 1;
                else if (Request.QueryString["type"] == "past_inv") count = 1;
                else count = Convert.ToInt32(file_upload.PostedFiles.Count);
                string[] attachment = new string[count];

                if (Request.QueryString["type"] == "curr_inv" || Request.QueryString["type"] == "past_inv")
                {
                    if (FilePath != "")
                    {
                        attachment[0] = FilePath;
                    }
                }
                else if (file_upload.HasFile)
                {
                    foreach (HttpPostedFile file in file_upload.PostedFiles)
                    {
                        file_upload.SaveAs(Server.MapPath("~/Doc/") + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName);
                        attachment[Count_int] = "~/Doc/" + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName;
                        Count_int++;
                    }
                }
                try
                {
                    Mailer(emailTo, eMail, subject, body, attachment).Wait(100);
                    // Mailer("itsabacus1@gmail.com", "acs.sanjay14@gmail.com", subject, body, attachment).Start();

                    //Mailer("itsabacus1@gmail.com", "sanjay@itsabacus.com", subject, body, attachment).Start();
                }
                catch (Exception ex) { }
                return 1;
            }
            catch (Exception ex)
            {
                string err_msg = "\"" + ex + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                return 0;
            }

        }


        static async Task Mailer(string to_address, string from_address, string subj, string body, string[] attachment)
        {
            try
            {
                var apikey = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendGrid"]);

                //var FromEmail = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendGridEmail"]);

                var client = new SendGridClient(apikey);
                var from = new EmailAddress(from_address, from_address);
                var subject = subj;
                var to = new EmailAddress(to_address, to_address);
                var htmlContent = body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                msg.AddBcc(from_address);

                //MailAddress rpto = new MailAddress("test@test.com");
                //var addresses = new[] { rpto };
                //myMessage.ReplyTo.li = addresses;

                //msg.ReplyTo(FromEmail);
                foreach (var item in attachment)
                {
                    try
                    {
                        if (item != null)
                        {
                            var path = System.Web.Hosting.HostingEnvironment.MapPath(item);
                            var bytes = File.ReadAllBytes(path);
                            var file = Convert.ToBase64String(bytes);
                            string[] name = item.Split('_');
                            string file_name = "";
                            try { file_name = name[1]; } catch (Exception ex) { file_name = name[0]; }
                            msg.AddAttachment(file_name, file);
                        }
                    }
                    catch (Exception ex) { }
                }

                var resposne = await client.SendEmailAsync(msg);
            }
            catch (Exception ex) { }
        }
        public int SendMail_mailer1(string eMail, string tBody, string Subject, string BlindCopy)
        {
            try
            {
                DataTable dt1 = (DataTable)ViewState["dt"];

                string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
                int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
                int portNumber = PORT;
                bool enableSSL = true;
                string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                //string emailTo = eMail;
                string subject = Subject;
                string body = tBody;

                var mail = new MailMessage();
                //mail.From = new MailAddress(emailFrom);
                //string sender_eMail = dt1.Rows[0]["sender_eMail"].ToString();

                string sender_eMail = eMail;
                string emailTo = dt1.Rows[0]["recipient_eMail"].ToString();

                mail.From = new MailAddress(sender_eMail, null);

                //mail.Headers.Add("X-DK-File", "test.txt");


                mail.To.Add(emailTo);
                mail.Bcc.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.ReplyTo = new MailAddress(sender_eMail);
                mail.IsBodyHtml = true;

                var privateKey = PrivateKeySigner.Create(@"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEAzAs4jLjd1kca5n4htaAelt2PWdapPfc5YsczvvrU58GePjDD
IaYP9P/+aEbOxg8GLwdbYSgJB7M1ypd5yexk54wgo1wgU6AMLgxu4a2cB6aEcyAd
NH0Koq9irhYuBa0/qjcT9Jm0MMLbIhdxutYzw5MP5KGvx8ONNvLd5N7cCw7zjVrp
Ak/01TSPXA+oHWlEbe8RuKds12/YsHp6zlPp1lhqD6LseyWIatahVDBuQVXhkVKq
9ydVSeHyCRvaqb44PlW2D6aobGY1k20HB3rCPNnr93FLJ5sOr5z/eMVeq/aFXCDO
IbywFQhhu/ha7Vpv0p0jOHJITSRQzzXcT+CQsQIDAQABAoIBAH2jgySTSHWCvvui
Ott9RpiawIQO+5MeQYWjJye3h5VU0T12BREZEcZIQryurO+jnKkknI3MexL0tHCU
qPc+yjsRO5+bQIR9jkJkgXoQznyfefrxkUoanIvj9p0/JwNz1DnZRD5ezmcf9JKf
YPYsox8P1L9xF62nqbJmBV/CIjfj2I6TJ1XfUcsROwKnu/4wfZue4aUvprIDwrp5
Wb8s/vl2uO431r3eyJ9ubLLRG/ajxVBO4Fje/UGSuOpyuj768QEfxnd21nPnHgVR
UZTM0g6jKfZLEpKcnnzLqsCx0kzCMqVEg4iMDSPmWUk2I1zGH2ffnhBNWEW3tJJp
pn8blskCgYEA98jOvHt2I72vefqH/Dk/nklq/bUXujjgX8r2CjHUorAirOWdKhsU
6uKRPiexBBdUZmWP6OUFuxC3fhdIoGTpN/yobCaj82DuEyZ/SfNEHeaCeIyuvfv0
AGNgWaHUtbqz4UUGlW4+8JKMh9AL0LEQKu0wx+/kVRQhhdT+kBKrmZcCgYEA0s8k
xQbNYoqXRvcdBvBTv4Rx00FhXOp+VtDceoEgBIKuDdOggVcCRm4GtZKY0sAg5W4X
R4wTu+SW9IO18KGY2UEM3ivD2lENKjNHppU4NbQvVBgxx3W6gwdKd7RtBMJ9gMii
XkaBoewZfRNfrWZqpaPYv4RRP5iTLV6wSDZXoPcCgYEAzwIUtaLvsCxozZ9gvHeX
jsYHfK4uhIW/7kfCBgJbgw9j6M5r3yGA+DsQ3LyMRr625FU1RX0QrJfqtIz/QAEO
VpfenXwqvMneHGGtNjrmTZSmq8/crRwxXaGofTmWW7z/StRAC9du/c1xWoWVWWST
/Ujr2B2yxOFsoEKx6euvMUECgYAQZFsPlv/RccVhl0WCjJ12fu3651KSzwkT5xm9
zNyYfTDbkmEgrYtXvqZ25/dKK/Zi4LSes521Nokmajdzhp1EB3Lgs7Z++15ysZoY
sfG0+1XSzC7Su6zNE3wO4tC3Vgg8Q12cxw69cIZq217NNPGF/7+S5M8Miuim1n4O
n2sg8QKBgDrEDwVYLubzSk1kE3N3pGS6NPqC+R780Dppni7BQ1U5oE5DNa8lsQ40
cJ0pApthi6lpBIy+MzHnD9DhvFXDe9HCF43frlSeNZiHovo8YpK2iIb+AKXzDhqg
J/M2ALvAvAzzSYgtOefqqhgsfF/LJw9g/1TCc7SaQh5QAzLy36hZ
-----END RSA PRIVATE KEY-----");
                var domainKeySigner = new DomainKeySigner(privateKey, "eventras.com", "eventras", new string[] { "From", "To", "Subject" });
                mail.DomainKeySign(domainKeySigner);


                var dkimSigner = new DkimSigner(privateKey, "eventras.com", "eventras", new string[] { "From", "To", "Subject" });
                mail.DkimSign(dkimSigner);

                if (Request.QueryString["type"] == "curr_inv")
                {
                    if (FilePath != "")
                    {
                        System.Net.Mail.Attachment Attachment;
                        Attachment = new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath("~/" + FilePath));
                        mail.Attachments.Add(Attachment);
                    }
                }
                else if (file_upload.HasFile)
                {
                    //mail.Attachments.Add((new Attachment(file_upload.PostedFile.InputStream, file_upload.FileName))); // commented for sendgrid
                }
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    //smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string err_msg = "\"" + ex + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                return 0;
            }

        }

        protected void bttn_Ok_Click(object sender, EventArgs e)
        {
            //execute pr_set_item(‘note’, @note_id, @person_id, @emp_id, 6, @input_subject)
            Obj_SET_ITEM obj = new Obj_SET_ITEM();

            obj.mode = "note";
            obj.id1 = Convert.ToInt32(0);
            obj.id2 = Convert.ToInt32(Person_id);
            obj.id3 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.id4 = Convert.ToInt32(6);
            obj.str1 = Convert.ToString(input_subject.Text);

            int Result = DTL_ITEM_Business.Put_SET_ITEM(obj);
            Response.Redirect(prevPage);
        }
    }
}