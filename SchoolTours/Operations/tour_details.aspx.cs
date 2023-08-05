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

namespace SchoolTours.Operations
{
    public partial class tour_details : System.Web.UI.Page
    {
        //static int tour_id = 0;
        //static int note_id = 0;
        //static int person_id = 0;
        //static int producer_id = 0;
        //static int operator_id = 0;

        //static int pmt_plan_ind_id = 0;
        //static int pax_ind_id = 0;
        //static int flying_ind_id = 0;
        //static int cmg_bus_ind_id = 0;
        //static int inv_type_ind_id = 0;
        //static int final_ind_id = 0;
        //static int options_nr = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["tour_id"] != null)
                {
                    if (Convert.ToInt32(Session["tour_id"].ToString()) != 0)
                    {
                        //note_id = 0;
                        //person_id = 0;
                        //producer_id = 0;
                        //operator_id = 0;

                        note_id.Value = "0";

                        pmt_plan_ind_id.Value = "0";
                        pax_ind_id.Value = "0";
                        flying_ind_id.Value = "0";
                        cmg_bus_ind_id.Value = "0";
                        inv_type_ind_id.Value = "0";
                        final_ind_id.Value = "0";
                        options_nr.Value = "0";
                        //tour_id = Convert.ToInt32(Convert.ToInt32(Session["tour_id"].ToString()));
                        OnPreLoad();
                        OnLoad();
                    }
                    else
                    {
                        Response.Redirect("ops");
                    }
                }
                else { Response.Redirect("ops"); }

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
                            ListItem li = new ListItem(item["tour_descr"].ToString(), item["tour_id"].ToString());
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

                    //tour_id = Obj_global_value.tour_id;

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
            //Execute pr_lst_items(‘year_evts’, @emp_id, @year) which returns a multiple row recordset with the following columns: 1.evt_id, 2.evt_nm. Use these values to populate select_evt. 
            //● Execute pr_lst_items(‘producer’, @emp_id) which returns a multiple row recordset with the following columns: 1.producer_id, 2.producer_nm.Use these values to populate select_producer. 
            //● Execute pr_lst_items(‘operator’, @emp_id) which returns a multiple row recordset with the following columns: 1.operator_id, 2.operator_nm.Use these values to populate select_operator. 
            //● Execute pr_dtl_item(‘tour’, @tour_id) which returns a single row recordset with the following columns. 
            try
            {
                int CurrentYear = Convert.ToInt32(DateTime.Now.Year);


                Obj_LST_ITEMS obj_evt = new Obj_LST_ITEMS();
                obj_evt.mode = "year_evts";
                obj_evt.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                obj_evt.str1 = Convert.ToString(Session["year_id"].ToString());
                DataTable dt_evt = DTL_ITEM_Business.Get_LST_ITEMS(obj_evt).Tables[0];

                select_evt.DataSource = dt_evt;
                select_evt.DataTextField = "evt_nm";
                select_evt.DataValueField = "evt_id";
                select_evt.DataBind();
                select_evt.Items.Insert(0, "Select");

                Obj_LST_ITEMS obj_producer = new Obj_LST_ITEMS();
                obj_producer.mode = "producer";
                obj_producer.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                DataTable dt_producer = DTL_ITEM_Business.Get_LST_ITEMS(obj_producer).Tables[0];

                select_producer.DataSource = dt_producer;
                select_producer.DataTextField = "column1";
                select_producer.DataValueField = "emp_id";
                select_producer.DataBind();
                select_producer.Items.Insert(0, "Producer");


                Obj_LST_ITEMS obj_operator = new Obj_LST_ITEMS();
                obj_operator.mode = "operator";
                obj_operator.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                DataTable dt_operator = DTL_ITEM_Business.Get_LST_ITEMS(obj_operator).Tables[0];

                select_operator.DataSource = dt_operator;
                select_operator.DataTextField = "operator_nm";
                select_operator.DataValueField = "operator_id";
                select_operator.DataBind();
                select_operator.Items.Insert(0, "Tour Operator");

                Obj_DTL_ITEM obj_tour = new Obj_DTL_ITEM();
                obj_tour.mode = "tour";
                obj_tour.id1 = Convert.ToString(select_tour.SelectedValue);
                DataTable dt_tour = DTL_ITEM_Business.GetItemDetails(obj_tour).Tables[0];

                div_tour_info.Text = dt_tour.Rows[0]["tour_descr"].ToString();
                select_evt.SelectedValue = dt_tour.Rows[0]["evt_id"].ToString();
                input_group_nm.Text = dt_tour.Rows[0]["group_nm"].ToString();

                try
                {
                    DateTime dt_start_date = DateTime.Parse(dt_tour.Rows[0]["start_date"].ToString());
                    DateTime dt_end_date = DateTime.Parse(dt_tour.Rows[0]["end_date"].ToString());

                    input_start_date.Text = dt_start_date.ToString("yyyy-MM-dd");
                    input_end_date.Text = dt_end_date.ToString("yyyy-MM-dd");
                }
                catch (Exception ex) { }

                //input_start_date.Text = dt_tour.Rows[0]["start_date"].ToString();
                //input_end_date.Text = dt_tour.Rows[0]["end_date"].ToString();
                select_producer.SelectedValue = dt_tour.Rows[0]["producer_id"].ToString();
                select_operator.SelectedValue = dt_tour.Rows[0]["operator_id"].ToString();
                input_pax_nr.Text = dt_tour.Rows[0]["pax_nr"].ToString();
                input_free_trip_nr.Text = dt_tour.Rows[0]["free_trip_nr"].ToString();
                input_driver_nr.Text = dt_tour.Rows[0]["driver_nr"].ToString();



                pmt_plan_ind_id.Value = Convert.ToString(dt_tour.Rows[0]["pmt_plan_ind"].ToString());
                pax_ind_id.Value = Convert.ToString(dt_tour.Rows[0]["pax_ind"].ToString());
                flying_ind_id.Value = Convert.ToString(dt_tour.Rows[0]["flying_ind"].ToString());
                cmg_bus_ind_id.Value = Convert.ToString(dt_tour.Rows[0]["cmg_bus_ind"].ToString());
                inv_type_ind_id.Value = Convert.ToString(dt_tour.Rows[0]["inv_type_ind"].ToString());
                if (dt_tour.Columns.Contains("final_ind"))
                {
                    final_ind_id.Value = Convert.ToString(dt_tour.Rows[0]["final_ind"].ToString());  //we are not find column  "final_ind" in response
                }
                if (pmt_plan_ind_id.Value == "2")
                    pmt_plan_ind.Checked = true;
                if (pax_ind_id.Value == "4")
                    pax_ind.Checked = true;
                if (flying_ind_id.Value == "8")
                    flying_ind.Checked = true;
                if (cmg_bus_ind_id.Value == "16")
                    cmg_bus_ind.Checked = true;
                if (inv_type_ind_id.Value == "32")
                    inv_type_ind.Checked = true;
                if (final_ind_id.Value == "64")
                    final_ind.Checked = true;

                int producer_id = Convert.ToInt32(dt_tour.Rows[0]["producer_id"].ToString());
                int operator_id = dt_tour.Rows[0]["operator_id"].ToString() == "" ? 0 : Convert.ToInt32(dt_tour.Rows[0]["operator_id"].ToString());
                //int person_id = Convert.ToInt32(dt_tour.Rows[0]["person_id"].ToString());

                person_id.Value = Convert.ToString(dt_tour.Rows[0]["person_id"].ToString());

                select_producer.SelectedValue = producer_id.ToString();
                select_operator.SelectedValue = operator_id.ToString();

                img_info.ToolTip = dt_tour.Rows[0]["mod_nm"].ToString() + " " + dt_tour.Rows[0]["mod_dt"].ToString();

                //mod_nm Concatenate with mod_dt to show mouseover prompt on img_info mod_dt Concatenate with mod_nm to show mouseover prompt on img_info contract_filename If NOT NULL,
                //display img_contract.Use value as parameter for onClick action of shwContact(contract_filename) phone Use value as parameter for onClick action of callPerson(phone) person_id 
                //Use value to populate local variable @person_id

                //Execute pr_lst_items(‘tour_reminder’, @tour_id) which returns a multiple row recordset with the following columns: 1.reminder_date, 2.reminder_descr.Populate div_reminder with these values, 
                //concatenating reminder_date and reminder_descr each on a single row. 
                //● Execute pr_lst_items(‘note’, @person_id) which returns two recordsets. The first is a multiple row
                //recordset with the following columns. | 1.note_id, 2.note_descr.
                //Populate select_notes from this list, with each item having an onclick action of dtlNote(note_id).The second recordset is a multiple row recordset with the following columns: 1.note_type_id, 2.note_type_descr.
                //Populate select_note_type from this list. ● Create two global boolean variables: @flying_ind and @inv_type_id.Set each item to true only if the corresponding value in the preceding recordset is > 0.
                //These booleans will be used in other screens to determine display of different screen elements or the name of URLs

                Obj_LST_ITEMS obj_tour_reminder = new Obj_LST_ITEMS();
                obj_tour_reminder.mode = "tour_reminder";
                obj_tour_reminder.id1 = Convert.ToInt32(select_tour.SelectedValue);

                DataTable dt_tour_reminder = DTL_ITEM_Business.Get_LST_ITEMS(obj_tour_reminder).Tables[0];

                DataTable dt_tour_r = new DataTable();
                dt_tour_r.Columns.AddRange(new DataColumn[1] { new DataColumn("Name") });
                for (int i = 0; i < dt_tour_reminder.Rows.Count; i++)
                {

                    dt_tour_r.Rows.Add(dt_tour_reminder.Rows[i]["reminder_date"].ToString() + " - " + dt_tour_reminder.Rows[i]["reminder_descr"].ToString());
                }

                div_reminder.DataSource = dt_tour_r;
                div_reminder.DataTextField = "Name";
                div_reminder.DataBind();

                GetNoteDetails();

            }
            catch (Exception ex)
            {

            }

        }

        //===================================================== Note

        #region Note

        public void dtlNote(object sender, EventArgs e)
        {
            //execute pr_dtl_item(‘note’, @note_id). This stored procedure returns a single row recordset with the following columns: 1.note_type_id, 2.note_txt, 3.mod_nm, 4.mod_dt.
            //Use note_type_id to select the appropriate item in select_note_type.Display note_txt in input_note_txt object.Display in mod_nm and mod_dt in div_mod_info
            if (select_notes.SelectedValue != "")
            {
                note_id.Value= Convert.ToString(select_notes.SelectedValue);
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "note";
                obj.id1 = Convert.ToString(select_notes.SelectedValue);
                DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    select_note_type.SelectedValue = dt.Rows[0]["note_type_id"].ToString();
                    input_note_txt.Text = dt.Rows[0]["note_txt"].ToString();
                    div_mod_info.Text = dt.Rows[0]["mod_nm"].ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('There is row count is 0 !!')", true);
                }
            }
        }
        public void delNote(object sender, EventArgs e)
        {
            //● On confirm, execute pr_del_item(‘note’, @note_id, @emp_id).  ● Then execute pr_lst_items(‘note’, @person_id) to repopulate select_notes
            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();

            obj.mode = "note";
            obj.id1 = Convert.ToInt32(note_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            int Response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (Response == 1)
            {
                GetNoteDetails();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failed')", true);
            }
        }
        public void newNote(object sender, EventArgs e)
        {
            //● Clear the select_note_type, input_note_txt, and div_mod_info fields. Also reset the note_id to 0. 
            select_note_type.SelectedValue = "Select";
            input_note_txt.Text = "";
            note_id.Value = "0";
            div_mod_info.Text = "";
        }
        public void setNote(object sender, EventArgs e)
        {
            //● Validate note_type and that input_note_txt is not empty.  
            //● Upon successful validation, execute pr_set_item(‘note’, @note_id, @person_id, @emp_id, @note_type_id, @note_text). The stored procedure will return the note_id.  
            //● Clear the select_note_type, input_note_txt, and div_mod_info fields.  
            //● Execute pr_lst_items(‘note’, @person_id) to repopulate the select_notes.

            Obj_SET_ITEM obj_set_note = new Obj_SET_ITEM();
            obj_set_note.mode = "note";
            //obj_set_note.id1 =  note_id;
            //obj_set_note.id2 = person_id;
            obj_set_note.id1 = Convert.ToInt32(note_id.Value);
            obj_set_note.id2 = Convert.ToInt32(person_id.Value);
            obj_set_note.id3 = Convert.ToInt32(Session["emp_id"].ToString());
            obj_set_note.id4 = Convert.ToInt32(select_note_type.SelectedValue);
            obj_set_note.str1 = Convert.ToString(input_note_txt.Text);

            int response = DTL_ITEM_Business.Put_SET_ITEM(obj_set_note);

            select_note_type.Items.Clear();
            input_note_txt.Text = "";
            div_mod_info.Text = "";
            GetNoteDetails();
        }
        public void GetNoteDetails()
        {
            note_id.Value = "0";
            Obj_LST_ITEMS obj_note = new Obj_LST_ITEMS();
            obj_note.mode = "note";
            obj_note.id1 = Convert.ToInt32(person_id.Value);

            DataSet dt_note = DTL_ITEM_Business.Get_LST_ITEMS(obj_note);
            select_notes.DataSource = dt_note.Tables[0];
            select_notes.DataTextField = "column1";
            select_notes.DataValueField = "note_id";
            select_notes.DataBind();

            select_note_type.DataSource = dt_note.Tables[1];
            select_note_type.DataTextField = "ref_descr";
            select_note_type.DataValueField = "ref_id";
            select_note_type.DataBind();
            select_note_type.Items.Insert(0, "Select");

            input_note_txt.Text = "";
            div_mod_info.Text = "";

        }

        #endregion Note

        //===================================================== Email

        public void sndMail(object sender, EventArgs e)
        {
            Response.Redirect("~/mailer?type=person&id=" + person_id.Value);
        }


        //===================================================== Tour

        #region Tour
        public void delTour(object sender, EventArgs e)
        {
            //First confirm that the user wants to complete this action.On confirm, execute pr_del_item(‘tour’, @tour_id, @emp_id).Then redirect to ops.aspx.
            Obj_DEL_ITEM obj_del_tour = new Obj_DEL_ITEM();
            obj_del_tour.mode = "tour";
            obj_del_tour.id1 = Convert.ToInt32(select_tour.SelectedValue);
            obj_del_tour.id2 = Convert.ToInt32(Session["emp_id"].ToString());

            int response = DTL_ITEM_Business.del_DEL_ITEM(obj_del_tour);
            if (response == 1)
            {
                string Baseurl = Convert.ToString(HttpContext.Current.Request.RawUrl);
                string RredirectUrl = "";
                if (Baseurl == "43.229.227.26")
                {
                    RredirectUrl = "schoolTours";
                }
                RredirectUrl += RredirectUrl + "/Operations/" + "ops";
                Response.Redirect(RredirectUrl);
            }


        }

        public void setTour(object sender, EventArgs e)
        {
            //Validate the following fields ○ select_event(item selected) ○ input_group_nm(not empty) ○ input_start_date(valid date in the future) 
            //○ input_end_date(valid date later than start_date) ○ select_producer(item selected) 
            //● Update the global @options_nr variable with the sum of inv_type_ind, cmg_bus_ind, flying_ind, pax_ind, and pmt_plan_ind. 
            //● Execute pr_set_item(‘tour’, @tour_id, @emp_id, @select_event, @select_producer, @group_nm, @start_date, @end_date, @options_nr, @input_pax_nr, @input_free_trip_nr, 
            //@input_driver_nr, @select_operator) which will return 1 if success, 0 if fail.

           int options_nr = Convert.ToInt32(Convert.ToInt32(inv_type_ind_id.Value) + Convert.ToInt32(cmg_bus_ind_id.Value) + Convert.ToInt32(flying_ind_id.Value) 
               + Convert.ToInt32(pax_ind_id.Value) + Convert.ToInt32(pmt_plan_ind_id.Value) + Convert.ToInt32(final_ind_id.Value)) + 1;

            Obj_SET_ITEM obj_set_tour = new Obj_SET_ITEM();

            obj_set_tour.mode = "tour";
            obj_set_tour.id1 = Convert.ToInt32(select_tour.SelectedValue);
            obj_set_tour.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj_set_tour.id3 = Convert.ToInt32(select_evt.SelectedValue);
            obj_set_tour.id4 = Convert.ToInt32(select_producer.SelectedValue);
            obj_set_tour.str1 = input_group_nm.Text.Trim();
            obj_set_tour.str2 = input_start_date.Text.Trim();
            obj_set_tour.str3 = input_end_date.Text.Trim();
            obj_set_tour.str4 = options_nr.ToString();
            obj_set_tour.str5 = input_pax_nr.Text.Trim();
            obj_set_tour.str6 = input_free_trip_nr.Text.Trim();
            obj_set_tour.str7 = input_driver_nr.Text.Trim();
            obj_set_tour.str8 = select_operator.SelectedValue.ToString() == "Tour Operator" ? null : select_operator.SelectedValue.ToString();


            //int Response = DTL_ITEM_Business.Put_SET_ITEM(obj_set_tour);
            //if (Response == 1)
            //{  
            DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj_set_tour).Tables[0];

            if (Convert.ToInt32(dt_Result.Rows[0]["rc"].ToString()) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The tour has been saved.')", true);
            }
            else
            {
                string err_msg = "\"" + dt_Result.Rows[0]["err_msg"].ToString() + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }
        #endregion Tour

        #region checkBox
        protected void pmt_plan_ind_CheckedChanged(object sender, EventArgs e)
        {
            chgOption("pmt_plan");
        }
        protected void pax_ind_CheckedChanged(object sender, EventArgs e)
        {
            chgOption("pax");
        }
        protected void flying_ind_CheckedChanged(object sender, EventArgs e)
        {
            chgOption("air");
        }
        protected void cmg_bus_ind_CheckedChanged(object sender, EventArgs e)
        {
            chgOption("bus");
        }
        protected void inv_type_ind_CheckedChanged(object sender, EventArgs e)
        {
            chgOption("inv");
        }
        protected void final_ind_CheckedChanged(object sender, EventArgs e)
        {
            chgOption("final");
        }
        public void chgOption(string type)
        {
            if (type == "pmt_plan")
            {
                if (pmt_plan_ind.Checked == true)
                    pmt_plan_ind_id.Value = "2";
                else
                    pmt_plan_ind_id.Value = "0";
            }
            else if (type == "pax")
            {
                if (pax_ind.Checked == true)
                    pax_ind_id.Value = "4";
                else
                    pax_ind_id.Value = "0";
            }
            else if (type == "air")
            {
                if (flying_ind.Checked == true)
                    flying_ind_id.Value = "8";
                else
                    flying_ind_id.Value = "0";
            }
            else if (type == "bus")
            {
                if (cmg_bus_ind.Checked == true)
                    cmg_bus_ind_id.Value = "16";
                else
                    cmg_bus_ind_id.Value = "0";
            }
            else if (type == "inv")
            {
                if (inv_type_ind.Checked == true)
                    inv_type_ind_id.Value = "32";
                else
                    inv_type_ind_id.Value = "0";
            }
            else if (type == "final")
            {
                if (final_ind.Checked == true)
                    final_ind_id.Value = "64";
                else
                    final_ind_id.Value = "0";
            }
            if (1 == 0)
            {
                #region OldCode
                //if (type == "pmt_plan")
                //{
                //    if (pmt_plan_ind.Checked == true)
                //        pmt_plan_ind_id = 32;
                //    else
                //        pmt_plan_ind_id = 0;
                //}
                //else if (type == "pax")
                //{
                //    if (pax_ind.Checked == true)
                //        pax_ind_id = 4;
                //    else
                //        pax_ind_id = 0;
                //}
                //else if (type == "air")
                //{
                //    if (flying_ind.Checked == true)
                //        flying_ind_id = 8;
                //    else
                //        flying_ind_id = 0;
                //}
                //else if (type == "bus")
                //{
                //    if (cmg_bus_ind.Checked == true)
                //        cmg_bus_ind_id = 16;
                //    else
                //        cmg_bus_ind_id = 0;
                //}
                //else if (type == "inv")
                //{
                //    if (inv_type_ind.Checked == true)
                //        inv_type_ind_id = 16;
                //    else
                //        inv_type_ind_id = 0;
                //}
                //else if (type == "final")
                //{
                //    if (final_ind.Checked == true)
                //        final_ind_id = 64;
                //    else
                //        final_ind_id = 0;
                //}
                #endregion OldCode
            }
        }

        #endregion checkBox

        #region PDFDoc
        protected void btn_attach_ctrc_Click(object sender, EventArgs e)
        {
            if (file_upload_ctrc.PostedFile.FileName != "")
            {
                file_upload_ctrc.SaveAs(Server.MapPath("~/Doc_Uploads/") + "ctrc_" + select_tour.SelectedValue + ".pdf");
            }
        }
        protected void btn_attach_itin_Click(object sender, EventArgs e)
        {
            if (file_upload_itin.PostedFile.FileName != "")
            {
                file_upload_itin.SaveAs(Server.MapPath("~/Doc_Uploads/") + "itin_" + select_tour.SelectedValue + ".pdf");
            }
        }
        protected void btn_download_itin_Click(object sender, EventArgs e)
        {
            downloadPDF("itin_");
        }
        protected void btn_download_ctrc_Click(object sender, EventArgs e)
        {
            downloadPDF("ctrc_");
        }
        public void downloadPDF(string type)
        {
            try
            {
                string filename = type + select_tour.SelectedValue + ".pdf";
                string wordDocName = "~/Doc_Uploads/" + filename;
                string wordDocName1 = "../Doc_Uploads/" + filename;
                if (File.Exists(Server.MapPath(wordDocName)))
                {
                    Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                    //Response.ContentType = "Application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    //Response.TransmitFile(Server.MapPath("~/Doc_Uploads/" + filename));
                    //Response.End();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This file does not exist yet.')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This file does not exist yet')", true);
            }
        }
        #endregion PDFDoc

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

        protected void img_mytour_Click(object sender, ImageClickEventArgs e)
        {
            //int a = tour_id;
            if (Convert.ToInt32(Session["tour_id"].ToString()) != 0)
            {
                //goMyTour() execute pr_dtl_item(‘mytour_load’, @tour_id.This stored procedure returns a single row recordset with the following columns: 
                //1.tour_id, 2.ind_ind, 3.person_id.Store these values in @tour_id, @ind_ind, and @person_id cookies.Then redirect the page to mytour_dashboard.aspx.

                try
                {
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "mytour_load";
                    obj.id1 = (Session["tour_id"].ToString());

                    DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];

                    string RredirectUrl = "../mytour/mytour_dashboard";
                    HttpCookie tour_id = new HttpCookie("CustomerFacing_tour");
                    tour_id.Value = dt.Rows[0]["tour_id"].ToString();
                    Response.Cookies.Add(tour_id);

                    HttpCookie ind_ind = new HttpCookie("CustomerFacing_ind_ind");
                    ind_ind.Value = dt.Rows[0]["ind_ind"].ToString();
                    Response.Cookies.Add(ind_ind);

                    HttpCookie person_id = new HttpCookie("CustomerFacing_person");
                    person_id.Value = dt.Rows[0]["person_id"].ToString();
                    Response.Cookies.Add(person_id);
                    ClientScript.RegisterStartupScript(GetType(), "popup", "window.open('" + RredirectUrl + "', 'pp');", true);
                    //Response.Redirect("<script>window.open(" + RredirectUrl + ",'_blank');</script>");
                    //                 Page.ClientScript.RegisterStartupScript(
                    //this.GetType(), "OpenWindow", "window.open(" + RredirectUrl + ",'_newtab');", true);
                    //Response.Redirect(RredirectUrl);
                }
                catch (Exception ex)
                {
                    string err_msg = "\"" + ex.Message + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }
            }
        }

        protected void sndInv(object sender, ImageClickEventArgs e)
        {
            //sndInv() Execute pr_mailer(‘mytour_inv’, @tour_id). This stored procedure returns a single row recordset with the following columns: 
            //1.recipient_eMail, 2.sender_eMail, 3.subject_txt, 4.body_txt.Use these fields to send an HTML eMail message. 
            try
            {
                Obj_PR_MAILER obj = new Obj_PR_MAILER();
                obj.mode = "mytour_inv";
                obj.id1 = Convert.ToInt32(select_tour.SelectedValue);
                DataTable dt = DTL_ITEM_Business.Get_PR_MAILER(obj).Tables[0];

                Models.Utility obj_Utility = new Models.Utility();
                obj_Utility.SendInvoice(dt.Rows[0]["subject_txt"].ToString(), dt.Rows[0]["sender_eMail"].ToString(), dt.Rows[0]["recipient_eMail"].ToString(), dt.Rows[0]["body_txt"].ToString(), null);
                //obj_Utility.SendInvoice(dt.Rows[0]["recipient_eMail"].ToString(), dt.Rows[0]["sender_eMail"].ToString(), dt.Rows[0]["sender_eMail"].ToString(), dt.Rows[0]["sender_eMail"].ToString(), null);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('mail sent successfully.')", true);
            }
            catch (Exception ex)
            {
                string err_msg = "\"" + ex.Message + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }
    }
}