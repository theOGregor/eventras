using SchoolToursBusiness;
using SchoolToursData.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolTours.Sales
{
    public partial class rpt_sales_prod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                onLoad();
            }
        }
        public void onLoad()
        {
            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
            obj.mode = "auth_emps";
            obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.str1 = "";
            obj.str2 = "";
            obj.str3 = "";


            DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

            select_emp_id.DataSource = dt;
            select_emp_id.DataTextField = "emp_nm";
            select_emp_id.DataValueField = "emp_id";
            select_emp_id.DataBind();
        }
        public void shwStats(object sender, EventArgs e)
        {
            try
            {
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "emp_sales";
                obj.id1 = select_emp_id.SelectedValue;
                obj.str1 = input_start_dt.Text;
                obj.str2 = input_end_dt.Text;

                DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                div_sales_calls.Text = dt.Rows[0]["sales_calls"].ToString();
                div_sales_mail.Text = dt.Rows[0]["sales_mail"].ToString();
                div_mass_mail.Text = dt.Rows[0]["mass_mail"].ToString()==""?"0": dt.Rows[0]["mass_mail"].ToString();
                div_comp_hotlist.Text = dt.Rows[0]["comp_hotlist"].ToString();
                div_future_hotlist.Text = dt.Rows[0]["future_hotlist"].ToString();
                div_past_hotlist.Text = dt.Rows[0]["past_due_hotlist"].ToString();
                div_contracts.Text = dt.Rows[0]["contracts"].ToString();
                

            }
            catch (Exception ex)
            {


            }
        }

    }
}