using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace SchoolTours.Sales
{
    public partial class lst_gen : System.Web.UI.Page
    {
        //static string host = HttpContext.Current.Request.Url.Host;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void ListGen_tag_new_Click(object sender, ImageClickEventArgs e)
        {
            if (input_state.Text != "")
            {
                ListItem litm = new ListItem();
                litm.Text = input_state.Text;
                litm.Value = "0";
                div_state_list.Items.Add(litm);
                input_state.Text = "";
            }
        }
        public void tagRemove_State(object sender, EventArgs e)
        {
            div_state_list.Items.RemoveAt(div_state_list.SelectedIndex);
        }
        public void clearFields(object sender, EventArgs e)
        {
            div_state_list.Items.Clear();
            input_state.Text = "";
            div_nbr_items.Text = "";
            div_list.Items.Clear();
            entity_tag_assign.Items.Clear();
            entity_sel_tags.Items.Clear();
            person_tag_assign.Items.Clear();
            person_sel_tags.Items.Clear();
            input_entity_tag_descr.Text = "";
            input_preson_tag_descr.Text = "";

        }
        public void genList(object sender, EventArgs e)
        {
            try
            {
                //execute pr_search(‘gen_display’, @emp_id, @div_state_list, @entity_tags, @person_tags) 

                //=========================== state
                string state = "";
                for (int index = 0; index < div_state_list.Items.Count; index++)
                {
                    ListItem item = div_state_list.Items[index];
                    state += item.Text + ",";
                }
                if (state != "")
                    state = state.Remove(state.Length - 1, 1);

                //=========================== entity
                string entity_tag_id = "";
                for (int index = 0; index < entity_sel_tags.Items.Count; index++)
                {
                    ListItem item = entity_sel_tags.Items[index];
                    entity_tag_id += item.Value + ",";
                }
                if (entity_tag_id != "")
                    entity_tag_id = entity_tag_id.Remove(entity_tag_id.Length - 1, 1);
                //=========================== person

                string person_tag_id = "";
                for (int index = 0; index < person_sel_tags.Items.Count; index++)
                {
                    ListItem item = person_sel_tags.Items[index];
                    person_tag_id += item.Value + ",";
                }
                if (person_tag_id != "")
                    person_tag_id = person_tag_id.Remove(person_tag_id.Length - 1, 1);

                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "gen_display";
                obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.str1 = state == "" ? null : state;
                obj.str2 = entity_tag_id == "" ? null : entity_tag_id;
                obj.str3 = person_tag_id == "" ? null : person_tag_id;

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];



                DataColumnCollection columns = dt.Columns;
                if (columns.Contains("rc"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + dt.Rows[0]["err_msg"].ToString() + "')", true);
                }
                else
                {
                    div_list.DataSource = dt;
                    div_list.DataTextField = "person_descr";
                    div_list.DataBind();
                    div_nbr_items.Text = "This List Contains " + dt.Rows.Count.ToString() + " Matching Items";
                }


            }
            catch (Exception ex)
            {

            }
        }
        public void mailList(object sender, EventArgs e)
        {
            //Response.Redirect("../mailer");
            Response.Redirect("../mass_mailer");
            //if (host == "43.229.227.26")
            //    Response.Redirect("../mailer.aspx");
            //else
            //    Response.Redirect("/mailer.aspx");
        }
        public void prnList(object sender, EventArgs e)
        {
            try
            {
                // pr_lst_items(‘gen_print’, @emp_id

                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "gen_print";
                obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());


                DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                string strFileName = "lst_gen.xlsx";
                DataTable dt = new DataTable("GridView_Data");
                GridView dg = new GridView();
                dg.DataSource = ds.Tables[0];
                dg.DataBind();

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds.Tables[0], "Property");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {

                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        //================================================================================= entity
        protected void input_entity_tag_descr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tag";
                obj.str1 = input_entity_tag_descr.Text;

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    ListItem li = new ListItem(item["tag_descr"].ToString(), item["tag_id"].ToString());
                    li.Attributes.Add("title", item["tag_descr"].ToString());
                    li.Attributes.Add("data-toggle", "tooltip");
                    li.Attributes.Add("data-container", "#tooltip_container");
                    //data-toggle="tooltip" data-container="#tooltip_container"
                    entity_tag_assign.Items.Add(li);
                }
                //entity_tag_assign.DataSource = dt;
                //entity_tag_assign.DataTextField = "tag_descr";
                //entity_tag_assign.DataValueField = "tag_id";
                //entity_tag_assign.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        public void tagSelect_entity(object sender, EventArgs e)
        {
            string tag_descr = Convert.ToString(entity_tag_assign.SelectedItem);
            string tag_id = Convert.ToString(entity_tag_assign.SelectedValue);

            int strCount = 0;
            for (int index = 0; index < entity_sel_tags.Items.Count; index++)
            {
                ListItem item = entity_tag_assign.Items[index];
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
                entity_sel_tags.Items.Add(litm);
            }

        }
        public void tagRemove_entity(object sender, EventArgs e)
        {
            entity_sel_tags.Items.RemoveAt(entity_sel_tags.SelectedIndex);
        }
        //================================================================================= person

        protected void input_preson_tag_descr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tag";
                obj.str1 = input_preson_tag_descr.Text;

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    ListItem li = new ListItem(item["tag_descr"].ToString(), item["tag_id"].ToString());
                    li.Attributes.Add("title", item["tag_descr"].ToString());
                    li.Attributes.Add("data-toggle", "tooltip");
                    li.Attributes.Add("data-container", "#tooltip_container1");
                    //data-toggle="tooltip" data-container="#tooltip_container"
                    person_tag_assign.Items.Add(li);
                }

                //person_tag_assign.DataSource = dt;
                //person_tag_assign.DataTextField = "tag_descr";
                //person_tag_assign.DataValueField = "tag_id";
                //person_tag_assign.DataBind();
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

    }
}