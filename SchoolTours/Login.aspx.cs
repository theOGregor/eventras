using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;



namespace SchoolTours
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
                Session.Clear();
                
            }
        }

        protected void signIn(object sender, EventArgs e)
        {
            if(input_passcode.Text.Trim()=="")
            {
                lbl_error_msg.Text = "password is required";
                return;
            }
            ObjLogin Obj = new ObjLogin();
            Obj.eMail = input_eMail.Text;
            Obj.passcode = input_passcode.Text;
            Obj.mode = "login";
            Obj.tempcode = "";

            DataSet ds = LoginBusiness.GetLoginDetail(Obj);

            if (ds != null && ds.Tables.Count > 0)
            {
                int Rc = Convert.ToInt32(ds.Tables[0].Rows[0]["Rc"]);
                if (Rc > 0)
                {
                    Session["emp_id"] = Rc;
                    Response.Redirect("Dashboard");
                }
                else if (Rc == -1)
                {
                    Response.Redirect("ResetPassword.aspx");
                }
                else 
                {
                    lbl_error_msg.Text = "Email and / or password incorrect. Please try again.";
                }
            }
        }

        protected void rstPasscode(object sender, EventArgs e)
        {
            ObjLogin Obj = new ObjLogin();
            Obj.eMail = input_eMail.Text;
            Obj.passcode = input_passcode.Text;
            Obj.mode = "reset";
            Obj.tempcode = "";
            
            DataSet ds = LoginBusiness.GetLoginDetail(Obj);

            if (ds != null && ds.Tables.Count > 0)
            {
                string passcode = Convert.ToString(ds.Tables[0].Rows[0]["passcode"]);
                if (passcode!= "")
                {
                    Response.Redirect("ResetPassword.aspx");
                }
                else 
                {
                    lbl_error_msg.Text = "Email and / or password incorrect. Please try again.";
                }
            }
        }

       
    }
}