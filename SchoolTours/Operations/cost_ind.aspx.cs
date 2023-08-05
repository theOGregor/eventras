using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;

namespace SchoolTours.Operations
{
    public partial class cost_ind : System.Web.UI.Page
    {
        //static int tour_id = 0;
        //static int cost_id = 0;
        //static int pax_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["tour_id"].ToString()) != 0)
                {
                    OPS_cost_id.Value = "0";
                    OPS_pax_id.Value = "0";
                    OPS_tour_id.Value = Convert.ToString(Convert.ToInt32(Session["tour_id"].ToString()));
                    OnPreLoad();
                    OnLoad();
                }
                else
                {
                    Response.Redirect("ops");
                }
            }
        }

        public void OnPreLoad()
        {
            try
            {

                int CurrentYear = Convert.ToInt32(DateTime.Now.Year);

                //select_year.Items.Insert(0, CurrentYear.ToString());
                //select_year.Items.Insert(1, (CurrentYear - 1).ToString());
                //select_year.Items.Insert(2, (CurrentYear - 2).ToString());
                select_year.Items.Insert(0, (CurrentYear - 3).ToString());
                select_year.Items.Insert(1, (CurrentYear - 2).ToString());
                select_year.Items.Insert(2, (CurrentYear - 1).ToString());
                select_year.Items.Insert(3, CurrentYear.ToString());
                select_year.Items.Insert(4, (CurrentYear + 1).ToString());
                select_year.Items.Insert(5, (CurrentYear + 2).ToString());
                select_year.Items.Insert(6, (CurrentYear + 3).ToString());


                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "division";
                obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

                select_div.DataSource = dt;
                select_div.DataTextField = "div_nm";
                select_div.DataValueField = "div_id";
                select_div.DataBind();
                select_div.Items.Insert(0, "Select");


                Obj_LST_ITEMS obj1 = new Obj_LST_ITEMS();
                obj1.mode = "emps";
                obj1.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                DataTable dt1 = DTL_ITEM_Business.Get_LST_ITEMS(obj1).Tables[0];

                select_emp.DataSource = dt1;
                select_emp.DataTextField = "employee_nm";
                select_emp.DataValueField = "emp_id";
                select_emp.DataBind();
                select_emp.Items.Insert(0, "Select");


                //Obj_LST_ITEMS obj2 = new Obj_LST_ITEMS();
                //obj2.mode = "year_evts";
                //obj2.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                //obj2.str1 = Convert.ToString(CurrentYear);

                //DataTable dt2 = DTL_ITEM_Business.Get_LST_ITEMS(obj2).Tables[0];
                //select_evt_serach.DataSource = dt2;
                //select_evt_serach.DataTextField = "evt_nm";
                //select_evt_serach.DataValueField = "evt_id";
                //select_evt_serach.DataBind();
                //select_evt_serach.Items.Insert(0, "Select");


                select_div.SelectedValue = Session["div_id"].ToString();
                select_emp.SelectedValue = Session["emp_id_ops"].ToString();
                select_year.SelectedValue = Session["year_id"].ToString();
                ListEvts();
                select_evt_serach.SelectedValue = Session["evt_id"].ToString();
                srchEvtsfunction();
                select_tour.SelectedValue = Session["tour_id"].ToString();

            }
            catch (Exception ex) { }
        }
        public void lstEvts(object sender, EventArgs e)
        {
            ListEvts();
        }
        public void ListEvts()
        {
            //lstEvts() execute pr_lst_items('evts’, @div_id, @emp_id, @year). This returns a multiple-row recordset
            //with the following columns: 1.evt_id, 2.evt_nm.Use these values to populate select_evt.

            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
            obj.mode = "evts";
            if (select_div.SelectedValue != "Select")
                obj.id1 = Convert.ToInt32(select_div.SelectedValue);

            obj.str1 = select_emp.SelectedValue == "Select" ? null : select_emp.SelectedValue;
            obj.str2 = select_year.SelectedValue == "" ? null : select_year.SelectedValue;


            DataTable dt2 = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

            select_evt_serach.DataSource = dt2;
            select_evt_serach.DataTextField = "evt_nm";
            select_evt_serach.DataValueField = "evt_id";
            select_evt_serach.DataBind();
            select_evt_serach.Items.Insert(0, "Select");
        }
        public void srchEvts(object sender, EventArgs e)
        {
            srchEvtsfunction();
        }
        public void srchEvtsfunction()
        {

            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tour";
                obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.str1 = select_div.SelectedValue == "Select" ? null : select_div.SelectedValue;
                obj.str2 = select_emp.SelectedValue == "Select" ? null : select_emp.SelectedValue;
                obj.str3 = select_year.SelectedValue == "" ? null : select_year.SelectedValue;
                obj.str4 = select_evt_serach.SelectedValue == "Select" ? null : select_evt_serach.SelectedValue;

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];
                select_tour.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("rc"))
                    {
                        string err_msg = "\"" + dt.Rows[0]["err_msg"].ToString() + "\"";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);

                    }
                    else
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(item["tour_descr"].ToString(), item["tour_id"].ToString());
                            li.Attributes.Add("title", item["tour_descr"].ToString());
                            li.Attributes.Add("data-toggle", "tooltip");
                            li.Attributes.Add("data-container", "#tooltip_container");
                            select_tour.Items.Add(li);
                        }
                        //select_tour.DataSource = dt;
                        //select_tour.DataTextField = "tour_descr";
                        //select_tour.DataValueField = "tour_id";
                        //select_tour.DataBind();
                    }
                }
            }
            catch (Exception ex) { }
        }
        public void getTourType(object sender, EventArgs e)
        {
            getTourTypefunction();

        }
        public void getTourTypefunction()
        {


            if (select_tour.SelectedValue != "")
            {
                try
                {
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "tour_type";
                    obj.id1 = select_tour.SelectedValue;

                    DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                    //inv_type_ind = Convert.ToInt32(dt.Rows[0]["inv_type_ind"].ToString());

                    //Obj_global_value.div_id = select_div.SelectedValue;
                    //Obj_global_value.emp_id = select_emp.SelectedValue;
                    //Obj_global_value.year_id = select_year.SelectedValue;
                    //Obj_global_value.evt_id = select_evt_serach.SelectedValue;

                    //Obj_global_value.tour_id = Convert.ToInt32(select_tour.SelectedValue);
                    //Obj_global_value.active_ind = Convert.ToInt32(dt.Rows[0]["active_ind"].ToString());
                    //Obj_global_value.pmt_plan_ind = Convert.ToInt32(dt.Rows[0]["pmt_plan_ind"].ToString());
                    //Obj_global_value.pax_ind = Convert.ToInt32(dt.Rows[0]["pax_ind"].ToString());
                    //Obj_global_value.flying_ind = Convert.ToInt32(dt.Rows[0]["flying_ind"].ToString());
                    //Obj_global_value.cmg_bus_ind = Convert.ToInt32(dt.Rows[0]["cmg_bus_ind"].ToString());
                    //Obj_global_value.inv_type_ind = Convert.ToInt32(dt.Rows[0]["inv_type_ind"].ToString());
                    //Obj_global_value.final_ind = Convert.ToInt32(dt.Rows[0]["final_ind"].ToString());

                    Session["div_id"] = select_div.SelectedValue;
                    Session["emp_id_ops"] = select_emp.SelectedValue;
                    Session["year_id"] = select_year.SelectedValue;
                    Session["evt_id"] = select_evt_serach.SelectedValue;

                    Session["tour_id"] = Convert.ToInt32(select_tour.SelectedValue);
                    Session["active_ind"] = Convert.ToInt32(dt.Rows[0]["active_ind"].ToString());
                    Session["pmt_plan_ind"] = Convert.ToInt32(dt.Rows[0]["pmt_plan_ind"].ToString());
                    Session["pax_ind"] = Convert.ToInt32(dt.Rows[0]["pax_ind"].ToString());
                    Session["flying_ind"] = Convert.ToInt32(dt.Rows[0]["flying_ind"].ToString());
                    Session["cmg_bus_ind"] = Convert.ToInt32(dt.Rows[0]["cmg_bus_ind"].ToString());
                    Session["inv_type_ind"] = Convert.ToInt32(dt.Rows[0]["inv_type_ind"].ToString());
                    Session["final_ind"] = Convert.ToInt32(dt.Rows[0]["final_ind"].ToString());

                    OPS_tour_id.Value = Convert.ToString(Session["tour_id"].ToString());
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void OnLoad()
        {
            // Execute pr_dtl_item(‘tour_descr’, @tour_id) which returns a single row recordset with the following column: 1. tour_descr. Use this value to populate div_tour_info. 
            //● Hard code the first item in select_pax. It should have an ID of 0 and a displayed value of “Group”. Execute pr_lst_items(‘pax’, @tour_id) 
            //which returns a multiple row recordset with the following columns: 1. pax_id, 2, pax_descr. Use these values to populate subsequent items in select_pax. 
            //● Execute pr_lst_items(‘cost_category’) which returns a multiple row recordset with the following columns: 1. category_id, 2. category_descr. Use these values to populate select_category. 
            //● Execute pr_lst_items(‘pmt_per’) which returns a multiple row recordset with the following columns 1. pmt_per_id, 2. pmt_per_descr. Use these values to populate select_pmt_per


            Obj_DTL_ITEM obj_pax = new Obj_DTL_ITEM();
            obj_pax.mode = "tour_descr";
            obj_pax.id1 = OPS_tour_id.Value.ToString();
            DataSet ds_pax = DTL_ITEM_Business.GetItemDetails(obj_pax);

            if (ds_pax.Tables[0].Rows.Count > 0)
            {
                div_tour_info.Text = ds_pax.Tables[0].Rows[0]["tour_descr"].ToString();
            }





            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
            obj.mode = "pax";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);

            DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

            select_pax.DataSource = ds.Tables[0];
            select_pax.DataTextField = "pax_descr";
            select_pax.DataValueField = "pax_id";
            select_pax.DataBind();
            //select_pax.Items.Insert(0, "Select");



            //DataTable dt_cost = ds.Tables[1];
            //ViewState["dt"] = dt_cost;
            //gv_group_cost.DataSource = dt_cost;
            //gv_group_cost.DataBind();

            //DataTable dt_cost_summary = ds.Tables[2];


            //DataTable dt_tour_r = new DataTable();
            //dt_tour_r.Columns.AddRange(new DataColumn[1] { new DataColumn("Name") });
            //for (int i = 0; i < dt_cost_summary.Columns.Count; i++)
            //{
            //    string Columns = dt_cost_summary.Columns[i].ToString();
            //    string cost = dt_cost_summary.Rows[0][i].ToString();


            //    dt_tour_r.Rows.Add(Columns + " - " + cost);
            //}

            //div_cost_summary.DataSource = dt_tour_r;
            //div_cost_summary.DataTextField = "Name";
            //div_cost_summary.DataBind();

            Obj_LST_ITEMS obj_cost_category = new Obj_LST_ITEMS();
            obj_cost_category.mode = "cost_category";
            DataTable ds_cost_category = DTL_ITEM_Business.Get_LST_ITEMS(obj_cost_category).Tables[0];

            select_category.DataSource = ds_cost_category;
            select_category.DataTextField = "category_descr";
            select_category.DataValueField = "category_id";
            select_category.DataBind();
            select_category.Items.Insert(0, "Cost Category");

            Obj_LST_ITEMS obj_pmt_per = new Obj_LST_ITEMS();
            obj_pmt_per.mode = "pmt_per";
            DataTable ds_pmt_per = DTL_ITEM_Business.Get_LST_ITEMS(obj_pmt_per).Tables[0];

            select_pmt_per.DataSource = ds_pmt_per;
            select_pmt_per.DataTextField = "pmt_per_descr";
            select_pmt_per.DataValueField = "pmt_per_id";
            select_pmt_per.DataBind();
            select_pmt_per.Items.Insert(0, "Per");

            if (ds.Tables[0].Rows.Count > 0)
            {
                OPS_pax_id.Value = Convert.ToString(ds.Tables[0].Rows[0]["pax_id"].ToString());
                dtlPax();
            }
        }

        public void delCost(object sender, EventArgs e)
        {
            //● After confirmation, execute pr_del_item(‘cost’, @cost_id, @emp_id) which returns 1 if successful, 0 if failure. ● Call resetForm() 
            //● Execute pr_list_items(‘costs_ind’, @pax_id) as shown in onLoad() below
            try
            {
                if (OPS_cost_id.Value != "0")
                {
                    Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                    obj.mode = "cost";
                    obj.id1 = Convert.ToInt32(OPS_cost_id.Value);
                    obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                    //DataTable dt = DTL_ITEM_Business.del_DEL_ITEM_DS(obj).Tables[0];

                    int response = DTL_ITEM_Business.del_DEL_ITEM(obj);


                    if (response == 1)
                    {
                        resetForm();
                        OnLoad();
                    }
                    else {
                        //string err_msg = "\"" + dt.Rows[0]["err_msg"].ToString() + "\"";
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('fail')", true);
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select contract first.')", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void newCost(object sender, EventArgs e)
        {

            resetForm();
        }
        public void setCost(object sender, EventArgs e)
        {
            //Validate select_category_id and select_pmt_per have an item selected. ● Validate input_cost_descr and input_cost_amt are not empty. 
            //● After validation completion, execute pr_set_item(‘cost_ind’, @tour_id, @emp_id, @cost_id, @category_id, @cost_descr, @cost_amt, @pmt_per_id, @pax_id) which returns 1 if successful, 0 if failure. 
            //● Call resetForm() ● Execute pr_list_items(‘cost’, @tour_id) as shown in onLoad() above. 
            try
            {
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "cost_ind";
                obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.id3 = Convert.ToInt32(OPS_cost_id.Value);
                obj.id4 = Convert.ToInt32(select_category.SelectedValue);
                obj.str1 = input_cost_descr.Text;
                obj.str2 = input_cost_amt.Text;
                obj.str3 = select_pmt_per.SelectedValue;
                obj.str4 = select_pax.SelectedValue;


                DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];

                if (Convert.ToInt32(dt_Result.Rows[0]["rc"].ToString()) > 0)
                {
                    resetForm();
                    OPS_cost_id.Value = "0";
                    OnLoad();
                }
                else
                {
                    string err_msg = "\"" + dt_Result.Rows[0]["err_msg"].ToString() + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }

            }
            catch (Exception ex)
            {

            }

        }
        protected void gv_group_cost_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cost")
            {
                OPS_cost_id.Value = Convert.ToString(e.CommandArgument);
                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow gvr = lb.Parent.Parent as GridViewRow;
                foreach (GridViewRow gr in gv_group_cost.Rows)
                {
                    gr.BackColor = System.Drawing.Color.White;
                }
                gvr.BackColor = System.Drawing.Color.LightGray;
                dtlCost();
            }
        }

        protected void gv_group_cost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_group_cost.PageIndex = e.NewPageIndex;
            gv_group_cost.DataSource = ViewState["dt"];
            gv_group_cost.DataBind();
        }

        public void resetForm()
        {
            OPS_cost_id.Value = "0";
            input_cost_descr.Text = "";

            input_cost_amt.Text = "";
            //div_cost_summary.Items.Clear();
            gv_cost_summary.DataSource = null;
            gv_cost_summary.DataBind();
            try
            {
                select_category.SelectedValue = "Cost Category";
                select_pmt_per.SelectedValue = "Per";
            }
            catch (Exception ex) { }

        }
        public void dtlCost()
        {
            // execute pr_dtl_item(‘cost’, @cost_id) which returns a single row with the following columns. 
            //Use these to populate the items on the screen. Mod_nm and mod_dt are not displayed until shwMod() is called.  
            try
            {
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();

                obj.mode = "cost";
                obj.id1 = OPS_pax_id.Value.ToString();
                DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    input_cost_descr.Text = dt.Rows[0]["cost_descr"].ToString();
                    select_category.SelectedValue = dt.Rows[0]["category_id"].ToString();
                    //input_cost_amt.Text = dt.Rows[0]["cost_amt"].ToString();
                    decimal amt = Convert.ToDecimal(dt.Rows[0]["cost_amt"].ToString());
                    if (dt.Rows[0]["cost_amt"].ToString() != "")
                        input_cost_amt.Text = amt.ToString("0.00");
                    else
                        input_cost_amt.Text = dt.Rows[0]["cost_amt"].ToString();
                    select_pmt_per.SelectedValue = dt.Rows[0]["pmt_per_id"].ToString();
                    lbl_mod_details.Text = dt.Rows[0]["mod_nm"].ToString() + " " + dt.Rows[0]["mod_dt"].ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record found.')", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void select_pax_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlPax();
        }
        public void dtlPax()
        {

            //Execute pr_dtl_item(‘costs_ind’, @pax_id) which returns a multiple row recordset with the following columns. Use this information to create the data grid in select_contract, 
            //each item having an onclick action of dtlCost()
            try
            {
                //if (select_pax.SelectedValue != "Select")
                //{
                OPS_pax_id.Value = Convert.ToString(select_pax.SelectedValue);
                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "costs_ind";
                obj.id1 = Convert.ToInt32(OPS_pax_id.Value);

                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);
                DataTable dt_cost = ds.Tables[0];
                ViewState["dt"] = dt_cost;
                gv_group_cost.DataSource = dt_cost;
                gv_group_cost.DataBind();

                DataTable dt_cost_summary = ds.Tables[1];

                DataTable dt_tour_r = new DataTable();
                dt_tour_r.Columns.AddRange(new DataColumn[2] { new DataColumn("Name"), new DataColumn("Amount") });
                DataRow row;
                for (int i = 0; i < dt_cost_summary.Columns.Count; i++)
                {
                    string Columns = dt_cost_summary.Columns[i].ToString();
                    string cost = dt_cost_summary.Rows[0][i].ToString();
                    row = dt_tour_r.NewRow();

                    //Map all the values in the columns
                    row["Name"] = Columns;
                    row["Amount"] = (Convert.ToDecimal(cost)).ToString("N2");

                    dt_tour_r.Rows.Add(row);
                    //dt_tour_r.Rows.Add(Columns + " - " + cost);
                }
                gv_cost_summary.DataSource = dt_tour_r;
                gv_cost_summary.DataBind();
                //div_cost_summary.DataSource = dt_tour_r;
                //div_cost_summary.DataTextField = "Name";
                //div_cost_summary.DataBind();
                // }
            }
            catch (Exception ex) { }
        }

        //================================================= Redirect
        protected void img_details_Click(object sender, ImageClickEventArgs e)
        {
            goPage("details");
        }
        protected void img_pax_Click(object sender, ImageClickEventArgs e)
        {
            goPage("pax");
        }
        protected void img_billing_Click(object sender, ImageClickEventArgs e)
        {
            goPage("billing");
        }
        protected void img_contracts_Click(object sender, ImageClickEventArgs e)
        {
            goPage("contract");
        }
        protected void img_cost_Click(object sender, ImageClickEventArgs e)
        {
            goPage("cost");
        }
        public void goPage(string value)
        {
            getTourTypefunction();
            string RredirectUrl = "";
            if (value == "details")
                RredirectUrl += "tour_details";
            else if (value == "pax")
                RredirectUrl += "pax";
            else if (value == "billing")
            {
                if (Convert.ToInt32(Session["inv_type_ind"].ToString()) == 0)
                    RredirectUrl += "billing_grp";
                else
                    RredirectUrl += "billing_ind";
            }
            else if (value == "contract")
                RredirectUrl += "contract";
            else if (value == "cost")
            {
                if (Convert.ToInt32(Session["inv_type_ind"].ToString()) == 0)
                    RredirectUrl += "cost_grp";
                else
                    RredirectUrl += "cost_ind";
            }
            Response.Redirect(RredirectUrl);
        }

    }
}