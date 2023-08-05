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
using System.Threading;
using System.Text;
using DKIM;
using System.Net.Mime;
using System.IO;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SchoolTours
{
    public partial class mass_mailer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                input_subject.Focus();
                OnLoad();
            }
        }
        public void OnLoad()
        {

            //Otherwise execute pr_dtl_item(‘emp_list’, @emp_id) which will return an
            //integer.Use this integer to update the value in div_nbr_items.

            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
            obj.mode = "emp_list";
            obj.id1 = (Session["emp_id"].ToString());


            DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

            int Count = Convert.ToInt32(ds.Tables[0].Rows[0]["nbr_emp_list"].ToString());
            if (Count == 0 || Count == 1)
                div_nbr_items.InnerText = "This Mailing Contains " + Count.ToString() + " Recipient";
            else
                div_nbr_items.InnerText = "This Mailing Contains " + Count.ToString() + " Recipients";

            //Execute pr_lst_items(‘emp_di vs, @emp_id) which returns a multiple row recordset with two columns: div_id, div_nm.
            //Use these values to populate the select_div_id object.

            Obj_LST_ITEMS obj_LST = new Obj_LST_ITEMS();
            obj_LST.mode = "emp_divs";
            obj_LST.id1 = Convert.ToInt32(Session["emp_id"].ToString());

            DataTable dt_divs = DTL_ITEM_Business.Get_LST_ITEMS(obj_LST).Tables[0];

            select_div_id.DataSource = dt_divs;
            select_div_id.DataTextField = "div_nm";
            select_div_id.DataValueField = "div_id";
            select_div_id.DataBind();
            select_div_id.Items.Insert(0, "Select");
        }

        public void sndMail(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            //execute pr_mailer(‘get_mm’, @emp_id, @select_div_id, @input_subject, @input_mail_txt)
            //which will return a multiple row recordset with the following columns.
            if (input_mail_txt.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please fill the mail body.')", true);
                return;
            }
            Obj_PR_MAILER obj = new Obj_PR_MAILER();
            obj.mode = "get_mm";
            obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
            if (select_div_id.SelectedValue != "Select")
                obj.id2 = Convert.ToInt32(select_div_id.SelectedValue);

            obj.str1 = input_subject.Text;
            obj.str2 = input_mail_txt.Text;
            try
            {
                obj.str2 = obj.str2.Replace("\r\n\r\n", "\r\n");
            }
            catch
            {
                obj.str2 = input_mail_txt.Text;
            }
            DataTable dt = DTL_ITEM_Business.Get_PR_MAILER(obj).Tables[0];
            if (dt.Rows[0]["from_address"].ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('from address is missing.')", true);
                return;
            }

            foreach (DataRow item in dt.Rows)
            {
                try
                {
                    //SendMail
                    int IsSendMail = SendMail(item);

                    if (IsSendMail == 1)
                    {
                        //Use the designated eMail protocol to send a message an HTML message with these columns.Also
                        //include the from_address as a BCC.After receiving confirmation of successful mail send, execute
                        //pr_mailer(‘record_mm’, @emp_id, @person_id, @subject), which returns 1 if success, 0 if failure.

                        Obj_PR_MAILER objRec = new Obj_PR_MAILER();
                        objRec.mode = "record_mm";
                        objRec.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                        objRec.id2 = Convert.ToInt32(item["person_id"].ToString());
                        objRec.str1 = input_subject.Text;

                        DataTable dtRec_mm = DTL_ITEM_Business.Get_PR_MAILER(objRec).Tables[0];

                    }
                }
                catch (Exception ex)
                {

                }
            }
            input_subject.Text = "";
            input_mail_txt.Text = "";
            select_div_id.SelectedValue = "Select";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Mail sent successfully!!!!')", true);
        }

        public int SendMail1(DataRow item)
        {
            try
            {
                string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
                int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
                int portNumber = PORT;
                bool enableSSL = true;
                string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string emailTo = item["to_address"].ToString();
                string emailBcc = item["from_address"].ToString();
                //emailTo = "acs.sanjay14@gmail.com";
                string subject = item["subject"].ToString();
                string body = item["message"].ToString();

                //string abcd = "http://43.229.227.26:81/gunaAgro/pdf/14290_Duplicate_For_Transporter.pdf";
                //body += "<br/><a href='" + abcd + "'>pdf</a> <br/>";


                string Url1 = Request.Url.AbsoluteUri;
                string[] splitUrl = Url1.Split('/');
                if (splitUrl[2] == "localhost:2108")
                {
                    Url1 = "http://localhost:2108/Doc";
                }
                else
                {
                    Url1 = "http://eventras.com/Doc";
                }

                if (file_upload.HasFile)
                {
                    foreach (HttpPostedFile file in file_upload.PostedFiles)
                    {
                        //mail.Attachments.Add((new Attachment(file.InputStream, file.FileName)));
                        file_upload.SaveAs(Server.MapPath("~/Doc/") + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName);
                    }
                }
                if (file_upload.HasFile)
                {
                    body += "please find the below mention file name & you can click on it & download those files.";
                }
                if (file_upload.HasFile)
                {
                    foreach (HttpPostedFile file in file_upload.PostedFiles)
                    {
                        string file_path = Url1 + "/" + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName; ;
                        body += "<br/><a href='" + file_path + "'>" + file.FileName + "</a>";
                    }
                }

                string Url = "http://eventras.com/Unsubscribe_Endpoint?Email=" + emailTo + "&div_nm=" + item[5].ToString();

                StringBuilder SB = new StringBuilder();
                SB.Append("<p>&nbsp;</p><br/><br/><br/><br/><br/><br/><br/><br/>");
                SB.Append("<table style=\"width: 100%;font-size: 10px;\"><tr><td style=\"text-align: center;\">");
                SB.Append(item[5].ToString() + " |7255 E Hampton Ave | Mesa, AZ 85205 | 800.289.6441 <br/>");
                SB.Append("This email was sent to you on behalf of " + item[5].ToString() + ".<br/>");
                SB.Append("Your email address is only being used with our company and will not be sold or distributed to anyone.<br/>");
                //SB.Append("<p>&nbsp;</p>");
                SB.Append("<a href='" + Url + "'>unsubscribe</a> <br/>");
                SB.Append("</td><tr></table>");
                body += SB.ToString();

                //string fotter = "<p>&nbsp;</p>";
                MailMessage mail = new MailMessage();

                //Commented by Meena and added below line   mail.From = new MailAddress(emailFrom, emailBcc);
                mail.From = new MailAddress(emailBcc, null);
                //mail.From = new MailAddress(emailBcc, "abc");
                //mail.Headers.Add("X-DK-File", "test.txt");

                mail.To.Add(emailTo);
                if (emailBcc != "")
                    mail.Bcc.Add(emailBcc);
                mail.Subject = subject;
                mail.Body = body;
                mail.ReplyTo = new MailAddress(emailBcc);

                mail.IsBodyHtml = true;

                //mail.BodyEncoding = UTF8Encoding.UTF8;
                //mail.BodyEncoding = Encoding.GetEncoding(1252);
                //mail.IsBodyHtml = true;
                ////htmlBody is a string containing the entire body text
                ////var htmlView = AlternateView.CreateAlternateViewFromString(body, new ContentType("text/html"));
                ////This does the trick
                ////htmlView.ContentType.CharSet = Encoding.UTF8.WebName;
                ////mail.AlternateViews.Add(htmlView);


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

                //if (file_upload.HasFile)
                //{
                //    foreach (HttpPostedFile file in file_upload.PostedFiles)
                //    {
                //        mail.Attachments.Add((new Attachment(file.InputStream, file.FileName)));
                //    }

                //}


                // mail.Attachments.Add(new Attachment(new MemoryStream(Encoding.UTF8.GetBytes("abc text content123")), "text.txt"));
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
                return 0;
            }
        }
        public int SendMail2(DataRow item)
        {
            try
            {
                string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
                int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
                int portNumber = PORT;
                bool enableSSL = true;
                string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string emailTo = item["to_address"].ToString();
                string emailBcc = item["from_address"].ToString();
                //emailTo = "acs.sanjay14@gmail.com";
                string subject = item["subject"].ToString();
                string body = item["message"].ToString();

                //string abcd = "http://43.229.227.26:81/gunaAgro/pdf/14290_Duplicate_For_Transporter.pdf";
                //body += "<br/><a href='" + abcd + "'>pdf</a> <br/>";


                //string Url1 = Request.Url.AbsoluteUri;
                //string[] splitUrl = Url1.Split('/');
                //if (splitUrl[2] == "localhost:2108")
                //{
                //    Url1 = "http://localhost:2108/Doc";
                //}
                //else
                //{
                //    Url1 = "http://eventras.com/Doc";
                //}

                //if (file_upload.HasFile)
                //{
                //    foreach (HttpPostedFile file in file_upload.PostedFiles)
                //    {
                //        //mail.Attachments.Add((new Attachment(file.InputStream, file.FileName)));
                //        file_upload.SaveAs(Server.MapPath("~/Doc/") + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName);
                //    }
                //}
                //if (file_upload.HasFile)
                //{
                //    body += "please find the below mention file name & you can click on it & download those files.";
                //}
                //if (file_upload.HasFile)
                //{
                //    foreach (HttpPostedFile file in file_upload.PostedFiles)
                //    {
                //        string file_path = Url1 + "/" + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName; ;
                //        body += "<br/><a href='" + file_path + "'>" + file.FileName + "</a>";
                //    }
                //}


                string Url = "http://eventras.com/Unsubscribe_Endpoint?Email=" + emailTo + "&div_nm=" + item[5].ToString();

                StringBuilder SB = new StringBuilder();
                SB.Append("<p>&nbsp;</p><br/><br/><br/><br/><br/><br/><br/><br/>");
                SB.Append("<table style=\"width: 100%;font-size: 10px;\"><tr><td style=\"text-align: center;\">");
                SB.Append(item[5].ToString() + " |7255 E Hampton Ave | Mesa, AZ 85205 | 800.289.6441 <br/>");
                SB.Append("This email was sent to you on behalf of " + item[5].ToString() + ".<br/>");
                SB.Append("Your email address is only being used with our company and will not be sold or distributed to anyone.<br/>");
                //SB.Append("<p>&nbsp;</p>");
                SB.Append("<a href='" + Url + "'>unsubscribe</a> <br/>");
                SB.Append("</td><tr></table>");
                body += SB.ToString();

                //string fotter = "<p>&nbsp;</p>";
                MailMessage mail = new MailMessage();

                //Commented by Meena and added below line   mail.From = new MailAddress(emailFrom, emailBcc);
                mail.From = new MailAddress(emailBcc, null);
                //mail.From = new MailAddress(emailBcc, "abc");
                //mail.Headers.Add("X-DK-File", "test.txt");

                mail.To.Add(emailTo);
                if (emailBcc != "")
                    mail.Bcc.Add(emailBcc);
                mail.Subject = subject;
                mail.Body = body;
                mail.ReplyTo = new MailAddress(emailBcc);

                mail.IsBodyHtml = true;

                //mail.BodyEncoding = UTF8Encoding.UTF8;
                //mail.BodyEncoding = Encoding.GetEncoding(1252);
                //mail.IsBodyHtml = true;
                ////htmlBody is a string containing the entire body text
                ////var htmlView = AlternateView.CreateAlternateViewFromString(body, new ContentType("text/html"));
                ////This does the trick
                ////htmlView.ContentType.CharSet = Encoding.UTF8.WebName;
                ////mail.AlternateViews.Add(htmlView);
                //mail.BodyEncoding = UTF8Encoding.UTF8;



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


                if (file_upload.HasFile)
                {
                    foreach (HttpPostedFile file in file_upload.PostedFiles)
                    {
                        //mail.Attachments.Add((new Attachment(file.InputStream, file.FileName)));// commented for send grid
                    }
                }

                // mail.Attachments.Add(new Attachment(new MemoryStream(Encoding.UTF8.GetBytes("abc text content123")), "text.txt"));
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
                return 0;
            }
        }
        public int SendMail(DataRow item)
        {
            try
            {
                string to_address = item["to_address"].ToString();
                //to_address = "acs.sanjay14@gmail.com";
                string from_address = item["from_address"].ToString();
                string subject = item["subject"].ToString();
                string body = item["message"].ToString();

                string Url = "http://eventras.com/Unsubscribe_Endpoint?Email=" + to_address + "&div_nm=" + item[5].ToString();

                StringBuilder SB = new StringBuilder();
                SB.Append("<p>&nbsp;</p><br/><br/><br/><br/><br/><br/><br/><br/>");
                SB.Append("<table style=\"width: 100%;font-size: 10px;\"><tr><td style=\"text-align: center;\">");
                SB.Append(item[5].ToString() + " |7255 E Hampton Ave | Mesa, AZ 85205 | 800.289.6441 <br/>");
                SB.Append("This email was sent to you on behalf of " + item[5].ToString() + ".<br/>");
                SB.Append("Your email address is only being used with our company and will not be sold or distributed to anyone.<br/>");
                //SB.Append("<p>&nbsp;</p>");
                SB.Append("<a href='" + Url + "'>unsubscribe</a> <br/>");
                SB.Append("</td><tr></table>");
                body += SB.ToString();
                int count = Convert.ToInt32(file_upload.PostedFiles.Count);
                string[] attachment = new string[count];
                int Count_int = 0;

                if (file_upload.HasFile)
                {
                    foreach (HttpPostedFile file in file_upload.PostedFiles)
                    {
                        //mail.Attachments.Add((new Attachment(file.InputStream, file.FileName)));
                        file_upload.SaveAs(Server.MapPath("~/Doc/") + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName);
                        attachment[Count_int] = Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName;
                        Count_int++;
                    }
                }
                //if (file_upload.HasFile)
                //{
                //    foreach (HttpPostedFile file in file_upload.PostedFiles)
                //    {
                //        attachment[Count_int] = file.FileName;
                //        Count_int++;
                //    }
                //}
                Mass_Mailer(to_address, from_address, subject, body, attachment).Start();
            }
            catch (Exception ex)
            { }
            return 1;
        }
        static async Task Mass_Mailer(string to_address, string from_address, string subj, string body, string[] attachment)
        {
            try
            {
                var apikey = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendGrid"]);

                //var FromEmail = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendGridEmail"]);
                var client = new SendGridClient(apikey);

                var from = new EmailAddress(from_address, from_address);
                var subject = subj;
                var to = new EmailAddress(to_address, to_address);
                //var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                msg.AddBcc(from_address);

                foreach (var item in attachment)
                {
                    try
                    {
                        if (item != null)
                        {
                            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Doc/" + item);
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

                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex) { }
        }
    }
}