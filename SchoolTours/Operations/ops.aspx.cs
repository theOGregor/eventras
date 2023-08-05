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
    public partial class ops : System.Web.UI.Page
    {
        //static int inv_type_ind = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                inv_type_ind.Value = "0";
                Obj_global_value.tour_id = 0;
                OnLoad();

            }
        }
        public void OnLoad()
        {
            //Execute pr_lst_items(‘division’, @emp_id) which returns a multiple row recordset with the following columns: 1.div_id, 2.div_nm.
            //Use these values to populate select_div with the first row being a prompt of “Division” followed by the items in the recordset. 
            //● Execute pr_lst_items(‘employee @emp_id) which returns a multiple row recordset with the following columns: 1.emp_id, 2.emp_nm.
            //Use these values to populate select_emp with the first row being a prompt of “Operator” followed by the items in the recordset. 
            //● Execute pr_lst_items(‘year_evts’, @emp_id, @year) which returns a multiple row recordset with the following columns: 1.evt_id, 2.evt_nm.
            //Use these values to populate select_evt with the first row being a prompt of “Event” followed by the items in the recordset
            try
            {

                int CurrentYear = Convert.ToInt32(DateTime.Now.Year);
                //select_year.Items.Insert(0, CurrentYear.ToString());
                //select_year.Items.Insert(1, (CurrentYear - 1).ToString());
                //select_year.Items.Insert(2, (CurrentYear - 2).ToString());
                //select_year.Items.Insert(3, (CurrentYear - 3).ToString());
                //select_year.Items.Insert(4, (CurrentYear + 1).ToString());
                //select_year.Items.Insert(5, (CurrentYear + 2).ToString());
                //select_year.Items.Insert(6, (CurrentYear + 3).ToString());


                select_year.Items.Insert(0, (CurrentYear - 3).ToString());
                select_year.Items.Insert(1, (CurrentYear - 2).ToString());
                select_year.Items.Insert(2, (CurrentYear - 1).ToString());
                select_year.Items.Insert(3, CurrentYear.ToString());
                select_year.Items.Insert(4, (CurrentYear + 1).ToString());
                select_year.Items.Insert(5, (CurrentYear + 2).ToString());
                select_year.Items.Insert(6, (CurrentYear + 3).ToString());
                select_year.SelectedValue = CurrentYear.ToString();

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


                Obj_LST_ITEMS obj2 = new Obj_LST_ITEMS();
                obj2.mode = "year_evts";
                obj2.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                obj2.str1 = Convert.ToString(CurrentYear);

                DataTable dt2 = DTL_ITEM_Business.Get_LST_ITEMS(obj2).Tables[0];
                select_evt.DataSource = dt2;
                select_evt.DataTextField = "evt_nm";
                select_evt.DataValueField = "evt_id";
                select_evt.DataBind();
                select_evt.Items.Insert(0, "Select");

                //Search();
            }
            catch (Exception ex) { }
        }
        public void srchEvts(object sender, EventArgs e)
        {
            Search();
        }
        public void Search()
        {
            // ​is called onChange of each of the select lists​. ​Execute pr_search(‘tour’, @emp_id, null, null, @select_div, @select_emp, @select_year, @select_evt)
            //which returns a multiple row recordset with the following columns: 1.tour_id, 2. tour_descr. Use these values to populate select_tour. 
            try
            {

                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tour";
                obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.str1 = select_div.SelectedValue == "Select" ? null : select_div.SelectedValue;
                obj.str2 = select_emp.SelectedValue == "Select" ? null : select_emp.SelectedValue;
                obj.str3 = select_year.SelectedValue == "" ? null : select_year.SelectedValue;
                obj.str4 = select_evt.SelectedValue == "Select" ? null : select_evt.SelectedValue;

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
                            ListItem li = new ListItem(item["tour_descr"].ToString(), item["tour_id"].ToString());
                            li.Attributes.Add("title", item["tour_descr"].ToString());
                            li.Attributes.Add("data-toggle", "tooltip");
                            li.Attributes.Add("data-container", "#tooltip_container");
                            //data-toggle="tooltip" data-container="#tooltip_container"
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

            select_evt.DataSource = dt2;
            select_evt.DataTextField = "evt_nm";
            select_evt.DataValueField = "evt_id";
            select_evt.DataBind();
            select_evt.Items.Insert(0, "Select");
        }
        public void getTourType(object sender, EventArgs e)
        {
            //execute pr_dtl_item(‘tour_type’, @tour_id) which returns a single row recordset with the following columns: 1. active_ind, 2. pmt_plan_ind, 3. pax_ind, 4. flying_ind , 5. cmg_bus_ind, 
            //6. inv_type_ind, 7. final_ind. Use these values to populate corresponding local variables
            if (select_tour.SelectedValue != "")
            {
                try
                {
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "tour_type";
                    obj.id1 = select_tour.SelectedValue;

                    DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                    inv_type_ind.Value = Convert.ToString(dt.Rows[0]["inv_type_ind"].ToString());

                    Obj_global_value.Session_Id = this.Session.SessionID;

                    //Obj_global_value.div_id = select_div.SelectedValue;
                    //Obj_global_value.emp_id = select_emp.SelectedValue;
                    //Obj_global_value.year_id = select_year.SelectedValue;
                    //Obj_global_value.evt_id = select_evt.SelectedValue;

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
                    Session["evt_id"] = select_evt.SelectedValue;

                    Session["tour_id"] = Convert.ToInt32(select_tour.SelectedValue);
                    Session["active_ind"] = Convert.ToInt32(dt.Rows[0]["active_ind"].ToString());
                    Session["pmt_plan_ind"] = Convert.ToInt32(dt.Rows[0]["pmt_plan_ind"].ToString());
                    Session["pax_ind"] = Convert.ToInt32(dt.Rows[0]["pax_ind"].ToString());
                    Session["flying_ind"] = Convert.ToInt32(dt.Rows[0]["flying_ind"].ToString());
                    Session["cmg_bus_ind"] = Convert.ToInt32(dt.Rows[0]["cmg_bus_ind"].ToString());
                    Session["inv_type_ind"] = Convert.ToInt32(dt.Rows[0]["inv_type_ind"].ToString());
                    Session["final_ind"] = Convert.ToInt32(dt.Rows[0]["final_ind"].ToString());
                }
                catch (Exception ex)
                {

                }

            }
        }

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

            if (select_tour.SelectedValue != "")
            {
                string RredirectUrl = "";
                if (value == "details")
                    RredirectUrl += "tour_details";
                else if (value == "pax")
                    RredirectUrl += "pax";
                else if (value == "billing")
                {
                    if (inv_type_ind.Value == "0")
                        RredirectUrl += "billing_grp";
                    else
                        RredirectUrl += "billing_ind";
                }
                else if (value == "contract")
                    RredirectUrl += "contract";
                else if (value == "cost")
                {
                    if (inv_type_ind.Value == "0")
                        RredirectUrl += "cost_grp";
                    else
                        RredirectUrl += "cost_ind";
                }
                Response.Redirect(RredirectUrl);
            }
        }

    }
}