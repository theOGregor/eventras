using SchoolToursBusiness;
using SchoolToursData.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolTours.ApplicationsSettings
{
    public partial class app_emp_role : System.Web.UI.Page
    {
        //static int employee_id = 0;
        //static int role_id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                employee_id.Value = "0";
                role_id.Value = "0";
                onLoad();
            }
        }

        public void onLoad()
        {
            input_given_nm.Text = "";
            input_last_nm.Text = "";
            input_phone.Text = "";
            input_eMail.Text = "";

            employee_id.Value = "0";
            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

            obj.mode = "emp_roles";
            DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

            ViewState["dt"] = dt;
            gv_employee_roles.DataSource = dt;
            gv_employee_roles.DataBind();
        }
        public void dtlEmp(object sender, EventArgs e)
        {

        }

        public void rstPasscode(object sender, EventArgs e)
        {
            //After validating that an employee is selected in div_employee/roles, execute pr_login(‘force’, @input_email) which returns 1 if successful, 0 if fail. 
            //If success, display message saying “The employee’s password has been reset to HAMPTON.” 
            try
            {
                if (input_eMail.Text.Trim() != "")
                {
                    ObjLogin obj = new ObjLogin();
                    obj.mode = "force";
                    obj.eMail = input_eMail.Text.Trim();

                    DataTable dt = LoginBusiness.GetLoginDetail(obj).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["rc"].ToString() == "1")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The employee’s password has been reset to HAMPTON.')", true);
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('fail')", true);
                        }
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email is empty')", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void delEmp(object sender, EventArgs e)
        {
            //First confirm whether the user wants to take this action. 
            //● If confirmed, execute pr_del_item(‘emp’, @employee_id, @emp_id) which returns 1 if successful and 0 if failure. 
            //● If successful, execute pr_lst_items(‘emp_roles’) as shown below in onLoad() function.
            try
            {
                Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                obj.mode = "emp";
                obj.id1 = Convert.ToInt32(employee_id.Value);
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
                if (response == 1)
                {
                    onLoad();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('fail')", true);
                }
            }
            catch (Exception ex)
            {
            }

        }
        public void newEmp(object sender, EventArgs e)
        {
            //set the @employee_id = 0 and clear values frominput_given_nm, input_last_nm, input_phone, and input_eMail
            employee_id.Value = "0";
            input_given_nm.Text = "";
            input_last_nm.Text = "";
            input_phone.Text = "";
            input_eMail.Text = "";

        }
        public void setEmp(object sender, EventArgs e)
        {
            //● Validate that input_given_nm, input_last_nm, input_phone, and input_eMail all have valid values. 
            //● If validated, execute pr_set_item(‘emp’, @employee_id, @emp_id, null, null, @given_nm, @last_nm, @phone, @eMail, ‘HAMPTON’) which returns 1 if successful and 0 if failure. 
            //“HAMPTON” is the default password for all new employees. ● If successful, execute pr_lst_items(‘emp_roles’) as shown above in onLoad() function.
            try
            {
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "emp";
                obj.id1 = Convert.ToInt32(employee_id.Value);
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.str1 = input_given_nm.Text.Trim();
                obj.str2 = input_last_nm.Text.Trim();
                obj.str3 = input_phone.Text.Trim().Replace(@".", string.Empty);
                obj.str4 = input_eMail.Text.Trim();
                obj.str5 = "HAMPTON";

                int response = DTL_ITEM_Business.Put_SET_ITEM(obj);
                if (response == 1)
                {
                    onLoad();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('fail')", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public int setEmpRole(string employee_id, string role_id, string role_ind)
        {
            // Get the @employee_id and @role_id from the clicked check box. If the box is now checked, set @role_ind value = 1. If it is unchecked, set @role_ind = 0. 
            //● Execute pr_set_item(‘emp_role’, @employee_id, @role_id, @role_ind, @emp_id) which returns 1 if successful, 0 if failure.  
            //● If success, leave the checkbox in its new state. If failure, display failure standard error message and revert checkbox to former status. 

            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "emp_role";
            obj.id1 = Convert.ToInt32(employee_id);
            obj.id2 = Convert.ToInt32(role_id);
            obj.id3 = Convert.ToInt32(role_ind);
            obj.id4 = Convert.ToInt32(Session["emp_id"].ToString());
            int response = DTL_ITEM_Business.Put_SET_ITEM(obj);
            return response;
        }

        protected void gv_employee_roles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string part = e.CommandArgument.ToString();

            try
            {
                if (e.CommandName == "dtlemployee")
                {
                    employee_id.Value = Convert.ToInt32(e.CommandArgument).ToString();

                    dtlEmp();
                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void gv_employee_roles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_employee_roles.PageIndex = e.NewPageIndex;

            gv_employee_roles.DataSource = ViewState["dt"];
            gv_employee_roles.DataBind();
        }
        public void dtlEmp()
        {
            // pr_dtl_item(‘emp’, @employee_id)

            try
            {
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "emp";
                obj.id1 = Convert.ToString(employee_id.Value);



                DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    input_given_nm.Text = dt.Rows[0]["given_nm"].ToString();
                    input_last_nm.Text = dt.Rows[0]["last_nm"].ToString();
                    input_phone.Text = phoneformatting(dt.Rows[0]["phone"].ToString());
                    input_eMail.Text = dt.Rows[0]["eMail"].ToString();
                    role_id.Value = Convert.ToInt32(dt.Rows[0]["role_id"].ToString()).ToString();
                }
                else
                {
                    input_given_nm.Text = "";
                    input_last_nm.Text = "";
                    input_phone.Text = "";
                    input_eMail.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                String role_ind = "";
                if (chk.Checked)
                    role_ind = "1"; //checked
                else
                    role_ind = "0"; //unchecked
                string Getvalue = chk.Text;
                string[] split = Getvalue.Split(';');
                int response = setEmpRole(split[0], split[1], role_ind);
                if (response == 1)
                {
                    onLoad();
                }
                else {
                    if (role_ind == "1")
                        chk.Checked = false;
                    else
                        chk.Checked = true;

                }
            }
            catch (Exception ex) { }

        }

        protected void input_phone_TextChanged(object sender, EventArgs e)
        {
            input_phone.Text = phoneformatting(input_phone.Text);
        }
        public string phoneformatting(string strPhone)
        {
            try
            {
                strPhone = strPhone.Replace(@".", string.Empty);
                strPhone = "" + strPhone.Substring(0, 3) + "." + strPhone.Substring(3, 3) + "." + strPhone.Substring(6);
                return strPhone;
            }
            catch (Exception ex)
            {
                return strPhone;
            }
        }
    }
}