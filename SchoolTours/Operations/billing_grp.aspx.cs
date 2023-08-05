using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;
using System.Text;
using System.Net;
using iTextSharp.text;
using System.IO;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace SchoolTours.Operations
{
    public partial class billing_grp : System.Web.UI.Page
    {
        //static int tour_id = 0;
        //static int inv_id = 0;
        //static int pmt_id = 0;
        //static int pax_nr = 0;
        // public static string emp_id ;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["empid"] = Convert.ToString(Session["emp_id"]);
                SetvalueInViewState();
                GenerateLog("IsPostBack Log -- " + Session["emp_id"]);

                if (Convert.ToInt32(Session["tour_id"].ToString()) != 0)
                {
                    OPS_tour_id.Value = "0";
                    OPS_pmt_id.Value = "0";
                    OPS_pax_nr.Value = "0";
                    OPS_inv_id.Value = "0";
                    OPS_tour_id.Value = Convert.ToString(Session["tour_id"].ToString());
                    OnPreLoad();
                    OnLoad();
                }
                else
                {
                    Response.Redirect("ops");
                }
            }
            if (IsPostBack)
            {
                GetvalueInViewState();
                GenerateLog("ViewState - " + Convert.ToString(ViewState["empid"]));
                // GenerateLog("Before Log -- " + SchoolTours.Models.globalVar._emp_id);

                Session["emp_id"] = Convert.ToString(ViewState["empid"]);
                GenerateLog("After Log -- " + Convert.ToString(Session["emp_id"]));
            }
            // GenerateLog("tour_id Log -- " + tour_id);
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
            catch (Exception ex)
            {
                GenerateLog("Error 101 " + ex.Message);
            }
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
            catch (Exception ex) { GenerateLog("Error 102 - " + ex.Message); }
        }
        public void getTourType(object sender, EventArgs e)
        {
            getTourTypefunction();

        }
        public void getTourTypefunction()
        {
            //GenerateLog("session - " + Session["emp_id"].ToString() + " ------ " + Obj_global_value.tour_id);

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
                    GenerateLog("Error 111 : " + ex.Message);
                }
            }
        }
        public void OnLoad()
        {
            //Execute pr_lst_items(‘billing_options’, @tour_id) which returns three multiple row recordsets: ○ Populate select_ratio from 1.ratio_id, 2.ratio_descr  
            //○ Populate select_pmt_per from 1.pmt_per_id, 2.pmt_per_descr  ○ Populate select_method from 1.method_id, 2.method_descr
            //● Execute pr_dtl_item(‘pax_nr’, @tour_id) which returns a single row recordset with the following columns: 1.pax_nr.If @options_nr & 4 = false, 
            //then display the value from the recordset in input_pax_nr.Otherwise display the text “PAX = pax_nr” in place of input__pax_nr in the same font size, font face, and color as the div_tour_info. 
            //● Execute pr_dtl_item(‘tour_descr’, @tour_id) which returns a single row recordset with the following column: 1.tour_descr.Use this value to populate div_tour_info. 
            //● Execute pr_lst_items(‘inv’, @tour_id) which returns a multiple row recordset with the following columns: 1.inv_id, 2.inv_descr.
            //Display these records in select_inv, each with an onclick action of dtlInv(inv_id). 
            //● Execute pr_lst_items(‘pmt’, @tour_id) which returns a multiple row recordset with the following columns: 1.pmt_id, 2.pmt_descr.Display these records in select_pmt, 
            ///each with an onclick action of dtlPmt(pmt_id). ● Execute pr_lst_items(‘bill_reminder’’ @tour_id) which returns a multiple row recordset with the following columns: 1.reminder_descr, 2.reminder_amt.
            /////Display these records in div_reminder.
            try
            {
                #region  billing_options
                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "billing_options";
                obj.id1 = Convert.ToInt32(OPS_tour_id.Value);

                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                select_ratio.DataSource = ds.Tables[0];
                select_ratio.DataTextField = "ratio_descr";
                select_ratio.DataValueField = "ratio_id";
                select_ratio.DataBind();
                select_ratio.Items.Insert(0, "Ratio");

                select_pmt_per.DataSource = ds.Tables[1];
                select_pmt_per.DataTextField = "pmt_per_descr";
                select_pmt_per.DataValueField = "pmt_per_id";
                select_pmt_per.DataBind();
                select_pmt_per.Items.Insert(0, "Per");

                select_pmt_method.DataSource = ds.Tables[2];
                select_pmt_method.DataTextField = "method_descr";
                select_pmt_method.DataValueField = "method_id";
                select_pmt_method.DataBind();
                select_pmt_method.Items.Insert(0, "Type");

                #endregion  billing_options


                Obj_DTL_ITEM obj_pax_nr = new Obj_DTL_ITEM();
                obj_pax_nr.mode = "pax_nr";
                obj_pax_nr.id1 = OPS_tour_id.Value.ToString();
                DataTable dt_pax_nr = DTL_ITEM_Business.GetItemDetails(obj_pax_nr).Tables[0];
                if (dt_pax_nr.Rows[0]["pax_nr"].ToString() != "")
                    OPS_pax_nr.Value = Convert.ToString(dt_pax_nr.Rows[0]["pax_nr"].ToString());

                if (Convert.ToInt32(Session["pax_ind"].ToString()) == 1)
                {
                    input_pax_nr.Text = OPS_pax_nr.Value.ToString();
                }
                else
                {
                    input_pax_nr.Text = "pax=" + OPS_pax_nr.Value.ToString();
                }
                // If @options_nr & 4 = false, then display the value from the recordset in input_pax_nr.Otherwise display the text “PAX = pax_nr” in place of input__pax_nr in the same font size, font face, and color as the div_tour_info. 
                //question


                Obj_DTL_ITEM obj_tour_descr = new Obj_DTL_ITEM();
                obj_tour_descr.mode = "tour_descr";
                obj_tour_descr.id1 = OPS_tour_id.Value.ToString();
                DataTable dt_tour_descr = DTL_ITEM_Business.GetItemDetails(obj_tour_descr).Tables[0];
                if (dt_tour_descr.Rows.Count != 0)
                {
                    div_tour_info.Text = dt_tour_descr.Rows[0]["tour_descr"].ToString();
                }



                Get_inv_details();
                Getpmt_details();


                Obj_PR_ACCOUNTING obj_bill_reminder = new Obj_PR_ACCOUNTING();
                obj_bill_reminder.mode = "bill_reminder"; //  bill_reminder
                obj_bill_reminder.id1 = Convert.ToInt32(OPS_tour_id.Value);
                DataTable dt_bill_reminder = DTL_ITEM_Business.Get_PR_ACCOUNTING(obj_bill_reminder).Tables[0];

                div_reminder.DataSource = dt_bill_reminder;
                div_reminder.DataTextField = "reminder_descr";
                div_reminder.DataValueField = "sorter";
                div_reminder.DataBind();
                //div_reminder.Items.Insert(0, "Select");

                gv_div_reminder.DataSource = dt_bill_reminder;
                gv_div_reminder.DataBind();

            }
            catch (Exception ex)
            { GenerateLog("Error 103 : " + ex.Message); }
        }
        public void Get_inv_details()
        {
            Obj_LST_ITEMS obj_inv = new Obj_LST_ITEMS();
            obj_inv.mode = "inv";
            obj_inv.id1 = Convert.ToInt32(OPS_tour_id.Value);
            DataTable dt_inv = DTL_ITEM_Business.Get_LST_ITEMS(obj_inv).Tables[0];

            select_inv.DataSource = dt_inv;
            select_inv.DataTextField = "inv_descr";
            select_inv.DataValueField = "inv_id";
            select_inv.DataBind();
        }
        public void Getpmt_details()
        {

            Obj_LST_ITEMS obj_pmt = new Obj_LST_ITEMS();
            obj_pmt.mode = "pmt";
            obj_pmt.id1 = Convert.ToInt32(OPS_tour_id.Value);
            DataTable dt_pmt = DTL_ITEM_Business.Get_LST_ITEMS(obj_pmt).Tables[0];

            select_pmt.DataSource = dt_pmt;
            select_pmt.DataTextField = "pmt_descr";
            select_pmt.DataValueField = "pmt_id";
            select_pmt.DataBind();
        }


        //protected void gv_group_payment_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //}

        public void setPaxNr(object sender, EventArgs e)
        {
            //Execute pr_set_item(‘pax_nr’, @tour_id, @pax_nr) which returns 1 if successful, 0 if failure. Display a message if update fails. 

            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "pax_nr";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
            obj.id2 = Convert.ToInt32(OPS_pax_nr.Value);

            DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];

            if (Convert.ToInt32(dt_Result.Rows[0]["rc"].ToString()) > 0)
            {
                Getpmt_details();
            }
            else
            {
                string err_msg = "\"" + dt_Result.Rows[0]["err_msg"].ToString() + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }

        }
        public void dtlInv(object sender, EventArgs e)
        {
            // execute pr_dtl_item(‘inv’, @inv_id) which returns a single row recordset with the following columns. Use these values to populate / select the corresponding objects.
            try
            {
                if (select_inv.SelectedValue != "")
                {
                    OPS_inv_id.Value = Convert.ToString(select_inv.SelectedValue);
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "inv";
                    obj.id1 = OPS_inv_id.Value.ToString();
                    DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];


                    input_inv_amt.Text = dt.Rows[0]["inv_amt"].ToString();
                    input_inv_memo.Text = dt.Rows[0]["memo"].ToString();
                    select_pmt_per.SelectedValue = dt.Rows[0]["pmt_per_id"].ToString();
                    select_ratio.SelectedValue = dt.Rows[0]["ratio_id"].ToString();

                    if (dt.Rows[0]["due_date"].ToString() != "")
                    {
                        //  DateTime dt_birthdate = DateTime.Parse(dt.Rows[0]["due_date"].ToString());

                        string provider = dt.Rows[0]["due_date"].ToString();
                        DateTime dt_birthdate = DateTime.Parse(provider.ToString());

                        input_due_date.Text = dt_birthdate.ToString("yyyy-MM-dd");
                    }

                }
            }
            catch (Exception ex)
            {
                GenerateLog("Error 104 : " + ex.Message);
            }
        }

        //======================================================== inv

        public void delInv(object sender, EventArgs e)
        {
            // After confirmation, execute pr_del_item(‘inv’, @inv_id) which returns 1 if success and 0 if failure.  
            //● If successful, execute pr_lst_items(‘inv’, @tour_id) which returns a multiple row recordset with the following columns: 1. inv_id, 2. inv_descr. Use these records to repopulate select_inv. 
            //● Clear / reset the following objects: input_due_date, select_ratio, input_inv_amt, select_pmt_per, input_inv_memo.

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "inv";
            obj.id1 = Convert.ToInt32(OPS_inv_id.Value);
            int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (response == 1)
            {
                Get_inv_details();
                Resetinv();
            }
            else
            {

            }

        }
        public void newInv(object sender, EventArgs e)
        {
            //set the @inv_id = 0 and clear / reset the following objects: input_due_date, select_ratio, input_inv_amt, select_pmt_per, input_inv_memo. 
            Resetinv();
        }
        public void setInv(object sender, EventArgs e)
        {
            // Execute pr_set_item(‘inv’, @inv_id, @tour_id, @ratio_id, @pmt_per_id, @inv_amt, @due_date, @inv_memo) which returns 1 if successful, 0 if failure.  
            //● If successful, execute pr_lst_items(‘inv’, @tour_id) which returns a multiple row recordset with the following columns: 1. inv_id, 2. inv_descr. Use these records to repopulate select_inv. 
            //● Clear / reset the following objects: input_due_date, select_ratio, input_inv_amt, select_pmt_per, input_inv_memo. 
            Obj_SET_ITEM obj = new Obj_SET_ITEM();

            obj.mode = "inv";
            obj.id1 = Convert.ToInt32(OPS_inv_id.Value);
            obj.id2 = Convert.ToInt32(OPS_tour_id.Value);
            obj.id3 = Convert.ToInt32(select_ratio.SelectedValue);
            obj.id4 = Convert.ToInt32(select_pmt_per.SelectedValue);
            obj.str1 = input_inv_amt.Text;
            obj.str2 = input_due_date.Text;
            obj.str3 = input_inv_memo.Text;

            obj.str1 = obj.str1.Replace(",", "");
            DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];
            if (Convert.ToInt32(dt_Result.Rows[0]["rc"].ToString()) > 0)
            {
                Resetinv();
                OPS_inv_id.Value = "0";
                Get_inv_details();
            }
            else
            {
                string err_msg = "\"" + dt_Result.Rows[0]["err_msg"].ToString() + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }
        public void Resetinv()
        {
            //set the @pmt_id = 0 and clear / reset the following objects: input_pmt_date, select_pmt_method, input_pmt_amt, input_pmt_memo. 
            OPS_inv_id.Value = "0";
            input_due_date.Text = "";
            input_inv_amt.Text = "";
            input_inv_memo.Text = "";
            select_ratio.SelectedValue = "Ratio";
            select_pmt_per.SelectedValue = "Per";
        }
        //======================================================== pmt 

        public void dtlPmt(object sender, EventArgs e)
        {
            // execute pr_dtl_item(‘pmt’, @pmt_id) which returns a single row recordset with the following columns. Use these values to populate / select the corresponding objects. 

            try
            {
                if (select_pmt.SelectedValue != "")
                {
                    OPS_pmt_id.Value = Convert.ToString(select_pmt.SelectedValue);
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "pmt";
                    obj.id1 = OPS_pmt_id.Value.ToString();
                    DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];

                    if (dt.Rows[0]["pmt_date"].ToString() != "")
                    {
                        DateTime dt_birthdate = DateTime.Parse(dt.Rows[0]["pmt_date"].ToString());
                        input_pmt_date.Text = dt_birthdate.ToString("yyyy-MM-dd");
                    }
                    input_pmt_memo.Text = dt.Rows[0]["memo"].ToString();
                    input_pmt_amt.Text = dt.Rows[0]["pmt_amt"].ToString();
                    //input_trans_nr.Text = dt.Rows[0]["trans_nr"].ToString();
                    input_trans_nr.Text = dt.Rows[0][4].ToString();

                    select_pmt_method.SelectedValue = dt.Rows[0]["pmt_method_id"].ToString();
                }
            }
            catch (Exception ex)
            {
                GenerateLog("Error 105 : " + ex.Message);
            }
        }
        public void delPmt(object sender, EventArgs e)
        {
            //After confirmation, execute pr_del_item(‘pmt’, @pmt_id) which returns 1 if success and 0 if failure.  
            //● If successful, execute pr_lst_items(‘pmt’, @tour_id) which returns a multiple row recordset with the following columns: 1.pmt_id, 2.pmt_descr.Use these records to repopulate select_pmt. 
            //● Clear / reset the following objects: input_pmt_date, select_pmt_method, input_pmt_amt, input_pmt_memo.

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "pmt";
            obj.id1 = Convert.ToInt32(OPS_pmt_id.Value);

            int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (response == 1)
            {
                Getpmt_details();
                ResetPmt();
            }
            else
            {

            }

        }
        public void newPmt(object sender, EventArgs e)
        {
            //set the @pmt_id = 0 and clear / reset the following objects: input_pmt_date, select_pmt_method, input_pmt_amt, input_pmt_memo. 
            ResetPmt();
        }
        public void setPmt(object sender, EventArgs e)
        {
            //Execute pr_set_item(‘pmt’, @tour_id, @pmt_id, @pmt_method_id, null, @pmt_date, @pmt_amt, @pmt_memo) which returns 1 if successful, 0 if failure. 
            //● If successful, execute pr_lst_items(‘pmt’, @tour_id) which returns a multiple row recordset with the following columns: 1.pmt_id, 2.pmt_descr.Use these records to repopulate select_pmt. 
            //● Clear / reset the following objects: input_pmt_date, select_pmt_method, input_pmt_amt, input_pmt_memo.

            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "pmt";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
            obj.id2 = Convert.ToInt32(OPS_pmt_id.Value);
            obj.id3 = Convert.ToInt32(select_pmt_method.SelectedValue);

            obj.str1 = (input_pmt_date.Text);
            obj.str2 = (input_pmt_amt.Text.Trim());
            obj.str3 = (input_pmt_memo.Text.Trim());
            obj.str4 = (input_trans_nr.Text.Trim());
            obj.str2 = obj.str2.Replace(",", "");
            DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];
            if (Convert.ToInt32(dt_Result.Rows[0]["rc"].ToString()) > 0)
            {
                ResetPmt();
                OPS_pmt_id.Value = "0";
                Getpmt_details();
            }
            else
            {
                string err_msg = "\"" + dt_Result.Rows[0]["err_msg"].ToString() + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }
        public void ResetPmt()
        {
            //set the @pmt_id = 0 and clear / reset the following objects: input_pmt_date, select_pmt_method, input_pmt_amt, input_pmt_memo. 
            OPS_pmt_id.Value = "0";
            input_pmt_date.Text = "";
            input_pmt_amt.Text = "";
            input_pmt_memo.Text = "";
            select_pmt_method.SelectedValue = "Type";
            input_trans_nr.Text = "";
        }

        //=======================================================Genrate PDF 
        protected void img_mail_inv_Click(object sender, ImageClickEventArgs e)
        {
            genInv("mail");
        }

        protected void img_print_inv_Click(object sender, ImageClickEventArgs e)
        {
            genInv("print");
        }

        public string phoneformatting(string strPhone)
        {
            try
            {
                strPhone = strPhone.Trim();
                int chars = strPhone.Length;
                if (chars == 10)
                {

                    if (strPhone != "")
                    {
                        strPhone = strPhone.Replace(@".", string.Empty);
                        strPhone = "" + strPhone.Substring(0, 3) + "." + strPhone.Substring(3, 3) + "." + strPhone.Substring(6);
                        return strPhone;
                    }
                    else
                    {
                        return strPhone;
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Phone number should be 10 digits')", true);
                    return strPhone;
                }
            }
            catch (Exception ex)
            {
                GenerateLog("Error 106 : " + ex.Message);
                return "";
            }
        }
        public void genInv(string Type)
        {
            if (OPS_inv_id.Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select invoice first.')", true);
                return;
            }
            //pr_accounting(‘invoice’, @inv_id, @emp_id)
            //pr_accounting(‘invoice’,@tour_id , @inv_id) -- according procedure 
            Obj_PR_ACCOUNTING obj = new Obj_PR_ACCOUNTING();
            obj.mode = "invoice";
            //obj.id1 = inv_id;
            //obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
            obj.id2 = Convert.ToInt32(OPS_inv_id.Value);
            DataSet ds = DTL_ITEM_Business.Get_PR_ACCOUNTING(obj);
            DataTable dt_filename = ds.Tables[0];

            string str_filename = dt_filename.Rows[0]["inv_filename"].ToString();

            if (dt_filename.Rows[0]["rc"].ToString() == "-1")
            {
                string wordDocName1 = "../INVOICES/" + str_filename;

                if (!File.Exists(Server.MapPath(wordDocName1)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This invoice has already been finalized, But the file does not exist in the given path.')", true);
                    return;
                }
                else
                {
                    Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                    return;
                }
            }

            if (dt_filename.Rows[0]["rc"].ToString() == "1")
            {
                DataTable dt_image = ds.Tables[1];
                if (dt_image.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('billing agent contact information missing.')", true);
                    return;
                }
                DataTable dt_Customer = ds.Tables[2];
                DataTable dt_inv = ds.Tables[3];

                StringBuilder SB = new StringBuilder();

                string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
                string strUrl = "";
                // if (url[2] == "localhost:2108")
                if (url[2].Contains("43.229.227.26"))
                {
                    strUrl = "http://" + url[2] + "/schoolTours/Images/";

                }
                else
                {
                    strUrl = "http://" + url[2] + "/Images/";

                }

                strUrl = strUrl + dt_image.Rows[0]["img"].ToString();

                #region old
                //SB.Append("<table style=\"border: 1px dashed !important; width: 100 %;\">");
                //SB.Append("<tr>");
                //SB.Append("<td style=\"width:33%;\"></td>");
                //SB.Append("<td style=\"\"><img src='" + strUrl + "' alt=\"Smiley face\" height =\"60\"/></td>");
                //SB.Append("<td style=\"\"></td>");
                //SB.Append("</tr>");
                //SB.Append("</table>");
                //SB.Append("<table style=\"width:100%;\">");
                //SB.Append("<tr>");
                //SB.Append("<td style=\"border:1px solid;text-align:center;\" > 7255 E Hampton Ave - Suite 127 - Mesa, AZ 85209 - " + dt_image.Rows[0]["local_phone"].ToString() + " - " + dt_image.Rows[0]["toll_free_phone"].ToString() + "</td>");
                //SB.Append("</tr>");
                //SB.Append("</table>");
                //SB.Append("<table style=\"width:100%;\">");
                //SB.Append("<tr>");
                //SB.Append("<td style=\"width:50%;\"> ");
                //SB.Append("<table>");
                //SB.Append("<tr><td>customerId: " + dt_Customer.Rows[0]["customer_id"].ToString() + "</td></tr>");
                //SB.Append("<tr><td>" + dt_Customer.Rows[0]["person_nm"].ToString() + "</td></tr>");
                //SB.Append("<tr><td>" + dt_Customer.Rows[0]["entity_descr"].ToString() + "</td></tr>");
                //SB.Append("<tr><td>" + dt_Customer.Rows[0]["address"].ToString() + "</td></tr>");
                //SB.Append("<tr><td>" + dt_Customer.Rows[0]["entity_csz"].ToString() + "</td></tr>");
                //SB.Append("</table>");
                //SB.Append("</td>");
                //SB.Append("<td style=\"width:50%;float:right;\"> ");
                //SB.Append("<table>");
                //SB.Append("<tr><td style=\"font-size:20px;\"><b>Progressive Invoice</b></td></tr>");
                //SB.Append("<tr><td style=\"font-size:15px;\">Invoice Date :" + dt_image.Rows[0]["inv_date"].ToString() + "</td></tr>");
                //SB.Append("<tr><td style=\"font-size:15px;\"> Invoice number:" + dt_inv.Rows[0]["inv_id"].ToString() + "</td></tr>");
                //SB.Append("</table>");
                //SB.Append("</td>");
                //SB.Append("</tr>");
                //SB.Append("</table>");
                //SB.Append("<table style=\"width:100%;\"> ");
                //SB.Append("<tr><td style=\"text-align:center; border - bottom:1px solid;\"> Please destach and return with payment</td></tr>");
                //SB.Append("</table>");
                //SB.Append("<table style=\"width:100 %;\"> ");
                //SB.Append("<thead>");
                //SB.Append("<tr><th style=\"border: 1px solid;\"> Date</th> <th style=\"border: 1px solid;\"> Description</th><th style=\"border: 1px solid;\"> Amount</th></tr>");
                //SB.Append("</thead>");
                //SB.Append("<tbody>");
                //for (int i = 0; i < dt_inv.Rows.Count; i++)
                //{
                //    SB.Append("<tr>");
                //    SB.Append("<td>" + dt_inv.Rows[i]["due_date"].ToString() + "</td>");
                //    SB.Append("<td>" + dt_inv.Rows[i]["inv_descr"].ToString() + "</td>");
                //    SB.Append("<td>" + dt_inv.Rows[i]["inv_amt"].ToString() + "</td></tr>");
                //}

                //SB.Append("</tbody>");
                //SB.Append("</table>");
                //SB.Append("<p>The terms containts in the letter of agreement supersede information on the invoice.</p>");
                //SB.Append("<p>pay the amount due as shown on the ACCOUNT STATEMENT</p>");
                //SB.Append("<p>" + dt_inv.Rows[0]["memo"].ToString() + "</p>");
                #endregion old


                #region New
                SB.Append("<html lang=\"en\">");
                SB.Append("<head>");
                SB.Append("<title>PDF</title>");
                SB.Append("</head>");
                SB.Append("<body>");
                SB.Append("<div class=\"page\">");
                SB.Append("<table style=\"border:1px solid transparent; border-collapse: separate; border-spacing: 20px 0.8rem; width: 100%;font-size: 12px;\"> ");
                SB.Append("<thead>");
                SB.Append("<tr>");
                SB.Append("<th colspan=\"5\" style=\"border:1px dashed black;padding: 20px;text-align:center;\"><img src='" + strUrl + "' alt=\"Smiley face\" width='300' height='100' /></th>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"border: 1px solid black;padding: 10px;text-align: center;\">");
                SB.Append(" 7255 E Hampton Ave - Suite 127 - Mesa, AZ 85209 - Phone: " + phoneformatting(dt_image.Rows[0]["local_phone"].ToString()) + "- Fax: " + phoneformatting(dt_image.Rows[0]["toll_free_phone"].ToString()));
                SB.Append("</td>");
                SB.Append("</tr>");

                SB.Append("<tr><td style=\"color: white;\">.</td><td></td></tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"3\" style=\"text-align: left; vertical-align: middle;\">");
                SB.Append("<b>Customer ID :</b> " + dt_Customer.Rows[0]["customer_id"].ToString() + " <br/>");
                SB.Append("<b>" + dt_Customer.Rows[0]["person_nm"].ToString() + "</b> <br/>");
                //SB.Append("<p style=\"width: 300px;\">" + dt_Customer.Rows[0]["entity_descr"].ToString() + "</p>");
                //SB.Append("<p>" + dt_Customer.Rows[0]["address"].ToString() + "</p>");
                //SB.Append("<p>" + dt_Customer.Rows[0]["entity_csz"].ToString() + "</p>");

                SB.Append(dt_Customer.Rows[0]["entity_descr"].ToString() + "<br/>");
                SB.Append(dt_Customer.Rows[0]["address"].ToString() + "<br/>");
                SB.Append(dt_Customer.Rows[0]["entity_csz"].ToString() + "<br/>");

                SB.Append("</td>");
                SB.Append("<td colspan=\"2\" style=\"text-align: right; vertical-align: middle;\">");
                SB.Append("<h4 style=\"font-size: 16px;\">Progressive Invoices</h4>");
                SB.Append("<b style=\"font-size: 14px;\">Invoice Date :</b> " + dt_image.Rows[0]["inv_date"].ToString() + " <br/>");
                SB.Append("<b style=\"font-size: 14px;\">Invoice Number :</b> " + dt_inv.Rows[0]["inv_id"].ToString() + " <br/>");
                SB.Append("</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"border-bottom: 1px solid black;padding: 10px;text-align: center;\">Please detach and return with payment</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding-top: 3em; padding-bottom: 2em;\"></td>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<th colspan=\"5\">");
                SB.Append("<table style=\"border:1px solid transparent; border-collapse: collapse; border-spacing: 20px 0.8rem; width: 100%;font-size: 12px;\">");
                SB.Append("<thead style=\"border:1px solid black;\">");
                SB.Append("<tr>");
                SB.Append("<th style=\"padding:5px 10px; text-align: left;border:1px solid black;\">Due Date</th>");
                SB.Append("<th style=\"padding:5px 10px; text-align: left;border:1px solid black;\">Description</th>");
                SB.Append("<th style=\"padding:5px 10px; text-align: right;border:1px solid black;\">Amount</th>");
                SB.Append("</tr>");
                SB.Append("</thead>");
                SB.Append("<tbody>");
                for (int i = 0; i < dt_inv.Rows.Count; i++)
                {
                    SB.Append("<tr>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: left;\">" + dt_inv.Rows[i]["due_date"].ToString() + "</td>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: left;\">" + dt_inv.Rows[i]["inv_descr"].ToString() + "</td>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: right;\">$ " + (dt_inv.Rows[i]["inv_amt"].ToString() == "" ? "0.00" : dt_inv.Rows[i]["inv_amt"].ToString()) + "</td>");
                    SB.Append("</tr>");
                }
                SB.Append("</tbody>");
                SB.Append("</table>");
                SB.Append("</th>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding: 10px 0 0 0;\">Memo: " + dt_inv.Rows[0]["memo"].ToString() + "</td>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding: 20px 0 0 0;font-size:10px;text-align:center;\">Please include the invoice number on your check. Terms described in the letter of agreement supersede information on this statement<br />");
                //if(dt_image.Rows[0]["img"].ToString())
                SB.Append("<b>Mail payment to " + dt_image.Rows[0]["div_nm"].ToString() + ". 7255 E Hampton Ave. Suite 127, Mesa, AZ 85209</b><br />");
                SB.Append("" + phoneformatting(dt_image.Rows[0]["toll_free_phone"].ToString()) + " * " + dt_image.Rows[0]["from_email"].ToString() + "</td>");
                //SB.Append("<b>Mail payment to Historic Productions. 7255 E Hampton Ave. Suite 127, Mesa, AZ 85209</b><br />");
                //SB.Append("800-626-8590 * " + dt_image.Rows[0]["producer_eMail"].ToString() + "</td>");
                SB.Append("</tr>");
                //SB.Append("<tr>");
                //SB.Append("<td colspan=\"5\" style=\"padding: 20px 0 0 0;\">The terms contained in the letter of agreement supersede information on the invoice.</td>");
                //SB.Append("</tr>");
                //SB.Append("<tr>");
                //SB.Append("<td colspan=\"5\" style=\"padding: 10px 0 0 0;\">Please pay the amount due as shown on the ACCOUNT STATEMENT</td>");
                //SB.Append("</tr>");
                SB.Append("</thead>");
                SB.Append("</table>");
                SB.Append("</div>");
                SB.Append("</body>");
                SB.Append("</html>");
                #endregion New


                string strPDf = SB.ToString();
                Get_inv_details();
                GeneratePDFReport("INVOICES", str_filename, "", strPDf);
            }

            if (Type == "mail")
            {
                /// send mail
                /// 
                string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
                string strUrl = "";
                //if (url[2] == "localhost:2108")
                if (url[2].Contains("43.229.227.26"))
                {
                    strUrl = "http://" + url[2] + "/schoolTours/INVOICES/";

                }
                else
                {
                    strUrl = "http://" + url[2] + "/INVOICES/";

                }

                string div_nm = "Invoice from " + ds.Tables[1].Rows[0]["div_nm"].ToString();
                string from_eMail = ds.Tables[1].Rows[0]["from_eMail"].ToString();
                string to_eMail = ds.Tables[2].Rows[0]["to_eMail"].ToString();
                StringBuilder SB = new StringBuilder();
                SB.Append("<p>Dear " + ds.Tables[2].Rows[0]["person_nm"].ToString() + ": </p>");
                SB.Append("<p>Please find attached the invoice for your upcoming trip.  Let me know if you have any questions or concerns.</p>");
                SB.Append("<p><br/><br/>Sincerely,<br/>" + ds.Tables[1].Rows[0]["div_nm"].ToString() + "<br/>" + phoneformatting(ds.Tables[1].Rows[0]["local_phone"].ToString()) + "</p>");
                string tBody = SB.ToString();
                //string tBody = "We have sent the attached invoice for your upcoming tour with " + ds.Tables[1].Rows[0]["div_nm"].ToString() + " . Please pay at your earliest convenience.” " + str_filename;
                //string inv_file_path = strUrl + str_filename;
                string inv_file_path = "INVOICES/" + str_filename;
                string Subject = "Invoice from " + ds.Tables[1].Rows[0]["div_nm"].ToString();
                // UtilityBusiness UB = new UtilityBusiness();
                Models.Utility UB = new Models.Utility();
                /// send mail
                string Response = UB.SendInvoice(div_nm, from_eMail, to_eMail, tBody, inv_file_path);
                if (Response == "1")
                {
                    Get_inv_details();
                    Resetinv();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' the mail was successfully sent')", true);
                    return;

                }
            }
            else if (Type == "print")
            {
                string Path = "~/INVOICES/" + str_filename;

                if (!File.Exists(Server.MapPath(Path)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('file not found.')", true);
                    return;
                }
                if (Path != string.Empty)
                {
                    string wordDocName1 = "../INVOICES/" + str_filename;
                    Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                    return;
                    //WebClient req = new WebClient();
                    //HttpResponse response = System.Web.HttpContext.Current.Response;
                    //string filePath = Path;
                    //response.Clear();
                    //response.ClearContent();
                    //response.ClearHeaders();
                    //response.Buffer = true;
                    //response.AddHeader("Content-Disposition", "attachment;filename=" + str_filename);
                    //byte[] data = req.DownloadData(Server.MapPath(filePath));
                    //response.BinaryWrite(data);
                    //response.End();
                }
            }

        }
        protected void genStmt(object sender, ImageClickEventArgs e)
        {
            GenerateLog("Session id 108 : " + Session["emp_id"].ToString());
            #region new 0
            Obj_PR_ACCOUNTING obj = new Obj_PR_ACCOUNTING();
            obj.mode = "statement";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"]);
            DataSet ds = DTL_ITEM_Business.Get_PR_ACCOUNTING(obj);

            DataTable dt_image = ds.Tables[1];
            DataTable dt_customer = ds.Tables[2];
            DataTable dt_cost = ds.Tables[3];
            DataTable dt_total_amount = ds.Tables[4];
            DataTable dt_act = ds.Tables[5];
            DataTable dt_total_amount_due = ds.Tables[6];

            string File_name = ds.Tables[0].Rows[0]["stmt_filename"].ToString();
            string str_img = dt_image.Rows[0]["img"].ToString();



            string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
            string strUrl = "";
            // if (url[2] == "localhost:2108")
            if (url[2].Contains("43.229.227.26"))
            {
                strUrl = "http://" + url[2] + "/schoolTours/Images/";

            }
            else
            {
                strUrl = "http://" + url[2] + "/Images/";

            }
            GenerateLog(strUrl);
            strUrl = strUrl + dt_image.Rows[0]["img"].ToString();

            StringBuilder SB = new StringBuilder();
            #endregion new 0
            GenerateLog("Session id 208 : " + Session["emp_id"].ToString());
            #region new 1
            SB.Append("<html>");
            SB.Append("<head>");
            SB.Append("<style>");
            SB.Append(".border {border: 1px solid black;}");
            SB.Append(".border-r {border-right: 1px solid black;}");
            SB.Append(".border-l {border-left: 1px solid black;}");
            SB.Append(".border-b {border-bottom: 1px solid black;}");
            SB.Append(".text-right {text-align: right;padding-right: 5px;}");
            SB.Append(".text-center {text-align: center;}");
            SB.Append("tr {height: 22px;}");
            SB.Append("img {width:300px;object-fit:cover;display: inline-block;}");
            SB.Append("</style>");
            SB.Append("</head>");
            SB.Append("<body>");
            SB.Append("<table style=\"width:100 %;font-size:12px;\"> ");
            SB.Append("<thead>");
            SB.Append("<tr>");
            SB.Append("<th colspan=\"5\" style=\"padding:20px;\"><table style=\"width:100%;\"><tr><th colspan=\"5\" style=\"text-align:center;\"><img src='" + strUrl + "' align ='bottom'/></th></tr></table></th>");
            SB.Append("</tr>");
            SB.Append("<tr><th colspan=\"5\" style=\"text-align:center;\">7255 E Hampton Ave - Suite 127 - Mesa, AZ 85209</th></tr>");
            SB.Append("</thead>");
            SB.Append("</table>");
            SB.Append("<table style=\"width:100%;font-size:12px;\">");
            SB.Append("<tr>");
            SB.Append("<th style=\"border-bottom:1px solid;border-top:1px solid;font-size:20px;text-align:center;padding-top: 5px;padding-bottom:5px;\">STATEMENT</th>");
            SB.Append("</tr>");
            SB.Append("</table>");
            SB.Append("<table style=\"width:100%;font-size:12px;\">");
            SB.Append("<tr>");
            SB.Append("<td>");
            SB.Append("<table style=\"width:100%;font-size:12px;\">");
            SB.Append("<tr><td>Statement Date: " + dt_image.Rows[0][3].ToString() + "</td></tr>");
            SB.Append("<tr><td>Customer ID: " + dt_customer.Rows[0][0].ToString() + "</td></tr>");
            SB.Append("<tr><td>Program Name: " + dt_customer.Rows[0][5].ToString() + "</td></tr>");
            SB.Append("</table>");
            SB.Append("</td>");
            SB.Append("<td>");
            SB.Append("<table style=\"width:100%;font-size:12px;\" > ");
            SB.Append("<tr><td>" + dt_customer.Rows[0][1].ToString() + "</td></tr>");
            SB.Append("<tr><td>" + dt_customer.Rows[0][2].ToString() + "</td></tr>");
            SB.Append("<tr><td>" + dt_customer.Rows[0][3].ToString() + "</td></tr>");
            SB.Append("<tr><td>" + dt_customer.Rows[0][4].ToString() + "</td></tr>");

            SB.Append("</table>");
            SB.Append("</td>");
            SB.Append("</tr>");
            SB.Append("</table>");
            SB.Append("<table style=\"width:100%;font-size:12px;border-collapse:collapse;\">");
            SB.Append("<tr>");
            SB.Append("<th class=\"border\" style=\"background-color:#808080;color:#ffffff;\"> Remittance Amount Enclosed</th>");
            SB.Append("<th class=\"border text-right\" style=\"width:35px;\">$  " + dt_total_amount_due.Rows[0]["total_amt_due"].ToString() + "</th>");
            SB.Append("</tr>");
            SB.Append("</table>");
            SB.Append("<p style=\"text-align:center;\">PACKAGE DESCRIPTION</p>");
            SB.Append("<table style=\"width:100%;font-size:12px;border-collapse:collapse;border:none;\"> ");
            SB.Append("<thead>");
            SB.Append("<tr style=\"background-color:#808080;color:#ffffff;\">");
            SB.Append("<th class=\"border text-center\">Item</th>");
            SB.Append("<th class=\"border text-center\">Description</th>");
            SB.Append("<th class=\"border text-center\">Price</th>");
            SB.Append("<th class=\"border text-center\">Qty</th>");
            SB.Append("<th class=\"border text-center\"> Sub Total</th>");
            SB.Append("</tr>");
            SB.Append("</thead>");
            SB.Append("<tbody>");
            for (int i = 0; i < dt_cost.Rows.Count; i++)
            {
                if (dt_cost.Rows.Count - 1 == i)
                {
                    SB.Append("<tr>");
                    SB.Append("<td class=\"border-r border-l border-b\">" + dt_cost.Rows[i]["cost_id"].ToString() + "</td>");
                    SB.Append("<td class=\"border-r border-b\">" + dt_cost.Rows[i]["cost_descr"].ToString() + "</td>");
                    SB.Append("<td class=\"border-r border-b text-right\">$ " + (dt_cost.Rows[i]["cost_amt"].ToString() == "" ? dt_cost.Rows[i]["cost_amt"].ToString() : String.Format("{0:0.00}", Convert.ToDecimal(dt_cost.Rows[i]["cost_amt"].ToString()))) + "</td>");
                    SB.Append("<td class=\"border-r border-b text-right\">" + (dt_cost.Rows[i]["cost_qty"].ToString()) + "</td>");
                    SB.Append("<td class=\"border-r border-b text-right\">$ " + (dt_cost.Rows[i]["cost_sub"].ToString() == "" ? dt_cost.Rows[i]["cost_sub"].ToString() : String.Format("{0:0.00}", Convert.ToDecimal(dt_cost.Rows[i]["cost_sub"].ToString()))) + "</td>");
                    SB.Append("</tr>");
                }
                else
                {

                    SB.Append("<tr>");
                    SB.Append("<td class=\"border-r border-l\">" + dt_cost.Rows[i]["cost_id"].ToString() + "</td>");
                    SB.Append("<td class=\"border-r\">" + dt_cost.Rows[i]["cost_descr"].ToString() + "</td>");
                    SB.Append("<td class=\"border-r text-right\">$ " + (dt_cost.Rows[i]["cost_amt"].ToString() == "" ? dt_cost.Rows[i]["cost_amt"].ToString() : String.Format("{0:0.00}", Convert.ToDecimal(dt_cost.Rows[i]["cost_amt"].ToString()))) + "</td>");
                    SB.Append("<td class=\"border-r text-right\">" + (dt_cost.Rows[i]["cost_qty"].ToString()) + "</td>");
                    SB.Append("<td class=\"border-r text-right\">$ " + (dt_cost.Rows[i]["cost_sub"].ToString() == "" ? dt_cost.Rows[i]["cost_sub"].ToString() : String.Format("{0:0.00}", Convert.ToDecimal(dt_cost.Rows[i]["cost_sub"].ToString()))) + "</td>");
                    SB.Append("</tr>");
                }

            }

            SB.Append("<tr>");
            SB.Append("<td colspan='4' style=\"text-align:right\"> Total Price</td>");
            SB.Append("<td style=\"border:1px solid black;\" class=\"text-right\">$ " + (dt_total_amount.Rows[0]["total_cost_amt"].ToString() == "" ? dt_total_amount.Rows[0]["total_cost_amt"].ToString() : String.Format("{0:0.00}", Convert.ToDecimal(dt_total_amount.Rows[0]["total_cost_amt"].ToString()))) + "</td>");
            SB.Append("</tr>");
            SB.Append("<tr>");
            SB.Append("<td colspan='4' style=\"text-align:right\"> Paid To Date</td>");
            SB.Append("<td style=\"border:1px solid black;\" class=\"text-right\">$ " + (dt_total_amount.Rows[0]["total_paid_amt"].ToString() == "" ? dt_total_amount.Rows[0]["total_paid_amt"].ToString() : String.Format("{0:0.00}", Convert.ToDecimal(dt_total_amount.Rows[0]["total_paid_amt"].ToString()))) + "</td>");
            SB.Append("</tr>");
            SB.Append("<tr>");
            SB.Append("<td colspan='4' style=\"text-align:right\">Total Remaining</td>");
            SB.Append("<td style=\"border:1px solid black;\" class=\"text-right\">$ " + (dt_total_amount.Rows[0]["total_remaining_amt"].ToString() == "" ? dt_total_amount.Rows[0]["total_remaining_amt"].ToString() : String.Format("{0:0.00}", Convert.ToDecimal(dt_total_amount.Rows[0]["total_remaining_amt"].ToString()))) + "</td>");
            SB.Append("</tr>");
            SB.Append("</tbody>");
            SB.Append("</table>");
            SB.Append("<p style=\"text-align:center;\">ACCOUNT ACTIVITY</p>");
            SB.Append("<table style=\"width:100%;font-size:12px;border-collapse:collapse;border:none;\">");
            SB.Append("<thead>");
            SB.Append("<tr style=\"background-color:#808080;color:#ffffff;\"> ");
            SB.Append("<th class=\"border text-center\">Date</th>");
            SB.Append("<th class=\"border text-center\">Description</th>");
            SB.Append("<th class=\"border text-center\">Amount</th>");
            SB.Append("<th class=\"border text-center\">Balance</th>");
            SB.Append("</tr>");
            SB.Append("</thead>");
            SB.Append("<tbody>");
            for (int i = 0; i < dt_act.Rows.Count; i++)
            {
                DateTime dt = DateTime.Parse(dt_act.Rows[i]["act_date"].ToString());
                string act_date = "";
                try { act_date = dt.ToString("yyyy-MM-dd"); }
                catch (Exception ex)
                {
                    act_date = dt.ToString();
                    GenerateLog("Error 107 : " + ex.Message);
                }

                if (dt_act.Rows.Count - 1 == i)
                {
                    SB.Append("<tr>");
                    SB.Append("<td class=\"border-r border-l border-b\">" + act_date + "</td>");
                    SB.Append("<td class=\"border-r border-b\">" + dt_act.Rows[i]["act_descr"].ToString() + "</td>");
                    SB.Append("<td class=\"border-r border-b text-right\">$ " + (dt_act.Rows[i]["act_amt"].ToString() == "" ? dt_act.Rows[i]["act_amt"].ToString() : String.Format("{0:0.00}", Convert.ToDecimal(dt_act.Rows[i]["act_amt"].ToString()))) + "</td>");
                    SB.Append("<td class=\"border-r border-b text-right\">$ " + (dt_act.Rows[i]["act_bal"].ToString() == "" ? dt_act.Rows[i]["act_bal"].ToString() : String.Format("{0:0.00}", dt_act.Rows[i]["act_bal"].ToString())) + "</td>");
                    SB.Append("</tr>");
                }
                else
                {
                    SB.Append("<tr>");
                    SB.Append("<td class=\"border-r border-l\">" + act_date + "</td>");
                    SB.Append("<td class=\"border-r\">" + dt_act.Rows[i]["act_descr"].ToString() + "</td>");
                    SB.Append("<td class=\"border-r text-right\">$ " + (dt_act.Rows[i]["act_amt"].ToString() == "" ? dt_act.Rows[i]["act_amt"].ToString() : String.Format("{0:0.00}", dt_act.Rows[i]["act_amt"].ToString())) + "</td>");
                    SB.Append("<td class=\"border-r text-right\">$ " + (dt_act.Rows[i]["act_bal"].ToString() == "" ? dt_act.Rows[i]["act_bal"].ToString() : String.Format("{0:0.00}", dt_act.Rows[i]["act_bal"].ToString())) + "</td>");
                    SB.Append("</tr>");
                }
            }
            SB.Append("<tr>");
            SB.Append("<td colspan='3' style=\"text-align:right\"> Amount Due This Period</td>");
            SB.Append("<td style=\"border:1px solid black;\" class=\"text-right\">$ " + (dt_total_amount_due.Rows[0][0].ToString() == "" ? dt_total_amount_due.Rows[0][0].ToString() : String.Format("{0:0.00}", dt_total_amount_due.Rows[0][0].ToString())) + "</td>");
            SB.Append("</tr>");
            SB.Append("</tbody>");
            SB.Append("</table>");

            SB.Append("<p style=\"text-align:center;font-size:10px;\"> ");
            SB.Append("Please include the invoice number on your check. Terms described in the letter of agreement supersede information on this statement<br />");
            //SB.Append("<b>Mail payment to Historic Productions. 7255 E Hampton Ave. Suite 127, Mesa, AZ 85209</b><br />");
            //SB.Append("800-626-8590 * service@historicproductions.com");

            SB.Append("<b>Mail payment to " + dt_image.Rows[0]["div_nm"].ToString() + ". 7255 E Hampton Ave. Suite 127, Mesa, AZ 85209</b><br />");
            SB.Append("" + phoneformatting(dt_image.Rows[0]["toll_free_phone"].ToString()) + " * " + dt_image.Rows[0]["producer_eMail"].ToString() + "");
            SB.Append("</p>");
            SB.Append("</body>");
            SB.Append("</html>");

            #endregion new 1
            string strhtml = SB.ToString();

            try
            {
                // string wordDocName1 = "../INVOICES/" + str_filename;
                // Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                // return;
                GenerateLog("Session id 308 : " + Session["emp_id"].ToString());
                GeneratePDFReport("pdf", File_name, "", strhtml);
                GenerateLog("Session id 408 : " + Session["emp_id"].ToString());
                string wordDocName1 = "../pdf/" + File_name;
                string RredirectUrl = "../pdf/" + File_name;
                ClientScript.RegisterStartupScript(GetType(), "popup123", "window.open('" + RredirectUrl + "', 'pp');", true);
                GenerateLog("Session id 508 : " + Session["emp_id"].ToString());
                //  try {
                //Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                //}catch(Exception ex) { }
                // return;
            }
            catch (Exception ex)
            {
                GenerateLog("Error 108 : " + ex.Message);
            }
        }

        public void GenerateLog(String Msg)
        {
            ////string appPath = HttpContext.Current.Request.ApplicationPath;
            ////appPath = appPath + " | " + Server.MapPath("");
            //String strDt = System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Year.ToString();

            //string QRAddress = System.Configuration.ConfigurationManager.AppSettings["QRAddress"];
            ////uttam edits start//
            //String aName = @"D:\SIte_Hosted_On_IIS\Viben_New_Repo\RestaurantPOS\Log\tmp" + strDt + ".txt";

            //if (QRAddress.Contains("vibenqr.in"))
            //    aName = @"D:\Site hosted IIS\Viben\Log\tmp" + strDt + ".txt";

            //// 1: Write single line to new file
            //using (StreamWriter writer = new StreamWriter(aName, true))
            //{
            //    writer.WriteLine("\r\n" + System.DateTime.Now + " > " + Msg);//+ " " + appPath);
            //}
            ////uttam edits ends


            //string appPath = Request.PhysicalApplicationPath;
            //string filePath = appPath + "/Log.txt";
            //StreamWriter w;
            //w = File.CreateText(filePath);
            //w.WriteLine(Msg);
            //w.Flush();
            //w.Close();

            string appPath = Request.PhysicalApplicationPath;
            string filePath = appPath + "/Log.txt";
            using (StreamWriter w = new StreamWriter(filePath, true))
            {
                //w = File.CreateText(filePath);
                w.WriteLine(Msg);
            }
        }

        public void genStmt_old(object sender, EventArgs e)
        {
            //execute pr_accounting(‘statement’ @tour_id, @emp_id) which returns eight recordsets.  
            //● The first is a single row recordset with the following column: 1. stmt_filename. Store the value of stmt_filename in a local variable
            //@stmt_filename for later use. 
            //● The second is a single row recordset with the following columns. Use these values to populate the objects on the 
            //document in the diagram below. Note that the image (which may be different for each invoice, depending on the division generating the invoice) 
            //should be constrained either 1 inch in height or 6 inches in width.

            Obj_PR_ACCOUNTING obj = new Obj_PR_ACCOUNTING();
            obj.mode = "statement";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            DataSet ds = DTL_ITEM_Business.Get_PR_ACCOUNTING(obj);

            DataTable dt_image = ds.Tables[1];
            DataTable dt_customer = ds.Tables[2];
            DataTable dt_cost = ds.Tables[3];
            DataTable dt_total_amount = ds.Tables[4];
            DataTable dt_act = ds.Tables[5];
            DataTable dt_total_amount_due = ds.Tables[6];

            string File_name = ds.Tables[0].Rows[0]["stmt_filename"].ToString();
            string str_img = dt_image.Rows[0]["img"].ToString();



            string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
            string strUrl = "";
            // if (url[2] == "localhost:2108")
            if (url[2].Contains("43.229.227.26"))
            {
                strUrl = "http://" + url[2] + "/schoolTours/Images/";

            }
            else
            {
                strUrl = "http://" + url[2] + "/Images/";

            }

            strUrl = strUrl + dt_image.Rows[0]["img"].ToString();

            StringBuilder SB = new StringBuilder();



            #region new 0

            SB.Append("<table style=\"border:1px solid transparent; border-collapse: separate; border-spacing: 20px 0.8rem; width:100%;font-size:12px;\">");
            SB.Append("<thead>");
            SB.Append("<tr>");
            //SB.Append("<th colspan=\"5\" style=\"border:1px dashed black;padding:20px;\"><img src='" + strUrl + " ' height='60' align='bottom' style='display:block'/></th>");
            //SB.Append("<th colspan=\"5\" style=\"border:1px dashed black;padding:20px;\"><table style=\"width:100%;\"><tr><th colspan=\"4\" style=\"width:15%;color:white\">.</th><th><img src='" + strUrl + " ' height='60' align='bottom' style='display:block' /></th></tr></table></th>");
            SB.Append("<th colspan=\"5\" style=\"border:1px dashed black;padding:20px;\"><table style=\"width:100%;\"><tr><th style=\"text-align: center;\"><img src='" + strUrl + " ' height='60' colspan=\"5\" align='bottom' style='display:inline;'/></th></tr></table></th>");
            SB.Append("</tr>");
            SB.Append("<tr>");
            SB.Append("<th colspan=\"5\" style=\"border:1px solid black;padding:10px;text-align: center;\">");
            SB.Append("7255 E Hampton Ave - Suite 127 - Mesa, AZ 85209 - " + dt_image.Rows[0]["local_phone"].ToString() + " - " + dt_image.Rows[0]["toll_free_phone"].ToString());
            SB.Append("</th>");
            SB.Append("</tr>");
            SB.Append("<tr>");

            SB.Append("<td colspan=\"3\" style=\"text-align:left;vertical-align:middle;\">");
            SB.Append("<b>Customer ID : " + dt_customer.Rows[0]["customer_id"].ToString() + "</b> <br/>");
            SB.Append("<b>" + dt_customer.Rows[0]["person_nm"].ToString() + "</b> <br/>");
            SB.Append("<p style=\"width: 300px;\">" + dt_customer.Rows[0]["entity_descr"].ToString() + "</p>");
            SB.Append("<p>" + dt_customer.Rows[0]["address"].ToString() + "</p>");
            SB.Append("<p>" + dt_customer.Rows[0]["entity_csz"].ToString() + "</p>");
            SB.Append("<p>" + dt_customer.Rows[0]["tour_descr"].ToString() + "</p>");
            SB.Append("</td>");

            SB.Append("<td colspan=\"2\" style=\"text-align:right;vertical-align:top;font-size:16px;\">");
            SB.Append("<h4>ACCOUNT STATEMENT</h4>");
            SB.Append("<b>" + dt_image.Rows[0]["stmt_date"].ToString() + "</b>");
            SB.Append("</td>");
            SB.Append("</tr>");

            SB.Append("<tr>");
            SB.Append("<th colspan=\"5\">");
            SB.Append("<table style=\"border:1px solid transparent;border-collapse:collapse;border-spacing:20px 0.8rem;width:100%;font-size:12px;\">");
            SB.Append("<thead style=\"border:1px solid black;\">");

            //SB.Append("<tr>");
            //SB.Append("<th colspan=\"5\" style=\"border:1px solid\">");
            //SB.Append("<table style=\"width:100%;font-size:12px;\">");
            //SB.Append("<tr>");
            //SB.Append("<th colspan=\"5\" style=\"text-align:center;padding: 10px;\">Package Description</th>");
            //SB.Append("</tr>");
            //SB.Append("<tr>");
            //SB.Append("<th style=\"padding:5px 10px;text-align:left;padding: 10px;\">Item</th>");
            //SB.Append("<th style=\"padding:5px 10px;text-align:left;padding: 10px;\">Description</th>");
            //SB.Append("<th style=\"padding:5px 10px;text-align:right;padding: 10px;\">Price</th>");
            //SB.Append("<th style=\"padding:5px 10px;text-align:right;padding: 10px;\">Quantity</th>");
            //SB.Append("<th style=\"padding:5px 10px;text-align:right;padding: 10px;\">Sub Total</th>");
            //SB.Append("</tr>");
            //SB.Append("</table");
            //SB.Append("</th>");
            //SB.Append("</tr>");

            SB.Append("<tr>");
            SB.Append("<th colspan=\"5\" style=\"text-align:center;padding: 10px;\">Package Description</th>");
            SB.Append("</tr>");
            SB.Append("<tr>");
            SB.Append("<th style=\"padding:5px 10px;text-align:left;padding: 10px;\">Item</th>");
            SB.Append("<th style=\"padding:5px 10px;text-align:left;padding: 10px;\">Description</th>");
            SB.Append("<th style=\"padding:5px 10px;text-align:right;padding: 10px;\">Price</th>");
            SB.Append("<th style=\"padding:5px 10px;text-align:right;padding: 10px;\">Quantity</th>");
            SB.Append("<th style=\"padding:5px 10px;text-align:right;padding: 10px;\">Sub Total</th>");
            SB.Append("</tr>");

            SB.Append("</thead>");
            SB.Append("<tbody>");
            for (int i = 0; i < dt_cost.Rows.Count; i++)
            {
                SB.Append("<tr>");
                SB.Append("<td style=\"padding:5px 10px;text-align:left;\">" + dt_cost.Rows[i]["cost_id"].ToString() + "</td>");
                SB.Append("<td style=\"padding:5px 10px;text-align:left;\">" + dt_cost.Rows[i]["cost_descr"].ToString() + "</td>");
                if (dt_cost.Rows[i]["cost_amt"].ToString() == "")
                    SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + String.Format("{0:0.00}", 0.00) + "</td>");
                else
                    SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt_cost.Rows[i]["cost_amt"].ToString())) + "</td>");

                SB.Append("<td style=\"padding:5px 10px;text-align:right;\">" + dt_cost.Rows[i]["cost_qty"].ToString() + "</td>");

                if (dt_cost.Rows[i]["cost_sub"].ToString() == "")
                    SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + String.Format("{0:0.00}", 0.00) + "</td>");
                else
                    SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt_cost.Rows[i]["cost_sub"].ToString())) + "</td>");
                SB.Append("</tr>");

            }

            SB.Append("<tr style=\"border:1px solid black;\">");
            SB.Append("<td style=\"padding:5px 10px;text-align:left;padding: 10px;\">Total Package Price</td>");
            SB.Append("<td style=\"padding:5px 10px;text-align:left;padding: 10px;\" colspan=\"3\"></td>");
            if (dt_total_amount.Rows[0]["total_cost_amt"].ToString() == "")
                SB.Append("<td style=\"padding:5px 10px;text-align:right;padding: 10px;\">$ " + String.Format("{0:0.00}", 0.00) + "</td>");
            else
                SB.Append("<td style=\"padding:5px 10px;text-align:right;padding: 10px;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt_total_amount.Rows[0]["total_cost_amt"].ToString())) + "</td>");
            SB.Append("</tr>");
            SB.Append("<tr style=\"border:1px solid black;\">");
            SB.Append("<td style=\"padding:5px 10px;text-align:left;padding: 10px;\">Paid to Date</td>");
            SB.Append("<td style=\"padding:5px 10px;text-align:left;padding: 10px;\" colspan=\"3\"></td>");
            if (dt_total_amount.Rows[0]["total_paid_amt"].ToString() == "")
                SB.Append("<td style=\"padding:5px 10px;text-align:right;padding: 10px;\">$ " + String.Format("{0:0.00}", 0.00) + "</td>");
            else
                SB.Append("<td style=\"padding:5px 10px;text-align:right;padding: 10px;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt_total_amount.Rows[0]["total_paid_amt"].ToString())) + "</td>");
            SB.Append("</tr>");
            SB.Append("<tr style=\"border:1px solid black;\">");
            SB.Append("<td style=\"padding:5px 10px;text-align:left;padding: 10px;\">Total Remaining</td>");
            SB.Append("<td style=\"padding:5px 10px;text-align:left;padding: 10px;\" colspan=\"3\"></td>");
            if (dt_total_amount.Rows[0]["total_remaining_amt"].ToString() == "")
                SB.Append("<td style=\"padding:5px 10px;text-align:right;padding: 10px;\">$ " + String.Format("{0:0.00}", 0.00) + "</td>");
            else
                SB.Append("<td style=\"padding:5px 10px;text-align:right;padding: 10px;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt_total_amount.Rows[0]["total_remaining_amt"].ToString())) + "</td>");
            SB.Append("</tr>");
            SB.Append("</tbody>");
            SB.Append("</table>");
            SB.Append("</th>");
            SB.Append("</tr>");
            SB.Append("<tr>");
            SB.Append("<th colspan=\"5\">");
            SB.Append("<table style=\"border:1px solid transparent;border-collapse:collapse;border-spacing:20px 0.8rem;width:100%;font-size:12px;\">");
            SB.Append("<thead style=\"border:1px solid black;\">");
            SB.Append("<tr>");
            SB.Append("<th colspan=\"5\" style=\"text-align:center;padding: 10px;\">Account Activity</th>");
            SB.Append("</tr>");
            SB.Append("<tr>");
            SB.Append("<th style=\"padding:5px 10px;text-align:left;padding: 10px;\">Date</th>");
            SB.Append("<th colspan=\"2\" style=\"padding:5px 10px;text-align:left;padding: 10px;\">Description</th>");
            SB.Append("<th style=\"padding:5px 10px;text-align:right;padding: 10px;\">Amount</th>");
            SB.Append("<th style=\"padding:5px 10px;text-align:right;padding: 10px;\">Balance</th>");
            SB.Append("</tr>");
            SB.Append("</thead>");
            SB.Append("<tbody>");
            for (int i = 0; i < dt_act.Rows.Count; i++)
            {
                SB.Append("<tr>");
                SB.Append("<td style=\"padding:5px 10px;text-align:left;\">" + dt_act.Rows[i]["act_date"].ToString() + "</td>");
                SB.Append("<td colspan=\"2\" style=\"padding:5px 10px;text-align:left;\">" + dt_act.Rows[i]["act_descr"].ToString() + "</td>");
                if (dt_act.Rows[i]["act_amt"].ToString() == "")
                    SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + "0.00" + "</td>");
                else
                    SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt_act.Rows[i]["act_amt"].ToString())) + "</td>");

                if (dt_act.Rows[i]["act_bal"].ToString() == "")
                    SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + "0.00" + "</td>");
                else
                    SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt_act.Rows[i]["act_bal"].ToString())) + "</td>");
                SB.Append("</tr>");
            }
            //border-right: solid 1px #FFF;
            SB.Append("<tr style=\"border:1px solid black;\">");
            SB.Append("<td style=\"padding:5px 10px;text-align:left; \">Total Amount Due</td>");
            SB.Append("<td style=\"padding:5px 10px;text-align:left;\" colspan=\"3\"></td>");
            if (dt_total_amount_due.Rows[0]["total_amt_due"].ToString() == "")
                SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + "0.00" + "</td>");
            else
                SB.Append("<td style=\"padding:5px 10px;text-align:right;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt_total_amount_due.Rows[0]["total_amt_due"].ToString())) + "</td>");
            SB.Append("</tr>");
            SB.Append("</tbody>");
            SB.Append("</table>");
            SB.Append("</th>");
            SB.Append("</tr>");

            SB.Append("<tr><th colspan=\"5\" style=\"color:white;\">.</th></tr>");
            SB.Append("<tr><td colspan=\"5\" style=\"text-align:left;padding: 10px;\">The terms contained in the letter of agreement supersede information on this invoice</td></tr>");
            SB.Append("</thead>");
            SB.Append("</table>");

            #endregion New

            string strhtml = SB.ToString();

            try
            {

                GeneratePDFReport("pdf", File_name, "", strhtml);

                string wordDocName1 = "../pdf/" + File_name;
                Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                return;
                //string Path = "~/pdf/" + File_name;
                //if (Path != string.Empty)
                //{
                //    WebClient req = new WebClient();
                //    HttpResponse response = System.Web.HttpContext.Current.Response;
                //    string filePath = Path;
                //    response.Clear();
                //    response.ClearContent();
                //    response.ClearHeaders();
                //    response.Buffer = true;
                //    response.AddHeader("Content-Disposition", "attachment;filename=" + File_name);
                //    byte[] data = req.DownloadData(Server.MapPath(filePath));
                //    response.BinaryWrite(data);
                //    response.End();
                //}
            }
            catch (Exception ex)
            {
                GenerateLog("Error 109 : " + ex.Message);
            }
        }
        public string GeneratePDFReport(string FolderName, string DocName, string SavePath, string HtmlBody)
        {
            try
            {
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 20f, 20f, 20f, 40f);

                string BaseDir = AppDomain.CurrentDomain.BaseDirectory + FolderName + "\\";
                String PackaginglistPDFPath = BaseDir + "\\" + DocName;
                PackaginglistPDFPath = PackaginglistPDFPath.Replace("//", "\\");
                string baseURLpath = SavePath + "//" + DocName;
                //DirectoryInfo dtIfo = Directory.CreateDirectory(BaseDir + SavePath);

                //if (dtIfo.Exists)
                //{
                PdfWriter pw = PdfWriter.GetInstance(pdfDoc, new FileStream(PackaginglistPDFPath, FileMode.Create));
                pw.PageEvent = new ITextEvents();
                pdfDoc.Open();
                StringReader cont = new System.IO.StringReader(HtmlBody);
                XMLWorkerHelper.GetInstance().ParseXHtml(pw, pdfDoc, cont);
                pdfDoc.Close();
                //}

                return "";
            }
            catch (Exception ex)
            {
                GenerateLog("Error 110 : " + ex.Message);
                return "";
            }
        }
        public class ITextEvents : PdfPageEventHelper
        {
            // This is the contentbyte object of the writer
            PdfContentByte cb;
            // we will put the final number of pages in a template
            PdfTemplate headerTemplate, footerTemplate;
            // this is the BaseFont we are going to use for the header / footer
            BaseFont bf = null;
            // This keeps track of the creation time
            DateTime PrintTime = DateTime.Now;

            #region Fields
            private string _header;
            #endregion

            #region Properties
            public string Header
            {
                get { return _header; }
                set { _header = value; }
            }
            #endregion

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    PrintTime = DateTime.Now;
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb = writer.DirectContent;
                    //headerTemplate = cb.CreateTemplate(100, 77);
                    footerTemplate = cb.CreateTemplate(200, 50);
                }
                catch (DocumentException de)
                {
                    //GenerateLog("Error 111");
                }
                catch (System.IO.IOException ioe)
                {
                    //GenerateLog("Error 112 : " + ioe.Data..Message);
                }
            }

            public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
            {
                base.OnEndPage(writer, document);

                iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

                iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
                //String text = "                                                        Create Date : " + System.DateTime.Now.ToShortDateString() + "                                                    Page " + writer.PageNumber + " of ";
                String text = "                                        Page " + writer.PageNumber + " of ";

                //Add paging to footer
                //{
                //    cb.BeginText();
                //    cb.SetFontAndSize(bf, 10);
                //    cb.SetTextMatrix(document.PageSize.GetRight(550), document.PageSize.GetBottom(15));
                //    cb.ShowText(text);
                //    cb.EndText();
                //    float len = bf.GetWidthPoint(text, 10);
                //    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(550) + len, document.PageSize.GetBottom(15));//uncomment it
                //}

                //Move the pointer and draw line to separate footer section from rest of page
                cb.MoveTo(20, document.PageSize.GetBottom(30));
                cb.LineTo(document.PageSize.Width - 20, document.PageSize.GetBottom(30));
                cb.Stroke();

            }

            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
                iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

                iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

                // String text = "Page " + writer.PageNumber + " of ";
                String text = "";
                //Add paging to header
                {
                    //cb.BeginText();
                    //cb.SetFontAndSize(bf, 12);
                    //cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(40));
                    //cb.ShowText("");
                    //cb.EndText();
                    //float len = bf.GetWidthPoint(text, 12);

                }

            }
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber - 1).ToString());
                footerTemplate.EndText();
            }
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

        //public void GenerateLog(String Msg)
        //{
        //    //string appPath = HttpContext.Current.Request.ApplicationPath;
        //    //appPath = appPath + " | " + Server.MapPath("");
        //    String strDt = System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Year.ToString();
        //    String aName = @"~\pdf\" + strDt + ".txt";

        //    // 1: Write single line to new file
        //    using (StreamWriter writer = new StreamWriter(aName, true))
        //    {
        //        writer.WriteLine("\r\n" + System.DateTime.Now + " > " + Msg);//+ " " + appPath);
        //    }
        //}

        protected void gv_div_reminder_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        public void SetvalueInViewState()
        {
            //try
            //{
            //    ViewState["VS_div_id"] = Convert.ToString(Obj_global_value.div_id);
            //    ViewState["VS_emp_id"] = Convert.ToString(Obj_global_value.emp_id);
            //    ViewState["VS_year_id"] = Convert.ToString(Obj_global_value.year_id);
            //    ViewState["VS_evt_id"] = Convert.ToString(Obj_global_value.evt_id);
            //    ViewState["VS_tour_id"] = Convert.ToString(Obj_global_value.tour_id);
            //    ViewState["VS_active_ind"] = Convert.ToString(Obj_global_value.active_ind);
            //    ViewState["VS_pmt_plan_ind"] = Convert.ToString(Obj_global_value.pmt_plan_ind);
            //    ViewState["VS_pax_ind"] = Convert.ToString(Obj_global_value.pax_ind);
            //    ViewState["VS_flying_ind"] = Convert.ToString(Obj_global_value.flying_ind);
            //    ViewState["VS_cmg_bus_ind"] = Convert.ToString(Obj_global_value.cmg_bus_ind);
            //    ViewState["VS_inv_type_ind"] = Convert.ToString(Obj_global_value.inv_type_ind);
            //    ViewState["VS_final_ind"] = Convert.ToString(Obj_global_value.final_ind);
            //}
            //catch (Exception ex) { }
        }
        public void GetvalueInViewState()
        {
            //try
            //{
            //    if (Obj_global_value.tour_id == 0)
            //    {
            //        Obj_global_value.div_id = Convert.ToString(ViewState["VS_div_id"]);
            //        Obj_global_value.emp_id = Convert.ToString(ViewState["VS_emp_id"]);
            //        Obj_global_value.year_id = Convert.ToString(ViewState["VS_year_id"]);
            //        Obj_global_value.evt_id = Convert.ToString(ViewState["VS_evt_id"]);
            //        Obj_global_value.tour_id = Convert.ToInt32(ViewState["VS_tour_id"]);
            //        Obj_global_value.active_ind = Convert.ToInt32(ViewState["VS_active_ind"]);
            //        Obj_global_value.pmt_plan_ind = Convert.ToInt32(ViewState["VS_pmt_plan_ind"]);
            //        Obj_global_value.pax_ind = Convert.ToInt32(ViewState["VS_pax_ind"]);
            //        Obj_global_value.flying_ind = Convert.ToInt32(ViewState["VS_flying_ind"]);
            //        Obj_global_value.cmg_bus_ind = Convert.ToInt32(ViewState["VS_cmg_bus_ind"]);
            //        Obj_global_value.inv_type_ind = Convert.ToInt32(ViewState["VS_inv_type_ind"]);
            //        Obj_global_value.final_ind = Convert.ToInt32(ViewState["VS_final_ind"]);
            //    }
            //}
            //catch (Exception ex) { }
        }
    }
}