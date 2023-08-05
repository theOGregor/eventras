using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;

namespace SchoolTours.ApplicationsSettings
{
    public partial class app_evt : System.Web.UI.Page
    {
        //static int evt_id = 0;
        //static int div_id = 0;
        //static int venue_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                evt_id.Value = "0";
                div_id.Value = "0";
                venue_id.Value = "0";
                onLoad();
            }
        }
        public void onLoad()
        {
            //Execute pr_lst_items(‘division’, @emp_id) which returns a multiple row recordset with the following columns:
            //1.div_id, 2.div_nm.Use these values to populate select_div, each with an onclick action of dtlDiv(). 
            //● Execute pr_lst_items(‘venues’) which returns a multiple row recordset with the following columns: 1. venue_id, 2. venue_descr. Use these values to populate select_venue
            try
            {
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
                obj1.mode = "venues";
                DataTable dt1 = DTL_ITEM_Business.Get_LST_ITEMS(obj1).Tables[0];

                select_venue.DataSource = dt1;
                //select_venue.DataTextField = "venue_descr";
                //select_venue.DataValueField = "venue_id";
                select_venue.DataTextField = "ref_descr";
                select_venue.DataValueField = "ref_id";
                select_venue.DataBind();
                select_venue.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {

            }
        }

        protected void select_div_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlDiv();
        }
        protected void select_venue_SelectedIndexChanged(object sender, EventArgs e)
        {
            // dtlEvt();
        }
        public void dtlDiv()
        {
            try
            {
                evt_id.Value = "0";
                input_evt_nm.Text = "";
                input_evt_descr.Text = "";
                select_venue.SelectedValue = "Select";
                input_start_date.Text = "";
                input_end_date.Text = "";

                if (select_div.SelectedValue != "Select")
                {
                    div_id.Value = Convert.ToInt32(select_div.SelectedValue).ToString();
                    Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                    obj.mode = "div_evts";
                    obj.id1 = Convert.ToInt32(select_div.SelectedValue);
                    DataSet ds = DTL_ITEM_Business.Get_LST_ITEMS(obj);

                    DataTable dt = ds.Tables[0];
                    //DataTable dt1 = ds.Tables[1];

                    select_evt.DataSource = dt;
                    select_evt.DataTextField = "evt_nm";
                    select_evt.DataValueField = "evt_id";
                    select_evt.DataBind();

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void dtlEvt(object sender, EventArgs e)
        {
            try
            {
                evt_id.Value = "0";
                input_evt_nm.Text = "";
                input_evt_descr.Text = "";
                select_venue.SelectedValue = "Select";
                input_start_date.Text = "";
                input_end_date.Text = "";

                if (select_evt.SelectedValue != "")
                {
                    evt_id.Value = Convert.ToInt32(select_evt.SelectedValue).ToString();
                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "evt";
                    obj.id1 = Convert.ToString(evt_id.Value);
                    DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {


                        input_evt_nm.Text = (dt.Rows[0]["evt_nm"].ToString());
                        input_evt_descr.Text = (dt.Rows[0]["evt_descr"].ToString());
                        try
                        {
                            DateTime start_date = DateTime.Parse(dt.Rows[0]["start_date"].ToString());
                            input_start_date.Text = start_date.ToString("yyyy-MM-dd");

                            DateTime end_date = DateTime.Parse(dt.Rows[0]["end_date"].ToString());
                            input_end_date.Text = end_date.ToString("yyyy-MM-dd");
                        }
                        catch (Exception ex) { }
                        input_evt_memo.Text = (dt.Rows[0]["memo"].ToString());
                        select_venue.SelectedValue = (dt.Rows[0]["venue_id"].ToString());
                    }

                    else {
                        select_evt.Text = "";
                        select_evt.Text = "";
                        input_start_date.Text = "";
                        input_start_date.Text = "";
                        input_evt_memo.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void cpyEvts(object sender, EventArgs e)
        {
            //Confirm that the user wants to copy all events from the selected division from last year to the next year. 
            //● If confirmed, execute pr_set_item(‘copy_evts’, @div_id, @emp_id) which returns 1 if successful, 0 if failure.  
            //● If successful, display message that all events for the selected division have been copied from last year to the new year.If failure, display standard error message.
            try
            {
                if (Convert.ToInt32(div_id.Value) != 0)
                {
                    Obj_SET_ITEM obj = new Obj_SET_ITEM();
                    obj.mode = "copy_evts";
                    obj.id1 = Convert.ToInt32(div_id.Value);
                    obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());

                    int Resposne = DTL_ITEM_Business.Put_SET_ITEM(obj);
                    if (Resposne == 1)
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('all events for the selected division have been copied from last year to the new year')", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('please select Div')", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void delEvt(object sender, EventArgs e)
        {
            //Confirm that the user wants to complete this action. 
            //● If confirmed, execute pr_del_item(‘evt’, @evt_id, @emp_id) which returns 1 if successful, 0 if failure. 
            //● Execute pr_lst_items(‘div_evts’ @div_id) as shown below in dtlDiv() function

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "evt";
            obj.id1 = Convert.ToInt32(evt_id.Value);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            if (response == 1)
            {
                dtlDiv();
            }
        }
        public void newEvt(object sender, EventArgs e)
        {
            // set the @evt_id = 0 and clear the following objects: input_evt_nm, input_evt_descr, select_venue, input_start_date, input_end date. 
            evt_id.Value = "0";
            input_evt_nm.Text = "";
            input_evt_descr.Text = "";
            select_venue.SelectedValue = "Select";
            input_start_date.Text = "";
            input_end_date.Text = "";
        }
        public void setEvt(object sender, EventArgs e)
        {
            //Validate that the following fields have valid entries: input_evt_nm, input_evt_descr, select_venue, input_start_date, and input_end_date. 
            //● If valid, execute pr_set_item(‘evt’, @div_id, @evt_id, @venue_id, @emp_id, @evt_descr, @evt_nm, @start_date, @end_date, @memo) which returns 1 if successful, 0 if failure. 
            //● If successful, execute pr_lst_items(‘div_evts’ @div_id) as shown above in dtlDiv() function.If failure, show standard error message.
            try
            {
                venue_id.Value = Convert.ToInt32(select_venue.SelectedValue).ToString();
                Obj_SET_ITEM obj = new Obj_SET_ITEM();
                obj.mode = "evt";
                obj.id1 = Convert.ToInt32(div_id.Value);
                obj.id2 = Convert.ToInt32(evt_id.Value);
                obj.id3 = Convert.ToInt32(venue_id.Value);
                obj.id4 = Convert.ToInt32(Session["emp_id"].ToString());
                obj.str1 = input_evt_descr.Text.Trim();
                obj.str2 = input_evt_nm.Text.Trim();
                obj.str3 = input_start_date.Text;
                obj.str4 = input_end_date.Text;
                obj.str5 = input_evt_memo.Text;

                int response = DTL_ITEM_Business.Put_SET_ITEM(obj);
                if (response == 1)
                {
                    dtlDiv();
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failure')", true);
                }
            }
            catch (Exception ex) { }
        }
    }
}