using SchoolToursData.Object;
using SchoolToursBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Net.Mail;
using DKIM;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using System.Threading;

namespace SchoolTours.Sales
{
    public partial class sales : System.Web.UI.Page
    {
        //private static int entity_id = 0;
        //private static int person_id = 0;
        //private static int hotlist_id = 0;
        //private static int note_id = 0;
        //private static string person_name = "";
        //public static int SaveCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                entity_id.Value = "0";
                person_id.Value = "0";
                hotlist_id.Value = "0";
                note_id.Value = "0";
                person_name.Value = "";
                SaveCount.Value = "0";
                div_entity_panel.Visible = false;
                div_person_panel.Visible = false;
            }
        }
        //[WebMethod]
        //public static void EntityAddTag()
        //{
        //    sales obj = new sales();
        //    obj.tagEntityCreate();
        //}

        //[WebMethod]
        //public static void PersonAddTag()
        //{
        //    sales obj = new sales();
        //    obj.tagPersonCreate();
        //}
        public void tagSearch(object sender, EventArgs e)
        {
            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tag";
                obj.str1 = input_tag_descr.Text;

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];

                div_add_tags.DataSource = dt;
                div_add_tags.DataTextField = "tag_descr";
                div_add_tags.DataValueField = "tag_id";
                div_add_tags.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        public void resetForm(object sender, EventArgs e)
        {
            input_entity_nm.Text = "";
            input_person_nm.Text = "";
            input_state.Text = "";
            div_entity_list.Items.Clear();
            input_entity_nm.Text = "";
            input_entity_nm.Text = "";
            input_entity_nm.Text = "";
            div_add_tags.Items.Clear();
            div_sel_tags.Items.Clear();
            input_tag_descr.Text = "";

            div_entity_panel.Visible = false;
            div_person_panel.Visible = false;
        }

        protected void bttn_search_Click(object sender, EventArgs e)
        {
            runSearch();
        }

        #region Tags
        //===================================================================== Tag
        public void tagSelect(object sender, EventArgs e)
        {
            string tag_descr = Convert.ToString(div_add_tags.SelectedItem);
            string tag_id = Convert.ToString(div_add_tags.SelectedValue);

            int strCount = 0;
            for (int index = 0; index < div_sel_tags.Items.Count; index++)
            {
                ListItem item = div_sel_tags.Items[index];
                if (tag_id == item.Value)
                {
                    strCount++;
                }
            }
            if (strCount == 0)
            {
                ListItem litm = new ListItem();
                litm.Text = tag_descr;
                litm.Value = tag_id.ToString();
                div_sel_tags.Items.Add(litm);
            }

        }
        public void tagRemove(object sender, EventArgs e)
        {
            div_sel_tags.Items.RemoveAt(div_sel_tags.SelectedIndex);
        }
        //===================================================================== Tag entity
        public void entity_tagSearch(object sender, EventArgs e)
        {
            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tag";
                obj.str1 = e_entity_tag_descr.Text.Trim();

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];

                e_entity_tag_assign.DataSource = dt;
                e_entity_tag_assign.DataTextField = "tag_descr";
                e_entity_tag_assign.DataValueField = "tag_id";
                e_entity_tag_assign.DataBind();

                //if (dt.Rows.Count == 0)
                //{
                //    tagEntityCreate();
                //}
            }
            catch (Exception ex)
            {

            }
        }
        public void tagSelect_entity(object sender, EventArgs e)
        {
            string tag_descr = Convert.ToString(e_entity_tag_assign.SelectedItem);
            string tag_id = Convert.ToString(e_entity_tag_assign.SelectedValue);

            int strCount = 0;
            for (int index = 0; index < e_entity_sel_tags.Items.Count; index++)
            {
                ListItem item = e_entity_sel_tags.Items[index];
                if (tag_id == item.Value)
                {
                    strCount++;
                }
            }
            if (strCount == 0)
            {
                ListItem litm = new ListItem();
                litm.Text = tag_descr;
                litm.Value = tag_id.ToString();
                e_entity_sel_tags.Items.Add(litm);
            }

        }
        public void tagRemove_entity(object sender, EventArgs e)
        {
            e_entity_sel_tags.Items.RemoveAt(e_entity_sel_tags.SelectedIndex);
        }

        public void tagECreate(object sender, EventArgs e)
        {
            tagEntityCreate();

        }
        public void tagEntityCreate()
        {
            if (e_entity_tag_descr.Text.Trim() != "")
            {
                ListItem litm = new ListItem();
                litm.Text = e_entity_tag_descr.Text.Trim();
                litm.Value = "0";
                e_entity_sel_tags.Items.Add(litm);
                e_entity_tag_descr.Text = "";
            }
        }
        //===================================================================== Tag person

        public void person_tagSearch(object sender, EventArgs e)
        {
            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tag";
                obj.str1 = person_tag_descr.Text;

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];

                person_tag_assign.DataSource = dt;
                person_tag_assign.DataTextField = "tag_descr";
                person_tag_assign.DataValueField = "tag_id";
                person_tag_assign.DataBind();
                //if (dt.Rows.Count == 0)
                //{
                //    tagPersonCreate();
                //}
            }
            catch (Exception ex)
            {

            }
        }
        public void tagSelect_person(object sender, EventArgs e)
        {
            string tag_descr = Convert.ToString(person_tag_assign.SelectedItem);
            string tag_id = Convert.ToString(person_tag_assign.SelectedValue);

            int strCount = 0;
            for (int index = 0; index < person_sel_tags.Items.Count; index++)
            {
                ListItem item = person_sel_tags.Items[index];
                if (tag_id == item.Value)
                {
                    strCount++;
                }
            }
            if (strCount == 0)
            {
                ListItem litm = new ListItem();
                litm.Text = tag_descr;
                litm.Value = tag_id.ToString();
                person_sel_tags.Items.Add(litm);
            }

        }
        public void tagRemove_person(object sender, EventArgs e)
        {
            person_sel_tags.Items.RemoveAt(person_sel_tags.SelectedIndex);
        }

        public void tagPCreate(object sender, EventArgs e)
        {
            tagPersonCreate();

        }
        public void tagPersonCreate()
        {
            int a = Convert.ToInt32(person_id.Value);
            if (person_tag_descr.Text.Trim() != "")
            {
                ListItem litm = new ListItem();
                litm.Text = person_tag_descr.Text.Trim();
                litm.Value = "0";
                person_sel_tags.Items.Add(litm);
                person_tag_descr.Text = "";
            }
        }

        #endregion Tags

        public void runSearch()
        {
            try
            {
                div_entity_panel.Visible = true;
                //entity_id = 0;
                //person_id = 0;
                note_id.Value = "0";
                hotlist_id.Value = "0";

                img_entity_save.Enabled = true;
                img_person_save.Enabled = true;
                img_save_note.Enabled = true;
                img_save_hotlist.Enabled = true;
                img_contract_save.Enabled = true;


                lbl_entity_descr.Text = "";
                lbl_entity_address.Text = "";
                lbl_entity_phone.Text = "";
                lbl_entity_Url.Text = "";
                div_entity_tags.Text = "";

                lbl_person_Name.Text = "";
                lbl_person_address.Text = "";
                lbl_person_office_phone.Text = "";
                lbl_person_eMail.Text = "";
                lbl_person_cell_phone.Text = "";
                div_person_tags.Text = "";

                div_entity_list.Items.Clear();
                div_person_list.Items.Clear();


                entity_list.Visible = true;
                new_entity_record.Visible = false;

                person_list.Visible = true;
                div_contract.Visible = false;
                div_hotlist.Visible = false;
                div_notes.Visible = false;
                new_person_record.Visible = false;
                lbl_entity_info.Text = "";
                lbl_person_info.Text = "";


                string tag_id = "";
                for (int index = 0; index < div_sel_tags.Items.Count; index++)
                {
                    ListItem item = div_sel_tags.Items[index];
                    tag_id += item.Value + ",";
                }
                if (tag_id != "")
                    tag_id = tag_id.Remove(tag_id.Length - 1, 1);

                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "sales";
                obj.str1 = input_entity_nm.Text == "" ? null : input_entity_nm.Text;
                obj.str2 = input_person_nm.Text == "" ? null : input_person_nm.Text;
                obj.str3 = input_state.Text == "" ? null : input_state.Text;
                obj.str4 = tag_id == "" ? null : tag_id;

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    div_entity_list.Items.Clear();
                    /// error show 
                }
                else {
                    if (dt.Columns.Contains("rc"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + dt.Rows[0]["err_msg"].ToString() + "')", true);
                        return;
                    }
                    else {
                        DataView dv_Entity_List = new DataView(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            ListItem li = new ListItem(item["entity_descr"].ToString(), item["entity_id"].ToString());
                            li.Attributes.Add("title", item["entity_descr"].ToString());
                            li.Attributes.Add("data-toggle", "tooltip");
                            li.Attributes.Add("data-container", "#tooltip_container");
                            //data-toggle="tooltip" data-container="#tooltip_container"
                            div_entity_list.Items.Add(li);
                        }
                        //div_entity_list.DataSource = dt;
                        //div_entity_list.DataTextField = "entity_descr";
                        //div_entity_list.DataValueField = "entity_id";
                        //div_entity_list.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void div_entity_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_person_Name.Text = "";
            lbl_person_address.Text = "";
            lbl_person_office_phone.Text = "";
            lbl_person_eMail.Text = "";
            lbl_person_cell_phone.Text = "";
            div_person_tags.Text = "";

            entity_id.Value = Convert.ToInt32(div_entity_list.SelectedValue).ToString();

            dtlEntity(Convert.ToInt32(entity_id.Value));

        }

        public void dtlEntity(int entity_id)
        {
            try
            {
                lbl_entity_info.Text = "";
                div_person_panel.Visible = true;
                div_contract.Visible = false;
                div_hotlist.Visible = false;
                div_notes.Visible = false;
                new_person_record.Visible = false;
                person_list.Visible = true;

                lbl_person_Name.Text = "";
                lbl_person_address.Text = "";
                lbl_person_office_phone.Text = "";
                lbl_person_eMail.Text = "";
                lbl_person_cell_phone.Text = "";
                div_person_tags.Text = "";
                lbl_person_info.Text = "";

                person_id.Value = "0";
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "entity";
                obj.id1 = Convert.ToString(entity_id);
                obj.str1 = "";
                obj.str2 = "";

                DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

                DataTable dt_entity = ds.Tables[0];
                DataTable dt_tags = ds.Tables[1];
                DataTable dt_persons = ds.Tables[2];

                lbl_entity_descr.Text = dt_entity.Rows[0]["entity_descr"].ToString();
                lbl_entity_address.Text = dt_entity.Rows[0]["address"].ToString() + "," + dt_entity.Rows[0]["city"].ToString() + "," + dt_entity.Rows[0]["state"].ToString() + " " + dt_entity.Rows[0]["zip"].ToString();
                lbl_entity_phone.Text = phoneformatting(dt_entity.Rows[0]["phone"].ToString());
                lbl_entity_Url.Text = dt_entity.Rows[0]["url"].ToString();

                lbl_entity_info.Text = "Modified " + dt_entity.Rows[0]["mod_dt"].ToString() + " by " + dt_entity.Rows[0]["mod_nm"].ToString();

                DataView dv_person_list = new DataView(dt_persons);

                div_person_list.DataSource = dt_persons;
                div_person_list.DataTextField = "column1";
                div_person_list.DataValueField = "person_id";
                div_person_list.DataBind();

                string str_entity_tag = "";
                foreach (DataRow item in dt_tags.Rows)
                {
                    str_entity_tag += item["tag_descr"].ToString() + ", ";
                }
                if (str_entity_tag.Length != 0)
                    str_entity_tag = (str_entity_tag.Remove(str_entity_tag.Length - 1, 1));

                div_entity_tags.Text = str_entity_tag;

                //zipLookup(dt_entity.Rows[0]["zip"].ToString(), "entity");
            }
            catch (Exception ex)
            {

            }
        }
        public void dtlPerson(object sender, EventArgs e)
        {
            fundtlPerson();
        }
        public void fundtlPerson()
        {
            try
            {
                lbl_person_info.Text = "";
                string person_list = "";
                try
                {
                    person_list = Convert.ToString(div_person_list.SelectedValue);
                    person_id.Value = Convert.ToInt32(div_person_list.SelectedValue).ToString();
                    person_name.Value = Convert.ToString(div_person_list.SelectedItem);
                }
                catch (Exception ex)
                {
                    person_list = Convert.ToString(person_id);
                }
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "person";
                obj.id1 = person_list;
                obj.str1 = "";
                obj.str2 = "";

                DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);


                DataTable dt_persons = ds.Tables[0];
                DataTable dt_tags = ds.Tables[1];

                lbl_person_Name.Text = dt_persons.Rows[0]["greeting"].ToString() + " " + dt_persons.Rows[0]["given_nm"].ToString() + " " + dt_persons.Rows[0]["last_nm"].ToString();
                lbl_person_address.Text = dt_persons.Rows[0]["address"].ToString() + "," + dt_persons.Rows[0]["city"].ToString() + "," + dt_persons.Rows[0]["state"].ToString() + " " + dt_persons.Rows[0]["zip"].ToString();


                lbl_person_office_phone.Text = phoneformatting(dt_persons.Rows[0]["office_phone"].ToString());
                lbl_person_eMail.Text = dt_persons.Rows[0]["eMail"].ToString();
                lbl_person_cell_phone.Text = phoneformatting(dt_persons.Rows[0]["cell_phone"].ToString());

                lbl_person_info.Text = "Modified " + dt_persons.Rows[0]["mod_dt"].ToString() + " by " + dt_persons.Rows[0]["mod_nm"].ToString();
                string str_person_tag = "";
                foreach (DataRow item in dt_tags.Rows)
                {
                    str_person_tag += item["tag_descr"].ToString() + ", ";
                }
                if (str_person_tag.Length != 0)
                    str_person_tag = str_person_tag.Remove(str_person_tag.Length - 1, 1);

                div_person_tags.Text = str_person_tag;
            }
            catch (Exception ex) { }
        }

        //============================================================== show div add entity 
        public void addEntity(object sender, EventArgs e)
        {
            shwItem("entity", 0);
        }
        public void editEntity(object sender, EventArgs e)
        {
            if (div_entity_list.SelectedValue != "")
            {
                shwItem("entity", Convert.ToInt32(div_entity_list.SelectedValue));
            }
            else {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('must ')", true);
            }
        }
        public void shwItem(string mode, int id)
        {
            try
            {
                if (mode == "entity")
                {
                    #region entity
                    div_person_mod_info.Text = "";
                    new_entity_record.Visible = true;
                    entity_list.Visible = false;

                    input_entity_descr.Text = "";
                    input_entity_address.Text = "";
                    input_entity_zip.Text = "";
                    input_entity_state.Text = "";
                    input_entity_city.Text = "";
                    input_entity_phone.Text = "";
                    input_entity_url.Text = "";
                    e_entity_tag_descr.Text = "";
                    e_entity_sel_tags.Items.Clear();
                    e_entity_tag_assign.Items.Clear();

                    if (id == 0)
                    {
                        lbl_entity_mode.Text = "New Entity Record";
                        // Add case 
                        entity_id.Value = "0";



                    }
                    else {
                        //edit case
                        //pr_dtl_item(‘entity’, @ID)


                        lbl_entity_mode.Text = "Edit Entity Record";
                        entity_id.Value = id.ToString();

                        Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                        obj.mode = "entity";
                        obj.id1 = Convert.ToString(entity_id.Value);

                        DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

                        DataTable dt_entity = ds.Tables[0];
                        DataTable dt_tags = ds.Tables[1];
                        DataTable dt_persons = ds.Tables[2];



                        input_entity_descr.Text = dt_entity.Rows[0]["entity_descr"].ToString();
                        input_entity_address.Text = dt_entity.Rows[0]["address"].ToString();
                        input_entity_zip.Text = dt_entity.Rows[0]["zip"].ToString();
                        input_entity_state.Text = dt_entity.Rows[0]["state"].ToString();
                        input_entity_city.Text = dt_entity.Rows[0]["city"].ToString();
                        input_entity_phone.Text = phoneformatting(dt_entity.Rows[0]["phone"].ToString());
                        input_entity_url.Text = dt_entity.Rows[0]["url"].ToString();

                        e_entity_sel_tags.DataSource = dt_tags;
                        e_entity_sel_tags.DataTextField = "tag_descr";
                        e_entity_sel_tags.DataValueField = "tag_id";
                        e_entity_sel_tags.DataBind();

                        div_entity_mod_info.Text = "Modified " + dt_entity.Rows[0]["mod_dt"].ToString() + " by " + dt_entity.Rows[0]["mod_nm"].ToString();
                    }
                    #endregion entity

                }
                else if (mode == "person")
                {
                    #region person
                    div_person_mod_info.Text = "";
                    new_person_record.Visible = true;
                    person_list.Visible = false;

                    input_person_greeting.Text = "";
                    input_given_name.Text = "";
                    input_last_nm.Text = "";
                    input_person_address.Text = "";
                    input_person_city.Text = "";
                    input_person_state.Text = "";
                    input_person_zip.Text = "";
                    input_person_landline.Text = "";
                    input_person_cell_phone.Text = "";
                    input_person_eMail.Text = "";

                    person_tag_descr.Text = "";
                    person_sel_tags.Items.Clear();
                    person_tag_assign.Items.Clear();

                    if (id == 0)
                    {
                        // Add case 
                        lbl_person_Mode.Text = "Add New Person";
                        person_id.Value = "0";


                    }
                    else {
                        //edit case

                        lbl_person_Mode.Text = "Edit Person";
                        person_id.Value = id.ToString();

                        Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                        obj.mode = "person";
                        obj.id1 = Convert.ToString(person_id.Value);

                        DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

                        DataTable dt_person = ds.Tables[0];
                        DataTable dt_tags = ds.Tables[1];


                        input_person_greeting.Text = dt_person.Rows[0]["greeting"].ToString();
                        input_given_name.Text = dt_person.Rows[0]["given_nm"].ToString();
                        input_last_nm.Text = dt_person.Rows[0]["last_nm"].ToString();
                        input_person_address.Text = dt_person.Rows[0]["address"].ToString();
                        input_person_city.Text = dt_person.Rows[0]["city"].ToString();
                        input_person_state.Text = dt_person.Rows[0]["state"].ToString();
                        input_person_zip.Text = dt_person.Rows[0]["zip"].ToString();
                        input_person_landline.Text = phoneformatting(dt_person.Rows[0]["office_phone"].ToString());
                        input_person_cell_phone.Text = phoneformatting(dt_person.Rows[0]["cell_phone"].ToString());
                        input_person_eMail.Text = dt_person.Rows[0]["eMail"].ToString();

                        person_sel_tags.DataSource = dt_tags;
                        person_sel_tags.DataTextField = "tag_descr";
                        person_sel_tags.DataValueField = "tag_id";
                        person_sel_tags.DataBind();
                        div_person_mod_info.Text = "Modified " + dt_person.Rows[0]["mod_dt"].ToString() + " by " + dt_person.Rows[0]["mod_nm"].ToString();

                    }
                    #endregion person
                }
                else if (mode == "note")
                {
                    #region Note

                    div_notes.Visible = true;
                    person_list.Visible = false;
                    lbl_note_for.Text = "Note for " + person_name.Value;
                    note_id.Value = "0";
                    input_note_descr.Text = "";
                    select_note_type.Items.Clear();
                    div_note_mod_info.Text = "";

                    //pr_lst_items(‘note’, @person_id)
                    Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

                    obj.mode = "note";
                    obj.id1 = Convert.ToInt32(person_id.Value);

                    DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                    DataTable dt_note = ds.Tables[0];
                    DataTable dt_ref = ds.Tables[1];

                    div_notes_list.DataSource = dt_note;
                    div_notes_list.DataTextField = "Column1";
                    div_notes_list.DataValueField = "note_id";
                    div_notes_list.DataBind();


                    select_note_type.DataSource = dt_ref;
                    select_note_type.DataTextField = "ref_descr";
                    select_note_type.DataValueField = "ref_id";
                    select_note_type.DataBind();
                    select_note_type.Items.Insert(0, "Select");

                    #endregion Note

                }
                else if (mode == "contract")
                {
                    #region contract
                    div_contract.Visible = true;
                    person_list.Visible = false;
                    lbl_contract_for.Text = "Create New Contract";

                    input_cost_amt.Text = "";
                    input_cost_descr.Text = "";

                    //pr_lst_items(‘curr_evts’)

                    Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                    obj.mode = "curr_evts";
                    DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                    DataTable dt = ds.Tables[0];
                    select_evt.DataSource = dt;
                    select_evt.DataTextField = "evt_nm";
                    select_evt.DataValueField = "evt_id";
                    select_evt.DataBind();
                    //select_evt.Items.Insert(0, "Select");
                    #endregion contract

                }
                else if (mode == "hotlist")
                {
                    #region hotlist
                    person_id.Value = id.ToString();
                    div_hotlist.Visible = true;
                    person_list.Visible = false;
                    lbl_hotlist_for.Text = "Hotlist for " + person_name.Value;
                    hotlist_id.Value = "0";
                    input_hotlist_date.Text = "";
                    input_hotlist_time.Text = "";
                    input_hotlist_notes_txt.Text = "";
                    div_hotlist_mod_info.Text = "";

                    //select_hotlist_method.Items.Clear();

                    Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                    obj.mode = "hotlist";
                    obj.id1 = Convert.ToInt32(person_id.Value);

                    DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                    DataTable dt_hotlist = ds.Tables[0];
                    DataTable dt_ref = ds.Tables[1];

                    div_hotlist_list.DataSource = dt_hotlist;
                    div_hotlist_list.DataTextField = "Column1";
                    div_hotlist_list.DataValueField = "hotlist_id";
                    div_hotlist_list.DataBind();


                    select_hotlist_method.DataSource = dt_ref;
                    select_hotlist_method.DataTextField = "ref_descr";
                    select_hotlist_method.DataValueField = "ref_id";
                    select_hotlist_method.DataBind();
                    select_hotlist_method.Items.Insert(0, "Select");
                    #endregion hotlist
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region Hide Div
        //=================================================================== Hide Div
        public void div_Hide_Entity(object sender, EventArgs e)
        {
            hideItem("div_entity_edit_panel");
        }
        public void div_Hide_person(object sender, EventArgs e)
        {
            hideItem("div_person_edit_panel");
        }
        public void div_Hide_contract(object sender, EventArgs e)
        {
            hideItem("div_contract");
        }
        public void div_Hide_hotlist(object sender, EventArgs e)
        {
            hideItem("div_hotlist");
        }
        public void div_Hide_note(object sender, EventArgs e)
        {
            hideItem("div_notes");
        }
        public void hideItem(string div_name)
        {
            if (div_name == "div_entity_edit_panel")
            {
                new_entity_record.Visible = false;
                entity_list.Visible = true;
                runSearch();
                try
                {
                    input_entity_descr.Text = "";
                    input_entity_address.Text = "";
                    input_entity_city.Text = "";
                    input_entity_state.Text = "";
                    input_entity_zip.Text = "";
                    input_entity_phone.Text = "";
                    input_entity_url.Text = "";
                    //e_entity_sel_tags.Text = "";
                    e_entity_tag_descr.Text = "";
                }
                catch (Exception ex)
                {

                }
            }
            else if (div_name == "div_person_edit_panel")
            {
                new_person_record.Visible = false;
                person_list.Visible = true;
                dtlEntity(Convert.ToInt32(entity_id.Value));
                try
                {
                    input_person_greeting.Text = "";
                    input_given_name.Text = "";
                    input_last_nm.Text = "";
                    input_person_address.Text = "";
                    input_person_city.Text = "";
                    input_person_state.Text = "";
                    input_person_zip.Text = "";
                    input_person_landline.Text = "";
                    input_person_cell_phone.Text = "";
                    input_person_eMail.Text = "";
                    //person_sel_tags.emp = "";
                    person_tag_descr.Text = "";
                }
                catch (Exception ex)
                {

                }
            }
            else if (div_name == "div_contract")
            {
                div_contract.Visible = false;
                person_list.Visible = true;
                try
                {
                    input_cost_descr.Text = "";
                    input_cost_amt.Text = "";
                }
                catch (Exception ex)
                {

                }

            }
            else if (div_name == "div_hotlist")
            {
                div_hotlist.Visible = false;
                person_list.Visible = true;
                try
                {
                    input_hotlist_date.Text = "";
                    input_hotlist_time.Text = "";
                    select_hotlist_method.SelectedValue = "Select";
                    input_hotlist_notes_txt.Text = "";
                }
                catch (Exception ex)
                {

                }
            }
            else if (div_name == "div_notes")
            {
                div_notes.Visible = false;
                person_list.Visible = true;
                try
                {
                    select_note_type.SelectedValue = "Select";
                    input_note_descr.Text = "";
                }
                catch (Exception ex)
                {

                }
            }
        }


        #endregion Hide Div


        #region Set values 
        public void setEntity(int entity_id)
        {
            try
            {

                string mode_type = "add";
                if (entity_id != 0)
                    mode_type = "edit";

                //execute pr_set_item(‘entity’, 0, @emp_id, null, null, @entity_descr,@address, @city, @state, @postal_code, @url)
                //execute pr_set_item('tag', @tag_id,@entity_id, @emp_id, @tag_descr, person')
                //execute pr_set_item('tag', 0, @entity_id,@emp_id, @tag_descr, 'entity')
                // hideItem(‘div_entity_edit_panel’)

                if (input_entity_zip.Text != "")
                {
                    zipLookup(input_entity_zip.Text, "entity");
                }

                #region entity
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "entity";
                obj.id1 = entity_id;
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.str1 = input_entity_descr.Text;
                obj.str2 = input_entity_address.Text;
                obj.str3 = input_entity_city.Text;
                obj.str4 = input_entity_state.Text;
                obj.str5 = input_entity_zip.Text;
                obj.str6 = input_entity_phone.Text;
                obj.str7 = input_entity_url.Text;
                //
                obj.str6 = obj.str6.Replace(@".", string.Empty);
                obj.str6 = obj.str6.Replace(@"-", string.Empty);
                //DataSet ds = DTL_ITEM_Business.Put_SET_ITEM_DS(obj);
                DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];

                #endregion entity
                if (Convert.ToInt32(dt_Result.Rows[0][0].ToString()) > 0)
                {
                    for (int index = 0; index < e_entity_sel_tags.Items.Count; index++)
                    {
                        ListItem item = e_entity_sel_tags.Items[index];
                        Obj_SET_ITEM objTag = new Obj_SET_ITEM();
                        objTag.mode = "tag";
                        //objTag.id1 = Convert.ToInt32(item.Value);
                        objTag.id2 = mode_type == "add" ? Convert.ToInt32(dt_Result.Rows[0][0].ToString()) : entity_id;
                        objTag.id3 = Convert.ToInt32(Session["emp_id"].ToString());
                        objTag.str1 = Convert.ToString(item.Text);
                        objTag.str2 = "entity";
                        int response_tag = DTL_ITEM_Business.Put_SET_ITEM(objTag);
                    }
                }
                else
                {
                    string err_msg = "\"" + dt_Result.Rows[0]["err_msg"].ToString() + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                    return;
                }
                hideItem("div_entity_edit_panel");
                if (entity_id == 0)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Entity added successfully!!')", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Entity updated successfully!!')", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + ex.Message + ")", true);
            }

        }
        public void setPerson(int person_id)
        {
            try
            {
                string mode_type = "add";
                if (person_id != 0)
                    mode_type = "edit";

                //execute pr_set_item (‘person’, 0, @emp_id, null, null, @title, @given_name,@last_nm, @address, @city, @state, @postal_code, @landline, @cell_phone, @eMail)
                //execute pr_set_item('tag', @tag_id,@person_id, @emp_id, @tag_descr, 'person')
                //pr_set_item('tag', 0, @person_id,@emp_id, @tag_descr, ‘person')
                // hideItem(‘div_person_edit_panel’).
                if (input_person_zip.Text != "")
                {
                    zipLookup(input_person_zip.Text, "person");
                }

                #region person
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "person";
                obj.id1 = person_id;
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.id3 = Convert.ToInt32(div_entity_list.SelectedValue);
                obj.str1 = input_person_greeting.Text;
                obj.str2 = input_given_name.Text;
                obj.str3 = input_last_nm.Text;
                obj.str4 = input_person_address.Text;
                obj.str5 = input_person_city.Text;
                obj.str6 = input_person_state.Text;
                obj.str7 = input_person_zip.Text;
                obj.str8 = input_person_landline.Text;
                obj.str9 = input_person_cell_phone.Text;
                obj.str10 = input_person_eMail.Text;

                //
                obj.str8 = obj.str8.Replace(@".", string.Empty);
                obj.str9 = obj.str9.Replace(@".", string.Empty);

                int response = DTL_ITEM_Business.Put_SET_ITEM(obj);
                if (response > 0)
                {
                    #endregion person

                    #region Tag
                    for (int index = 0; index < person_sel_tags.Items.Count; index++)
                    {
                        ListItem item = person_sel_tags.Items[index];
                        Obj_SET_ITEM objTag = new Obj_SET_ITEM();
                        objTag.mode = "tag";
                        //objTag.id1 = Convert.ToInt32(item.Value);
                        objTag.id2 = mode_type == "add" ? response : person_id;
                        objTag.id3 = Convert.ToInt32(Session["emp_id"].ToString());
                        objTag.str1 = Convert.ToString(item.Text);
                        objTag.str2 = "person";
                        int response_tag = DTL_ITEM_Business.Put_SET_ITEM(objTag);
                    }
                    #endregion Tag

                }
                dtlEntity(Convert.ToInt32(div_entity_list.SelectedValue));
                hideItem("div_person_edit_panel");
                fundtlPerson();
            }
            catch (Exception ex)
            {

            }

        }
        public void setHotlist(int hotlist_id)
        {
            //execute pr_set_item (‘hotlist’, @hotlist_id, @emp_id, @method_id,@person_id, @date, @time, @notes_txt, @entity_id)
            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "hotlist";
            obj.id1 = hotlist_id;
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.id3 = Convert.ToInt32(select_hotlist_method.SelectedValue.ToString());
            obj.id4 = Convert.ToInt32(person_id.Value);

            obj.str1 = (input_hotlist_date.Text);
            obj.str2 = (input_hotlist_time.Text);
            obj.str3 = (input_hotlist_notes_txt.Text);
            obj.str4 = (entity_id.Value.ToString());


            DataTable dt_Result = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];

            if (dt_Result.Rows.Count > 0)
            {

                if (!dt_Result.Columns.Contains("rc"))
                {

                    shwItem("hotlist", Convert.ToInt32(person_id.Value));
                }
                else
                {
                    string err_msg = "\"" + dt_Result.Rows[0]["err_msg"].ToString() + "\"";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                }
            }
        }
        public void setNote(int note_id)
        {
            // execute pr_set_item(‘note’, @note_id, @person_id, @emp_id, @note_type_id, @note_text)
            try
            {
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "note";
                obj.id1 = note_id;
                obj.id2 = Convert.ToInt32(person_id.Value);
                obj.id3 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.id4 = Convert.ToInt32(select_note_type.SelectedValue.ToString());

                obj.str1 = (input_note_descr.Text);
                note_id = DTL_ITEM_Business.Put_SET_ITEM(obj);

                shwItem("note", Convert.ToInt32(person_id.Value));
            }
            catch (Exception ex)
            {

            }
        }

        #endregion Set values 

        public void addPerson(object sender, EventArgs e)
        {
            if (div_entity_list.SelectedValue != "")
            {
                shwItem("person", 0);
            }
        }
        public void editPerson(object sender, EventArgs e)
        {
            if (Convert.ToInt32(person_id.Value) != 0)
            {
                shwItem("person", Convert.ToInt32(person_id.Value));
            }
        }
        public void addUpdateEntity(object sender, EventArgs e)
        {
            //Thread.Sleep(3000);
            //if (SaveCount == 0)
            //{
            //    SaveCount++;
            setEntity(Convert.ToInt32(entity_id.Value));
            //    SaveCount = 0;
            //}
        }
        public void addUpdateperson(object sender, EventArgs e)
        {
            //Thread.Sleep(3000);
            //if (SaveCount == 0)
            //{
            //    SaveCount++;
            setPerson(Convert.ToInt32(person_id.Value));
            //    SaveCount = 0;
            //}
        }

        //===================================================================== Show note, contract ,hotlist
        public void showNote(object sender, EventArgs e)
        {
            if (div_person_list.SelectedValue != "")
            {
                shwItem("note", Convert.ToInt32(div_person_list.SelectedValue));
            }
        }
        public void showcontract(object sender, EventArgs e)
        {
            if (Convert.ToInt32(entity_id.Value) == 0 || Convert.ToInt32(person_id.Value) == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('user must select an entity and a person first')", true);
            }
            else {
                shwItem("contract", 0);
            }
        }
        public void showhotlist(object sender, EventArgs e)
        {
            if (div_person_list.SelectedValue != "")
            {
                shwItem("hotlist", Convert.ToInt32(div_person_list.SelectedValue));
            }
        }


        #region Delete 

        //=========================================================================== Delete 

        public void delEntity(object sender, EventArgs e)
        {
            //pr_del_item(‘entity’, @entity_id, @emp_id)

            if (Convert.ToInt32(entity_id.Value) != 0)
            {
                Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                obj.mode = "entity";
                obj.id1 = Convert.ToInt32(div_entity_list.SelectedValue);

                int Result = DTL_ITEM_Business.del_DEL_ITEM(obj);
                if (Result == 1)
                {
                    lbl_entity_descr.Text = "";
                    lbl_entity_address.Text = "";
                    lbl_entity_phone.Text = "";
                    lbl_entity_Url.Text = "";
                    runSearch();
                }
            }
        }
        public void delPerson(object sender, EventArgs e)
        {
            //pr_del_item(‘entity’, @entity_id, @emp_id)

            if (Convert.ToInt32(person_id.Value) != 0)
            {
                Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                obj.mode = "person";
                obj.id1 = Convert.ToInt32(person_id);

                int Result = DTL_ITEM_Business.del_DEL_ITEM(obj);
                if (Result == 1)
                {
                    lbl_person_Name.Text = "";
                    lbl_person_address.Text = "";
                    lbl_person_office_phone.Text = "";
                    lbl_person_eMail.Text = "";
                    lbl_person_cell_phone.Text = "";
                    //runSearch();
                }
            }
            dtlEntity(Convert.ToInt32(entity_id.Value));
        }
        public void delHotlist(object sender, EventArgs e)
        {
            try
            {
                //pr_del_item(‘hotlist’, @hotlist_id, @emp_id)
                if (div_hotlist_list.SelectedValue != "")
                {
                    Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                    obj.mode = "hotlist";
                    obj.id1 = Convert.ToInt32(div_hotlist_list.SelectedValue);
                    obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());


                    int Result = DTL_ITEM_Business.del_DEL_ITEM(obj);
                    if (Result == 1)
                    {
                        input_hotlist_date.Text = "";
                        input_hotlist_time.Text = "";
                        select_hotlist_method.SelectedValue = "Select";
                        input_hotlist_notes_txt.Text = "";
                        shwItem("hotlist", Convert.ToInt32(person_id.Value));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void delNote(object sender, EventArgs e)
        {
            try
            {
                if (div_notes_list.SelectedValue != "")
                {
                    Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
                    obj.mode = "note";
                    obj.id1 = Convert.ToInt32(div_notes_list.SelectedValue);
                    obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());


                    int Result = DTL_ITEM_Business.del_DEL_ITEM(obj);
                    if (Result == 1)
                    {
                        try
                        {
                            select_note_type.SelectedValue = "0";
                        }
                        catch (Exception ex)
                        {
                            select_note_type.SelectedValue = "Select";
                        }
                        input_note_descr.Text = "0";
                        div_note_mod_info.Text = "";
                        shwItem("note", Convert.ToInt32(person_id.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                shwItem("note", Convert.ToInt32(person_id.Value));
            }
        }

        #endregion Delete 

        #region Phone format
        //============================================================================== phone format
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
                return "";
            }
        }
        protected void input_entity_phone_TextChanged(object sender, EventArgs e)
        {
            int chars = input_entity_phone.Text.Length;

            if (chars >= 10)
            {
                string entity_phone = input_entity_phone.Text.Substring(0, 10);
                var isNumeric = !string.IsNullOrEmpty(entity_phone) && entity_phone.All(Char.IsDigit);
                if (isNumeric == true)
                {
                    input_entity_phone.Text = phoneformatting(entity_phone);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Phone number field only allows the number')", true);
                    input_entity_phone.Text = "";
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Phone number should be 10 digits')", true);
                input_entity_phone.Text = "";
                return;
            }
        }

        protected void input_person_landline_TextChanged(object sender, EventArgs e)
        {
            //input_person_landline.Text = phoneformatting(input_person_landline.Text);
            int chars = input_person_landline.Text.Length;
            if (chars >= 10)
            {
                string entity_phone = input_person_landline.Text.Substring(0, 10);
                var isNumeric = !string.IsNullOrEmpty(entity_phone) && entity_phone.All(Char.IsDigit);
                if (isNumeric == true)
                {
                    input_person_landline.Text = phoneformatting(entity_phone);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Phone number field only allows the number')", true);
                    input_person_landline.Text = "";
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Phone number should be 10 digits')", true);
                input_person_landline.Text = "";
                return;
            }
        }

        protected void input_person_cell_phone_TextChanged(object sender, EventArgs e)
        {
            //input_person_cell_phone.Text = phoneformatting(input_person_cell_phone.Text);
            int chars = input_person_cell_phone.Text.Length;

            if (chars >= 10)
            {
                string entity_phone = input_person_cell_phone.Text.Substring(0, 10);
                var isNumeric = !string.IsNullOrEmpty(entity_phone) && entity_phone.All(Char.IsDigit);
                if (isNumeric == true)
                {
                    input_person_cell_phone.Text = phoneformatting(entity_phone);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Phone number field only allows the number')", true);
                    input_person_cell_phone.Text = "";
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Phone number should be 10 digits')", true);
                input_person_cell_phone.Text = "";
                return;
            }
        }

        #endregion Phone format

        //===================================================================================== hotlist

        public void dtlHotlist(object sender, EventArgs e)
        {
            try
            {
                if (div_hotlist_list.SelectedValue != "")
                {
                    hotlist_id.Value = Convert.ToInt32(div_hotlist_list.SelectedValue).ToString();
                    //execute pr_dtl_item(‘hotlist’, @hotlist_id)
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "hotlist";
                    obj.id1 = (div_hotlist_list.SelectedValue);
                    DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        input_hotlist_time.Text = ds.Tables[0].Rows[0]["time"].ToString();
                        input_hotlist_notes_txt.Text = ds.Tables[0].Rows[0]["notes_txt"].ToString();
                        select_hotlist_method.SelectedValue = ds.Tables[0].Rows[0]["method_id"].ToString();
                        div_hotlist_mod_info.Text = "Modified " + ds.Tables[0].Rows[0]["mod_tm"].ToString() + " by " + ds.Tables[0].Rows[0]["mod_nm"].ToString();
                        DateTime date = DateTime.ParseExact(ds.Tables[0].Rows[0]["date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dt = DateTime.Parse(ds.Tables[0].Rows[0]["date"].ToString());
                        input_hotlist_date.Text = dt.ToString("yyyy-MM-dd");

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void newHotlist(object sender, EventArgs e)
        {

            try
            {
                hotlist_id.Value = "0";
                input_hotlist_date.Text = "";
                input_hotlist_time.Text = "";
                input_hotlist_notes_txt.Text = "";
                div_hotlist_mod_info.Text = "";
                select_hotlist_method.SelectedValue = "0";
            }
            catch (Exception ex)
            {
            }


        }
        protected void img_save_hotlist_Click(object sender, ImageClickEventArgs e)
        {
            //Thread.Sleep(3000);
            //if (SaveCount == 0)
            //{
            //    SaveCount++;
            setHotlist(Convert.ToInt32(hotlist_id.Value));
            //    SaveCount = 0;
            //}
        }

        //===================================================================================== note
        public void dtlNote(object sender, EventArgs e)
        {
            try
            {
                if (div_notes_list.SelectedValue != "")
                {
                    hotlist_id.Value = Convert.ToInt32(div_notes_list.SelectedValue).ToString();
                    //execute pr_dtl_item(‘hotlist’, @hotlist_id)
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "note";
                    obj.id1 = (div_notes_list.SelectedValue);
                    DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        input_note_descr.Text = ds.Tables[0].Rows[0]["note_txt"].ToString();
                        select_note_type.SelectedValue = ds.Tables[0].Rows[0]["note_type_id"].ToString();
                        div_note_mod_info.Text = "Modified " + ds.Tables[0].Rows[0]["mod_tm"].ToString() + " by " + ds.Tables[0].Rows[0]["mod_nm"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void newNote(object sender, EventArgs e)
        {

            try
            {
                note_id.Value = "0";
                input_note_descr.Text = "";
                div_note_mod_info.Text = "";
                select_note_type.SelectedValue = "0";
            }
            catch (Exception ex)
            {

            }


        }
        protected void img_save_note_Click(object sender, ImageClickEventArgs e)
        {
            //Thread.Sleep(3000);
            //if (SaveCount == 0)
            //{
            //    SaveCount++;
            setNote(Convert.ToInt32(note_id.Value));
            //    SaveCount = 0;
            //}
        }

        //========================================================================== Save contract setTour
        public void setTour(object sender, EventArgs e)
        {
            //Thread.Sleep(3000);
            //if (SaveCount == 0)
            //{
            //    SaveCount++;
            //pr_set_item(‘tour_new’, @emp_id, @entity_id,@person_id, @evt_id, @cost_descr,@cost_amt) -- old

            // Execute pr_set_item(‘tour_new’, @emp_id, @entity_id, @person_id, @evt_id, @cost_descr,
            //@cost_amt, ‘MYTOURTRAVEL’). This stored procedure returns a single row recordset with the
            //following columns: 1.Tour_id.
            img_contract_save.Enabled = false;

            #region tour_new
            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "tour_new";
            obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.id2 = Convert.ToInt32(entity_id.Value);
            obj.id3 = Convert.ToInt32(person_id.Value);
            obj.id4 = Convert.ToInt32(select_evt.SelectedValue);
            obj.str1 = Convert.ToString(input_cost_descr.Text);
            obj.str2 = Convert.ToString(input_cost_amt.Text);
            obj.str3 = "MYTOURTRAVEL";

            int tour_new_id = DTL_ITEM_Business.Put_SET_ITEM(obj);
            if (tour_new_id > 0)
            {
                //2.Execute pr_mailer(‘mytour_inv’, @tour_id).This stored procedure returns a single row recordset
                //with the following columns: 1.recipient_eMail, 2.sender_eMail, 3.subject_txt, 4.body_txt.Use
                //these fields to send an HTML eMail message.
                //3.Redirect the user to tour_details.aspx and use the load the contents of the page.
                //Send mail
                //SendMail(tour_new_id); /// comment by sanjay ----
                //Redirect to tour_details.html 
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Redirect to tour_details.html')", true);

                Response.Redirect("../Operations/tour_details");
            }
            else {

            }
            //    SaveCount = 0;
            //}
            #endregion tour_new

            div_contract.Visible = false;
            person_list.Visible = true;
            img_contract_save.Enabled = true;

        }
        public void SendMail(int tour_new_id)
        {
            try
            {
                Obj_PR_MAILER obj = new Obj_PR_MAILER();
                obj.mode = "mytour_inv";
                obj.id1 = tour_new_id;
                DataTable dt = DTL_ITEM_Business.Get_PR_MAILER(obj).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Models.Utility obj_Utility = new Models.Utility();
                    obj_Utility.SendInvoice(dt.Rows[i]["subject_txt"].ToString(), dt.Rows[i]["sender_eMail"].ToString(), dt.Rows[i]["recipient_eMail"].ToString(), dt.Rows[i]["body_txt"].ToString(), null);
                }

                foreach (DataRow dr in dt.Rows)
                {

                    if (1 == 0)
                    {
                        string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["smtpAddreessEmail"];
                        int PORT = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPORT"]);
                        int portNumber = PORT;
                        bool enableSSL = true;
                        string emailFrom = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                        string password = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                        string sender_eMail = dr.ItemArray[1].ToString();
                        string emailTo = dr.ItemArray[0].ToString();
                        string subject = dr.ItemArray[2].ToString();
                        string body = dr.ItemArray[3].ToString();
                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress(sender_eMail, null);
                        mail.ReplyTo = new MailAddress(sender_eMail);
                        mail.To.Add(emailTo);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        var privateKey = PrivateKeySigner.Create(@"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEAzAs4jLjd1kca5n4htaAelt2PWdapPfc5YsczvvrU58GePjDD
IaYP9P/+aEbOxg8GLwdbYSgJB7M1ypd5yexk54wgo1wgU6AMLgxu4a2cB6aEcyAd
NH0Koq9irhYuBa0/qjcT9Jm0MMLbIhdxutYzw5MP5KGvx8ONNvLd5N7cCw7zjVrp
Ak/01TSPXA+oHWlEbe8RuKds12/YsHp6zlPp1lhqD6LseyWIatahVDBuQVXhkVKq
9ydVSeHyCRvaqb44PlW2D6aobGY1k20HB3rCPNnr93FLJ5sOr5z/eMVeq/aFXCDO
IbywFQhhu/ha7Vpv0p0jOHJITSRQzzXcT+CQsQIDAQABAoIBAH2jgySTSHWCvvui
Ott9RpiawIQO+5MeQYWjJye3h5VU0T12BREZEcZIQryurO+jnKkknI3MexL0tHCU
qPc+yjsRO5+bQIR9jkJkgXoQznyfefrxkUoanIvj9p0/JwNz1DnZRD5ezmcf9JKf
YPYsox8P1L9xF62nqbJmBV/CIjfj2I6TJ1XfUcsROwKnu/4wfZue4aUvprIDwrp5
Wb8s/vl2uO431r3eyJ9ubLLRG/ajxVBO4Fje/UGSuOpyuj768QEfxnd21nPnHgVR
UZTM0g6jKfZLEpKcnnzLqsCx0kzCMqVEg4iMDSPmWUk2I1zGH2ffnhBNWEW3tJJp
pn8blskCgYEA98jOvHt2I72vefqH/Dk/nklq/bUXujjgX8r2CjHUorAirOWdKhsU
6uKRPiexBBdUZmWP6OUFuxC3fhdIoGTpN/yobCaj82DuEyZ/SfNEHeaCeIyuvfv0
AGNgWaHUtbqz4UUGlW4+8JKMh9AL0LEQKu0wx+/kVRQhhdT+kBKrmZcCgYEA0s8k
xQbNYoqXRvcdBvBTv4Rx00FhXOp+VtDceoEgBIKuDdOggVcCRm4GtZKY0sAg5W4X
R4wTu+SW9IO18KGY2UEM3ivD2lENKjNHppU4NbQvVBgxx3W6gwdKd7RtBMJ9gMii
XkaBoewZfRNfrWZqpaPYv4RRP5iTLV6wSDZXoPcCgYEAzwIUtaLvsCxozZ9gvHeX
jsYHfK4uhIW/7kfCBgJbgw9j6M5r3yGA+DsQ3LyMRr625FU1RX0QrJfqtIz/QAEO
VpfenXwqvMneHGGtNjrmTZSmq8/crRwxXaGofTmWW7z/StRAC9du/c1xWoWVWWST
/Ujr2B2yxOFsoEKx6euvMUECgYAQZFsPlv/RccVhl0WCjJ12fu3651KSzwkT5xm9
zNyYfTDbkmEgrYtXvqZ25/dKK/Zi4LSes521Nokmajdzhp1EB3Lgs7Z++15ysZoY
sfG0+1XSzC7Su6zNE3wO4tC3Vgg8Q12cxw69cIZq217NNPGF/7+S5M8Miuim1n4O
n2sg8QKBgDrEDwVYLubzSk1kE3N3pGS6NPqC+R780Dppni7BQ1U5oE5DNa8lsQ40
cJ0pApthi6lpBIy+MzHnD9DhvFXDe9HCF43frlSeNZiHovo8YpK2iIb+AKXzDhqg
J/M2ALvAvAzzSYgtOefqqhgsfF/LJw9g/1TCc7SaQh5QAzLy36hZ
-----END RSA PRIVATE KEY-----");
                        var domainKeySigner = new DomainKeySigner(privateKey, "eventras.com", "eventras", new string[] { "From", "To", "Subject" });
                        mail.DomainKeySign(domainKeySigner);


                        var dkimSigner = new DkimSigner(privateKey, "eventras.com", "eventras", new string[] { "From", "To", "Subject" });
                        mail.DkimSign(dkimSigner);
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                        //return 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        //protected void input_entity_zip_TextChanged(object sender, EventArgs e)
        //{
        //    if (input_entity_zip.Text != "")
        //    {
        //        zipLookup(input_entity_zip.Text, "entity");
        //    }
        //}

        //protected void input_person_zip_TextChanged(object sender, EventArgs e)
        //{
        //    if (input_person_zip.Text != "")
        //    {
        //        zipLookup(input_person_zip.Text, "person");
        //    }
        //}
        public void zipLookup(string Zip, string mode)
        {
            #region no use
            try
            {
                string zipcodeKey = WebConfigurationManager.AppSettings["zipcodeKey"];

                string url = "http://www.zipcodeapi.com/rest/" + zipcodeKey + "/info.json/" + Zip + "/radians";
                string TimeZone = "";
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers["X-Api-Key"] = "key";
                    client.Headers["X-Api-Secret"] = "secret";

                    string s = client.DownloadString(url);

                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic j = jsonSerializer.Deserialize<dynamic>(s);
                    string city = j["city"].ToString();
                    string state = j["state"].ToString();
                    TimeZone = j["timezone"]["timezone_abbr"].ToString();
                    if (mode == "entity")
                    {
                        input_entity_city.Text = city;
                        input_entity_state.Text = state;

                    }
                    else
                    {
                        input_person_city.Text = city;
                        input_person_state.Text = state;
                    }

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showTimeZone('" + TimeZone + "');", true);
            }
            catch (WebException ex)
            {
                //if (mode == "entity")
                //{
                //    input_entity_zip.Text = "";
                //    input_entity_city.Text = "";
                //    input_entity_state.Text = "";
                //}
                //else {
                //    input_person_zip.Text = "";
                //    input_person_city.Text = "";
                //    input_person_state.Text = "";
                //}
                //var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                //dynamic obj = JsonConvert.DeserializeObject(resp);
                //var messageFromServer = obj.error_msg;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ messageFromServer + "')", true);


                //return messageFromServer;
            }
            #endregion no use

        }

        protected void imgPersonMail_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../mailer?type=person&id=" + person_id.Value);
        }

        protected void e_tag_new_search_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tag";
                obj.str1 = e_entity_tag_descr.Text.Trim();

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];

                e_entity_tag_assign.DataSource = dt;
                e_entity_tag_assign.DataTextField = "tag_descr";
                e_entity_tag_assign.DataValueField = "tag_id";
                e_entity_tag_assign.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void person_tag_new_Search_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tag";
                obj.str1 = person_tag_descr.Text.Trim();

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];

                person_tag_assign.DataSource = dt;
                person_tag_assign.DataTextField = "tag_descr";
                person_tag_assign.DataValueField = "tag_id";
                person_tag_assign.DataBind();
            }
            catch (Exception ex)
            {

            }
        }




        //protected void img_url_Click(object sender, ImageClickEventArgs e)
        //{
        //    string url = lbl_entity_Url.Text;

        //    //ClientScript.RegisterStartupScript(GetType(), "popup", "window.open('" + url + "', 'pp');", true);
        //    //  Page.ClientScript.RegisterStartupScript(
        //    // this.GetType(), "OpenWindow", "window.open(" + url + ",'_newtab');", true);
        //     Response.Redirect("<script>window.open(" + url + ",'_blank');</script>");
        //   // Response.Redirect(url);
        //    //Response.Redirect(url, false);

        //}

        //protected void img_info_entity_Click(object sender, ImageClickEventArgs e)
        //{
        //}
        //protected void img_info_person_Click(object sender, ImageClickEventArgs e)
        //{
        //}
    }
}