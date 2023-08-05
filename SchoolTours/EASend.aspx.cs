using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EASendMail;

using System.Net.Http;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading;

namespace SchoolTours
{
    public partial class EASend : System.Web.UI.Page
    {
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

        const string clientID = "524873686285-vrpttuuvv2fuclfjq6vaelt5lu0jc1uk.apps.googleusercontent.com";
        const string clientSecret = "pmEF0WaVFGZpJGm5RuogUbDy";
        const string scope = "openid%20profile%20email%20https://mail.google.com";
        const string authUri = "https://accounts.google.com/o/oauth2/v2/auth";
        const string tokenUri = "https://www.googleapis.com/oauth2/v4/token";

        string redirectUri = "http://localhost:2108/EASend";
        protected void Page_Load(object sender, EventArgs e)
        {
            // SendMailWithXOAUTH2("acs.sanjay14@gmail.com", "");
            //RegisterAsyncTask(new PageAsyncTask(DoOauthAndSendEmail));

            try
            {
                if (!IsPostBack)
                {
                    //EASend p = new EASend();
                    //if (Request.QueryString["code"] == null)
                    //{
                    //    p.DoOauthAndSendEmail();
                    //}
                    //else if (Request.QueryString["code"] != null)
                    //{
                    //    string code = Convert.ToString(Request.QueryString["code"]);
                    //    p.getAccesstoken(code);
                    //}

                }
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.ToString());
            }

        }

        void SendMailWithXOAUTH2(string userEmail, string accessToken)
        {
            try
            {
                // Gmail SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");
                // enable SSL connection
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                // Using 587 port, you can also use 465 port
                oServer.Port = 587;

                // use Gmail SMTP OAUTH 2.0 authentication
                oServer.AuthType = SmtpAuthType.XOAUTH2;
                // set user authentication
                oServer.User = userEmail;
                // use access token as password
                oServer.Password = accessToken;

                SmtpMail oMail = new SmtpMail("TryIt");
                // Your Gmail email address
                oMail.From = userEmail;

                // Please change recipient address to yours for test
                oMail.To = "acs.sanjay14@gmail.com";
                oMail.To = "sanjay@itsabacus.com";
                oMail.To = "itsabacus1@gmail.com";

                oMail.Subject = "test email from gmail account with OAUTH 2";
                oMail.TextBody = "this is a test email sent from c# project with gmail.";

                //oMail.AddAttachment("d:\\inv_24_2459.pdf");
                //oMail.AddAttachment(Server.MapPath(file_upload.FileName));

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
                        file_upload.SaveAs(Server.MapPath("~/Doc/") + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName);
                        string file_path = Url1 + "/" + Convert.ToInt32(Session["emp_id"].ToString()) + "_" + file.FileName; ;
                        oMail.AddAttachment(file_path);
                    }
                }
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
                //attachment = new System.Net.Mail.Attachment("D:/inv_24_2459.pdf");
                //attachment = new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath("~/" + " INVOICES/inv_25_3478.pdf"));
                //attachment = new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath("D:/ACS-sanjay/ACS-Project/school_tours/Code/SchoolTours_v3/SchoolTours/INVOICES/inv_25_3478.pdf"));
                // oMail.AddAttachment(attachment.ToString());
                Console.WriteLine("start to send email using OAUTH 2.0 ...");

                //SmtpClient oSmtp = new SmtpClient(); /// commented by sanjay
                // oSmtp.SendMail(oServer, oMail);
            }
            catch (Exception ex)
            {

            }
        }

        static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        async Task getAccesstoken(string code)
        {
            await RequestAccessToken(code, redirectUri);

        }
        async Task DoOauthAndSendEmail()
        {
            // Creates a redirect URI using an available port on the loopback address.
            string redirectUri = "http://localhost:2108/EASend";
            //Console.WriteLine("redirect URI: " + redirectUri);

            // Creates an HttpListener to listen for requests on that redirect URI.
            // var http = new HttpListener();
            // http.Prefixes.Add(redirectUri);
            Console.WriteLine("Listening ...");
            // http.Start(); //commented by sanjay

            // Creates the OAuth 2.0 authorization request.
            string authorizationRequest = string.Format("{0}?response_type=code&scope={1}&redirect_uri={2}&client_id={3}",
                authUri,
                scope,
                Uri.EscapeDataString(redirectUri),
                clientID
            );

            // Opens request in the browser.
            System.Diagnostics.Process.Start(authorizationRequest);

            // Waits for the OAuth authorization response.
            // var context = await http.GetContextAsync();

            // Brings the Console to Focus.
            BringConsoleToFront();

            // Sends an HTTP response to the browser.
            // var response = context.Response;
            //string responseString = string.Format("<html><head></head><body>Please return to the app and close current window.</body></html>");
            //var buffer = Encoding.UTF8.GetBytes(responseString);
            //response.ContentLength64 = buffer.Length;
            //var responseOutput = response.OutputStream;
            //Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            //{
            //    responseOutput.Close();
            //    http.Stop();
            //    Console.WriteLine("HTTP server stopped.");
            //});

            // Checks for errors.
            //if (context.Request.QueryString.Get("error") != null)
            //{
            //    Console.WriteLine(string.Format("OAuth authorization error: {0}.", context.Request.QueryString.Get("error")));
            //    return;
            //}

            //if (context.Request.QueryString.Get("code") == null)
            //{
            //    Console.WriteLine("Malformed authorization response. " + context.Request.QueryString);
            //    return;
            //}

            //// extracts the code
            //var code = context.Request.QueryString.Get("code");
            //Console.WriteLine("Authorization code: " + code);

            //string responseText = await RequestAccessToken(code, redirectUri);
            //Console.WriteLine(responseText);

            //OAuthResponseParser parser = new OAuthResponseParser();
            //parser.Load(responseText);

            // var user = parser.EmailInIdToken;
            // var accessToken = parser.AccessToken;

            // Console.WriteLine("User: {0}", user);
            //  Console.WriteLine("AccessToken: {0}", accessToken);

            //  SendMailWithXOAUTH2(user, accessToken);
        }

        async Task<string> RequestAccessToken(string code, string redirectUri)
        {
            Console.WriteLine("Exchanging code for tokens...");

            // builds the  request
            string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&client_secret={3}&grant_type=authorization_code",
                code,
                Uri.EscapeDataString(redirectUri),
                clientID,
                clientSecret
                );

            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenUri);

            //HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(authUri);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            byte[] _byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = _byteVersion.Length;

            Stream stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
            stream.Close();

            try
            {
                // gets the response
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    return await reader.ReadToEndAsync();
                }

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Console.WriteLine("HTTP: " + response.StatusCode);
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            // reads response body
                            string responseText = await reader.ReadToEndAsync();
                            Console.WriteLine(responseText);
                        }
                    }
                }

                throw ex;
            }
            catch (Exception ex)
            {
                string aa = "";
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        // Hack to bring the Console window to front.

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public void BringConsoleToFront()
        {
            SetForegroundWindow(GetConsoleWindow());
        }

        protected void btn_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendmail_Click(object sender, EventArgs e)
        {
            SendMailWithXOAUTH2("acs.sanjay14@gmail.com", "ya29.A0AfH6SMAAsqjxkjrt-aoBhtVRBlwuvup3gMLCiQd_3wSFoDge1OFiMsdCRmP_CSXEof-JXWGfBQ9Bj_im0_xcoy6ISjb8M62oiUKmfzk2qiei0NBGtxxUXvThHC9kSYz5XZN-Cj25WaW7TjpogDVBUQoY8pme");
        }

        protected void btnSendGrid_Click(object sender, EventArgs e)
        {
            try
            { Execute().Start(); }
            catch (Exception ex)
            { }
            string aa = "";
            //var items = await task;
        }
        static async Task Execute()
        {
            try
            {
                //s_cts.CancelAfter(5000);
                //var filename = file_upload.FileName();

                var apikey = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendGrid"]);
                var client = new SendGridClient(apikey);
                var from = new EmailAddress("acs.sanjay14@gmail.com", "");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("itsabacus1@gmail.com", "itsabacus1@gmail.com");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                //var path = System.Web.Hosting.HostingEnvironment.MapPath("~/INVOICES/inv_25_3478.pdf");
                //var bytes = File.ReadAllBytes(path);

                //var file = Convert.ToBase64String(bytes);
                //msg.AddAttachment("inv_25_3478.pdf", file);
                var response = await client.SendEmailAsync(msg);


            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                s_cts.Dispose();
            }
        }

        protected void txtchange_TextChanged(object sender, EventArgs e)
        {
            lblmsg.Text = txtchange.Text;
        }


        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //public static extern IntPtr GetConsoleWindow();

        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        //public void BringConsoleToFront()
        //{
        //    SetForegroundWindow(GetConsoleWindow());
        //}

    }
}