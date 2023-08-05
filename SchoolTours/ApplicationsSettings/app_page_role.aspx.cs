using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;



namespace SchoolTours.ApplicationsSettings
{
    public partial class app_page_role : System.Web.UI.Page
    {
        //public static int page_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page_id.Value = "0";
                onLoad();
            }
        }
        public void onLoad()
        {
            // Execute pr_lst_items(‘page_roles’) which returns a multiple row recordset with the following columns:  
            input_menu_nm.Text = "";
            input_page_descr.Text = "";
            input_page_url.Text = "";
            page_id.Value = "0";
            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

            obj.mode = "page_roles";
            DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];
            ViewState["dt"] = dt;
            gv_div_roles.DataSource = dt;
            gv_div_roles.DataBind();
        }
        protected void gv_div_roles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_div_roles.PageIndex = e.NewPageIndex;

            gv_div_roles.DataSource = ViewState["dt"];
            gv_div_roles.DataBind();
        }

        public void dtlPage()
        {
            //execute pr_dtl_item(‘page’, @page_id) which returns a single row recordset with the following columns: 1.page_descr, 2.menu_nm, 3.page_url.Use 
            //these values to populate input_page_descr, input_menu_nm, and input_page_url respectively
            try
            {
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "page";
                obj.id1 = Convert.ToString(page_id.Value);

                DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    input_menu_nm.Text = dt.Rows[0]["menu_nm"].ToString();
                    input_page_descr.Text = dt.Rows[0]["page_descr"].ToString();
                    input_page_url.Text = dt.Rows[0]["page_url"].ToString();
                }
                else
                {
                    input_menu_nm.Text = "";
                    input_page_descr.Text = "";
                    input_page_url.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void delPage(object sender, EventArgs e)
        {
            //If confirmed, execute pr_del_item(‘page’, @page_id, @emp_id) which returns 1 if successful and 0 if failure. 
            //If successful, execute pr_lst_items(‘page_roles’) as shown below in onLoad() function.
            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();

            obj.mode = "page";
            obj.id1 = Convert.ToInt32(page_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (response == 1)
            {
                onLoad();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
            }
        }
        public void newPage(object sender, EventArgs e)
        {
            //set the @page_id = 0 and clear values from input_page_descr, input_menu_nm, and input_page_url. 
            page_id.Value = "0";
            input_menu_nm.Text = "";
            input_page_descr.Text = "";
            input_page_url.Text = "";

        }
        public void setPage(object sender, EventArgs e)
        {
            //Validate that input_page_descr, input_menu_nm, and input_page_url all have values.
            //If validated, execute pr_set_item(‘page’, @page_id, @emp_id, null, null, @page_descr, @menu_nm, @page_url) which returns 1 if successful and 0 if failure.
            //If successful, execute pr_lst_items(‘page_roles’) as shown above in onLoad() function
            Obj_SET_ITEM obj = new Obj_SET_ITEM();

            obj.mode = "page";
            obj.id1 = Convert.ToInt32(page_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.str1 = input_page_descr.Text.Trim();
            obj.str2 = input_menu_nm.Text.Trim();
            obj.str3 = input_page_url.Text.Trim();

            int response = DTL_ITEM_Business.Put_SET_ITEM(obj);
            if (response == 1)
            {
                onLoad();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
            }

        }

        protected void gv_div_roles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string part = e.CommandArgument.ToString();
                int rowIndex = Int32.Parse(part);
                CheckBox chk = null;
                //if (rowIndex != -1)
                //{
                //    chk = (CheckBox)gv_div_roles.Rows[rowIndex].Cells[0].FindControl("chk_role_Admin");
                //    // Write your logic here
                //}

                if (e.CommandName == "dtlPage")
                {
                    page_id.Value= Convert.ToInt32(e.CommandArgument).ToString();

                    dtlPage();
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
                int response = setPageRole(split[0], split[1], role_ind);
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
        public int setPageRole(string page_id, string role_id, string role_ind)
        {
            //● Get the @page_id and @role_id from the clicked check box. If the box is now checked, set @role_ind value = 1.If it is unchecked, set @role_ind = 0. 
            //● Execute pr_set_item(‘page_role’, @page_id, @role_id, @role_ind, @emp_id) which returns 1 if successful, 0 if failure.  
            //● If success, leave the checkbox in its new state.If failure, display failure standard error message and revert checkbox to former status.
            try
            {
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "page_role";
                obj.id1 = Convert.ToInt32(page_id);
                obj.id2 = Convert.ToInt32(role_id);
                obj.id3 = Convert.ToInt32(role_ind);
                obj.id4 = Convert.ToInt32(Session["emp_id"].ToString());

                int Response = DTL_ITEM_Business.Put_SET_ITEM(obj);
                return Response;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }

}