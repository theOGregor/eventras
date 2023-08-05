using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;
using System.Drawing;

namespace SchoolTours.Operations
{
    public partial class contract : System.Web.UI.Page
    {
        //static int tour_id = 0;
        //static int contract_id = 0;
        //static int nr_days = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["tour_id"].ToString()) != 0)
                {
                    contract_id.Value = "0";
                    nr_days.Value = "0";
                    //tour_id = Convert.ToInt32(Session["tour_id"].ToString());
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

                    //tour_id = Convert.ToInt32(Session["tour_id"].ToString());
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void OnLoad()
        {
            // Execute pr_lst_items(‘contracts’, @tour_id) which returns two recordsets. The firest is a single row recordset with the following column: 1. tour_descr. Use this information to populate div_tour_info. 
            ///● The second recordset is a multiple row recordset with the following columns. Use this information to create the data grid in select_contract, each item having an onclick action of dtlContract(). 
            try
            {
                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

                obj.mode = "contracts";
                //obj.id1 = tour_id;
                obj.id1 = Convert.ToInt32(select_tour.SelectedValue);
                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                DataTable dt_tour = ds.Tables[0];
                DataTable dt_contract = ds.Tables[1];

                try
                {
                    div_tour_info.Text = dt_tour.Rows[0]["tour_descr"].ToString();
                }
                catch (Exception ex) { }
                ViewState["dt"] = dt_contract;
                gv_contract.DataSource = dt_contract;
                gv_contract.DataBind();

                //pr_lst_items(‘contract_type’)

                Obj_LST_ITEMS obj_contract_type = new Obj_LST_ITEMS();

                obj_contract_type.mode = "contract_type";


                DataTable dt_contract_type = DTL_ITEM_Business.Get_LST_ITEMS(obj_contract_type).Tables[0];


                select_contract_type.Items.Clear();
                select_contract_type.DataSource = dt_contract_type;
                select_contract_type.DataTextField = "contract_type_descr";
                select_contract_type.DataValueField = "contract_type_id";
                select_contract_type.DataBind();
                select_contract_type.Items.Insert(0, "Contract Type");

                //The select_date_type should be programmatically populated with the following items: Specific Date, Deposit Date, and Tour Date


                select_date_type.Items.Clear();
                select_date_type.Items.Insert(0, "Date Type");
                select_date_type.Items.Insert(1, "Specific Date");
                //select_date_type.Items.Insert(2, "Deposit Date");
                //select_date_type.Items.Insert(3, "Tour Date");
            }
            catch (Exception ex) { }
        }

        protected void gv_contract_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "contract")
            {
                try
                {
                    contract_id.Value = Convert.ToString(e.CommandArgument);
                    LinkButton lb = e.CommandSource as LinkButton;
                    GridViewRow gvr = lb.Parent.Parent as GridViewRow;
                    foreach (GridViewRow gr in gv_contract.Rows)
                    {
                        gr.BackColor = System.Drawing.Color.White;
                    }
                    gvr.BackColor = System.Drawing.Color.LightGray;
                    dtlContract();
                }
                catch (Exception ex) { }
            }
        }

        protected void gv_contract_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_contract.PageIndex = e.NewPageIndex;
            gv_contract.DataSource = ViewState["dt"];
            gv_contract.DataBind();
        }
        public void dtlContract()
        {
            //execute pr_dtl_item(‘contract’, @contract_id) which returns a single row with the following columns.Use these to populate the items on the screen.If status_ind = 1, 
            //chkbx_status should be checked. Mod_nm and mod_dt are not displayed until shwMod() is called.

            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();

            obj.mode = "contract";
            obj.id1 = contract_id.Value.ToString();

            DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DateTime dt_con_date = DateTime.Parse(dt.Rows[0]["contract_date"].ToString());
                input_date.Text = dt_con_date.ToString("yyyy-MM-dd");

                input_contract_descr.Text = dt.Rows[0]["contract_descr"].ToString();
                select_contract_type.SelectedValue = dt.Rows[0]["contract_type_id"].ToString();
                lbl_mod_details.Text = dt.Rows[0]["mod_nm"].ToString() + " " + dt.Rows[0]["mod_dt"].ToString();
                int chk_Status = Convert.ToInt32(dt.Rows[0]["status_ind"].ToString());
                if (chk_Status == 0)
                    chk_status.Checked = false;
                else
                    chk_status.Checked = true;
                select_date_type.SelectedValue = "Specific Date"; // added by sanjay 
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No row found')", true);
            }
        }

        public void shwDtCtrl(object sender, EventArgs e)
        {
            //If select_date_type = Specific Date, show the input_date control. Otherwise show the toggle_minus and input_nr_days control.Both of these controls, 
            //though shown above and below each other in the diagram above, should display inline between select_date_type and select_contract_type controls. 
            if (select_date_type.SelectedValue != "Date Type")
            {
                string date_type = select_date_type.SelectedValue;
                if (date_type == "Specific Date")
                {
                    input_date.Visible = true;
                    div_input_date.Visible = true;

                    div_plusMin_toggle.Visible = false;
                    input_nr_days.Text = "";

                    //toggle_plus.Visible = false;
                    //toggle_minus.Visible = false;
                }
                else if (date_type == "Deposit Date")
                {
                    input_date.Visible = false;
                    div_input_date.Visible = false;
                    div_plusMin_toggle.Visible = true;
                    toggle_plus.Visible = true;
                    toggle_minus.Visible = false;

                }
                else if (date_type == "Tour Date")
                {
                    input_date.Visible = false;
                    div_input_date.Visible = false;
                    div_plusMin_toggle.Visible = true;
                    toggle_plus.Visible = false;
                    toggle_minus.Visible = true;

                }
            }
            else
            {
                div_plusMin_toggle.Visible = false;
            }
        }
        public void setContract(object sender, EventArgs e)
        {
            //● If select_date_type = Specific Date, validate that input_date contains a valid date
            //● If select_date_type = Deposit Date, validate that toggle_plus is showing and that input_nr_days contains an integer value.Set local variable @nr_days = input_nr_days. 
            //● If select_date_type = Tour Date, validate that the toggle_minus is showing and that input_nr_days contains an integer value.Set local variable @nr_days = -input_nr_days. 
            //● Validate select_contract_type has an item selected. ● Validate input_contract_descr is not empty. 
            //● After validation completion, execute pr_set_item(‘contract’, @tour_id, @emp_id, @contract_id, @contract_type_id, @nr_days, @contract_date, @contract_descr, @status_ind, @date_type) 
            //which returns 1 if successful, 0 if failure. ● Call resetForm() ● Execute pr_list_items(‘contract’, @tour_id) as shown in onLoad() above.
            try
            {

                if (select_date_type.SelectedValue == "Deposit Date")
                {
                    nr_days.Value = Convert.ToString(input_nr_days.Text);
                }
                else if (select_date_type.SelectedValue == "Tour Date")
                {
                    nr_days.Value = Convert.ToString(input_nr_days.Text);
                    nr_days.Value = Convert.ToString(Convert.ToInt32(nr_days.Value) * -1);
                }
                Obj_SET_ITEM obj = new Obj_SET_ITEM();

                obj.mode = "contract";
                obj.id1 = Convert.ToInt32(select_tour.SelectedValue);
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.id3 = Convert.ToInt32(contract_id.Value);
                obj.id4 = Convert.ToInt32(select_contract_type.SelectedValue);
                obj.str1 = nr_days.ToString();
                obj.str2 = input_date.Text;
                obj.str3 = input_contract_descr.Text;
                obj.str4 = chk_status.Checked == true ? "1" : "0";
                obj.str5 = select_date_type.SelectedItem.ToString();



                DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];
                if (Convert.ToInt32(dt_Result.Rows[0]["rc"].ToString()) > 0)
                {
                    resetForm();
                    contract_id.Value = "0";
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
                string err_msg = ex.Message;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }


        }
        public void newContract(object sender, EventArgs e)
        {
            resetForm();
            contract_id.Value = "0";

        }
        public void delContract(object sender, EventArgs e)
        {
            //After confirmation, execute pr_del_item(‘contract’, @contract_id, @emp_id) which returns 1 if successful, 0 if failure. ● Call resetForm() 
            //● Execute pr_list_items(‘contract’, @tour_id) as shown in onLoad() below
            try
            {
                if (contract_id.Value != "0")
                {
                    Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                    obj.mode = "contract";
                    obj.id1 = Convert.ToInt32(contract_id.Value);
                    obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());

                    DataTable dt_Result = DTL_ITEM_Business.del_DEL_ITEM_DS(obj).Tables[0];
                    if (Convert.ToInt32(dt_Result.Rows[0]["rc"].ToString()) > 0)
                    {
                        resetForm();
                        OnLoad();
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select contract first.')", true);
                }
            }
            catch (Exception ex)
            {
                resetForm();
                OnLoad();
            }
        }
        public void resetForm()
        {
            try
            {
                div_plusMin_toggle.Visible = false;
                input_contract_descr.Text = "";
                input_date.Text = "";
                input_date.Text = "";
                select_contract_type.SelectedValue = "Contract Type";
                select_date_type.SelectedValue = "Date Type";
                contract_id.Value = "0";
                div_input_date.Visible = true;
                input_date.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void toggle_plus_Click(object sender, ImageClickEventArgs e)
        {
            toggle_plus.Visible = false;
            toggle_minus.Visible = true;
        }

        protected void toggle_minus_Click(object sender, ImageClickEventArgs e)
        {
            toggle_plus.Visible = true;
            toggle_minus.Visible = false;
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