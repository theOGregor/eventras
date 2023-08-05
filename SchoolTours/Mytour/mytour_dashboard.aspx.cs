using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursBusiness;
using SchoolToursData.Object;
using System.Data;
using System.IO;

namespace SchoolTours.Mytour
{
    public partial class mytour_dashboard : System.Web.UI.Page
    {
        //public static int person_id = 0;
        //public static int tour_id = 0;
        //public static int int_invoice_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    person_id.Value = "0";
                    if (Request.Cookies["CustomerFacing_person"].Value != "")
                    {
                        person_id.Value = Convert.ToString(Request.Cookies["CustomerFacing_person"].Value);

                        if (Request.Cookies["CustomerFacing_tour"].Value != "")
                        {
                            tour_id.Value = Convert.ToString(Request.Cookies["CustomerFacing_tour"].Value);
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
                //OnLoad();
            }
        }

        public void OnLoad()
        {

            //execute pr_dtl_item(‘mytour’, @tour_id, @person_id, @ind_ind) which returns five recordsets: 
            //● The first is a single row recordset with the following columns: 1.leader_nm, 2.operator_nm, 3.entity_nm, 4.img, 5.evt_nm, 6.evt_date, 7.days_nr, 8.memo, 9.pax_descr, 10.pax_ind, 
            //11.leader_id, 12.operator_id.  ○ If operator_nm is not null, display the operator_nm as the name in div_entity_nm.Otherwise, display leader_nm, followed by a dash, and then entity_nm. 
            //○ Display the appropriate img in div_div_img, stretching to fill either the width or the height. ○ Display evt_nm and evt_date in div_evt_nm. 
            //○ Display days_nr in div_countdown. 
            //○ Display memo ind div_evt_memo, which should scroll vertically if necessary. 
            //○ Display pax_descr as the top row in div_fin_info. ○ If pax_ind = 4, then display the text in div_notice. 
            //○ Create a local variable @admin_ind with a default value of 0.If either leader_id or operator_id = @person_id, assign the value of 1 to the @admin_ind variable. 
            //● The second is a multiple row recordset with the following column: 1.reminder_descr.Display each of these rows as the second through fifth lines in div_fin_info. 
            //● The third is a single row recordset with the following columns: 1.inv_id, 2.inv_amt, 3.current_inv.If current_inv is not null, 
            //display this as the next row in div_fin_info.Store inv_amt in the cookie @inv_amt to be used in mytour_pmt.aspx.Form a local variable for the filename of the invoice 
            //for use in the docDownload() function above: ○ If @ind_ind = 0 then filename is ‘inv_@tourID_@invID.pdf’. ○ If @ind_ind = 1 then filename is ‘inv_ind_@tourID_@invID_@personID.pdf’. 
            //● The fourth is a single row recordset with the following column: 1.next_inv.If this value is not null, display this as the last row in div_fin_info. 
            //● The fifth is a multiple row recordset with the following columns: 1.contract_descr, 2.contract_date, 3.days_nr.Display each row on a separate line in div_contract_info

            try
            {
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "mytour";
                obj.id1 = tour_id.Value.ToString();
                obj.str1 = person_id.Value.ToString();
                //obj.str2 = Obj_global_Mytour_Login.ind_ind.ToString();
                obj.str2 = Convert.ToInt32(Request.Cookies["CustomerFacing_ind_ind"].Value).ToString();
                DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);
                if (ds.Tables[0].Rows[0]["pax_ind"].ToString() == "4")
                {
                    if (ds.Tables[3].Rows.Count > 0)
                        div_notice.Text = ds.Tables[2].Rows[0]["next_inv"].ToString();
                }
                if (ds.Tables[0].Rows[0]["leader_id"].ToString() == person_id.Value.ToString() || ds.Tables[0].Rows[0]["operator_id"].ToString() == person_id.Value.ToString())
                {
                    HttpCookie admin_ind = new HttpCookie("CustomerFacing_admin_ind");
                    admin_ind.Value = "1";
                    Response.Cookies.Add(admin_ind);
                    //Obj_global_Mytour_Login.admin_ind = 1;
                }
                gv_div_fin_info.DataSource = ds.Tables[2];
                gv_div_fin_info.DataBind();
                try
                {
                    string pax_descr = ds.Tables[0].Rows[0]["pax_descr"].ToString();
                    DataTable dt_Dynamic = new DataTable();
                    dt_Dynamic.Columns.AddRange(new DataColumn[3] { new DataColumn("inv_id", typeof(int)), new DataColumn("inv_amt", typeof(string)), new DataColumn("current_inv", typeof(string)) });
                    if (pax_descr != "")
                    {
                        string[] Arr_pax_descr = pax_descr.Split('-');
                        dt_Dynamic.Rows.Add(0, Arr_pax_descr[1], Arr_pax_descr[0]);
                    }
                    DataTable Dt_reminder = ds.Tables[1];
                    DataView view = Dt_reminder.DefaultView;
                    //view.Sort = "mod_dt desc";
                    view.Sort = "sorter asc";
                    Dt_reminder = view.ToTable();
                    foreach (DataRow dr in Dt_reminder.Rows)
                    {
                        dt_Dynamic.Rows.Add(0, dr.ItemArray[1].ToString() == "" ? dr.ItemArray[1] : "$ " + String.Format("{0:0.00}", Convert.ToDecimal(dr.ItemArray[1].ToString())), dr.ItemArray[0]);
                    }
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        dt_Dynamic.Rows.Add(dr.ItemArray[0], dr.ItemArray[1].ToString() == "" ? dr.ItemArray[1] : "$ " + String.Format("{0:0.00}", Convert.ToDecimal(dr.ItemArray[1].ToString())), dr.ItemArray[2]);
                    }
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        if (dr.ItemArray[0].ToString() != "")
                        {
                            string[] Arr_Next_inv = dr.ItemArray[0].ToString().Split('-');
                            dt_Dynamic.Rows.Add(0, Arr_Next_inv[1], Arr_Next_inv[0]);
                        }
                    }
                    gv_div_fin_info.DataSource = dt_Dynamic;
                    gv_div_fin_info.DataBind();

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        int_invoice_id.Value = Convert.ToString(ds.Tables[2].Rows[0].ItemArray[0].ToString());
                    }
                }
                catch (Exception ex) { }

                gv_div_contract_info.DataSource = ds.Tables[4];
                gv_div_contract_info.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        #region docDownload
        protected void img_mytour_itin_Click(object sender, ImageClickEventArgs e)
        {
            docDownload("itin");
        }

        protected void img_mytour_inv_Click(object sender, ImageClickEventArgs e)
        {
            docDownload("inv");
        }

        protected void img_mytour_stmt_Click(object sender, ImageClickEventArgs e)
        {
            docDownload("stmt");
        }
        public void docDownload(string type)
        {
            try
            {
                if (type == "itin")
                {
                    string str_filename = type + "_" + tour_id.Value + ".pdf";
                    string Path = "~/Doc_Uploads/" + str_filename;
                    if (!File.Exists(Server.MapPath(Path)))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The file does not exist on this location, please add this file on the tour detail.')", true);
                        return;
                    }
                    string wordDocName1 = "../Doc_Uploads/" + str_filename;
                    Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                }
                else if (type == "inv")
                {
                    if (int_invoice_id.Value =="0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('There is no current invoice exist on this locatio n,Please generate invoice and check again. ')", true);
                        return;
                    }
                    else {
                        string str_filename = "";//"inv" + "_" + tour_id + "_" + int_invoice_id + ".pdf";

                        if (Convert.ToInt32(Request.Cookies["CustomerFacing_ind_ind"].Value) == 0)
                        {
                            str_filename = "inv" + "_" + tour_id.Value + "_" + int_invoice_id.Value + ".pdf";
                        }
                        else
                        {
                            str_filename = "inv_ind" + "_" + tour_id.Value + "_" + int_invoice_id.Value + "_" + person_id.Value + ".pdf";
                        }
                        string Path = "~/INVOICES/" + str_filename;
                        if (!File.Exists(Server.MapPath(Path)))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The file does not exist on this location, please generate the invoice and check again.')", true);
                            return;
                        }
                        string wordDocName1 = "../INVOICES/" + str_filename;
                        Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                    }
                }
                else if (type == "stmt")
                {
                    string str_filename = "stmt" + "_" + tour_id.Value + ".pdf";
                    string Path = "~/pdf/" + str_filename;
                    if (!File.Exists(Server.MapPath(Path)))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The file does not exist on this location, please generate the statment and check again.')", true);
                        return;
                    }
                    string wordDocName1 = "../pdf/" + str_filename;
                    Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion docDownload

        #region goTo
        protected void img_mytour_pax_Click(object sender, ImageClickEventArgs e)
        {
            goTo("rooming");
        }

        protected void img_mytour_pmt_Click(object sender, ImageClickEventArgs e)
        {
            goTo("payment");
        }

        protected void img_mail_Click(object sender, ImageClickEventArgs e)
        {
            goTo("contact");
        }
        public void goTo(string RedirectPage)
        {
            string RredirectUrl = "";
            if (RedirectPage == "rooming")
                RredirectUrl += "mytour_pax";
            else if (RedirectPage == "payment")
                RredirectUrl += "mytour_pmt";
            else if (RedirectPage == "contact")
                RredirectUrl += "mytour_mail";

            Response.Redirect(RredirectUrl);
        }

        #endregion goTo

        #region modContract
        protected void btn_download_itin_Click(object sender, EventArgs e)
        {
            modContract("view");
        }

        protected void btn_attach_itin_Click(object sender, EventArgs e)
        {
            modContract("attach");
        }
        public void modContract(string option)
        {
            if (option == "view")
            {
                string str_filename = "ctrc" + "_" + tour_id.Value + ".pdf";
                string Path = "~/Doc_Uploads/" + str_filename;
                if (!File.Exists(Server.MapPath(Path)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('file not found.')", true);
                    return;
                }
                string wordDocName1 = "../Doc_Uploads/" + str_filename;
                Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");

            }
            else if (option == "attach")
            {
                if (file_upload_itin.PostedFile.FileName != "")
                {
                    file_upload_itin.SaveAs(Server.MapPath("~/Doc_Uploads/") + "ctrc_" + tour_id.Value + ".pdf");
                }
            }
        }
        #endregion modContract

        protected void gv_div_fin_info_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string part = e.CommandArgument.ToString();

            try
            {
                if (e.CommandName == "inv_id")
                {
                    //string str_id = Convert.ToString(e.CommandArgument);
                    //string[] str_split = str_id.Split(';');
                    //string amt = str_split[0].Replace("$", "");
                    //Obj_global_Mytour_Login.inv_amt = Convert.ToDecimal(amt);

                    //int_invoice_id = Convert.ToInt32(str_split[1]); --- -for selection stop sanjay

                    //HttpCookie invoice_id = new HttpCookie("CustomerFacing_invoice");
                    //invoice_id.Value = e.CommandArgument.ToString();
                    //Response.Cookies.Add(invoice_id);
                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void btn_ititinerary_Click(object sender, EventArgs e)
        {
            if (file_upload_ititinerary.PostedFile.FileName != "")
            {
                file_upload_ititinerary.SaveAs(Server.MapPath("~/Doc_Uploads/") + "itin_" + tour_id.Value + ".pdf");
            }
        }

        protected void btn_ititinerary_view_Click(object sender, EventArgs e)
        {
            docDownload("itin");
        }
    }
}