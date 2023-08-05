using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;

namespace SchoolTours.Accounting
{
    public partial class pmt_finder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnLoad();
            }
        }

        public void OnLoad()
        {
            // Execute pr_lst_items(‘tour’) which returns a multiple row recordset with the following columns: 1. tour_id, 2. tour_nm. Use these values to populate select_tour. 
            //● Execute pr_lst_items(‘pmt_methods’) which returns a multiple row recordset with the following columns: 1. pmt_method_id, 2. pmt_method_descr. 
            //Use these values to populate select pmt_method. 
            try
            {
                gv_pmt_finder.DataSource = null;
                gv_pmt_finder.DataBind();

                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "tour";
                DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

                select_tour.DataSource = dt;
                select_tour.DataTextField = "tour_nm";
                select_tour.DataValueField = "tour_id";
                select_tour.DataBind();
                select_tour.Items.Insert(0, "Tour");

                Obj_LST_ITEMS obj_pmt_methods = new Obj_LST_ITEMS();
                obj_pmt_methods.mode = "pmt_methods";
                DataTable dt_pmt_methods = DTL_ITEM_Business.Get_LST_ITEMS(obj_pmt_methods).Tables[0];

                select_pmt_method.DataSource = dt_pmt_methods;
                select_pmt_method.DataTextField = "ref_descr";
                select_pmt_method.DataValueField = "ref_id";
                select_pmt_method.DataBind();
                select_pmt_method.Items.Insert(0, "Pmt Method");
            }
            catch (Exception ex)
            {

            }
        }

        public void clrFields(object sender, EventArgs e)
        {
            try
            {
                select_tour.SelectedValue = "Select";
                select_pmt_method.SelectedValue = "Select";
                input_pmt_amt.Text = "";
                input_start_date.Text = "";
                input_end_date.Text = "";
                input_memo.Text = "";


                gv_pmt_finder.DataSource = null;
                gv_pmt_finder.DataBind();
            }
            catch (Exception ex) { }
        }
        public void pmtSearch(object sender, EventArgs e)
        {
            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "pmt_finder";
                if (select_tour.SelectedValue != "Tour")
                    obj.id1 = Convert.ToInt32(select_tour.SelectedValue);
                if (select_pmt_method.SelectedValue != "Pmt Method")
                    obj.id2 = Convert.ToInt32(select_pmt_method.SelectedValue);
                obj.str1 = input_pmt_amt.Text.Trim() == "" ? null : input_pmt_amt.Text.Trim();
                obj.str2 = input_start_date.Text == "" ? null : input_start_date.Text;
                obj.str3 = input_end_date.Text == "" ? null : input_end_date.Text;
                obj.str4 = input_memo.Text == "" ? null : input_memo.Text;
                if (obj.str1 != null)
                    obj.str1 = obj.str1.Replace(",", "");
                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("rc"))
                    {
                        string err_msg = "\"" + dt.Rows[0]["err_msg"].ToString() + "\"";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);

                        gv_pmt_finder.DataSource = null;
                        gv_pmt_finder.DataBind();
                        return;
                    }
                    gv_pmt_finder.DataSource = dt;
                    gv_pmt_finder.DataBind();
                }
                else
                {
                    gv_pmt_finder.DataSource = null;
                    gv_pmt_finder.DataBind();
                }

            }
            catch (Exception ex) { }
        }
    }
}