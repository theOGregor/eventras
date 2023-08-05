using SchoolToursBusiness;
using SchoolToursData.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace SchoolTours.Sales
{
    public partial class lookup : System.Web.UI.Page
    {
        //public static int entity_person_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                div_entity_panel.Visible = false;
                div_All_panel.Visible = true;
                entity_person_id.Value = "0";
            }
        }

        public void srchEMail(object sender, EventArgs e)
        {
            try
            {
                div_entity_panel.Visible = false;
                div_All_panel.Visible = true;
                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "rev_eMail";
                obj.str1 = input_lookup_value.Text;

                DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

                div_list.DataSource = dt;
                div_list.DataTextField = dt.Columns[1].ColumnName;
                div_list.DataValueField = dt.Columns[0].ColumnName;
                div_list.DataBind();
                //foreach (DataRow item in dt.Rows)
                //{
                //    System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(item["Column1"].ToString());
                //    li.Attributes.Add("title", item["Column1"].ToString());
                //    li.Attributes.Add("data-toggle", "tooltip");
                //    li.Attributes.Add("data-container", "#tooltip_container");
                //    div_list.Items.Add(li);
                //}
            }
            catch (Exception ex)
            {

            }
        }

        public void srchPhone(object sender,EventArgs e)
        {
            try
            {
                div_entity_panel.Visible = false;
                div_All_panel.Visible = true;

                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

                Regex regex = new Regex(@"^[0-9]+$");
                string str = input_lookup_value.Text;
                if (!regex.IsMatch(str))
                {
                    obj.mode = "rev_eMail";
                }                        
                else
                {
                    obj.mode = "rev_phone";

                }
                obj.str1 = input_lookup_value.Text;
                DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

                div_list.DataSource = dt;
                div_list.DataTextField = dt.Columns[1].ColumnName;
                div_list.DataValueField = dt.Columns[0].ColumnName;
                div_list.DataBind();
                //foreach (DataRow item in dt.Rows)
                //{
                //    System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(item["Column1"].ToString());
                //    li.Attributes.Add("title", item["Column1"].ToString());
                //    li.Attributes.Add("data-toggle", "tooltip");
                //    li.Attributes.Add("data-container", "#tooltip_container");
                //    div_list.Items.Add(li);
                //}
            }
            catch (Exception ex)
            {

            }
        }
        protected void div_entity_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            div_entity_panel.Visible = true;
            div_All_panel.Visible = false;
            entity_person_id.Value = Convert.ToInt32(div_list.SelectedValue).ToString();

            dtlEntity(Convert.ToInt32(entity_person_id.Value));

        }

        public void dtlEntity(int entity_person_id)
        {
            try
            {
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "person";
                obj.id1 = Convert.ToString(entity_person_id);
                obj.str1 = "";
                obj.str2 = "";

                DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

                DataTable dt_person = ds.Tables[0];
                DataTable dt_tags = ds.Tables[1];

                if (dt_person.Rows.Count > 0)
                {
                    input_person_greeting.Text = dt_person.Rows[0]["greeting"].ToString() + " " + dt_person.Rows[0]["given_nm"].ToString() + " " + dt_person.Rows[0]["last_nm"].ToString();
                    input_person_address.Text = dt_person.Rows[0]["address"].ToString() + " " + dt_person.Rows[0]["city"].ToString() + " " + dt_person.Rows[0]["state"].ToString();
                    input_person_zip.Text = dt_person.Rows[0]["zip"].ToString();
                    input_person_landline.Text = phoneformatting(dt_person.Rows[0]["office_phone"].ToString());
                    input_person_cell_phone.Text = phoneformatting(dt_person.Rows[0]["cell_phone"].ToString());
                    input_person_eMail.Text = dt_person.Rows[0]["eMail"].ToString();
                }
                if (dt_tags.Rows.Count > 0)
                {
                    div_tag_list.Text = dt_tags.Rows[0]["tag_descr"].ToString();
                }
                else
                {
                    div_tag_list.Text = "";
                }

                Obj_LST_ITEMS objN = new Obj_LST_ITEMS();

                objN.mode = "note";
                objN.id1 =(entity_person_id);

                DataSet dsN = DTL_ITEM_Business.Get_LST_ITEMS(objN);

                DataTable dt_note = dsN.Tables[0];
                DataTable dt_ref = dsN.Tables[1];

                if (dt_note.Rows.Count > 0)
                {
                    div_notes_list.DataSource = dt_note;
                    div_notes_list.DataTextField = "Column1";
                    div_notes_list.DataValueField = "note_id";
                    div_notes_list.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string phoneformatting(string strPhone)
        {
            try
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
            catch (Exception ex)
            {
                return strPhone;
            }
        }

        public void div_Hide_Entity(object sender, EventArgs e)
        {
            div_entity_panel.Visible = false;
            div_All_panel.Visible = true;
        }

        protected void img_person_eMail_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../mailer?type=person&id=" + entity_person_id.Value);
        }
    }
}