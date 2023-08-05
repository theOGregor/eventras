using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net.Mail;
using NUnit.Framework;
using DKIM;
using System.Net;

namespace SchoolTours
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str20 = getASPNET20machinekey();
            string str11 = getASPNET11machinekey();
        }
        public string getASPNET20machinekey()
        {
            StringBuilder aspnet20machinekey = new StringBuilder();
            string key64byte = getRandomKey(64);
            string key32byte = getRandomKey(32);
            aspnet20machinekey.Append("<machineKey \n");
            aspnet20machinekey.Append("validationKey=\"" + key64byte + "\"\n");
            aspnet20machinekey.Append("decryptionKey=\"" + key32byte + "\"\n");
            aspnet20machinekey.Append("validation=\"SHA1\" decryption=\"AES\"\n");
            aspnet20machinekey.Append("/>\n");
            return aspnet20machinekey.ToString();
        }

        public string getASPNET11machinekey()
        {
            StringBuilder aspnet11machinekey = new StringBuilder();
            string key64byte = getRandomKey(64);
            string key24byte = getRandomKey(24);

            aspnet11machinekey.Append("<machineKey ");
            aspnet11machinekey.Append("validationKey=\"" + key64byte + "\"\n");
            aspnet11machinekey.Append("decryptionKey=\"" + key24byte + "\"\n");
            aspnet11machinekey.Append("validation=\"SHA1\"\n");
            aspnet11machinekey.Append("/>\n");
            return aspnet11machinekey.ToString();
        }

        public string getRandomKey(int bytelength)
        {
            byte[] buff = new byte[bytelength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            StringBuilder sb = new StringBuilder(bytelength * 2);
            for (int i = 0; i < buff.Length; i++)
                sb.Append(string.Format("{0:X2}", buff[i]));
            return sb.ToString();
        }

        protected void btn_send_Click(object sender, EventArgs e)
        {
            var msg = new MailMessage();

            msg.From = new MailAddress("sanjay@eventras.com", "sanjay wagadre");
            msg.To.Add(new MailAddress("acs.sanjay14@gmail.com", "Port587"));
            msg.Subject = "Testing DKIM.Net test 5-10 ";
            msg.Body = "Hello World sanjay ";




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
            msg.DomainKeySign(domainKeySigner);


            var dkimSigner = new DkimSigner(privateKey, "eventras.com", "eventras", new string[] { "From", "To", "Subject" });
            msg.DkimSign(dkimSigner);

            using (SmtpClient smtp = new SmtpClient("eventras.com", 587))
            {
                smtp.Credentials = new NetworkCredential("admin@eventras.com", "Yigy10*4");
                //smtp.EnableSsl = enableSSL;
                smtp.Send(msg);
                //return 1;
            }
            // new SmtpClient().Send(msg);
        }
    }

}