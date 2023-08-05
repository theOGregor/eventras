using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolTours
{
    public partial class Demopage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static void EntityAddTag(string val, string key)
        {
            Demopage2 obj = new Demopage2();
            obj.tagEntityCreate(val);

            
        }
      
        public void tagEntityCreate(string val)
        {
            string aa = Convert.ToString(val);

           // string aaaa = TextBox1.Text;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        
    }
}