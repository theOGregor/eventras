using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;
using System.Reflection;

namespace SchoolTours
{
    public partial class Unsubscribe_Endpoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Email"] != null)
                {
                    string Email = Convert.ToString(Request.QueryString["Email"]);
                    string div_nm = Convert.ToString(Request.QueryString["div_nm"]);
                    OnLoad(Email, div_nm);
                }
                else
                {
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                }
            }
            else
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

            }
        }
        public void OnLoad(string Email, string div_nm)
        {
            //executes pr_mailer(‘unsubscribe’, null, null, @email_address)
            try
            {
                Obj_PR_MAILER obj = new Obj_PR_MAILER();
                obj.mode = "unsubscribe";
                obj.str1 = div_nm;
                obj.str2 = Email;
                DataTable dt = null;
                try
                {
                    dt = DTL_ITEM_Business.Get_PR_MAILER(obj).Tables[0];
                }
                catch (Exception ex)
                {

                }
                string err_msg = "";
                if (dt.Rows[0][0].ToString() == "0")
                {
                    err_msg = "Your attempt to unsubscribe was unsuccessful. Please call our toll free number to unsubscribe.";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                    //return;
                }
                else {
                    err_msg = "The eMail address " + Email + " has been unsubscribed from our service.";
                    //err_msg = "\"" + err_msg + "\"";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                    //return;
                }
                err_msg = "<p>" + err_msg + "</p>";
                err_msg += "<p>Close this current window manually.</p>";

                divContent.InnerHtml = err_msg;
                exampleModal.Visible = true;
            }
            catch (Exception ex)
            {
                string err_msg = "\"" + ex.Message + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }

        protected void bttn_Ok_Click(object sender, EventArgs e)
        {
            //          PropertyInfo isreadonly =
            //typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
            //"IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            //          // make collection editable
            //          isreadonly.SetValue(this.Request.QueryString, false, null);
            //          // remove
            //          this.Request.QueryString.Remove("Email");
            //          this.Request.QueryString.Remove("div_nm");

            Response.Redirect("/Unsubscribe_Endpoint");

            //Request.QueryString.Clear();
            //Request.QueryString.Remove("Email");
            //Request.QueryString.Remove("div_nm");
            //Response.Write("<script>parent.close_window();</script>");
            //Response.Redirect("/Login");
            //string jScript = "<script>window.close();</script>";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);
            // this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }
    }
}