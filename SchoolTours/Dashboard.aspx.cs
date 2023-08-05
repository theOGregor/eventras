using SchoolToursBusiness;
using SchoolToursData.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.IO;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;

namespace SchoolTours
{
    public partial class Dashboard : System.Web.UI.Page
    {
        static int hotlist_id = 0;
        static int method_id = 0;
        static int person_id = 0;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count != 0)
                {
                    div_entity_panel.Visible = false;
                    onLoad();
                }
            }
        }
        public void onLoad()
        {
            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();

            string Url = HttpContext.Current.Request.RawUrl;

            string[] splitUrl = Url.Split('/');
            Url = splitUrl[splitUrl.Length - 1] + ".html";

            obj.id1 = Session["emp_id"].ToString();
            obj.mode = "dashboard";
            obj.str1 = Url;

            DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

            if (ds != null && ds.Tables.Count > 0)
            {
                lbl_current_inv.Text = Convert.ToString(ds.Tables[0].Rows[0]["sum_curr_inv"]);
                lbl_past_inv.Text = Convert.ToString(ds.Tables[0].Rows[0]["sum_past_inv"]);
                lbl_hotlist.Text = Convert.ToString(ds.Tables[0].Rows[0]["sum_hotlist"]);
                lbl_contract.Text = Convert.ToString(ds.Tables[0].Rows[0]["sum_contract"]);
                Session["given_nm"] = Convert.ToString(ds.Tables[0].Rows[0]["given_nm"]);

                shwPanel("contract");
                shwPanel("hotlist");
                shwPanel("past_inv");
                shwPanel("curr_inv");
            }
        }

        public void shwPanel(string value)
        {
            try
            {
                Obj_LST_ITEMS Obj = new Obj_LST_ITEMS();
                Obj.mode = "panel";
                Obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());
                Obj.str1 = value;
                Obj.str2 = "";
                Obj.str3 = "";

                DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(Obj).Tables[0];
                if (value == "curr_inv")
                {
                    if (dt.Rows.Count > 0)
                    {
                        gv_curr_inv.DataSource = dt;
                        gv_curr_inv.DataBind();
                    }
                }
                else if (value == "past_inv")
                {
                    if (dt.Rows.Count > 0)
                    {
                        gv_past_inv.DataSource = dt;
                        gv_past_inv.DataBind();
                    }
                }
                else if (value == "hotlist")
                {
                    try
                    {
                        string HotlistDate = System.DateTime.Now.Date.ToShortDateString();
                        lblHotlistDate.Text = HotlistDate;
                    }
                    catch (Exception ex)
                    {
                        lblHotlistDate.Text = System.DateTime.Now.ToString();
                    }
                    //if (dt.Rows.Count > 0)
                    //{

                    gv_hotlist.DataSource = dt;
                    gv_hotlist.DataBind();
                    //}
                }

                else if (value == "contract")
                {
                    if (dt.Rows.Count > 0)
                    {
                        gv_contract.DataSource = dt;
                        gv_contract.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_curr_inv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            div_entity_panel.Visible = false;
            try
            {
                if (e.CommandName == "sndItem")
                {
                    string str_id = Convert.ToString(e.CommandArgument);
                    string[] str_split = str_id.Split(';');

                    int inv_id = Convert.ToInt32(str_split[0]);

                    if (str_split[1] == "0")
                    {
                        //If type = ‘curr_inv’and ind_ind =0, execute pr_accounting(‘invoice’, @inv_id) which returns multiple recordsets.
                        //Use these recordsets to create an invoice and save it as a PDF(see Group Billing, genInv() below).Redirect the browser to mailer.aspx? type = curr_inv & id = inv_id.
                        genInv("invoice", inv_id, "sndItem");
                        Response.Redirect("mailer?type=curr_inv&id=" + str_split[0]);
                    }
                    else {
                        //If type =‘curr_inv’ and ind_ind = 1, execute pr_accounting(‘invoice_ind’, @inv_id) which returns two recordsets.   
                        //Use these recordsets to create multiple invoices and save them as a PDF(see Independent Billing, genInv() below).Multiple invoices will be sent automatically via   eMail.
                        genInv_ind("invoice_ind", inv_id, "sndItem");
                    }
                }
                else if (e.CommandName == "prnItem")
                {
                    string str_id = Convert.ToString(e.CommandArgument);
                    string[] str_split = str_id.Split(';');

                    int inv_id = Convert.ToInt32(str_split[0]);
                    if (str_split[1] == "0")
                    {
                        genInv("invoice", inv_id, "prnItem");
                    }
                    else
                    {
                        //genInv("invoice_ind", inv_id, "prnItem");
                        genInv("invoice", inv_id, "prnItem");
                    }
                }
                else {
                    return;
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void gv_past_inv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            div_entity_panel.Visible = false;
            try
            {
                if (e.CommandName == "sndItem")
                {
                    string str_id = Convert.ToString(e.CommandArgument);
                    string[] str_split = str_id.Split(';');
                    if (str_split[1] == "0")
                    {
                        Response.Redirect("mailer?type=past_inv&id=" + str_split[0]);
                    }
                    else {
                        Response.Redirect("mailer?type=past_inv_ind&id=" + str_split[0]);
                    }
                }
                else if (e.CommandName == "prnItem")
                {
                    string str_id = Convert.ToString(e.CommandArgument);
                    string[] str_split = str_id.Split(';');

                    int inv_id = Convert.ToInt32(str_split[0]);
                    if (str_split[1] == "0")
                    {
                        genInv("invoice", inv_id, "prnItem");
                    }
                    else
                    {
                        //genInv("invoice_ind", inv_id, "prnItem");
                        genInv("invoice", inv_id, "prnItem");
                    }
                }
                else {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_contract_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            div_entity_panel.Visible = false;
            try
            {
                if (e.CommandName == "sndItem")
                {
                    string str_id = Convert.ToString(e.CommandArgument);

                    Response.Redirect("mailer?type=contract&id=" + str_id);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void gv_hotlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //div_entity_panel.Visible = false;
            try
            {
                if (e.CommandName == "hotlist_id")
                {
                    string str_id = Convert.ToString(e.CommandArgument);
                    string[] str_split = str_id.Split(';');
                    hotlist_id = Convert.ToInt32(str_split[0]);
                    try
                    {
                        method_id = Convert.ToInt32(str_split[1]);
                    }
                    catch (Exception ex) { }
                    div_Re_contact_popup.Visible = true;
                }
                if (e.CommandName == "sndItem")
                {
                    string str_id = Convert.ToString(e.CommandArgument);

                    Response.Redirect("mailer?type=person&id=" + str_id);
                }
                else if (e.CommandName == "sndpersonId")
                {

                    int str_id = Convert.ToInt32(e.CommandArgument);
                    if (str_id > 0)
                    {
                        person_id = str_id;
                        div_hotlist_summary.Visible = false;
                        div_entity_panel.Visible = true;
                        dtlEntity(str_id);
                    }

                }
                else if (e.CommandName == "cancelHotlist")
                {
                    string str_id = Convert.ToString(e.CommandArgument);

                    cancelHotlist(str_id);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void dtlEntity(int entity_id)
        {
            try
            {
                div_entity_panel.Visible = true;
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                obj.mode = "person";
                obj.id1 = Convert.ToString(entity_id);
                obj.str1 = "";
                obj.str2 = "";

                DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

                DataTable dt_person = ds.Tables[0];
                DataTable dt_tags = ds.Tables[1];


                input_person_greeting.Text = dt_person.Rows[0]["greeting"].ToString() + " " + dt_person.Rows[0]["given_nm"].ToString() + " " + dt_person.Rows[0]["last_nm"].ToString();
                input_person_address.Text = dt_person.Rows[0]["address"].ToString() + " " + dt_person.Rows[0]["city"].ToString() + " " + dt_person.Rows[0]["state"].ToString();
                input_person_zip.Text = dt_person.Rows[0]["zip"].ToString();
                input_person_landline.Text = phoneformatting(dt_person.Rows[0]["office_phone"].ToString());
                input_person_cell_phone.Text = phoneformatting(dt_person.Rows[0]["cell_phone"].ToString());
                input_person_eMail.Text = dt_person.Rows[0]["eMail"].ToString();

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
                objN.id1 = (entity_id);

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
            div_hotlist_summary.Visible = true;
            div_entity_panel.Visible = false;

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('#div_hotlist_summary').modal('show');</script>", true);

        }
        public void cancelHotlist(string hotlist_id)
        {

            //cancelHotlist(hotlist_id) execute pr_set_item (‘hotlist_cancel’, @hotlist_id, @emp_id). Execute
            //shwPanel(‘hotlist’) to refresh the list in the panel.

            try
            {
                Obj_SET_ITEM obj = new Obj_SET_ITEM();

                obj.mode = "hotlist_cancel";
                obj.id1 = Convert.ToInt32(hotlist_id); ;
                obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());

                int response = DTL_ITEM_Business.Put_SET_ITEM(obj);

                shwPanel("hotlist");
            }            catch (Exception ex)            {

            }
        }

        public void compHotlist(object sender, EventArgs e)
        {
            Obj_SET_ITEM obj = new Obj_SET_ITEM();

            obj.mode = "hotlist_complete";
            obj.id1 = hotlist_id;
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());


            DataTable dt = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];

            if (Convert.ToInt32(dt.Rows[0]["rc"].ToString()) > 0)
            {
                div_Re_contact_popup.Visible = false;
                hotlist_id = 0;
                onLoad();
                shwPanel("hotlist");
            }
            else
            {
                string err_msg = "\"" + dt.Rows[0]["err_msg"].ToString() + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                return;
            }
        }
        public void schedHotlist(object sender, EventArgs e)
        {
            // schedHotlist(hotlist_id,input_nbr,input_type.value) 


            Obj_SET_ITEM obj = new Obj_SET_ITEM();

            obj.mode = "hotlist_update";
            obj.id1 = hotlist_id;
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            obj.id3 = Convert.ToInt32(select_hotlist_method.SelectedValue) * Convert.ToInt32(input_nbr.Text);
            obj.id4 = method_id;

            DataTable dt = DTL_ITEM_Business.Put_SET_ITEM_DS(obj).Tables[0];

            if (Convert.ToInt32(dt.Rows[0]["rc"].ToString()) > 0)
            {
                div_Re_contact_popup.Visible = false;
                hotlist_id = 0;
                method_id = 0;
                select_hotlist_method.SelectedValue = "1";
                input_nbr.Text = "";
                onLoad();
                shwPanel("hotlist");

            }
            else
            {
                string err_msg = "\"" + dt.Rows[0]["err_msg"].ToString() + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);

            }

        }
        //============================================================== Show pop 
        #region  Show pop
        protected void lnk_c_invoice_Click(object sender, EventArgs e)
        {
            div_current_inv_summary.Visible = true;
            shwPanel("curr_inv");
        }

        protected void lnk_Hotlist_Click(object sender, EventArgs e)
        {
            div_hotlist_summary.Visible = true;
            shwPanel("hotlist");
        }

        protected void lnk_PastDue_inv_Click(object sender, EventArgs e)
        {
            div_past_inv_summary.Visible = true;
            shwPanel("past_inv");
        }

        protected void lnk_Contract_Thresholds_Click(object sender, EventArgs e)
        {
            div_contract_summary.Visible = true;
            shwPanel("contract");
        }

        #endregion  Show pop

        //=========== Close POPup
        protected void btn_close_Click(object sender, ImageClickEventArgs e)
        {
            div_current_inv_summary.Visible = false;
            div_hotlist_summary.Visible = false;
            div_past_inv_summary.Visible = false;
            div_contract_summary.Visible = false;
            onLoad();
        }
        //=========== Close Hotlist POPup
        protected void img_delete_hotlist_Click(object sender, ImageClickEventArgs e)
        {

            //First confirm that the user wants to complete this action. On confirm, clear all fields
            //in the panel. Execute pr_del_item(‘hotlist’, @hotlist_id, @emp_id).

            Obj_DEL_ITEM obj = new Obj_DEL_ITEM();
            obj.mode = "hotlist";
            obj.id1 = Convert.ToInt32(hotlist_id);
            obj.id2 = Convert.ToInt32(Session["emp_id"].ToString());
            int response = DTL_ITEM_Business.del_DEL_ITEM(obj);
            onLoad();
            shwPanel("hotlist");
            div_Re_contact_popup.Visible = false;
            hotlist_id = 0;

        }

        protected void img_person_eMail_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("mailer?type=person&id=" + person_id);
        }

        //================================================================ Create pdf

        public void genInv(string Type, int inv_id, string TypeMode)
        {

            Obj_PR_ACCOUNTING obj = new Obj_PR_ACCOUNTING();
            obj.mode = Type;
            //obj.id1 = null;
            obj.id2 = inv_id;
            DataSet ds = DTL_ITEM_Business.Get_PR_ACCOUNTING(obj);
            DataTable dt_filename = ds.Tables[0];

            string str_filename = dt_filename.Rows[0]["inv_filename"].ToString();

            if (dt_filename.Rows[0]["rc"].ToString() == "-1")
            {

            }

            else if (dt_filename.Rows[0]["rc"].ToString() == "1")
            {
                DataTable dt_image = ds.Tables[1];
                if (dt_image.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('information missing.')", true);
                    return;
                }
                DataTable dt_Customer = ds.Tables[2];
                DataTable dt_inv = ds.Tables[3];

                StringBuilder SB = new StringBuilder();

                string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
                string strUrl = "";
                // if (url[2] == "localhost:2108")
                if (url[2].Contains("43.229.227.26"))
                {
                    strUrl = "http://" + url[2] + "/schoolTours/Images/";
                }
                else
                {
                    strUrl = "http://" + url[2] + "/Images/";
                }

                strUrl = strUrl + dt_image.Rows[0]["img"].ToString();

                #region New
                SB.Append("<html lang=\"en\">");
                SB.Append("<head>");
                SB.Append("<title>PDF</title>");
                SB.Append("</head>");
                SB.Append("<body>");
                SB.Append("<div class=\"page\">");
                SB.Append("<table style=\"border:1px solid transparent; border-collapse: separate; border-spacing: 20px 0.8rem; width: 100%;font-size: 12px;\"> ");
                SB.Append("<thead>");
                SB.Append("<tr>");
                SB.Append("<th colspan=\"5\" style=\"border:1px dashed black;padding: 20px;\"><img src='" + strUrl + "' alt=\"Smiley face\" height =\"60\"/></th>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"border: 1px solid black;padding: 10px;text-align: center;\">");
                SB.Append(" 7255 E Hampton Ave - Suite 127 - Mesa, AZ 85209 - " + dt_image.Rows[0]["local_phone"].ToString() + " - " + dt_image.Rows[0]["toll_free_phone"].ToString());
                SB.Append("</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td colspan=\"3\" style=\"text-align: left; vertical-align: middle;\">");
                SB.Append("<b>Customer ID :</b> " + dt_Customer.Rows[0]["customer_id"].ToString() + " <br/>");
                SB.Append("<b>" + dt_Customer.Rows[0]["person_nm"].ToString() + "</b> <br/>");
                SB.Append("<p style=\"width: 300px;\">" + dt_Customer.Rows[0]["entity_descr"].ToString() + "</p>");
                SB.Append("<p>" + dt_Customer.Rows[0]["address"].ToString() + "</p>");
                SB.Append("<p>" + dt_Customer.Rows[0]["entity_csz"].ToString() + "</p>");
                SB.Append("</td>");
                SB.Append("<td colspan=\"2\" style=\"text-align: right; vertical-align: middle;\">");
                SB.Append("<h4 style=\"font-size: 16px;\">Progressive Invoices</h4>");
                SB.Append("<b>Invoice Date :</b>" + dt_image.Rows[0]["inv_date"].ToString() + " <br/>");
                SB.Append("<b>Invoice Number :</b> " + dt_inv.Rows[0]["inv_id"].ToString() + " <br/>");
                SB.Append("</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"border-bottom: 1px solid black;padding: 10px;text-align: center;\">Please detach and return with payment</td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding-top: 3em; padding-bottom: 2em;\"></td>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<th colspan=\"5\">");
                SB.Append("<table style=\"border:1px solid transparent; border-collapse: collapse; border-spacing: 20px 0.8rem; width: 100%;font-size: 12px;\">");
                SB.Append("<thead style=\"border:1px solid black;\">");
                SB.Append("<tr>");
                SB.Append("<th style=\"padding:5px 10px; text-align: left;border:1px solid black;\">Due Date</th>");
                SB.Append("<th style=\"padding:5px 10px; text-align: left;border:1px solid black;\">Description</th>");
                SB.Append("<th style=\"padding:5px 10px; text-align: right;border:1px solid black;\">Amount</th>");
                SB.Append("</tr>");
                SB.Append("</thead>");
                SB.Append("<tbody>");
                for (int i = 0; i < dt_inv.Rows.Count; i++)
                {
                    SB.Append("<tr>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: left;\">" + dt_inv.Rows[i]["due_date"].ToString() + "</td>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: left;\">" + dt_inv.Rows[i]["inv_descr"].ToString() + "</td>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: right;\">" + dt_inv.Rows[i]["inv_amt"].ToString() + "</td>");
                    SB.Append("</tr>");
                }
                SB.Append("</tbody>");
                SB.Append("</table>");
                SB.Append("</th>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding: 10px 0 0 0;\">Memo: " + dt_inv.Rows[0]["memo"].ToString() + "</td>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding: 20px 0 0 0;\">The terms contained in the letter of agreement supersede information on the invoice.</td>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding: 10px 0 0 0;\">Please pay the amount due as shown on the ACCOUNT STATEMENT</td>");
                SB.Append("</tr>");
                SB.Append("</thead>");
                SB.Append("</table>");
                SB.Append("</div>");
                SB.Append("</body>");
                SB.Append("</html>");
                #endregion New


                string strPDf = SB.ToString();

                GeneratePDFReport("INVOICES", str_filename, "", strPDf);
            }

            if (TypeMode == "sndItem")
                return;
            else {
                string wordDocName1 = "/INVOICES/" + str_filename;

                if (!File.Exists(Server.MapPath(wordDocName1)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice is not yet generated.')", true);
                    return;
                }
                else {
                    Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                    return;
                }
            }
        }

        public void genInv_ind(string Type, int inv_id, string TypeMode)
        {
            StringBuilder SB = new StringBuilder();
            DataSet ds = null;
            Obj_PR_ACCOUNTING obj = new Obj_PR_ACCOUNTING();

            obj.mode = "invoice_ind";
            obj.id2 = inv_id;
            ds = DTL_ITEM_Business.Get_PR_ACCOUNTING(obj);


            DataTable dt_filename = ds.Tables[0];
            DataTable dt_image = ds.Tables[1];

            if (dt_image.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Details not found.')", true);
                return;
            }
            DataTable dt_Customer = ds.Tables[2];
            DataTable dt_inv = ds.Tables[3];
            string str_filename = dt_filename.Rows[0]["inv_filename"].ToString();

            if (dt_filename.Rows[0]["rc"].ToString() == "-1")
            {
                return;
            }
            else {

                string[] url = (System.Web.HttpContext.Current.Request.Url.AbsoluteUri).Split('/');
                string strUrl = "";
                // if (url[2] == "localhost:2108")
                if (url[2].Contains("43.229.227.26"))
                {
                    strUrl = "http://" + url[2] + "/schoolTours/Images/";
                }
                else
                {
                    strUrl = "http://" + url[2] + "/Images/";
                }

                strUrl = strUrl + dt_image.Rows[0]["img"].ToString();

                #region New
                SB.Append("<html lang=\"en\">");
                SB.Append("<head>");
                SB.Append("<title>PDF</title>");
                SB.Append("</head>");
                SB.Append("<body>");
                SB.Append("<div class=\"page\">");
                SB.Append("<table style=\"border:1px solid transparent; border-collapse: separate; border-spacing: 20px 0.8rem; width: 100%;font-size: 12px;\"> ");
                SB.Append("<thead>");
                SB.Append("<tr>");
                SB.Append("<th colspan=\"5\" style=\"border:1px dashed black;padding: 20px;\"><img src='" + strUrl + "' alt=\"Smiley face\" height =\"60\"/></th>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"border:1px solid black;padding: 10px;\">");
                SB.Append(" 7255 E Hampton Ave - Suite 127 - Mesa, AZ 85209 - " + dt_image.Rows[0]["local_phone"].ToString() + " - " + dt_image.Rows[0]["toll_free_phone"].ToString());
                SB.Append("</td>");
                SB.Append("</tr>");



                SB.Append("<tr><td style=\"color: white;\">.</td><td></td></tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"3\" style=\"text-align: left; vertical-align: middle;\">");
                SB.Append("<b>Customer ID :</b> " + dt_Customer.Rows[0]["customer_id"].ToString() + " <br/>");
                SB.Append("<b>" + dt_Customer.Rows[0]["person_nm"].ToString() + "</b> <br/>");
                SB.Append("<p style=\"width: 300px;\">" + dt_Customer.Rows[0]["entity_descr"].ToString() + "</p>");
                SB.Append("<p>" + dt_Customer.Rows[0]["address"].ToString() + "</p>");
                SB.Append("<p>" + dt_Customer.Rows[0]["entity_csz"].ToString() + "</p>");
                SB.Append("</td>");
                SB.Append("<td colspan=\"2\" style=\"text-align: right; vertical-align: middle;\">");
                SB.Append("<h4>Progressive Invoices</h4>");
                SB.Append("<b>Invoice Date :</b>" + dt_image.Rows[0]["inv_date"].ToString() + " <br/>");
                SB.Append("<b>Invoice Number :</b> " + dt_inv.Rows[0]["inv_id"].ToString() + " <br/>");
                SB.Append("</td>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding-top: 3em; padding-bottom: 2em;\"></td>");
                SB.Append("</tr>");

                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"border-bottom:1px solid black;padding: 10px;text-align: center;\">Please detach and return with payment</td>");
                SB.Append("</tr>");


                SB.Append("<tr>");
                SB.Append("<th colspan=\"5\">");
                SB.Append("<table style=\"border:1px solid transparent; border-collapse: collapse; border-spacing: 20px 0.8rem; width: 100%;font-size: 12px;\">");
                SB.Append("<thead style=\"border:1px solid black;\">");
                SB.Append("<tr>");
                SB.Append("<th style=\"padding:5px 10px; text-align: left;border:1px solid black;\">Due Date</th>");
                SB.Append("<th style=\"padding:5px 10px; text-align: left;border:1px solid black;\">Description</th>");
                SB.Append("<th style=\"padding:5px 10px; text-align: right;border:1px solid black;\">Amount</th>");
                SB.Append("</tr>");
                SB.Append("</thead>");
                SB.Append("<tbody>");
                for (int i = 0; i < dt_inv.Rows.Count; i++)
                {
                    SB.Append("<tr>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: left;\">" + dt_inv.Rows[i]["due_date"].ToString() + "</td>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: left;\">" + dt_inv.Rows[i]["inv_descr"].ToString() + "</td>");
                    SB.Append("<td style=\"padding:5px 10px; text-align: right;\">" + dt_inv.Rows[i]["inv_amt"].ToString() + "</td>");
                    SB.Append("</tr>");
                }
                SB.Append("</tbody>");
                SB.Append("</table>");
                SB.Append("</th>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding: 10px 0 0 0;\">Memo: " + dt_inv.Rows[0]["memo"].ToString() + "</td>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding: 20px 0 0 0;\">The terms contained in the letter of agreement supersede information on the invoice.</td>");
                SB.Append("</tr>");
                SB.Append("<tr>");
                SB.Append("<td colspan=\"5\" style=\"padding: 10px 0 0 0;\">Please pay the amount due as shown on the ACCOUNT STATEMENT</td>");
                SB.Append("</tr>");
                SB.Append("</thead>");
                SB.Append("</table>");
                SB.Append("</div>");
                SB.Append("</body>");
                SB.Append("</html>");
                #endregion New

                string strPDf = SB.ToString();

                GeneratePDFReport("INVOICES", str_filename, "", strPDf);
            }

            if (TypeMode == "sndItem")
            {

            }
            else {
                string wordDocName1 = "/INVOICES/" + str_filename;

                if (!File.Exists(Server.MapPath(wordDocName1)))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invoice is not yet generated.')", true);
                    return;
                }
                else {
                    Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                    return;
                }
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

        protected void img_Cancel_hotlist_Click(object sender, ImageClickEventArgs e)
        {
            div_Re_contact_popup.Visible = false;
        }
    }
}