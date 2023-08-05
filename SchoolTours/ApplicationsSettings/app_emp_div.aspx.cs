using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;
using System.IO;

namespace SchoolTours.ApplicationsSettings
{
    public partial class app_emp_div : System.Web.UI.Page
    {
        //static int div_id = 0;
        //static int employee_id = 0;
        //static string employee_nm = "";
        //static int div_emp_id = 0;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                div_id.Value = "0";
                employee_id.Value = "0";
                employee_nm.Value = "";
                div_emp_id.Value = "0";

                onLoad();
            }

            if (IsPostBack && file_upload.PostedFile != null)
            {
                if (file_upload.PostedFile.FileName.Length > 0)
                {
                    imgAttach();
                }
            }
        }

        public void onLoad()
        {
            try
            {

                div_id.Value = "0";
                input_div_nm.Text = "";
                input_toll_free_phone.Text = "";
                input_local_phone.Text = "";
                input_url.Text = "";
                div_logo.ImageUrl = "";
                input_email.Text = "";

                //execute pr_lst_items(‘division’, @emp_id) 

                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "division";
                obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());

                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    select_div.DataSource = dt;
                    select_div.DataTextField = "div_nm";
                    select_div.DataValueField = "div_id";
                    select_div.DataBind();
                    select_div.Items.Insert(0, "Select");
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void dtlDiv(object sender, EventArgs e)
        {
            //execute pr_dtl_item(‘div’, @div_id) 
            try
            {
                if (select_div.SelectedValue != "Select")
                {
                    div_id.Value = Convert.ToInt32(select_div.SelectedValue).ToString();
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "div";
                    obj.id1 = (select_div.SelectedValue);
                    DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);
                    DataTable dt = ds.Tables[0];
                    DataTable dt_employee = ds.Tables[1];
                    DataTable dt_employee_div = ds.Tables[2];


                    if (dt.Rows.Count > 0)
                    {
                        input_div_nm.Text = dt.Rows[0]["div_nm"].ToString();
                        input_toll_free_phone.Text = phoneformatting(dt.Rows[0]["toll_free_phone"].ToString());
                        input_local_phone.Text = phoneformatting(dt.Rows[0]["local_phone"].ToString());
                        input_url.Text = dt.Rows[0]["url"].ToString();

                        div_logo.ImageUrl = "~/Images/" + dt.Rows[0]["img"].ToString();
                    }

                    select_employee.DataSource = dt_employee;
                    select_employee.DataTextField = "employee_nm";
                    select_employee.DataValueField = "emp_id";
                    select_employee.DataBind();

                    select_div_employee.DataSource = dt_employee_div;
                    select_div_employee.DataTextField = "employee_nm";
                    select_div_employee.DataValueField = "div_emp_id";
                    select_div_employee.DataBind();
                }
                else
                {
                    input_div_nm.Text = "";
                    input_toll_free_phone.Text = "";
                    input_local_phone.Text = "";
                    input_url.Text = "";

                    div_logo.ImageUrl = "";
                    select_employee.Items.Clear(); select_div_employee.Items.Clear();
                    
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void newDiv(object sender, EventArgs e)
        {
            try
            {
                select_div.SelectedValue = "Select";
                div_id.Value = "0";
                input_div_nm.Text = "";
                input_toll_free_phone.Text = "";
                input_local_phone.Text = "";
                input_url.Text = "";
                div_logo.ImageUrl = "";
                input_email.Text = "";
                select_employee.Items.Clear();
                select_div_employee.Items.Clear();

            }
            catch (Exception ex)
            {

            }

        }
        public void setDiv(object seder, EventArgs e)
        {
            //Validate that input_div_nm, input_toll_free_phone, input_local_phone, and input_url all have valid values. 
            //If validated, execute pr_set_item(‘div’, @div_id, @emp_id, null, null, @div_nm, @toll_free_phone, @local_phone, @url) which returns 1 if successful, 0 if failure. 
            //If successful, execute pr_lst_items(‘divs’) as shown above in onLoad() function.

            Obj_SET_ITEM obj = new Obj_SET_ITEM();

            obj.mode = "div";
            obj.id1 = Convert.ToInt32(div_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.str1 = input_div_nm.Text.Trim();
            obj.str2 = input_toll_free_phone.Text.Replace(@".", string.Empty);
            obj.str3 = input_local_phone.Text.Replace(@".", string.Empty);
            obj.str4 = input_url.Text.Trim();

            int result = DTL_ITEM_Business.Put_SET_ITEM(obj);
            if (result == 1)
            {
                onLoad();
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('fail')", true);
            }
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
        public void dtlEmp(object sender, EventArgs e)
        {
            //execute pr_dtl_item(‘div_emp’, @div_employee_id)  which returns a single row recordset with the following column: 1. eMail. Display this value in input_email object. 
            try
            {
                if (select_div_employee.SelectedValue != "")
                {
                    div_emp_id.Value = Convert.ToInt32(select_div_employee.SelectedValue).ToString();
                    employee_nm.Value = Convert.ToString(select_div_employee.SelectedItem);

                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "div_emp";
                    obj.id1 = Convert.ToString(div_emp_id);
                    DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                    input_email.Text = dt.Rows[0]["eMail"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void imgAttach()
        {
            // execute pr_set_item(‘div_img’, @div_id, @emp_id, null, null, @filename) which returns 1 if successful, 0 if failure. 
            if (Convert.ToInt32(div_id.Value) != 0)
            {
                if (File.Exists(Server.MapPath("~/Images/") + file_upload.PostedFile.FileName))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('this file name alreday exists in server path ,please choose the unique filename')", true);
                }
                else {

                    file_upload.SaveAs(Server.MapPath("~/Images/") + file_upload.PostedFile.FileName);

                    Obj_SET_ITEM obj = new Obj_SET_ITEM();

                    obj.mode = "div_img";
                    obj.id1 = Convert.ToInt32(div_id.Value);
                    obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                    obj.str1 = file_upload.PostedFile.FileName;

                    int response = DTL_ITEM_Business.Put_SET_ITEM(obj);
                    if (response == 1)
                    {
                        div_logo.ImageUrl = "~/Images/" + file_upload.PostedFile.FileName;
                    }
                }
            }
            else {

            }
        }
        public void delDiv(object sender, EventArgs e)
        {
            //If confirmed, execute pr_del_item(‘div’, @div_id, @emp_id) which returns a 1 if successful, 0 if failure. 
            //● If successful, execute pr_lst_items(‘divs’) as shown below in onLoad() function.

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "div";
            obj.id1 = Convert.ToInt32(div_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());

            int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (response == 1)
            {
                onLoad();
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('fail')", true);
            }
        }

        //public void setEmpPage(object sender, EventArgs e)
        //{
        //    //Validate that input_eMail contains a valid eMail address.  
        //    //● If validated, execute pr_set_item(‘div_emp’, @div_id, @employee_id, @emp_id, null, @eMail) which returns 1 if successful, 0 if failure. 
        //    //● If successful, deselect the employee’s name in select_employee and clear input_email. 
        //    if (select_employee.SelectedValue != "")
        //    {
        //        Obj_SET_ITEM obj = new Obj_SET_ITEM();
        //        obj.mode = "div_emp";
        //        obj.id1 = div_id;
        //        obj.id2 = employee_id;
        //        obj.id3 = Convert.ToInt32(Session["emp_id"].ToString());
        //        obj.str1 = input_email.Text;

        //        int response = DTL_ITEM_Business.Put_SET_ITEM(obj);
        //        if (response == 1)
        //        {
        //            input_email.Text = "";
        //        }
        //    }

        //}
        public void delEmpPage(object sender, EventArgs e)
        {
            //If confirmed, execute pr_del_item(‘emp_div’, @div_id, @employee_id, @emp_id) which returns a 1 if successful, 0 if failure.
            //● If successful, deselect the employee’s name in select_employee and clear input_email. 

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();

            obj.mode = "emp_div";
            obj.id1 = Convert.ToInt32(div_id.Value);
            obj.id2 = Convert.ToInt32(employee_id.Value);
            obj.id3 = Convert.ToInt32(Session["emp_id"].ToString());
            int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (response == 1)
            {
                select_employee.Items.RemoveAt(select_employee.Items.IndexOf(select_employee.SelectedItem));
                input_email.Text = "";
            }
            else
            {

            }
        }

        public void setDivEmpEmail(object sender, EventArgs e)
        {
            // ​If input_email contains a valid eMail address, execute pr_set_item(‘div_emp_eMail’, @div_emp_id, null, null, null, @input_eMail) which returns 1 if successful, 0 if failure. 
            if (select_div_employee.SelectedValue != "")
            {
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "div_emp_eMail";
                obj.id1 = Convert.ToInt32(select_div_employee.SelectedValue);
                obj.str1 = input_email.Text.Trim();

                int response = DTL_ITEM_Business.Put_SET_ITEM(obj);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select div employee first')", true);
            }


        }
        public void setDivEmp(object sender, EventArgs e)
        {
            //Validate that employee does not already exist in select_employee_div. ● If valid, execute pr_set_item(‘div_emp’, @div_id, @employee_id) which returns two recordsets: 
            //○ The first is a multiple row recordset with the following columns: 1.employee_id, 2.employee_nm.Use these values to populate select_employee. 
            //○ The second is a multiple row recordset with the following columns: 1.div_employee_id, 2.employee_nm.Use these values to populate select_div_employee.
            try
            {
                if (Convert.ToInt32(div_id.Value) == 0 || select_employee.SelectedValue == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select div and employee first')", true);
                    return;
                }
                int strCount = 0;
                for (int index = 0; index < select_div_employee.Items.Count; index++)
                {
                    ListItem item = select_div_employee.Items[index];
                    if (select_employee.SelectedItem.ToString() == item.Text)
                    {
                        strCount++;
                    }
                }
                if (strCount == 0)
                {
                    //execute pr_set_item(‘div_emp’, @div_id, @employee_id)
                    Obj_SET_ITEM obj = new Obj_SET_ITEM();
                    obj.mode = "div_emp";
                    obj.id1 = Convert.ToInt32(div_id.Value);
                    obj.id2 = Convert.ToInt32(select_employee.SelectedValue);

                    DataSet ds = DTL_ITEM_Business.Put_SET_ITEM_DS(obj);

                    DataTable dt_employee = ds.Tables[0];
                    DataTable dt_employee_div = ds.Tables[1];

                    select_employee.DataSource = dt_employee;
                    select_employee.DataTextField = "employee_nm";
                    select_employee.DataValueField = "emp_id";
                    select_employee.DataBind();

                    select_div_employee.DataSource = dt_employee_div;
                    select_div_employee.DataTextField = "employee_nm";
                    select_div_employee.DataValueField = "div_emp_id";
                    select_div_employee.DataBind();

                    input_email.Text = "";
                }
            }
            catch (Exception ex) { }
        }
        public void delEmpDiv(object sender, EventArgs e)
        {
            //If confirmed, execute pr_del_item(‘div_emp’, @div_employee_id) which returns two recordsets: 
            try
            {
                if (Convert.ToInt32(div_id.Value) == 0 || select_div_employee.SelectedValue == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select div and Div employee first')", true);
                    return;
                }

                Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                obj.mode = "div_emp";
                obj.id1 = Convert.ToInt32(select_div_employee.SelectedValue);
                obj.id2 = Convert.ToInt32(div_id.Value);



                DataSet ds = DTL_ITEM_Business.del_DEL_ITEM_DS(obj);

                DataTable dt_employee = ds.Tables[0];
                DataTable dt_employee_div = ds.Tables[1];

                select_employee.DataSource = dt_employee;
                select_employee.DataTextField = "employee_nm";
                select_employee.DataValueField = "emp_id";
                select_employee.DataBind();

                select_div_employee.DataSource = dt_employee_div;
                select_div_employee.DataTextField = "employee_nm";
                select_div_employee.DataValueField = "div_emp_id";
                select_div_employee.DataBind();

                input_email.Text = "";

            }
            catch (Exception ex)
            {

            }
        }

        protected void input_toll_free_phone_TextChanged(object sender, EventArgs e)
        {
            input_toll_free_phone.Text = phoneformatting(input_toll_free_phone.Text);
        }
        protected void input_local_phone_TextChanged(object sender, EventArgs e)
        {
            input_local_phone.Text = phoneformatting(input_local_phone.Text);
        }
    }
}