using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursBusiness;
using SchoolToursData.Object;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text;
using System.Web.Services;

namespace SchoolTours.Mytour
{
    public partial class mytour_pax : System.Web.UI.Page
    {
        //static int tour_id = 0;
        //static int pax_id = 0;
        //static int room_id = 0;
        //static int admin_ind = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.Cookies["CustomerFacing_admin_ind"].Value != "")
                    {
                        admin_ind.Value = Convert.ToString(Request.Cookies["CustomerFacing_admin_ind"].Value);
                    }
                    if (Request.Cookies["CustomerFacing_tour"].Value != "")
                    {
                        tour_id.Value = Convert.ToString(Request.Cookies["CustomerFacing_tour"].Value);
                    }
                    OnLoad();
                }
                catch (Exception ex)
                {
                    Response.Redirect("../mytour_index.aspx");
                }
            }
        }
        [WebMethod]
        public static string addupdateroom(string id1, string id2, string admin_ind, string tour_id)
        {
            if (admin_ind == "1")
            {
                if (id1 == "0")
                {
                    //  paxid.Value = Convert.ToString(id2);
                    mytour_pax pax_obj = new mytour_pax();
                    pax_obj.newRoom(id2, tour_id);
                }
                else
                {
                    // room_id.Value = Convert.ToInt32(id1);
                    // pax_id.Value = Convert.ToInt32(id2);
                    mytour_pax pax_obj = new mytour_pax();
                    pax_obj.addPaxRoom(tour_id, id2, id1);
                }
            }
            return id1 + "~" + id2;
        }
        [WebMethod]
        public static string jquerypaxdetail(string id1)
        {
            //pax_id.Value = Convert.ToInt32(id1);
            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
            obj.mode = "pax";
            //obj.id1 = pax_id.ToString();
            obj.id1 = id1;
            DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
            mytour_pax pax_obj = new mytour_pax();
            string str_dt = pax_obj.DataTableToJSONWithStringBuilder(dt);
            return str_dt;
            // return "";
        }
        [WebMethod]
        public static string jquerydelPaxRoom(string room_id, string pax_id, string admin_ind, string tour_id)
        {
            if (admin_ind == "1")
            {
                Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                obj.mode = "pax_room";
                obj.id1 = Convert.ToInt32(tour_id);
                obj.id2 = Convert.ToInt32(pax_id);
                obj.id3 = Convert.ToInt32(room_id); ;

                int Response = DTL_ITEM_Business.del_DEL_ITEM(obj);

            }
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
            OnLoad();
        }
        public void addPaxRoom(string tour_id, string pax_id, string room_id)
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
        public void OnLoad()
        {
            //● Execute pr_dtl_item(‘tour’, @tour_id) which returns a single row recordset.  ○ Display the contents of the 1st column (tour_descr) in div_tour_info.  
            //○ Display the contents of the 6th column(pax_nr) in div_nr_pax. 
            //○ Display the contents of the 13th - 17th columns as Q = quad_nr / T = triple_nr / D = double_nr / S = single_nr / X = other_nr in div_nr_rooms 
            //● Execute pr_list_items(‘tour_options’, @tour_id) which returns four multiple row recordsets: ○ Populate select_role from 1.role_id, 2.role_descr  
            //○ Populate select_age from 1.age_id, 2.age_descr  
            //○ Populate select_sex from 1.sex_id, 2.sex_descr  ○ Populate select_diet from 1.diet_id, 2.diet_descr ○ If @inv_type_ind = 1, populate select_payer from 1.payer_id, 2.payer_nm  
            //● Execute pr_lst_items(‘pax’, @tour_id) which returns a multiple row recordset with the following columns: 1.pax_id, 2.pax_descr.
            //Populate select_pax from this recordset with a onClick action of dtlPax(pax_id). 
            //● Execute pr_lst_items(‘tour_rooms’, @tour_id) which returns a multiple row recordset with the following columns: 1.room_id, 2.pax_id, 3.pax_nm, 4.panel_color, 5.room_size.
            //Use this recordset to create div_room_@room_id of the color from panel_color, displaying the room_size in upper right corner, and the pax_nms listed, 
            //each with an onDblClick action of delPaxRoom(pax_id). 
            //● If @flying_ind = 1, display the input_birthdate object. ● If @inv_type_ind = 1, display the select_payer, input_eMail, and input_phone objects.

            try
            {
                Obj_DTL_ITEM obj_tour = new Obj_DTL_ITEM();
                obj_tour.mode = "tour";
                obj_tour.id1 = Convert.ToString(tour_id.Value);
                DataTable dt_tour = DTL_ITEM_Business.GetItemDetails(obj_tour).Tables[0];

                //div_tour_info.Text = dt_tour.Rows[0]["tour_descr"].ToString();
                div_nr_pax.Text = dt_tour.Rows[0]["pax_nr"].ToString();

                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "tour_options";
                obj.id1 = Convert.ToInt32(tour_id.Value);
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
                select_diet.Items.Insert(0, "Diet");

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
                obj_tour_rooms.id1 = Convert.ToInt32(tour_id.Value);
                DataSet ds_tour_rooms = DTL_ITEM_Business.Get_LST_ITEMS(obj_tour_rooms);

                CreateRoom(ds_tour_rooms);
                if (Obj_global_value.inv_type_ind == 1)
                {
                    select_payer.Visible = true;
                    input_eMail.Visible = true;
                    input_phone.Visible = true;
                }
                else if (Obj_global_value.inv_type_ind == 0)
                {
                    select_payer.Visible = false;
                    input_eMail.Visible = false;
                    input_phone.Visible = false;
                }
                if (Obj_global_value.flying_ind == 0)
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
            div_Room.InnerHtml = SB.ToString();
        }
        public void CreateRoom1(DataSet ds_tour_rooms)
        {
            try
            {
                StringBuilder SB = new StringBuilder();

                DataTable dt = ds_tour_rooms.Tables[2];
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "room_id");
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
                                        SB.Append("<p class='text-center  mb-0 border-bottom border-dark' style='background-color: " + "#A9E5FF" + ";'>" + dr_final[0].ItemArray[2].ToString() + "</p>");
                                    else if (dr_final[0].ItemArray[3].ToString() == "FEMALE")
                                        SB.Append("<p class='text-center  mb-0 border-bottom border-dark' style='background-color: " + "#FFDAF4" + ";'>" + dr_final[0].ItemArray[2].ToString() + "</p>");
                                    else if (dr_final[0].ItemArray[3].ToString() == "MIXED")
                                        SB.Append("<p class='text-center  mb-0 border-bottom border-dark' style='background-color: " + "#F8F9BE" + ";'>" + dr_final[0].ItemArray[2].ToString() + "</p>");

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

                div_Room.InnerHtml = SB.ToString();
            }
            catch (Exception ex)
            {

            }
        }
        public void CreateRoomOld(DataSet ds_tour_rooms)
        {

            StringBuilder SB = new StringBuilder();

            DataTable dt = ds_tour_rooms.Tables[2];
            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "room_id");
            SB.Append("<div class='col-xl-4 col-lg-4 col-md-4 col-sm-6 mb-3 dropBox' >");
            SB.Append("<div class='card h-100 overflow-hidden border-0' >");
            SB.Append("<div class='d-flex justify-content-center align-items-center h-100 bg-secondary droptrue' id='0' ondragover='dragOver(event);' ondrop='drop(event);'>");
            //SB.Append("<a href='javascript: void(0);' class='text-center d-block' onclick=NewRoomCreate();>");
            SB.Append("<img src='http://43.229.227.26:81/schoolTours/Content/images/icon_plus_new.png' alt='' Width='35'/>");
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
                    SB.Append("<div class='col-xl-4 col-lg-4 col-md-4 col-sm-6 mb-3 dropBox ' >");
                    SB.Append("<div class='card h-100 overflow-hidden border-0'>");
                    SB.Append("<div class='h-100 p-2 droptrue' style='background-color: " + dr[0].ItemArray[3].ToString() + ";' id='" + dr[0].ItemArray[0].ToString() + "' ondragover='dragOver(event);' ondrop='drop(event);'>");
                    SB.Append("<p class='text-right text-white mb-0'>" + dr[0].ItemArray[2].ToString() + "</p>");
                    //if (dr[0].ItemArray[2].ToString() == "Single")
                    //    SB.Append("<p class='text-right text-white mb-0'>" + dr[0].ItemArray[2].ToString() + "</p>");
                    //else if (dr[0].ItemArray[2].ToString() == "Double")
                    //    SB.Append("<p class='text-right text-white mb-0'>" + dr[0].ItemArray[2].ToString() + "</p>");
                    //else if (dr[0].ItemArray[2].ToString() == "Triple")
                    //    SB.Append("<p class='text-right text-white mb-0'>" + dr[0].ItemArray[2].ToString() + "</p>");
                    //else if (dr[0].ItemArray[2].ToString() == "Quad")
                    //    SB.Append("<p class='text-right text-white mb-0'>" + dr[0].ItemArray[2].ToString() + "</p>");
                    //else if (dr[0].ItemArray[2].ToString() == "Quint")
                    //    SB.Append("<p class='text-right text-white mb-0'>" + dr[0].ItemArray[2].ToString() + "</p>");
                    //else if (dr[0].ItemArray[2].ToString() == "Hex")
                    //    SB.Append("<p class='text-right text-white mb-0'>" + dr[0].ItemArray[2].ToString() + "</p>");
                    for (int j = 0; j < dr.Length; j++)
                    {
                        SB.Append("<a href='#' ondblclick='javascript:fun_delPaxRoom(" + dr[j].ItemArray[0].ToString() + "," + dr[j].ItemArray[1].ToString() + ");' class='d-block'>" + dr[j].ItemArray[4].ToString() + "</a>");
                    }
                    SB.Append("</div>");
                    SB.Append("</div>");
                    SB.Append("</div>");
                }
            }

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
            select_diet.SelectedValue = "Diet";
            select_payer.SelectedValue = "Payer";

            Obj_LST_ITEMS obj_pax = new Obj_LST_ITEMS();
            obj_pax.mode = "pax";
            obj_pax.id1 = Convert.ToInt32(tour_id.Value);
            DataTable dt_obj_pax = DTL_ITEM_Business.Get_LST_ITEMS(obj_pax).Tables[0];

            StringBuilder SB = new StringBuilder();


            DataView view = dt_obj_pax.DefaultView;

            view.Sort = "sorter asc";
            dt_obj_pax = view.ToTable();
            for (int i = 0; i < dt_obj_pax.Rows.Count; i++)
            {
                SB.Append("<div class='text-white' id=" + dt_obj_pax.Rows[i]["pax_id"].ToString() + " draggable='true' ondragstart='dragStart(event);' onclick='javascript: fun_getpaxdetails(" + dt_obj_pax.Rows[i]["pax_id"].ToString() + "); '>" + dt_obj_pax.Rows[i]["pax_descr"].ToString() + "</div>");
            }

            div_pax.InnerHtml = SB.ToString();

        }

        protected void input_phone_TextChanged(object sender, EventArgs e)
        {
            input_phone.Text = phoneformatting(input_phone.Text);
        }
        public void delPax(object sender, EventArgs e)
        {
            if (pax_id.Value != "0")
            {
                Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                obj.mode = "pax";
                obj.id1 = Convert.ToInt32(tour_id.Value);
                obj.id2 = Convert.ToInt32(pax_id.Value);
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
        public void setPax(object sender, EventArgs e)
        {
            try
            {
                if (Obj_global_value.flying_ind == 1)
                {
                    if (input_birthdate.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please fill valid date.')", true);
                        return;
                    }
                }
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "pax";
                obj.id1 = Convert.ToInt32(tour_id.Value);
                if (Session["emp_id"] != null)
                    obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.id3 = Convert.ToInt32(pax_id.Value == "" ? "0" : pax_id.Value);
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
                }
                else
                {
                    string err_msg = "\"" + dt.Rows[0]["err_msg"].ToString() + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }
            }
            catch (Exception ex)
            {
                string err_msg = "\"" + ex.Message + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }

        public void AddnewPax()
        {
            //Set @pax_id = 0 ● Clear values from input_given_nm, input_last_nm, and (if necessary) input_birthdate. 
            //● Set the following drop-downs back to a blank value: select_role, select_age, select_sex, select_diet, and (if necessary) select_payer. 
            try
            {
                pax_id.Value = "0";
                input_given_nm.Text = "";
                input_last_nm.Text = "";
                select_role.SelectedValue = "Role";
                select_age.SelectedValue = "Age";
                select_sex.SelectedValue = "Sex";
                select_diet.SelectedValue = "Diet";
                select_payer.SelectedValue = "Payer";
                input_birthdate.Text = "";
                input_eMail.Text = "";
                input_phone.Text = "";
            }
            catch (Exception ex) { }
        }


        public void goHome(object sender, EventArgs e)
        {
            Response.Redirect("mytour_dashboard");
        }
        public void prnList(object sender, EventArgs e)
        {
            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

            obj.mode = "tour_rooms";
            obj.id1 = Convert.ToInt32(tour_id.Value);
            DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

            CreatePDF(ds);
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

        public void CreatePDF(DataSet ds)
        {

            DataTable dt_imgage = ds.Tables[0];
            DataTable dt_person = ds.Tables[1];
            DataTable dt_room = ds.Tables[2];

            StringBuilder SB = new StringBuilder();

            string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
            string strUrl = "";
            if (url[2] == "localhost:2108")
            {
                strUrl = "http://" + url[2] + "/Images/";
            }
            else if (url[2].Contains("43.229.227.26"))
            {
                strUrl = "http://" + url[2] + "/schoolTours/Images/";
            }
            strUrl = strUrl + dt_imgage.Rows[0]["img"].ToString();

            #region new 
            SB.Append("<html lang=\"en\">");
            SB.Append("<head>");
            SB.Append("<title>PDF</title>");
            SB.Append("</head>");
            SB.Append("<body>");
            SB.Append("<div class=\"page\">");
            SB.Append("<table style=\"border:1px solid transparent; border-collapse: separate; border-spacing: 20px 0.8rem;\">");
            SB.Append("<thead>");

            SB.Append("<tr>");
            SB.Append("<td colspan=\"4\" style=\"border:1px solid black;padding: 20px;\"><table style=\"width:100%;\"><tr><td style=\"width:10%;color:white\">.</td><td colspan=\"3\"><img src='" + strUrl + " ' height='60' align='bottom' style='display:block;'></img></td></tr></table></td>");
            SB.Append("</tr>");

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

                    int Room_id = Convert.ToInt32(distinctValues.Rows[i]["room_id"].ToString());
                    DataRow[] dr = dt_room.Select("room_id = " + Room_id);

                    DataTable dt_new = dt_room.Clone();

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
                            loopvalue++;
                            DataRow[] dr_final = dt_new.Select("room_id = '" + Room_id + "' AND room_size = '" + room_size + "' AND room_type = '" + distinctroom_type.Rows[k]["room_type"].ToString() + "'");

                            if (dr_final.Length != 0)
                            {
                                if (dr_final[0].ItemArray[3].ToString() == "MALE")
                                {
                                    SB.Append("<td style=\"background:" + "#A9E5FF" + ";padding:1em 1em\">");
                                    SB.Append("<p style=\"text-align:center; margin-bottom:10px;\">" + dr[0].ItemArray[2].ToString() + "</p>");
                                }
                                else if (dr_final[0].ItemArray[3].ToString() == "FEMALE")
                                {
                                    SB.Append("<td style=\"background:" + "#FFDAF4" + ";padding:1em 1em\">");
                                    SB.Append("<p style=\"text-align:center; margin-bottom:10px;\">" + dr[0].ItemArray[2].ToString() + "</p>");
                                }
                                else if (dr_final[0].ItemArray[3].ToString() == "MIXED")
                                {
                                    SB.Append("<td style=\"background:" + "#F8F9BE" + ";padding:1em 1em\">");
                                    SB.Append("<p style=\"text-align:center; margin-bottom:10px;\">" + dr[0].ItemArray[2].ToString() + "</p>");
                                }

                                //SB.Append("<p style=\"text-align:center; margin-bottom:10px;\">" + dr[0].ItemArray[2].ToString() + "</p>");
                                for (int z = 0; z < dr_final.Length; z++)
                                {
                                    SB.Append("<p style=\"padding:0px 10px;\">" + dr_final[z].ItemArray[4].ToString() + "</p>");
                                }
                                if (dr_final.Length != 0)
                                    SB.Append("</td>");
                            }
                        }

                        if (loopvalue == 4)
                        {
                            SB.Append("</tr>");
                            loopvalue = 0;
                        }
                    }
                }
            }
            if (loopvalue > 0)
            {
                SB.Append("</tr>");
            }

            SB.Append("</thead>");
            SB.Append("</table>");
            SB.Append("</div>");
            SB.Append("</body>");
            SB.Append("</html>");
            #endregion new 

            string strSB = SB.ToString();
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
        public void CreatePDFOld(DataSet ds)
        {

            DataTable dt_imgage = ds.Tables[0];
            DataTable dt_person = ds.Tables[1];
            DataTable dt_room = ds.Tables[2];

            StringBuilder SB = new StringBuilder();

            string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
            string strUrl = "";
            if (url[2] == "localhost:2108")
            {
                strUrl = "http://" + url[2] + "/Images/";
            }
            else if (url[2].Contains("43.229.227.26"))
            {
                strUrl = "http://" + url[2] + "/schoolTours/Images/";
            }
            strUrl = strUrl + dt_imgage.Rows[0]["img"].ToString();

            #region new 
            SB.Append("<html lang=\"en\">");
            SB.Append("<head>");
            SB.Append("<title>PDF</title>");
            SB.Append("</head>");
            SB.Append("<body>");
            SB.Append("<div class=\"page\">");
            SB.Append("<table style=\"border:1px solid transparent; border-collapse: separate; border-spacing: 20px 0.8rem;\">");
            SB.Append("<thead>");

            SB.Append("<tr>");
            SB.Append("<td colspan=\"4\" style=\"border:1px solid black;padding: 20px;\"><table style=\"width:100%;\"><tr><td style=\"width:10%;color:white\">.</td><td colspan=\"3\"><img src='" + strUrl + " ' height='60' align='bottom' style='display:block;'></img></td></tr></table></td>");
            SB.Append("</tr>");

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

                    for (int j = 0; j < dr.Length; j++)
                    {
                        SB.Append("<p style=\"padding:0px 10px;\">" + dr[j].ItemArray[2].ToString() + "</p>");
                    }

                    SB.Append("</td>");
                    if (loopvalue == 4)
                    {
                        SB.Append("</tr>");
                        loopvalue = 0;
                    }
                }
            }
            if (loopvalue > 0)
            {
                SB.Append("</tr>");
            }

            SB.Append("</thead>");
            SB.Append("</table>");
            SB.Append("</div>");
            SB.Append("</body>");
            SB.Append("</html>");
            #endregion new 

            string strSB = SB.ToString();
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
    }
}