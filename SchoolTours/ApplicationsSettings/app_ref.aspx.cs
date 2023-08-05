using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursBusiness;
using SchoolToursData.Object;
using System.Data;

namespace SchoolTours.ApplicationsSettings
{
    public partial class app_ref : System.Web.UI.Page
    {

        //public static int ref_id = 0;
        //public static int list_id = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ref_id.Value = "0";
                list_id.Value = "0";
                onLoad();
            }
        }


        public void onLoad()
        {
            //Execute pr_lst_items(‘list’, @emp_id) which returns a multiple row recordset with the following columns: 1. list_id, 2. list_descr.
            //Use these values to populate select_list, each with an onclick action of dtlRef(ref_id). 
            try
            {
                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "list";
                obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());

                DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];


                select_list.DataSource = dt;
                select_list.DataTextField = "list_descr";
                select_list.DataValueField = "list_id";
                select_list.DataBind();
                select_list.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {

            }
        }

        protected void select_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (select_list.SelectedValue != "Select")
            {
                list_id.Value = Convert.ToInt32(select_list.SelectedValue).ToString();
                dtlList();
            }
        }
        public void dtlList()
        {
            // execute pr_lst_items(‘ref’, @emp_id) which returns a multiple row recordset with the following columns: 1. ref_id, 2. Ref_descr. Use these values to populate select_ref. 

            try
            {
                ref_id.Value = "0";
                input_ref_descr.Text = "";

                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "ref";
                //obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.id1 = Convert.ToInt32(list_id.Value);
                DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

                select_ref.DataSource = dt;
                select_ref.DataTextField = "ref_descr";
                select_ref.DataValueField = "ref_id";
                select_ref.DataBind();
                //select_list.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {

            }
        }
        public void dtlRef (object sender,EventArgs e)
        {
            // copy the value from the selected item in select_ref to the input_ref_descr
            if (select_ref.SelectedValue != "")
            {
                ref_id.Value = Convert.ToInt32(select_ref.SelectedValue).ToString();
                input_ref_descr.Text = Convert.ToString(select_ref.SelectedItem);
            }
        }
        public void delRef(object sender, EventArgs e)
        {
            //Confirm that the user wants to complete this action. ● If confirmed, execute pr_del_item(‘ref’, @ref_id, @emp_id) which returns 1 if successful, 0 if failure.
            //Execute pr_lst_items(‘ref’, @emp_id) which returns a multiple row recordset with the following columns: 1.ref_id, 2.ref_descr.
            //Use these values to populate select_ref, each with an onclick action of dtlRef(ref_id).

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "ref";
            obj.id1 = Convert.ToInt32(ref_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            int Response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (Response == 1)
            {
                dtlList();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
            }

        }
        public void newRef(object sender, EventArgs e)
        {
            //set @ref_id = 0 and clear contents of input_ref_descr and focus cursor on input_ref_descr.
            ref_id.Value = "0";
            input_ref_descr.Text= "";
            input_ref_descr.Focus();

        }
        public void setRef(object sender, EventArgs e)
        {
            //Execute pr_set_item(‘ref’, @ref_id, @emp_id, @list_id, null, @ref_descr) which 1 if successful, 0 if failure.
            //If success, execute pr_lst_items(‘ref’, @emp_id) which returns a multiple row recordset with the following columns: 1.ref_id, 2.ref_descr.
            //Use these values to populate select_ref, each with an onclick action of dtlRef(ref_id).

            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "ref";
            obj.id1= Convert.ToInt32(ref_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.id3 = Convert.ToInt32(list_id);
            obj.str1= Convert.ToString(input_ref_descr.Text.Trim());

            int Response = DTL_ITEM_Business.Put_SET_ITEM(obj);
            if (Response == 1)
            {
                dtlList();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
            }


        }


    }
}