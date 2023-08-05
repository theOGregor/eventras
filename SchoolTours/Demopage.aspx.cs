using EASendMail;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace SchoolTours
{
    public partial class Demopage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetDefaultButton(TextBox1, Button1);
            SetDefaultButton1(TextBox2, Button2);
        }

        private void SetDefaultButton(TextBox txt, Button defaultButton)



        {
            txt.Attributes.Add("onkeydown", "funfordefautenterkey1(" + defaultButton.ClientID + ",event)");
        }
        private void SetDefaultButton1(TextBox txt, Button defaultButton)

        {
            txt.Attributes.Add("onkeydown", "funfordefautenterkey2(" + defaultButton.ClientID + ",event)");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Label1.Text = TextBox2.Text;
        }
        protected void ButtonIN_Click(object sender, EventArgs e)

        {
            message.Text = TextBox1.Text;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label2.Text = TextBox3.Text;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string aa = "";
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
            //    int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
            //    int portNumber = PORT;
            //    bool enableSSL = true;
            //    string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
            //    string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];

            //    string emailBcc = "sanjay@itsabacus.com";
            //    string emailTo = "acs.sanjay14@gmail.com";
            //    string subject = "DKIM test";
            //    string body = "Hello world";


            //    SmtpMail oMail = new SmtpMail("TryIt");

            //    // Set sender email address, please change it to yours
            //    oMail.From = emailFrom;

            //    // Set recipient email address, please change it to yours
            //    oMail.To = emailTo;

            //    // Set email subject
            //    oMail.Subject = subject;

            //    // Set email body
            //    oMail.TextBody = body;
            //    // oMail.AddAttachment("http://www.emailarchitect.net/webapp/img/logo.jpg");

            //    // Add DomainKeys and DKIM configuration file
            //    // Then this email will be signed automatically
            //    oMail.Headers.ReplaceHeader("X-DK-File", "c:\\emailarchitectdomainkeys.txt");

            //    // Your SMTP server address
            //    SmtpServer oServer = new SmtpServer(smtpAddress);

            //    // User and password for ESMTP authentication, if your server doesn't require
            //    // User authentication, please remove the following codes.
            //    oServer.User = emailFrom;
            //    oServer.Password = password;

            //    oMail.ReplyTo = emailBcc;
            //    oMail.Sender = emailBcc;
            //    // mail.ReplyTo = new MailAddress(emailBcc);
            //    //mail.From = new MailAddress(emailBcc, null);

            //    // Most mordern SMTP servers require SSL/TLS connection now.
            //    // ConnectTryTLS means if server supports SSL/TLS, SSL/TLS will be used automatically.
            //    //oServer.ConnectType = SmtpConnectType.ConnectTryTLS;

            //    // If your SMTP server uses 587 port
            //    oServer.Port = portNumber;

            //    // If your SMTP server requires SSL/TLS connection on 25/587/465 port
            //    // oServer.Port = 25; // 25 or 587 or 465
            //    // oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //    Console.WriteLine("start to send email ...");

            //    SmtpClient oSmtp = new SmtpClient();
            //    oSmtp.SendMail(oServer, oMail);

            //    Console.WriteLine("email was sent successfully!");
            //}
            //catch (SmtpTerminatedException exp)
            //{
            //    Console.WriteLine(exp.Message);
            //}
            //catch (SmtpServerException exp)
            //{
            //    Console.WriteLine("Exception: Server Respond: {0}", exp.ErrorMessage);
            //}
            //catch (System.Net.Sockets.SocketException exp)
            //{
            //    Console.WriteLine("Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message);
            //}
            //catch (System.ComponentModel.Win32Exception exp)
            //{
            //    Console.WriteLine("Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message);
            //}
            //catch (System.Exception exp)
            //{
            //    Console.WriteLine("Exception: Common: {0}", exp.Message);
            //}
        }

        protected void btn_mailKit_Click(object sender, EventArgs e)
        {
            try
            {

                string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
                int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
                int portNumber = PORT;
                bool enableSSL = true;
                string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];

                string emailBcc = "sanjay@itsabacus.com";
                string emailTo = "acs.sanjay14@gmail.com";
                string subject = "DKIM test";
                string body = "Hello world";

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (emailFrom,
                                         emailFrom
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (emailTo,
                                         emailTo
                                         ));
                mimeMessage.Subject = subject; //Subject  
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = body
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(smtpAddress, portNumber);
                    //client.Connect(smtpAddress, portNumber, false);
                    client.Authenticate(
                        emailFrom,
                        password
                        );
                    //await client.SendAsync(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully !!");
                    Console.ReadLine();
                    //await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_new_Click(object sender, EventArgs e)
        {

        }
    }
}