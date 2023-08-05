using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web.Services;
//using Microsoft.Office.Interop.Excel;

//using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;
//using Microsoft.Office.Interop.Excel.Application excel;
//using Microsoft.Office.Interop.Excel.Workbook worKbooK;
//using Microsoft.Office.Interop.Excel.Worksheet worksheet
//using Microsoft.Office.Interop.Excel.Range celLrangE;

namespace SchoolTours.Operations
{
    public partial class pax : System.Web.UI.Page
    {
        //static int tour_id = 0;
        //static int pax_id = 0;
        //static int room_id = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["tour_id"].ToString()) != 0)
                {
                    OPS_pax_id.Value = "0";
                    OPS_room_id.Value = "0";
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

        [WebMethod]
        public static string addupdateroom(string id1, string id2, string tour_id)
        {
            if (id1 == "0")
            {
                //OPS_pax_id.Value = Convert.ToInt32(id2);
                pax pax_obj = new pax();
                pax_obj.newRoom(id2, tour_id);
            }
            else
            {
                //room_id = Convert.ToInt32(id1);
                // pax_id = Convert.ToInt32(id2);
                pax pax_obj = new pax();
                pax_obj.addPaxRoom(id1, id2, tour_id);
            }
            return id1 + "~" + id2;
        }
        [WebMethod]
        public static string jquerypaxdetail(string id1)
        {
            //pax_id = Convert.ToInt32(id1);
            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
            obj.mode = "pax";
            obj.id1 = id1.ToString();
            DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
            pax pax_obj = new pax();
            string str_dt = pax_obj.DataTableToJSONWithStringBuilder(dt);
            return str_dt;
        }
        [WebMethod]
        public static string jquerydelPaxRoom(string room_id, string pax_id, string tour_id)
        {

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "pax_room";
            obj.id1 = Convert.ToInt32(tour_id);
            obj.id2 = Convert.ToInt32(pax_id);
            obj.id3 = Convert.ToInt32(room_id); ;

            int Response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            return "";

        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
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


                select_div.SelectedValue = (Session["div_id"].ToString());
                select_emp.SelectedValue = (Session["emp_id_ops"].ToString());
                select_year.SelectedValue = (Session["year_id"].ToString());
                ListEvts();
                select_evt_serach.SelectedValue = (Session["evt_id"].ToString());
                srchEvtsfunction();
                select_tour.SelectedValue = (Session["tour_id.ToString()"].ToString());

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
            //● Execute pr_dtl_item(‘tour’, @tour_id) which returns a single row recordset.  
            //○ Display the contents of the 1st column(tour_descr) in div_tour_info.  
            //○ Display the contents of the 6th column(pax_nr) in div_nr_pax. 
            //○ Display the contents of the 13th - 17th columns as Q = quad_nr / T = triple_nr / D = double_nr / S = single_nr / X = other_nr in div_nr_rooms 
            //● Execute pr_list_items(‘tour_options’, @tour_id) which returns four multiple row recordsets: ○ Populate select_role from 1.role_id, 2.role_descr  ○ Populate select_age from 1.age_id, 2.age_descr  
            //○ Populate select_sex from 1.sex_id, 2.sex_descr  
            //○ Populate select_diet from 1.diet_id, 2.diet_descr ○ If @options_nr & 32 = true, populate select_payer from 1.payer_id, 2.payer_nm  
            //● Execute pr_lst_items(‘pax’, @tour_id) which returns a multiple row recordset with the following columns: 1.pax_id, 2.pax_descr.Populate select_pax from this recordset with a onClick action of dtlPax(pax_id). 
            //● Execute pr_lst_items(‘tour_rooms’, @tour_id) which returns a multiple row recordset with the following columns: 1.room_id, 2.pax_id, 3.pax_nm, 4.panel_color, 5.room_size.
            //Use this recordset to create div_room_@room_id of the color from panel_color, displaying the room_size in upper right corner, and the pax_nms listed, each with an onDblClick action of delPaxRoom(pax_id). 
            //● If @options_nr & 8, display the input_birthdate object. 
            //● If @options_nr & 32 display the select_payer, input_eMail, and input_phone objects.
            try
            {
                Obj_DTL_ITEM obj_tour = new Obj_DTL_ITEM();
                obj_tour.mode = "tour";
                obj_tour.id1 = Convert.ToString(OPS_tour_id.Value);
                DataTable dt_tour = DTL_ITEM_Business.GetItemDetails(obj_tour).Tables[0];

                div_tour_info.Text = dt_tour.Rows[0]["tour_descr"].ToString();
                div_nr_pax.Text = dt_tour.Rows[0]["pax_nr"].ToString();

                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "tour_options";
                obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                select_role.DataSource = ds.Tables[0];
                select_role.DataTextField = "role_descr";
                select_role.DataValueField = "role_id";
                select_role.DataBind();
                select_role.Items.Insert(0, "Role");

                select_age.DataSource = ds.Tables[1];
                select_age.DataTextField = "age_descr";
                select_age.DataValueField = "age_id";
                select_age.DataBind();
                select_age.Items.Insert(0, "Age");

                select_sex.DataSource = ds.Tables[2];
                select_sex.DataTextField = "sex_descr";
                select_sex.DataValueField = "sex_id";
                select_sex.DataBind();
                select_sex.Items.Insert(0, "Sex");


                select_diet.DataSource = ds.Tables[3];
                select_diet.DataTextField = "diet_descr";
                select_diet.DataValueField = "diet_id";
                select_diet.DataBind();
                //   select_diet.Items.Insert(0, "Diet");

                select_payer.DataSource = ds.Tables[4];
                select_payer.DataTextField = "payer_nm";
                select_payer.DataValueField = "pax_id";
                select_payer.DataBind();
                select_payer.Items.Insert(0, "Payer");

                lbl_quad_nr.Text = dt_tour.Rows[0]["quad_nr"].ToString();
                lbl_triple_nr.Text = dt_tour.Rows[0]["triple_nr"].ToString();
                lbl_double_nr.Text = dt_tour.Rows[0]["double_nr"].ToString();
                lbl_single_nr.Text = dt_tour.Rows[0]["single_nr"].ToString();
                lbl_other_nr.Text = dt_tour.Rows[0]["other_nr"].ToString();

                GetPaxDetails();

                Obj_LST_ITEMS obj_tour_rooms = new Obj_LST_ITEMS();
                obj_tour_rooms.mode = "tour_rooms";
                obj_tour_rooms.id1 = Convert.ToInt32(OPS_tour_id.Value);
                DataSet ds_tour_rooms = DTL_ITEM_Business.Get_LST_ITEMS(obj_tour_rooms);

                CreateRoom(ds_tour_rooms);
                if (Convert.ToInt32(Session["inv_type_ind"].ToString()) == 1)
                {
                    select_payer.Visible = true;
                    input_eMail.Visible = true;
                    input_phone.Visible = true;
                }
                else if (Convert.ToInt32(Session["inv_type_ind"].ToString()) == 0)
                {
                    select_payer.Visible = false;
                    input_eMail.Visible = false;
                    input_phone.Visible = false;
                }
                if (Convert.ToInt32(Session["flying_ind"].ToString()) == 0)
                {
                    input_birthdate.Visible = false;
                }
                else
                {
                    input_birthdate.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void CreateRoom(DataSet ds_tour_rooms)
        {

            StringBuilder SB = new StringBuilder();

            DataTable dt = ds_tour_rooms.Tables[2];
            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "room_id");

            for (int i = distinctValues.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = distinctValues.Rows[i];
                if (Convert.ToInt32(dr.ItemArray[0]) == 0)
                    dr.Delete();
            }
            distinctValues.AcceptChanges();

            SB.Append("<div class='col-xl-4 col-lg-4 col-md-4 col-sm-6 mb-3 dropBox' >");
            SB.Append("<div class='card h-100 overflow-hidden border-0' >");
            SB.Append("<div class='d-flex justify-content-center align-items-center h-100 bg-secondary droptrue' id='0' ondragover='dragOver(event);' ondrop='drop(event);'>");
            //SB.Append("<a href='javascript: void(0);' class='text-center d-block' onclick=NewRoomCreate();>");
            SB.Append("<img src='http://43.229.227.26:81/schoolTours/Content/images/icon_plus_new.png' alt='' Width='35' id='0' ondragover='dragOver(event);' ondrop='drop(event);'/>");
            // SB.Append("</a>");
            SB.Append("</div>");
            SB.Append(" </div>");
            SB.Append("</div>");


            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {

                if (distinctValues.Rows[i]["room_id"].ToString() != "")
                {
                    int Room_id = Convert.ToInt32(distinctValues.Rows[i]["room_id"].ToString());
                    DataRow[] dr = dt.Select("room_id = " + Room_id);

                    DataTable dt_new = dt.Clone();

                    foreach (DataRow r in dr)
                    {
                        dt_new.ImportRow(r);
                    }
                    DataView viewroom_size = new DataView(dt_new);
                    DataTable distinctRoomSize = viewroom_size.ToTable(true, "room_size");

                    for (int j = 0; j < distinctRoomSize.Rows.Count; j++)
                    {

                        string room_size = Convert.ToString(distinctRoomSize.Rows[j]["room_size"].ToString());

                        DataView viewroom_type = new DataView(dt_new);
                        DataTable distinctroom_type = viewroom_size.ToTable(true, "room_type");


                        for (int k = 0; k < distinctroom_type.Rows.Count; k++)
                        {
                            DataRow[] dr_final = dt_new.Select("room_id = '" + Room_id + "' AND room_size = '" + room_size + "' AND room_type = '" + distinctroom_type.Rows[k]["room_type"].ToString() + "'");
                            if (dr_final.Length != 0)
                            {
                                SB.Append("<div class='col-xl-4 col-lg-4 col-md-4 col-sm-6 mb-3 dropBox ' >");
                                SB.Append("<div class='card h-100 overflow-hidden border-dark'>");

                                SB.Append("<div class='h-100 droptrue'  id='" + dr_final[0].ItemArray[0].ToString() + "' ondragover='dragOver(event);' ondrop='drop(event);'>");
                                if (dr_final[0].ItemArray[3].ToString() == "MALE")
                                    SB.Append("<p class='text-center  mb-0 border-bottom border-dark' style='background-color: " + "#A9E5FF" + ";color:black;'>" + dr_final[0].ItemArray[2].ToString() + "</p>");
                                else if (dr_final[0].ItemArray[3].ToString() == "FEMALE")
                                    SB.Append("<p class='text-center  mb-0 border-bottom border-dark' style='background-color: " + "#FFDAF4" + ";color:black;'>" + dr_final[0].ItemArray[2].ToString() + "</p>");
                                else if (dr_final[0].ItemArray[3].ToString() == "MIXED")
                                    SB.Append("<p class='text-center mb-0 border-bottom border-dark' style='background-color: " + "#F8F9BE" + ";color:black;'>" + dr_final[0].ItemArray[2].ToString() + "</p>");

                                for (int z = 0; z < dr_final.Length; z++)
                                {
                                    SB.Append("<a href='#' ondblclick='javascript:fun_delPaxRoom(" + dr_final[z].ItemArray[0].ToString() + "," + dr_final[z].ItemArray[1].ToString() + ");' class='d-block p-2'>" + dr_final[z].ItemArray[4].ToString() + "</a>");
                                }

                                SB.Append("</div>");
                                SB.Append("</div>");
                                SB.Append("</div>");
                            }
                        }

                    }

                }
            }

            //for (int i = 0; i < distinctValues.Rows.Count; i++)
            //{

            //    if (distinctValues.Rows[i]["room_id"].ToString() != "")
            //    {
            //        int Room_id = Convert.ToInt32(distinctValues.Rows[i]["room_id"].ToString());
            //        DataRow[] dr = dt.Select("room_id = " + Room_id);
            //        SB.Append("<div class='col-xl-4 col-lg-4 col-md-4 col-sm-6 mb-3 dropBox ' >");
            //        SB.Append("<div class='card h-100 overflow-hidden border-0'>");
            //        SB.Append("<div class='h-100 p-2 droptrue' style='background-color: " + dr[0].ItemArray[3].ToString() + ";' id='" + dr[0].ItemArray[0].ToString() + "' ondragover='dragOver(event);' ondrop='drop(event);'>");
            //        SB.Append("<p class='text-right text-white mb-0'>" + dr[0].ItemArray[4].ToString() + "</p>");
            //        for (int j = 0; j < dr.Length; j++)
            //        {
            //            SB.Append("<a href='#' ondblclick='javascript:fun_delPaxRoom(" + dr[j].ItemArray[0].ToString() + "," + dr[j].ItemArray[1].ToString() + ");' class='d-block'>" + dr[j].ItemArray[2].ToString() + "</a>");
            //        }
            //        SB.Append("</div>");
            //        SB.Append("</div>");
            //        SB.Append("</div>");
            //    }
            //}

            div_Room.InnerHtml = SB.ToString();
        }
        public void GetPaxDetails()
        {
            input_given_nm.Text = "";
            input_last_nm.Text = "";
            input_birthdate.Text = "";
            input_phone.Text = "";
            input_eMail.Text = "";
            select_role.SelectedValue = "Role";
            select_age.SelectedValue = "Age";
            select_sex.SelectedValue = "Sex";
            //select_diet.SelectedValue = "Diet";  //04sep2020
            //select_payer.SelectedValue = "Payer";

            Obj_LST_ITEMS obj_pax = new Obj_LST_ITEMS();
            obj_pax.mode = "pax";
            obj_pax.id1 = Convert.ToInt32(OPS_tour_id.Value);
            DataTable dt_obj_pax = DTL_ITEM_Business.Get_LST_ITEMS(obj_pax).Tables[0];

            StringBuilder SB = new StringBuilder();


            DataView view = dt_obj_pax.DefaultView;
            //view.Sort = "mod_dt desc";
            view.Sort = "sorter asc";
            dt_obj_pax = view.ToTable();
            for (int i = 0; i < dt_obj_pax.Rows.Count; i++)
            {
                SB.Append("<div title='" + dt_obj_pax.Rows[i]["pax_descr"].ToString() + "' class='text-white' id=" + dt_obj_pax.Rows[i]["pax_id"].ToString() + " draggable='true' ondragstart='dragStart(event);' onclick='javascript: fun_getpaxdetails(" + dt_obj_pax.Rows[i]["pax_id"].ToString() + "); '>" + dt_obj_pax.Rows[i]["pax_descr"].ToString() + "</div>");
            }

            div_pax.InnerHtml = SB.ToString();
            //select_pax.DataSource = dt_obj_pax;
            //select_pax.DataTextField = "pax_descr";
            //select_pax.DataValueField = "pax_id";
            //select_pax.DataBind();
        }


        public void dtlPax()
        {
            try
            {
                //execute pr_dtl_item(‘pax’, @pax_id) which returns a single row recordset with the following columns: 
                if (OPS_pax_id.Value != "0")
                {

                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "pax";
                    obj.id1 = OPS_pax_id.Value.ToString();
                    DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];


                    input_given_nm.Text = dt.Rows[0][0].ToString();
                    input_last_nm.Text = dt.Rows[0]["last_nm"].ToString();
                    select_role.SelectedValue = dt.Rows[0]["role_id"].ToString();
                    select_age.SelectedValue = dt.Rows[0]["age_id"].ToString();
                    select_sex.SelectedValue = dt.Rows[0]["sex_id"].ToString();
                    select_diet.SelectedValue = dt.Rows[0]["diet_id"].ToString() == "" ? "Diet" : dt.Rows[0]["diet_id"].ToString();
                    select_payer.SelectedValue = dt.Rows[0]["payer_id"].ToString() == "" ? "Payer" : dt.Rows[0]["payer_id"].ToString();

                    input_eMail.Text = dt.Rows[0]["eMail"].ToString();
                    input_phone.Text = phoneformatting(dt.Rows[0]["phone"].ToString());
                    if (dt.Rows[0]["birth_date"].ToString() != "")
                    {
                        DateTime dt_birthdate = DateTime.Parse(dt.Rows[0]["birth_date"].ToString());
                        input_birthdate.Text = dt_birthdate.ToString("yyyy-MM-dd");
                    }



                    //newRoom();



                }
            }
            catch (Exception ex)
            {

            }
        }
        public void delPax(object sender, EventArgs e)
        {
            //First confirm that the user wants to complete this action.  ● On confirm, execute pr_del_item(‘pax’, @tour_id, @pax_id).  
            //● Clear all fields(see newPax() below). ● Execute pr_lst_items(‘pax’, @tour_id) to populate select pax (see onLoad() below). 
            if (OPS_pax_id.Value != "0")
            {
                Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                obj.mode = "pax";
                obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
                obj.id2 = Convert.ToInt32(OPS_pax_id.Value);
                int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
                if (response == 1)
                {
                    GetPaxDetails();

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Pax first.')", true);
            }

        }
        public void newPax(object sender, EventArgs e)
        {
            AddnewPax();
        }
        public void AddnewPax()
        {
            //Set @pax_id = 0 ● Clear values from input_given_nm, input_last_nm, and (if necessary) input_birthdate. 
            //● Set the following drop-downs back to a blank value: select_role, select_age, select_sex, select_diet, and (if necessary) select_payer. 
            try
            {
                OPS_pax_id.Value = "0";
                input_given_nm.Text = "";
                input_last_nm.Text = "";
                select_role.SelectedValue = "Role";
                select_age.SelectedValue = "Age";
                select_sex.SelectedValue = "Sex";
                //select_diet.SelectedValue = "Diet";
                //select_payer.SelectedValue = "Payer";
                input_birthdate.Text = "";
                input_eMail.Text = "";
                input_phone.Text = "";
            }
            catch (Exception ex) { }
        }
        public void setPax(object sender, EventArgs e)
        {
            //● First validate that input_given_nm and input_last_nm are not blank.Validate select_role, select_age, and select_sex have selected values. 
            //If @options_nr &8 = true also validate input_birthdate for a valid date. ● After successful validation, 
            //execute pr_set_item(‘pax’, @tour_id, @emp_id, @pax_id, @role_id, @given_nm, @last_nm, @age_id, @sex_id, @diet_id, @birthdate, @payer_id, @eMail, @phone) 
            //which will return the pax_id if success, 0 if failure.
            //● Execute pr_lst_items(‘pax’, @tour_id) which returns a multiple row recordset with the following columns: 1.pax_id, 2.pax_descr.Populate select_pax from this recordset with a onClick action of dtlPax(pax_id).
            try
            {
                if (Convert.ToInt32(Session["flying_ind"].ToString()) == 1)
                {
                    if (input_birthdate.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please fill valid date.')", true);
                        return;
                    }
                }
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "pax";
                obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.id3 = Convert.ToInt32(OPS_pax_id.Value);
                obj.id4 = Convert.ToInt32(select_role.SelectedValue);
                obj.str1 = input_given_nm.Text.Trim();
                obj.str2 = input_last_nm.Text.Trim();
                obj.str3 = select_age.SelectedValue;
                obj.str4 = select_sex.SelectedValue;
                obj.str5 = select_diet.SelectedValue;
                //obj.str6 = input_birthdate.Text;
                //obj.str7 = select_payer.SelectedValue;
                obj.str6 = select_payer.SelectedValue == "Payer" ? "" : select_payer.SelectedValue;
                obj.str7 = input_birthdate.Text;
                obj.str8 = input_eMail.Text;
                obj.str9 = input_phone.Text.Replace(@".", string.Empty);

                DataTable dt = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];
                if (Convert.ToInt32(dt.Rows[0]["rc"].ToString()) > 0)
                {

                    GetPaxDetails();
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    string err_msg = "\"" + dt.Rows[0]["err_msg"].ToString() + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }
            }
            catch (Exception ex) { }
        }

        public void newRoom(string pax_id, string tour_id)
        {
            //When a user drags a person’s name from select_pax and drops it on the new room panel, 
            //execute pr_set_item(‘pax_room’, @tour_id, @pax_id, 0) which returns a single row recordset: 1.room_id. 
            //● The first recordset is single row recordset with the following columns: 1.pax_id, 2.pax_nm, 3.panel_color, 4.room_count.
            //Use this recordset to update / create div_room_ @room_id of the color from panel_color, 
            //displaying the room_size in upper right corner, and the pax_nms listed, each with an onDblClick action of delPaxRoom(pax_id). 
            //● Execute pr_lst_items(‘pax’, @tour_id) to re - populate select pax(see onLoad() below).

            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "pax_room";
            obj.id1 = Convert.ToInt32(tour_id);
            obj.id2 = Convert.ToInt32(pax_id);
            obj.id3 = 0;
            DataSet ds = DTL_ITEM_Business.Put_SET_ITEM_DS(obj);
        }
        public void addPaxRoom(string room_id, string pax_id, string tour_id)
        {
            //pr_set_item(‘pax_room’, @tour_id, @pax_id, @room_id) which returns a single row recordset: 1. room_id

            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "pax_room";
            obj.id1 = Convert.ToInt32(tour_id);
            obj.id2 = Convert.ToInt32(pax_id);
            obj.id3 = Convert.ToInt32(room_id);
            DataSet ds = DTL_ITEM_Business.Put_SET_ITEM_DS(obj);

            OnLoad();
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
        protected void input_phone_TextChanged(object sender, EventArgs e)
        {
            input_phone.Text = phoneformatting(input_phone.Text);
        }

        public void delPaxRoom()
        {
            // execute pr_del_item(‘pax_room’, @tour_id, @pax_id, @room_id) which returns a single row recordset with the following column: 1. room_count. If room_count = 0, 
            //remove panel from page. Otherwise remove the name that was double-clicked from the room panel. 

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "pax_room";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
            obj.id2 = Convert.ToInt32(OPS_pax_id.Value);
            obj.id3 = Convert.ToInt32(OPS_room_id.Value);
            DataSet ds = DTL_ITEM_Business.del_DEL_ITEM_DS(obj);

            Response.Redirect(Request.RawUrl);
        }


        public void prnList(object sender, EventArgs e)
        {
            //Execute pr_lst_items(‘tour_rooms’, @tour_id) which returns three recordsets. Map the data in these three recordsets as shown below. 
            //● The first is a single row recordset with the following columns: A.img, B.local_phone, C.toll_free_phone.. 
            //● The second is a single row recordset with the following columns: D.person_nm, E.entity_descr, F.evt_nm, G.print_date.  
            //● The third is a multiple row recordset with the following columns: 1.room_id, 2.pax_id, 3.pax_nm, 4.panel_color, 5.room_size.
            //Use this recordset to create a PDF document that matches the look and feel of the div_room_assign.

            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

            obj.mode = "tour_rooms";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
            DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

            CreatePDF(ds);
        }
        public void CreatePDF(DataSet ds)
        {

            DataTable dt_imgage = ds.Tables[0];
            DataTable dt_person = ds.Tables[1];
            DataTable dt_room = ds.Tables[2];

            StringBuilder SB = new StringBuilder();

            string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
            string strUrl = "";
            //if (url[2] == "localhost:2108")
            if (url[2].Contains("43.229.227.26"))
            {
                strUrl = "http://" + url[2] + "/schoolTours/Images/";

            }
            else
            {
                strUrl = "http://" + url[2] + "/Images/";

            }
            strUrl = strUrl + dt_imgage.Rows[0]["img"].ToString();
            #region old
            //SB.Append("<html lang=\"en\">");
            //SB.Append("<head>");
            //SB.Append("<title>PDF</title>");

            //SB.Append("</head>");
            //SB.Append("<body>");
            //SB.Append("<div class=\"page\">");
            //SB.Append("<table style=\"border:1px solid transparent;border-collapse:collapse;\">");
            //SB.Append("<thead>");
            //SB.Append("<tr>");
            //SB.Append("<th colspan=\"4\" style=\"border:2px dashed black;padding:20px;text-align:center;\"><img src='" + strUrl + " ' height='150' align='bottom' style='display: block'></img></th>");
            //SB.Append("</tr>");
            //SB.Append("<tr><td colspan=\"4\" style=\"color:white;\">.</td></tr>");

            //SB.Append("<tr style=\"border: 1px solid;\"> ");
            //SB.Append("<td colspan=\"4\" style=\"text-align: center;\">7255 E Hampton Ave - Suite 127 - Mesa, AZ 85209 - " + phoneformatting(dt_imgage.Rows[0]["local_phone"].ToString()) + "-" + phoneformatting(dt_imgage.Rows[0]["toll_free_phone"].ToString()) + "</td>");
            //SB.Append("</tr>");

            //SB.Append("<tr><td colspan=\"4\" style=\"color:white;\">.</td></tr>");

            //SB.Append("<tr style=\"border:1px solid;\"><td colspan=\"4\">");
            //SB.Append("<table>");
            //SB.Append("<tr>");
            //SB.Append("<td style=\"text-align: center;\">" + dt_person.Rows[0]["person_nm"].ToString() + " - " + dt_person.Rows[0]["entity_descr"].ToString() + "</td>");
            //SB.Append("</tr>");
            //SB.Append("<tr>");
            //SB.Append("<td style=\"text-align: center;\">" + dt_person.Rows[0]["evt_nm"].ToString() + "</td>");
            //SB.Append("</tr>");
            //SB.Append("<tr>");
            //SB.Append("<td style=\"text-align: center;\"> Roomming List" + dt_person.Rows[0]["print_date"].ToString() + "</td>");
            //SB.Append("</tr>");
            //SB.Append("</table>");

            //SB.Append("</td></tr>");


            //DataView view = new DataView(dt_room);
            //DataTable distinctValues = view.ToTable(true, "room_id");
            //int Ivalue = 0;
            //int loopvalue = 0;


            //for (int i = Ivalue; i < distinctValues.Rows.Count; i++)
            //{

            //    if (distinctValues.Rows[i]["room_id"].ToString() != "")
            //    {
            //        if (loopvalue == 0)
            //        {
            //            SB.Append("<tr><td colspan=\"4\" style=\"color:white;\">.</td></tr>");
            //            SB.Append("<tr>");
            //        }
            //        loopvalue++;
            //        int Room_id = Convert.ToInt32(distinctValues.Rows[i]["room_id"].ToString());
            //        DataRow[] dr = dt_room.Select("room_id = " + Room_id);
            //        //SB.Append("<th style=\"width:1px; color: white;\">.</th>");
            //        SB.Append("<th>");
            //        SB.Append("<table style=\"background: " + dr[0].ItemArray[3].ToString() + ";color:#fff;margin-right:5px;\">");
            //        SB.Append("<tr style=\"float:right;\"><td>" + dr[0].ItemArray[4].ToString() + "</td></tr>");
            //        for (int j = 0; j < dr.Length; j++)
            //        {
            //            SB.Append("<tr><td>" + dr[j].ItemArray[2].ToString() + "</td></tr>");
            //        }
            //        SB.Append("</table>");
            //        SB.Append("</th>");
            //        if (loopvalue == 4)
            //        {
            //            SB.Append("</tr>");
            //            loopvalue = 0;
            //            //break;
            //        }
            //    }
            //}
            //if (loopvalue > 0)
            //{
            //    SB.Append("</tr>");
            //}


            //SB.Append("</thead>");
            //SB.Append("</table>");
            //SB.Append("</div>");
            //SB.Append("</body>");
            //SB.Append("</html>");
            #endregion old

            #region new 
            SB.Append("<html lang=\"en\">");
            SB.Append("<head>");
            //SB.Append("<meta charset=\"UTF-8\">");
            SB.Append("<title>PDF</title>");
            SB.Append("</head>");
            SB.Append("<body>");
            SB.Append("<div class=\"page\">");
            SB.Append("<table style=\"border:1px solid transparent; border-collapse: separate; border-spacing: 20px 0.8rem;\">");
            SB.Append("<thead>");

            SB.Append("<tr>");
            SB.Append("<td colspan=\"4\" style=\"border:1px solid black;padding: 20px;\"><table style=\"width:100%;\"><tr><td style=\"width:10%;color:white\">.</td><td colspan=\"3\"><img src='" + strUrl + " ' height='60' align='bottom' style='display:block;'></img></td></tr></table></td>");
            SB.Append("</tr>");

            //SB.Append("<tr>");
            //SB.Append("<td colspan=\"4\" style=\"border: 1px dashed black;padding: 20px;text-align:center;\"><img src='" + strUrl + " ' height='60' align='bottom' style='display:block;'></img></td>");
            //SB.Append("</tr>");
            SB.Append("<tr>");
            SB.Append("<td colspan=\"4\" style=\"border: 2px solid black; padding: 10px;\">");
            SB.Append("7255 E Hampton Ave - Suite 127 - Mesa, AZ 85209 - " + phoneformatting(dt_imgage.Rows[0]["local_phone"].ToString()) + " - " + phoneformatting(dt_imgage.Rows[0]["toll_free_phone"].ToString()));
            SB.Append("</td>");
            SB.Append("</tr>");
            SB.Append("<tr><td colspan=\"4\" style=\"color:white;\">.</td></tr>");
            SB.Append("<tr>");
            SB.Append("<td colspan=\"4\" style=\"border: 1px solid black;padding: 10px;\">");
            SB.Append("<p style=\"text-align:center;\">" + dt_person.Rows[0]["person_nm"].ToString() + " - " + dt_person.Rows[0]["entity_descr"].ToString() + "</p>");
            SB.Append("<p style=\"text-align:center;\">" + dt_person.Rows[0]["evt_nm"].ToString() + "</p>");
            SB.Append("<p style=\"text-align:center;\">Roomming List" + dt_person.Rows[0]["print_date"].ToString() + "</p>");
            SB.Append("</td>");
            SB.Append("</tr>");

            DataView view = new DataView(dt_room);
            DataTable distinctValues = view.ToTable(true, "room_id");
            int Ivalue = 0;
            int loopvalue = 0;

            for (int i = Ivalue; i < distinctValues.Rows.Count; i++)
            {

                if (distinctValues.Rows[i]["room_id"].ToString() != "")
                {
                    if (loopvalue == 0)
                    {
                        SB.Append("<tr><td colspan=\"4\" style=\"color:white;\">.</td></tr>");
                        SB.Append("<tr>");
                    }
                    loopvalue++;
                    int Room_id = Convert.ToInt32(distinctValues.Rows[i]["room_id"].ToString());
                    DataRow[] dr = dt_room.Select("room_id = " + Room_id);

                    SB.Append("<td style=\"background:" + dr[0].ItemArray[3].ToString() + ";color:#fff;padding:1em 1em\">");
                    SB.Append("<p style=\"text-align:right; margin-bottom:10px;\">" + dr[0].ItemArray[4].ToString() + "</p>");
                    //SB.Append("<ul style=\"margin: 0px;padding: 0px;list-style:none !important;\">");
                    for (int j = 0; j < dr.Length; j++)
                    {
                        SB.Append("<p style=\"padding:0px 10px;\">" + dr[j].ItemArray[2].ToString() + "</p>");
                        // SB.Append("<li style=\"list-style:none !important;\">" + dr[j].ItemArray[2].ToString() + "</li>");

                    }
                    //SB.Append("</ul>");
                    SB.Append("</td>");
                    if (loopvalue == 4)
                    {
                        SB.Append("</tr>");
                        loopvalue = 0;
                        //break;
                    }
                }
            }
            if (loopvalue > 0)
            {
                SB.Append("</tr>");
            }
            //SB.Append("<tr>");
            //SB.Append("<th style=\"background: blue;color: #fff;padding:1em 1em\">");
            //SB.Append("<p style=\"text-align:right; margin-bottom:10px;\">Quad</p>");
            //SB.Append("<ul style=\"margin: 0px;padding: 0px;\">");
            //SB.Append("<li style=\"list-style: none;\">List 1</li>");
            //SB.Append("<li style=\"list-style: none;\">List 2</li>");
            //SB.Append("<li style=\"list-style: none;\">List 3</li>");
            //SB.Append("<li style=\"list-style: none;\">List 4</li>");
            //SB.Append("</ul>");
            //SB.Append("</th>");
            //SB.Append("</tr>");


            SB.Append("</thead>");
            SB.Append("</table>");
            SB.Append("</div>");
            SB.Append("</body>");
            SB.Append("</html>");
            #endregion new 

            string strSB = SB.ToString();


            //div_Print.InnerHtml = strSB;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm123();", true);
            //return;
            try
            {

                GeneratePDFReport("pdf", "div_room_assign.pdf", "", strSB);

                string Path = "~/pdf/" + "div_room_assign.pdf";
                if (Path != string.Empty)
                {
                    WebClient req = new WebClient();
                    HttpResponse response = System.Web.HttpContext.Current.Response;
                    string filePath = Path;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=div_room_assign.pdf");
                    byte[] data = req.DownloadData(Server.MapPath(filePath));
                    response.BinaryWrite(data);
                    response.End();
                }
            }
            catch (Exception ex)
            {

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
                DirectoryInfo dtIfo = Directory.CreateDirectory(BaseDir + SavePath);

                if (dtIfo.Exists)
                {
                    PdfWriter pw = PdfWriter.GetInstance(pdfDoc, new FileStream(PackaginglistPDFPath, FileMode.Create));
                    pw.PageEvent = new ITextEvents();
                    pdfDoc.Open();
                    StringReader cont = new System.IO.StringReader(HtmlBody);
                    XMLWorkerHelper.GetInstance().ParseXHtml(pw, pdfDoc, cont);
                    pdfDoc.Close();
                }

                return baseURLpath.Replace("\\", "/");
            }
            catch (Exception ex)
            {
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
                }
                catch (System.IO.IOException ioe)
                {

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


        protected void btn_Rooming_List_Click(object sender, EventArgs e)
        {
            try
            {
                // If “Rooming List” was clicked, execute pr_lst_items(‘tour_rooms’, @tour_id) which returns three
                //recordsets.Map the data in these three recordsets as shown below.
                //○ The first is a single row recordset with the following columns: A.img, B.local_phone, C.
                //toll_free_phone..
                //○ The second is a single row recordset with the following columns: D.person_nm, E.
                //entity_descr, F.evt_nm, G.print_date.
                //○ The third is a multiple row recordset with the following columns: 1.room_id, 2.room_size,
                //3.room_type, 4.pax_nm, 5.pax _role.Use this recordset to create the Excel
                //spreadsheet shown below..

                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

                obj.mode = "tour_rooms";
                obj.id1 = Convert.ToInt32(OPS_tour_id.Value);

                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);


                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Rooming_list.xls");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<style>  .txt " + "\r\n" + " {mso-style-parent:style0;mso-number-format:\"" + @"\@" + "\"" + ";} " + "\r\n" + "</style>");
                //sets font
                HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;mso-number-format:General;'>");
                HttpContext.Current.Response.Write("<BR><BR><BR>");

                HttpContext.Current.Response.Write("<Table style='width: 50%;'>");

                HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
                HttpContext.Current.Response.Write("<TD colspan=5></TD>");
                HttpContext.Current.Response.Write("</TR>");


                HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
                HttpContext.Current.Response.Write("<TD colspan=5>" + ds.Tables[1].Rows[0]["entity_descr"].ToString() + "</TD>");
                HttpContext.Current.Response.Write("</TR>");

                //HttpContext.Current.Response.Write("<TR style='text-align: center;'><TD colspan=5></TD></TR>");

                HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
                HttpContext.Current.Response.Write("<TD colspan=5>" + ds.Tables[1].Rows[0]["print_date"].ToString() + "</TD>");
                HttpContext.Current.Response.Write("</TR>");

                HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
                HttpContext.Current.Response.Write("<TD colspan=5>" + ds.Tables[1].Rows[0]["evt_nm"].ToString() + "</TD>");
                HttpContext.Current.Response.Write("</TR>");



                HttpContext.Current.Response.Write("<TR style='text-align: center;'><TD colspan=5></TD></TR>");


                HttpContext.Current.Response.Write("<TR style='font-weight:bold;text-align: center;border:1px solid;'>");
                //HttpContext.Current.Response.Write("<TD style=width:10%;'>Nbr.</TD><TD style=width:10%;'>Size</TD><TD style=width:10%;'>Type</TD><TD style=width:10%;'>Name</TD><TD style=width:10%;'>Role</TD>");
                HttpContext.Current.Response.Write("<TD>Nbr.</TD><TD>Size</TD><TD>Type</TD><TD>Name</TD><TD>Role</TD>");
                HttpContext.Current.Response.Write("</TR>");

                HttpContext.Current.Response.Write("</Table>");
                DataTable dt = ds.Tables[2];

                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "room_id");

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    int Room_id = Convert.ToInt32(distinctValues.Rows[i]["room_id"].ToString());
                    DataRow[] dr = dt.Select("room_id = " + Room_id);

                    DataTable dt_new = dt.Clone();

                    foreach (DataRow r in dr)
                    {
                        dt_new.ImportRow(r);
                    }
                    HttpContext.Current.Response.Write("<table><tr style='text-align:center;'><td colspan=5></td></tr></table>");

                    HttpContext.Current.Response.Write("<Table style='border:1px solid;'>");
                    foreach (DataRow item in dt_new.Rows)
                    {
                        HttpContext.Current.Response.Write("<TR style='text-align:center;'>");
                        HttpContext.Current.Response.Write("<TD>" + item.ItemArray[0].ToString() + "</TD>");
                        HttpContext.Current.Response.Write("<TD>" + item.ItemArray[2].ToString() + "</TD>");
                        HttpContext.Current.Response.Write("<TD>" + item.ItemArray[3].ToString() + "</TD>");
                        HttpContext.Current.Response.Write("<TD>" + item.ItemArray[4].ToString() + "</TD>");
                        HttpContext.Current.Response.Write("<TD>" + item.ItemArray[5].ToString() + "</TD>");
                        HttpContext.Current.Response.Write("</TR>");
                    }
                    HttpContext.Current.Response.Write("</Table>");
                }
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    string err_msg = "\"" + ex.Message + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }

            }
        }

        protected void btn_Participant_List_Click(object sender, EventArgs e)
        {
            //If “Participant List” was clicked, execute pr_lst_items(‘tour_rooms’, @tour_id) which returns two recordsets. 
            //Map the data in these three recordsets as shown below.
            //The first is a single row recordset with the following columns: A.img, B.local_phone, C.toll_free_phone..
            //The second is a single row recordset with the following columns: D.person_nm, E.entity_descr, F.evt_nm, G.print_date.
            //Do not use the third recordset.
            //Execute pr_list_items((‘pax_list’, @tour_id) which returns a multiple row recordset with the following columns: 
            //1.pax_nm, 2.sex, 3.role, 4.age, 5.diet, 6.birthdate, 7.room_nbr.Use this recordset to create the Excel spreadsheet shown below.

            try
            {
                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

                obj.mode = "tour_rooms";
                obj.id1 = Convert.ToInt32(OPS_tour_id.Value);

                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                Obj_LST_ITEMS obj_pax_list = new Obj_LST_ITEMS();

                obj_pax_list.mode = "pax_list";
                //obj_pax_list.id1 = Convert.ToInt32(OPS_pax_id.Value);
                obj_pax_list.id1 = Convert.ToInt32(OPS_tour_id.Value);

                DataSet ds_pax_list = DTL_ITEM_Business.Get_LST_ITEMS(obj_pax_list);


                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Participant_List.xls");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<style>  .txt " + "\r\n" + " {mso-style-parent:style0;mso-number-format:\"" + @"\@" + "\"" + ";} " + "\r\n" + "</style>");
                //sets font
                HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;mso-number-format:General;'>");
                HttpContext.Current.Response.Write("<BR><BR><BR>");

                HttpContext.Current.Response.Write("<Table style='width:50%;>");

                HttpContext.Current.Response.Write("<TR style='text-align:center;'>");
                HttpContext.Current.Response.Write("<TD colspan=7></TD>");
                HttpContext.Current.Response.Write("</TR>");

                //HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
                //HttpContext.Current.Response.Write("<TD colspan=7>" + phoneformatting(ds.Tables[0].Rows[0]["local_phone"].ToString()) + "/" + phoneformatting(ds.Tables[0].Rows[0]["toll_free_phone"].ToString()) + "</TD>");
                //HttpContext.Current.Response.Write("</TR>");

                HttpContext.Current.Response.Write("<TR style='text-align: center;'><TD colspan=7></TD></TR>");

                HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
                HttpContext.Current.Response.Write("<TD colspan=7>" + ds.Tables[1].Rows[0]["person_nm"].ToString() + " - " + ds.Tables[1].Rows[0]["entity_descr"].ToString() + "</TD>");
                HttpContext.Current.Response.Write("</TR>");

                HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
                HttpContext.Current.Response.Write("<TD colspan=7>" + ds.Tables[1].Rows[0]["evt_nm"].ToString() + "</TD>");
                HttpContext.Current.Response.Write("</TR>");

                HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
                HttpContext.Current.Response.Write("<TD colspan=7>" + "Participant List (" + ds.Tables[1].Rows[0]["print_date"].ToString() + ")</TD>");
                HttpContext.Current.Response.Write("</TR>");

                HttpContext.Current.Response.Write("<TR style='text-align: center;'><TD colspan=7></TD></TR>");
                HttpContext.Current.Response.Write("</Table>");


                HttpContext.Current.Response.Write("<Table style='border:1px solid;border-collapse:collapse;'>");
                HttpContext.Current.Response.Write("<TR style='font-weight:bold;text-align: center;border:1px solid;'>");
                HttpContext.Current.Response.Write("<TD style='width:15%;border:1px solid;'>Name</TD><TD style='width:15%;border:1px solid;'>Sex</TD><TD style='width:15%;border:1px solid;'>Role</TD><TD style='width:15%;border:1px solid;'>Age</TD><TD style='width:15%;border:1px solid;'>Diet</TD><TD style='width:15%;border:1px solid;'>Birthdate</TD><TD style='width:15%;border:1px solid;'>Room</TD>");
                HttpContext.Current.Response.Write("</TR>");

                foreach (DataRow item in ds_pax_list.Tables[0].Rows)
                {
                    HttpContext.Current.Response.Write("<TR style='text-align: center;border:1px solid;' >");
                    HttpContext.Current.Response.Write("<TD style='border:1px solid;'>" + item.ItemArray[0].ToString() + "</TD>");
                    HttpContext.Current.Response.Write("<TD style='border:1px solid;'>" + item.ItemArray[1].ToString() + "</TD>");
                    HttpContext.Current.Response.Write("<TD style='border:1px solid;'>" + item.ItemArray[2].ToString() + "</TD>");
                    HttpContext.Current.Response.Write("<TD style='border:1px solid;'>" + item.ItemArray[3].ToString() + "</TD>");
                    HttpContext.Current.Response.Write("<TD style='border:1px solid;'>" + item.ItemArray[4].ToString() + "</TD>");
                    HttpContext.Current.Response.Write("<TD style='border:1px solid;'>" + item.ItemArray[5].ToString() + "</TD>");
                    HttpContext.Current.Response.Write("<TD style='border:1px solid;'>" + item.ItemArray[6].ToString() + "</TD>");

                    HttpContext.Current.Response.Write("</TR>");
                }

                HttpContext.Current.Response.Write("</Table>");

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    string err_msg = "\"" + ex.Message + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }

            }
        }

        #region Not use
        protected void btn_Rooming_List_Click_1(object sender, EventArgs e)
        {
            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
            obj.mode = "tour_rooms";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);
            DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

            DataTable dt = ds.Tables[2];

            try
            {

                string path = Server.MapPath("..\\exportedfiles\\");
                if (!Directory.Exists(path))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
                {
                    Directory.CreateDirectory(path);
                }

                File.Delete(path + "Rooming_list.xlsx"); // DELETE THE FILE BEFORE CREATING A NEW ONE.
                Excel.Application xlAppToExport = new Excel.Application();
                xlAppToExport.Workbooks.Add("");

                // ADD A WORKSHEET.
                Excel.Worksheet xlWorkSheetToExport = default(Excel.Worksheet);
                xlWorkSheetToExport = (Excel.Worksheet)xlAppToExport.Sheets["Sheet1"];

                // ROW ID FROM WHERE THE DATA STARTS SHOWING.
                int iRowCnt = 7;

                // SHOW THE HEADER.
                //xlWorkSheetToExport.Cells[1, 1] = ds.Tables[1].Rows[0]["entity_descr"].ToString();
                //xlWorkSheetToExport.Cells[2, 1] = ds.Tables[1].Rows[0]["person_nm"].ToString();
                //xlWorkSheetToExport.Cells[3, 1] = ds.Tables[1].Rows[0]["evt_nm"].ToString();
                //xlWorkSheetToExport.Cells[4, 1] = "";

                xlWorkSheetToExport.Cells[1, 1] = phoneformatting(ds.Tables[0].Rows[0]["local_phone"].ToString()) + "/" + phoneformatting(ds.Tables[0].Rows[0]["toll_free_phone"].ToString());
                xlWorkSheetToExport.Cells[2, 1] = ds.Tables[1].Rows[0]["person_nm"].ToString() + " - " + ds.Tables[1].Rows[0]["entity_descr"].ToString();
                xlWorkSheetToExport.Cells[3, 1] = ds.Tables[1].Rows[0]["evt_nm"].ToString();
                xlWorkSheetToExport.Cells[4, 1] = "Rooming List (" + ds.Tables[1].Rows[0]["print_date"].ToString() + ")";




                //Excel.Range range = xlWorkSheetToExport.Cells[1, 1] as Excel.Range;
                //range.EntireRow.Font.Name = "Calibri";
                //range.EntireRow.Font.Bold = true;
                //range.EntireRow.Font.Size = 14;


                //Excel.Range range_1 = xlWorkSheetToExport.Cells[2, 1] as Excel.Range;
                //range_1.EntireRow.Font.Name = "Calibri";
                //range_1.EntireRow.Font.Bold = true;
                //range_1.EntireRow.Font.Size = 14;

                //Excel.Range range_2 = xlWorkSheetToExport.Cells[3, 1] as Excel.Range;
                //range_2.EntireRow.Font.Name = "Calibri";
                //range_2.EntireRow.Font.Bold = true;
                //range_2.EntireRow.Font.Size = 14;

                xlWorkSheetToExport.Range["A1:E1"].MergeCells = true;       // MERGE CELLS OF THE HEADER.
                xlWorkSheetToExport.Range["A2:E2"].MergeCells = true;       // MERGE CELLS OF THE HEADER.
                xlWorkSheetToExport.Range["A3:E3"].MergeCells = true;       // MERGE CELLS OF THE HEADER.
                xlWorkSheetToExport.Range["A4:E4"].MergeCells = true;       // MERGE CELLS OF THE HEADER.


                xlWorkSheetToExport.Range["A1:E1"].Font.Bold = true;
                xlWorkSheetToExport.Range["A2:E2"].Font.Bold = true;
                xlWorkSheetToExport.Range["A3:E3"].Font.Bold = true;
                xlWorkSheetToExport.Range["A4:E4"].Font.Bold = true;

                xlWorkSheetToExport.Range["A1:E1"].Font.Size = 14;
                xlWorkSheetToExport.Range["A2:E2"].Font.Size = 14;
                xlWorkSheetToExport.Range["A3:E3"].Font.Size = 14;
                xlWorkSheetToExport.Range["A4:E4"].Font.Size = 14;



                // SHOW COLUMNS ON THE TOP.
                xlWorkSheetToExport.Cells[iRowCnt - 1, 1] = "Nbr.";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 2] = "Size";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 3] = "Type";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 4] = "Name";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 5] = "Role";



                #region distinctValues
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "room_id");
                iRowCnt = iRowCnt + 1;
                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {

                    int Room_id = Convert.ToInt32(distinctValues.Rows[i]["room_id"].ToString());
                    DataRow[] dr = dt.Select("room_id = " + Room_id);

                    DataTable dt_new = dt.Clone();

                    foreach (DataRow r in dr)
                    {
                        dt_new.ImportRow(r);
                    }

                    int j = 0; string S_Count = "A" + iRowCnt; string E_Count = "";
                    for (j = 0; j <= dt_new.Rows.Count - 1; j++)
                    {
                        xlWorkSheetToExport.Cells[iRowCnt, 1] = dt.Rows[j].Field<Int32>("room_id");
                        xlWorkSheetToExport.Cells[iRowCnt, 2] = dt.Rows[j].Field<string>("room_size");
                        xlWorkSheetToExport.Cells[iRowCnt, 3] = dt.Rows[j].Field<string>("room_type");
                        xlWorkSheetToExport.Cells[iRowCnt, 4] = dt.Rows[j].Field<string>("pax_nm");
                        xlWorkSheetToExport.Cells[iRowCnt, 5] = dt.Rows[j].Field<string>("ref_descr");
                        iRowCnt = iRowCnt + 1;
                        E_Count = "E" + iRowCnt;

                        xlWorkSheetToExport.get_Range("" + S_Count + ":" + E_Count + "").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    }


                    //Excel.Range workSheet_range = xlAppToExport.ActiveCell.Worksheet.Cells["" + S_Count + ":" + E_Count + ""] as Excel.Range;
                    //workSheet_range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);


                    // xlWorkSheetToExport.Rows["" + S_Count + ":" + E_Count + ""].
                    //Excel.Range range_5 = xlWorkSheetToExport.Rows["" + S_Count + ":" + E_Count + ""] as Excel.Range;
                    // range_5.Style.BottomBorder.Type = BorderStyle.Dashed;

                    iRowCnt = iRowCnt + 1;
                    //Excel.Range range1 = xlAppToExport.ActiveCell.Worksheet.Cells[5, 1] as Excel.Range;
                    //range1.AutoFormat(ExcelAutoFormat.xlRangeAutoFormatList3);
                }
                #endregion distinctValues

                #region Old
                //int i;
                //for (i = 0; i <= dt.Rows.Count - 1; i++)
                //{
                //    xlWorkSheetToExport.Cells[iRowCnt, 1] = dt.Rows[i].Field<Int32>("room_id");
                //    xlWorkSheetToExport.Cells[iRowCnt, 2] = dt.Rows[i].Field<string>("room_size");
                //    xlWorkSheetToExport.Cells[iRowCnt, 3] = dt.Rows[i].Field<string>("room_type");
                //    xlWorkSheetToExport.Cells[iRowCnt, 4] = dt.Rows[i].Field<string>("pax_nm");
                //    xlWorkSheetToExport.Cells[iRowCnt, 5] = dt.Rows[i].Field<string>("ref_descr");

                //    iRowCnt = iRowCnt + 1;
                //}

                #endregion Old


                // FINALLY, FORMAT THE EXCEL SHEET USING EXCEL'S AUTOFORMAT FUNCTION.
                Excel.Range range2 = xlAppToExport.ActiveCell.Worksheet.Cells[5, 1] as Excel.Range;
                range2.AutoFormat(ExcelAutoFormat.xlRangeAutoFormatList3);


                for (int i = 1; i <= 10; i++) // this will apply it from col 1 to 10
                {
                    xlWorkSheetToExport.Columns[i].ColumnWidth = 15;
                }

                xlWorkSheetToExport.get_Range("A1", "A6").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



                // SAVE THE FILE IN A FOLDER.
                xlWorkSheetToExport.SaveAs(path + "Rooming_list.xlsx");

                // CLEAR.
                xlAppToExport.Workbooks.Close();

                xlAppToExport.Quit();
                xlAppToExport = null;
                xlWorkSheetToExport = null;

                /// Download the file 
                /// 
                  // Clear Rsponse reference  
                Response.Clear();
                // Add header by specifying file name  
                Response.ContentType = "application/xlsx";
                Response.AppendHeader("Content-Disposition", "attachment; filename=Rooming_list.xlsx");
                Response.TransmitFile(Server.MapPath("~/exportedfiles/Rooming_list.xlsx"));
                Response.End();

            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    string err_msg = "\"" + ex.Message + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }
            }
        }
        protected void btn_Participant_List_Click_1(object sender, EventArgs e)
        {
            try
            {
                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "tour_rooms";
                obj.id1 = Convert.ToInt32(OPS_tour_id.Value);

                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                Obj_LST_ITEMS obj_pax_list = new Obj_LST_ITEMS();
                obj_pax_list.mode = "pax_list";
                obj_pax_list.id1 = Convert.ToInt32(OPS_tour_id.Value);

                DataSet ds_pax_list = DTL_ITEM_Business.Get_LST_ITEMS(obj_pax_list);


                string path = Server.MapPath("..\\exportedfiles\\");
                if (!Directory.Exists(path))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
                {
                    Directory.CreateDirectory(path);
                }

                File.Delete(path + "Participant_List.xlsx"); // DELETE THE FILE BEFORE CREATING A NEW ONE.
                Excel.Application xlAppToExport = new Excel.Application();
                xlAppToExport.Workbooks.Add("");

                // ADD A WORKSHEET.
                Excel.Worksheet xlWorkSheetToExport = default(Excel.Worksheet);
                xlWorkSheetToExport = (Excel.Worksheet)xlAppToExport.Sheets["Sheet1"];

                // ROW ID FROM WHERE THE DATA STARTS SHOWING.
                int iRowCnt = 7;

                xlWorkSheetToExport.Cells[1, 1] = phoneformatting(ds.Tables[0].Rows[0]["local_phone"].ToString()) + "/" + phoneformatting(ds.Tables[0].Rows[0]["toll_free_phone"].ToString());
                xlWorkSheetToExport.Cells[2, 1] = ds.Tables[1].Rows[0]["person_nm"].ToString() + " - " + ds.Tables[1].Rows[0]["entity_descr"].ToString();
                xlWorkSheetToExport.Cells[3, 1] = ds.Tables[1].Rows[0]["evt_nm"].ToString();
                xlWorkSheetToExport.Cells[4, 1] = "Rooming List (" + ds.Tables[1].Rows[0]["print_date"].ToString() + ")";


                xlWorkSheetToExport.Range["A1:G1"].MergeCells = true;       // MERGE CELLS OF THE HEADER.
                xlWorkSheetToExport.Range["A2:G2"].MergeCells = true;       // MERGE CELLS OF THE HEADER.
                xlWorkSheetToExport.Range["A3:G3"].MergeCells = true;       // MERGE CELLS OF THE HEADER.
                xlWorkSheetToExport.Range["A4:G4"].MergeCells = true;       // MERGE CELLS OF THE HEADER.

                xlWorkSheetToExport.Cells[iRowCnt - 1, 1] = "Name";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 2] = "Sex";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 3] = "Role";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 4] = "Age";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 5] = "Diet";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 6] = "Birthdate";
                xlWorkSheetToExport.Cells[iRowCnt - 1, 7] = "Room";

                foreach (DataRow item in ds_pax_list.Tables[0].Rows)
                {

                    xlWorkSheetToExport.Cells[iRowCnt, 1] = item.ItemArray[0].ToString();
                    xlWorkSheetToExport.Cells[iRowCnt, 2] = item.ItemArray[1].ToString();
                    xlWorkSheetToExport.Cells[iRowCnt, 3] = item.ItemArray[2].ToString();
                    xlWorkSheetToExport.Cells[iRowCnt, 4] = item.ItemArray[3].ToString();
                    xlWorkSheetToExport.Cells[iRowCnt, 5] = item.ItemArray[4].ToString();
                    xlWorkSheetToExport.Cells[iRowCnt, 6] = item.ItemArray[5].ToString();
                    xlWorkSheetToExport.Cells[iRowCnt, 7] = item.ItemArray[6].ToString();

                    iRowCnt = iRowCnt + 1;
                }
                Excel.Range range2 = xlAppToExport.ActiveCell.Worksheet.Cells[5, 1] as Excel.Range;
                range2.AutoFormat(ExcelAutoFormat.xlRangeAutoFormatList3);

                for (int i = 1; i <= 10; i++) // this will apply it from col 1 to 10
                {
                    //xlWorkSheetToExport.Columns[i].ColumnWidth = 15;
                }

                xlWorkSheetToExport.get_Range("A1", "A6").Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



                // SAVE THE FILE IN A FOLDER.
                xlWorkSheetToExport.SaveAs(path + "Participant_List.xlsx");

                // CLEAR.
                xlAppToExport.Workbooks.Close();

                xlAppToExport.Quit();
                xlAppToExport = null;
                xlWorkSheetToExport = null;

                /// Download the file 
                /// 
                // Clear Rsponse reference  
                Response.Clear();
                // Add header by specifying file name  
                Response.ContentType = "application/xlsx";
                Response.AppendHeader("Content-Disposition", "attachment; filename=Participant_List.xlsx");
                Response.TransmitFile(Server.MapPath("~/exportedfiles/Participant_List.xlsx"));
                Response.End();
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    string err_msg = "\"" + ex.Message + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }
            }
        }

        protected void btn_Rooming_List_Click_Old(object sender, EventArgs e)
        {
            // If “Rooming List” was clicked, execute pr_lst_items(‘tour_rooms’, @tour_id) which returns three
            //recordsets.Map the data in these three recordsets as shown below.
            //○ The first is a single row recordset with the following columns: A.img, B.local_phone, C.
            //toll_free_phone..
            //○ The second is a single row recordset with the following columns: D.person_nm, E.
            //entity_descr, F.evt_nm, G.print_date.
            //○ The third is a multiple row recordset with the following columns: 1.room_id, 2.room_size,
            //3.room_type, 4.pax_nm, 5.pax _role.Use this recordset to create the Excel
            //spreadsheet shown below..

            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

            obj.mode = "tour_rooms";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);

            DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Rooming_list.xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<style>  .txt " + "\r\n" + " {mso-style-parent:style0;mso-number-format:\"" + @"\@" + "\"" + ";} " + "\r\n" + "</style>");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;mso-number-format:General;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");

            HttpContext.Current.Response.Write("<Table style='width: 100%;'>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=5></TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=5>" + phoneformatting(ds.Tables[0].Rows[0]["local_phone"].ToString()) + "/" + phoneformatting(ds.Tables[0].Rows[0]["toll_free_phone"].ToString()) + "</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'><TD colspan=5></TD></TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=5>" + ds.Tables[1].Rows[0]["person_nm"].ToString() + " - " + ds.Tables[1].Rows[0]["entity_descr"].ToString() + "</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=5>" + ds.Tables[1].Rows[0]["evt_nm"].ToString() + "</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=5>" + "Rooming List (" + ds.Tables[1].Rows[0]["print_date"].ToString() + ")</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'><TD colspan=5></TD></TR>");


            HttpContext.Current.Response.Write("<TR style='font-weight:bold;text-align: center;' >");
            //HttpContext.Current.Response.Write("<TD style=width:10%;'>Nbr.</TD><TD style=width:10%;'>Size</TD><TD style=width:10%;'>Type</TD><TD style=width:10%;'>Name</TD><TD style=width:10%;'>Role</TD>");
            HttpContext.Current.Response.Write("<TD>Nbr.</TD><TD>Size</TD><TD>Type</TD><TD>Name</TD><TD>Role</TD>");
            HttpContext.Current.Response.Write("</TR>");

            foreach (DataRow item in ds.Tables[2].Rows)
            {
                HttpContext.Current.Response.Write("<TR style='text-align:center;'>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[0].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[2].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[3].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[4].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[5].ToString() + "</TD>");
                HttpContext.Current.Response.Write("</TR>");
            }

            HttpContext.Current.Response.Write("</Table>");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }

        protected void btn_Participant_List_Click_Old(object sender, EventArgs e)
        {
            //If “Participant List” was clicked, execute pr_lst_items(‘tour_rooms’, @tour_id) which returns two recordsets. 
            //Map the data in these three recordsets as shown below.
            //The first is a single row recordset with the following columns: A.img, B.local_phone, C.toll_free_phone..
            //The second is a single row recordset with the following columns: D.person_nm, E.entity_descr, F.evt_nm, G.print_date.
            //Do not use the third recordset.
            //Execute pr_list_items((‘pax_list’, @tour_id) which returns a multiple row recordset with the following columns: 
            //1.pax_nm, 2.sex, 3.role, 4.age, 5.diet, 6.birthdate, 7.room_nbr.Use this recordset to create the Excel spreadsheet shown below.


            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

            obj.mode = "tour_rooms";
            obj.id1 = Convert.ToInt32(OPS_tour_id.Value);

            DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

            Obj_LST_ITEMS obj_pax_list = new Obj_LST_ITEMS();

            obj_pax_list.mode = "pax_list";
            obj_pax_list.id1 = Convert.ToInt32(OPS_pax_id.Value);

            DataSet ds_pax_list = DTL_ITEM_Business.Get_LST_ITEMS(obj_pax_list);


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Participant_List.xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<style>  .txt " + "\r\n" + " {mso-style-parent:style0;mso-number-format:\"" + @"\@" + "\"" + ";} " + "\r\n" + "</style>");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;mso-number-format:General;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");

            HttpContext.Current.Response.Write("<Table>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=7></TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=7>" + phoneformatting(ds.Tables[0].Rows[0]["local_phone"].ToString()) + "/" + phoneformatting(ds.Tables[0].Rows[0]["toll_free_phone"].ToString()) + "</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'><TD colspan=7></TD></TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=7>" + ds.Tables[1].Rows[0]["person_nm"].ToString() + " - " + ds.Tables[1].Rows[0]["entity_descr"].ToString() + "</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=7>" + ds.Tables[1].Rows[0]["evt_nm"].ToString() + "</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'>");
            HttpContext.Current.Response.Write("<TD colspan=7>" + "Rooming List (" + ds.Tables[1].Rows[0]["print_date"].ToString() + ")</TD>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR style='text-align: center;'><TD colspan=7></TD></TR>");

            HttpContext.Current.Response.Write("<TR style='font-weight:bold;text-align: center;' >");
            HttpContext.Current.Response.Write("<TD style='width:15%;'>Name</TD><TD style='width:15%;'>Sex</TD><TD style='width:15%;'>Role</TD><TD style='width:15%;'>Age</TD><TD style='width:15%;'>Diet</TD><TD style='width:15%;'>Birthdate</TD><TD style='width:15%;'>Room</TD>");
            HttpContext.Current.Response.Write("</TR>");

            foreach (DataRow item in ds_pax_list.Tables[0].Rows)
            {
                HttpContext.Current.Response.Write("<TR style='text-align: center;' >");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[0].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[1].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[2].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[3].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[4].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[5].ToString() + "</TD>");
                HttpContext.Current.Response.Write("<TD>" + item.ItemArray[6].ToString() + "</TD>");

                HttpContext.Current.Response.Write("</TR>");
            }

            HttpContext.Current.Response.Write("</Table>");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        #endregion Not use

    }
}