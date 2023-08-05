using SchoolToursBusiness;
using SchoolToursData.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolTours
{
    public partial class Mytour_ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void savPasscode(object sender, EventArgs e)
        {
            ObjLogin Obj = new ObjLogin();

            Obj.eMail = input_eMail.Text;
            Obj.passcode = input_passcode.Text;
            Obj.mode = "mytour_update";
            Obj.tempcode = input_tempcode.Text;
                       
            Obj.ind_ind = Convert.ToInt32(Request.Cookies["CustomerFacing_ind_ind"].Value);

            DataSet ds = LoginBusiness.GetLoginDetail(Obj);

            if (ds != null && ds.Tables.Count > 0)
            {
                int Rc = Convert.ToInt32(ds.Tables[0].Rows[0]["Rc"]);
                if (Rc == 1)
                {
                    Response.Redirect("mytour_index");
                }
                else
                {
                    lblErrorMsg.Text = "eMail and / or password not correct. Please try again";
                }
            }
        }
    }
}