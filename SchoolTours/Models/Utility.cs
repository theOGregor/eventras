using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using DKIM;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;

namespace SchoolTours.Models
{
   
    public class Utility
    {
        public string SendInvoice_old(string div_nm, string from_eMail, string to_eMail, string tBody, string inv_file_path)
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
                // mail.From = new MailAddress(emailFrom, from_eMail);
                mail.From = new MailAddress(from_eMail, null);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
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


                System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(PDFPath + inv_file_path);
                attachment = new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath("~/" + inv_file_path));
                //attachment = new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath(inv_file_path));
                mail.Attachments.Add(attachment);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    //smtp.EnableSsl = enableSSL;
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
        public string SendInvoice(string div_nm, string from_eMail, string to_eMail, string tBody, string inv_file_path)
        {
            try
            {
                string[] attachment = new string[1];
                attachment[0] = inv_file_path;
                SendInvoice_sendGrid(to_eMail, from_eMail, div_nm, tBody, attachment).Wait(100);
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        static async Task SendInvoice_sendGrid(string to_address, string from_address, string subj, string body, string[] attachment)
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
                            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/" + item);
                            var bytes = File.ReadAllBytes(path);
                            var file = Convert.ToBase64String(bytes);
                            string[] name = item.Split('_');
                            string file_name = "";
                            try { file_name = name[1]; } catch (Exception ex) { file_name = name[0]; }
                            msg.AddAttachment(item, file);
                        }
                    }
                    catch (Exception ex) { }
                }
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
                //var resposne = await client.SendEmailAsync(msg);
            }
            catch (Exception ex) { }
        }
    }
}