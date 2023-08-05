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
    public partial class app_profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                onLoad();
            }
        }
        public void onLoad()
        {
            //Execute pr_dtl_item(‘emp’, @emp_id)
            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();

            obj.mode = "emp";
            obj.id1 = Session["emp_id"].ToString();

            DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
            if (dt.Rows.Count > 0)
            {
                input_given_nm.Text = dt.Rows[0]["given_nm"].ToString();
                input_last_nm.Text = dt.Rows[0]["last_nm"].ToString();
                input_phone.Text = phoneformatting(dt.Rows[0]["phone"].ToString());
                input_eMail.Text = dt.Rows[0]["eMail"].ToString();
            }

        }
        public void setEmp(object sender, EventArgs e)
        {
            //Validate that the following fields have valid values: input_given_nm, input_last_name, input_phone, input_eMail.
            // Only validate the passcode fields if input_passcode is not empty.Validate input_code_new for complexity standards and matching input_code_new / input_code_vfy. 
            //Execute pr_set_item(‘emp’, @emp_id, @emp_id, null, null, @given_nm, @last_nm, @phone, @eMail, @passcode_new, @passcode_old) which returns 1 if successful, 2 if failure.

            Obj_SET_ITEM obj = new Obj_SET_ITEM();

            obj.mode = "emp";
            obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());

            obj.str1 = input_given_nm.Text.Trim();
            obj.str2 = input_last_nm.Text.Trim();
            obj.str3 = input_phone.Text.Trim().Replace(@".", string.Empty);
            obj.str4 = input_eMail.Text.Trim();
            obj.str5 = input_passcode_new.Text.Trim();
            obj.str6 = input_passcode_old.Text.Trim();
            int Result = DTL_ITEM_Business.Put_SET_ITEM(obj);
            if(Result==1)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('successfully update information')", true);
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failed')", true);
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

        protected void input_phone_TextChanged(object sender, EventArgs e)
        {
            input_phone.Text = phoneformatting(input_phone.Text);
        }
    }
}