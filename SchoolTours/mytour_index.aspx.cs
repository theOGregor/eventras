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
    public partial class mytour_index : System.Web.UI.Page
    {
        //The radio_ind_ind should have values of “leader of a group tour” = 0 and “member of independent tour” =
        //1. Use this value as @ind_ind in functions below.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Obj_global_Mytour_Login.ind_ind = 0;
                Response.Cookies["CustomerFacing_ind_ind"].Value = null;
                Response.Cookies["CustomerFacing_tour"].Value = null;
                Response.Cookies["CustomerFacing_person"].Value = null;
                Response.Cookies["CustomerFacing_admin_ind"].Value = null;
            }
        }

        public void signIn(object sender, EventArgs e)
        {
            if (input_passcode.Text.Trim() == "")
            {
                div_error_msg.Text = "password is required";
                return;
            }
            ObjLogin Obj = new ObjLogin();

            Obj.mode = "mytour_login";
            Obj.eMail = input_eMail.Text;
            Obj.passcode = input_passcode.Text;
            HttpCookie ind_ind = new HttpCookie("CustomerFacing_ind_ind");
            if (radio_ind_ind_leader.Checked == true)
            {
                Obj.ind_ind = 0;
                //Obj_global_Mytour_Login.ind_ind = 0;
                ind_ind.Value = 0.ToString();
            }
            else {
                Obj.ind_ind = 1;
                //Obj_global_Mytour_Login.ind_ind = 1;
                ind_ind.Value = 1.ToString();
            }
            Response.Cookies.Add(ind_ind);
            DataSet ds = LoginBusiness.GetLoginDetail(Obj);

            if (ds != null && ds.Tables.Count > 0)
            {
                int Rc = Convert.ToInt32(ds.Tables[0].Rows[0]["Rc"]);
                if (Rc > 0)
                {
                    HttpCookie person_id = new HttpCookie("CustomerFacing_person");
                    person_id.Value = Rc.ToString();
                    Response.Cookies.Add(person_id);

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        HttpCookie tour_id = new HttpCookie("CustomerFacing_tour");
                        tour_id.Value = ds.Tables[1].Rows[0]["tour_id"].ToString();
                        Response.Cookies.Add(tour_id);
                    }
                    else {
                        div_error_msg.Text = "Not getting tour_id from Database.";
                        return;
                    }
                    Response.Redirect("mytour/mytour_dashboard");
                }
                else if (Rc == -1)
                {
                    Response.Redirect("Mytour_ResetPassword");
                }
                else if (Rc == 0)
                {
                    div_error_msg.Text = "Email and / or password incorrect. Please try again.";
                }
                else
                {
                    div_error_msg.Text = "Email and / or password incorrect. Please try again.";
                }
            }
        }
        public void rstPasscode(object sender, EventArgs e)
        {
            ObjLogin Obj = new ObjLogin();
            Obj.eMail = input_eMail.Text;
            Obj.passcode = null;
            Obj.mode = "mytour_reset";
            Obj.tempcode = null;
            HttpCookie ind_ind = new HttpCookie("CustomerFacing_ind_ind");
            if (radio_ind_ind_leader.Checked == true)
            {
                Obj.ind_ind = 0;
                //Obj_global_Mytour_Login.ind_ind = 0;
                ind_ind.Value = 0.ToString();
            }
            else {
                Obj.ind_ind = 1;
                // Obj_global_Mytour_Login.ind_ind = 1;
                ind_ind.Value = 1.ToString();
            }
            Response.Cookies.Add(ind_ind);
            DataSet ds = LoginBusiness.GetLoginDetail(Obj);

            if (ds != null && ds.Tables.Count > 0)
            {
                string passcode = Convert.ToString(ds.Tables[0].Rows[0]["passcode"]);
                if (passcode != "")
                {
                    Response.Redirect("Mytour_ResetPassword");
                }
                else
                {
                    div_error_msg.Text = "Email and / or password incorrect. Please try again.";
                }
            }
        }
    }
}