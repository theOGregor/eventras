using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SchoolToursBusiness
{
    public class UtilityBusiness
    {
        public string SendMail(string eMail, string Subject, string tBody)
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
                    return "1";
                }
                //return 0;
                return "0";
            }
            catch (Exception ex)
            {
                return "0";
            }
        }


        public string SendInvoice(string div_nm, string from_eMail, string to_eMail, string tBody, string inv_file_path)
        {
            //1.div_nm, 2.from_eMail, 3.to_eMail, 4.inv_filename.Send an eMail from @from_eMail to @to_eMail with the 
            //subject “Invoice from “ +@div_nm and a body saying “We have sent the attached invoice for your upcoming tour with “ +@div_nm “. 

            try
            {

                string PDFPath = System.Configuration.ConfigurationManager.AppSettings["PDFPath"];
                string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
                int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
                int portNumber = PORT;
                bool enableSSL = true;
                string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string emailTo = to_eMail;
                string subject = div_nm;
                string body = tBody;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom, from_eMail);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(PDFPath + inv_file_path);
                attachment = new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath("~/"+ inv_file_path));
                //attachment = new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath(inv_file_path));
                mail.Attachments.Add(attachment);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                    return "1";
                }
                //return 0;
                return "0";
            }
            catch (Exception ex)
            {
                return "0";
            }

            return "";
        }

    }
}
