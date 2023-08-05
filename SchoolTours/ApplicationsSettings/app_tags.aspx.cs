using SchoolToursBusiness;
using SchoolToursData.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolTours.ApplicationsSettings
{
    public partial class app_tags : System.Web.UI.Page
    {
        //public static int tag_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tag_id.Value = "0";
            }
        }
        protected void input_tag_search_TextChanged(object sender, EventArgs e)
        {
            srchTag();
        }
        public void srchTag()
        {
            try
            {
                input_tag_descr.Text = "";
                tag_id.Value = "0";
                Obj_PR_SEARCH obj = new Obj_PR_SEARCH();
                obj.mode = "tag";
                obj.str1 = input_tag_search.Text;

                DataTable dt = DTL_ITEM_Business.Get_PR_SEARCH(obj).Tables[0];

                select_tag.DataSource = dt;
                select_tag.DataTextField = "tag_descr";
                select_tag.DataValueField = "tag_id";
                select_tag.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void dtlTag(object sender, EventArgs e)
        {
            if (select_tag.SelectedValue != "")
            {
                tag_id.Value = Convert.ToInt32(select_tag.SelectedValue).ToString();
                input_tag_descr.Text = Convert.ToString(select_tag.SelectedItem);
            }
        }
        public void mrgTag(object sender, EventArgs e)
        {
            //Confirm that the user has selected two items in select_tag.  
            //If two items are selected, extract the two tag_ids from the select into @tag_id_1 and @tag_id_2 and execute pr_set_item(‘merge_tag’, @tag_id_1, @tag_id_2, @emp_id) 
            //which returns 1 if successful, 0 if failure.  ● If successful, execute pr_search(‘tag’, @input_tag_search.value) which returns a multiple row recordset with the following columns: 1.tag_id, 2.tag_descr
            //Use these values to populate select_tag, each with an onclick action of dtlTag(tag_id).

            int tag_id_1 = 0;
            int tag_id_2 = 0;
            int count = 0;


            foreach (ListItem item in select_tag.Items)
            {
                if (item.Selected)
                {
                    if (count == 0)
                    {
                        tag_id_1 = Convert.ToInt32(item.Value);
                        count++;
                    }
                    else if (count == 1)
                    {
                        tag_id_2 = Convert.ToInt32(item.Value);
                        count++;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            if (count == 2)
            {
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "merge_tag";
                obj.id1 = tag_id_1;
                obj.id2 = tag_id_2;
                obj.id3 = Convert.ToInt32(Session["emp_id"].ToString());
                int Response = DTL_ITEM_Business.Put_SET_ITEM(obj);
                if (Response == 1)
                {
                    srchTag();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('only two tag merge.')", true);
            }
        }
        public void delTag(object sender, EventArgs e)
        {
            //First confirm whether the user wants to complete this action.  
            //On confirmation, execute pr_del_item(‘tag’, @tag_id, @emp_id) which returns 1 if successful, 0 if failure.  
            //If successful, execute pr_search(‘tag’, @input_tag_search.value) which returns a multiple row recordset with the following columns: 1.tag_id, 2.tag_descr.Use these values to populate select_tag
            //each with an onclick action of dtlTag(tag_id).

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "tag";
            obj.id1 = Convert.ToInt32(tag_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            int Response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (Response == 1)
            {
                srchTag();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
            }
        }
        public void newTag(object sender, EventArgs e)
        {
            tag_id.Value = "0";
            input_tag_descr.Text = "";
        }
        public void setTag(object sender, EventArgs e)
        {
            //Execute pr_set_item(‘tag_new’, @tag_id, @emp_id, null, null, @input_tag_descr)  which returns 1 if successful, 0 if failure.  
            //If successful, execute pr_search(‘tag’, @input_tag_search.value) which returns a multiple row recordset with the following columns: 1.tag_id, 2.tag_descr
            //.Use these values to populate select_tag, each with an onclick action of dtlTag(tag_id)

            Obj_SET_ITEM obj = new Obj_SET_ITEM();
            obj.mode = "tag_new";
            obj.id1 = Convert.ToInt32(tag_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.str1 = Convert.ToString(input_tag_descr.Text.Trim());
            int Response = DTL_ITEM_Business.Put_SET_ITEM(obj);
            if (Response == 1)
            {
                srchTag();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
            }
        }


    }
}