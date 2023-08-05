using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursBusiness;
using SchoolToursData.Object;
using System.Data;

namespace SchoolTours
{
    public partial class Defect : System.Web.UI.Page
    {
        static int defect_id = 0;
        static int memo_id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Onload();
            }
        }
        public void Onload()
        {
            //Check user’s permissions for this page with the global checkRole() function.If not allowed,
            //redirect browser to login.aspx.
            //● Execute pr_lst_items(‘defects’) which returns a multiple row recordset with the following columns:
            //1.defect_id, 2.defect_descr.Use these values to populate select_list, each with an onclick action
            //of dtlDefect(defect_id).
            //● Execute pr_lst_items(‘defect_status’) which returns a multiple row recordset with the following
            //columns: 1.defect_status_id, 2.defect_status_descr.Use these values to populate the
            //select_status object.
            try
            {
                defect_id = 0;
                memo_id = 0;

                OnloadDefects();
                //OnloadMemo();
            }
            catch (Exception ex)
            {

            }
        }

        public void OnloadDefects()
        {
            Obj_LST_ITEMS obj_defects = new Obj_LST_ITEMS();
            obj_defects.mode = "defects";
            DataTable dt_defects = DTL_ITEM_Business.Get_LST_ITEMS(obj_defects).Tables[0];


            div_defect.DataSource = dt_defects;
            div_defect.DataTextField = "defect_descr";
            div_defect.DataValueField = "defect_id";
            div_defect.DataBind();

            Obj_LST_ITEMS obj_defect_status = new Obj_LST_ITEMS();
            obj_defect_status.mode = "defect_status";
            DataTable dt_defect_status = DTL_ITEM_Business.Get_LST_ITEMS(obj_defect_status).Tables[0];


            select_status.DataSource = dt_defect_status;
            select_status.DataTextField = "ref_descr";
            select_status.DataValueField = "ref_id";
            select_status.DataBind();
            select_status.Items.Insert(0, "Select");

        }
        public void GetDefectDetails()
        {
            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
            obj.mode = "defect";
            obj.id1 = defect_id.ToString();
            DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

            DataTable dt_defect = ds.Tables[0];

            input_defect_descr.Text = dt_defect.Rows[0]["defect_descr"].ToString();
            input_defect_title.Text = dt_defect.Rows[0]["defect_title"].ToString();


            DataTable dt_memo = ds.Tables[1];

            div_memo.DataSource = dt_memo;
            div_memo.DataTextField = "memo_descr";
            div_memo.DataValueField = "memo_id";
            div_memo.DataBind();
        }

        protected void dtlDefect(object sender, EventArgs e)
        {
            //Execute pr_dtl_item(‘defect’, @defect_id) which returns two recordsets.
            //● The first is a multiple row recordset with the following columns: 1.defect_title, 2.defect_descr.
            //Use these values to populate input_defect_title and input_defect_descr.
            //● The second is a multiple row recordset with the following columns: 1.memo_id, 2.memo_descr.
            //Use these values to populate the div_memo object, each item with an onClick action of
            //dtlMemo(memo_id).
            if (div_defect.SelectedValue != "")
            {
                defect_id = Convert.ToInt32(div_defect.SelectedValue);
                GetDefectDetails();
            }
        }

        protected void newDefect(object sender, EventArgs e)
        {
            defect_id = 0;
            input_defect_descr.Text = "";
            input_defect_title.Text = "";
            input_defect_title.Focus();
            //set @defect_id = 0 and clear contents of input_defect_descr and focus cursor on
            //input_defect_title.
        }
        protected void setDefect(object sender, EventArgs e)
        {
            //Validate that the following fields have valid entries: input_defect_title, input_defect_descr.
            //● If valid, execute pr_set_item(‘defect’, @emp_id, @null, @null, @defect_title, @defect_descr)
            //which returns 1 if successful, 0 if failure.
            //● If successful, execute pr_lst_items(‘defects’) as shown above in onLoad() function.If failure, show
            //standard error message

            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "defect";
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.str1 = input_defect_title.Text;
            obj.str2 = input_defect_descr.Text;

            DataSet ds = DTL_ITEM_Business.Put_SET_ITEM_DS(obj);

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["rc"].ToString()) > 0)
            {
                input_defect_descr.Text = "";
                input_defect_title.Text = "";

                OnloadDefects();
            }
            else
            {
                string err_msg = "\"" + ds.Tables[0].Rows[0]["err_msg"].ToString() + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }


        }
        protected void dtlMemo(object sender, EventArgs e)
        {
            if (div_memo.SelectedValue != "")
            {
                memo_id = Convert.ToInt32(div_memo.SelectedValue);
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "memo";
                obj.id1 = memo_id.ToString();
                DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];

                input_memo_descr.Text = dt.Rows[0]["memo_descr"].ToString();
                select_status.SelectedValue = dt.Rows[0]["defect_status_id"].ToString();

            }
            //execute pr_dtl_item(‘memo’, @memo_id) which returns a multiple row recordset with the
            //following columns: 1.defect_status_id, 2.memo_descr.Use status_id to select the appropriate row in
            //select_status object.Display memo_descr in input_memo_descr object.
        }
        protected void delMemo(object sender, EventArgs e)
        {
            if (memo_id == 0 || defect_id == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select Defect & Memo first.')", true);
            }
            else {
                Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                obj.mode = "memo";
                obj.id1 = memo_id;
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString()); ;

                int result = DTL_ITEM_Business.del_DEL_ITEM(obj);
                memofields();

                GetDefectDetails();
            }
            //First confirm whether the user wants to complete this action.
            //● On confirmation, execute pr_del_item(‘memo’, @memo_id, @emp_id) which returns 1 if
            //successful, 0 if failure.
            //● If successful, execute pr_lst_items(‘memo’, @memo_id) which returns a multiple row recordset
            //with the following columns: 1.memo_id, 2.memo_descr.Use these values to populate
            //div_memo, each with an onclick action of dtlMemo(memo_id).
        }
        protected void newMemo(object sender, EventArgs e)
        {
            memofields();
            //set @memo_id = 0, clear item selected in select_status object, clear contents of
            //input_memo_descr and focus cursor on select_status..
        }

        public void memofields()
        {
            memo_id = 0;
            select_status.SelectedValue = "Select";
            input_memo_descr.Text = "";
        }
        protected void setMemo(object sender, EventArgs e)
        {
            if (defect_id == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select Defect first.')", true);
            }
            else {
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "memo";
                obj.id1 = defect_id;
                obj.id2 = memo_id;
                obj.id3 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.id4 = Convert.ToInt32(select_status.SelectedValue.ToString());
                obj.str1 = Convert.ToString(input_memo_descr.Text);

                DataSet ds = DTL_ITEM_Business.Put_SET_ITEM_DS(obj);

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["rc"].ToString()) > 0)
                {
                    select_status.SelectedValue = "Select";
                    input_memo_descr.Text = "";

                    GetDefectDetails();
                }
                else
                {
                    string err_msg = "\"" + ds.Tables[0].Rows[0]["err_msg"].ToString() + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }
            }


            //Validate that the following fields have valid entries: select_status, input_memo_descr.
            //● If valid, execute pr_set_item(‘memo’, @defect_id, @memo_id, @emp_id, @status_id,
            //@memo_descr,) which returns 1 if successful, 0 if failure.
            //● If successful, execute pr_lst_items(‘memo’, @defect_id) and populate div_memo as as shown
            //above in dtlDefect() function.If failure, show standard error message.
        }

    }
}