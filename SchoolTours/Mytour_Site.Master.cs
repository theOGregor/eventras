using SchoolToursBusiness;
using SchoolToursData.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolTours
{
    public partial class Mytour_Site : System.Web.UI.MasterPage
    {
        public static int person_id = 0;
        public static int tour_id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.Cookies["CustomerFacing_person"].Value != "")
                    {
                        person_id = Convert.ToInt32(Request.Cookies["CustomerFacing_person"].Value);

                        if (Request.Cookies["CustomerFacing_tour"].Value != "")
                        {
                            tour_id = Convert.ToInt32(Request.Cookies["CustomerFacing_tour"].Value);
                        }
                        OnLoad();
                    }
                    else
                    {
                        Response.Redirect("../mytour_index.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("../mytour_index.aspx");
                }
            }
        }
        public void OnLoad()
        {
            //
            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
            obj.mode = "mytour";
            obj.id1 = tour_id.ToString();
            obj.str1 = person_id.ToString();
            obj.str2 = Convert.ToInt32(Request.Cookies["CustomerFacing_ind_ind"].Value).ToString();
            DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

            if (ds.Tables[0].Rows.Count > 0)
            {
                div_entity_nm.Text = ds.Tables[0].Rows[0]["operator_nm"].ToString() == "" ? ds.Tables[0].Rows[0]["leader_nm"].ToString() : ds.Tables[0].Rows[0]["operator_nm"].ToString();
                div_div_img.ImageUrl = "~/Images/" + ds.Tables[0].Rows[0]["img"].ToString();

                StringBuilder SB = new StringBuilder();
                SB.Append("<a href='javascript: void(0);' class='txt-light-blue font-weight-bold d-block'>" + ds.Tables[0].Rows[0]["evt_nm"].ToString() + "</a>");
                //SB.Append("<a href='javascript: void(0);' class='txt-light-blue font-weight-bold d-block'>Memorial Day Parade</a>");
                SB.Append("<p class='d-block txt-light-blue mb-0'>" + ds.Tables[0].Rows[0]["evt_date"].ToString() + "</p>");

                div_evt_nm.InnerHtml = SB.ToString();

                div_countdown.Text = ds.Tables[0].Rows[0]["days_nr"].ToString();
                div_evt_memo.InnerHtml = ds.Tables[0].Rows[0]["memo"].ToString();


            }
        }
    }
}