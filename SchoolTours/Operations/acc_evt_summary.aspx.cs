using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursData.Object;
using SchoolToursBusiness;
using System.Data;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Net;

namespace SchoolTours.Operations
{
    public partial class acc_evt_summary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { OnLoad(); }
        }

        public void OnLoad()
        {
            //Execute pr_lst_items(‘division’, @emp_id) which returns a multiple row recordset with the following columns: 1.div_id, 2.div_nm.Use these values to populate select_div. 
            //● The select_year object should programmatically contain the current year and the four previous years. The current year should be selected. 
            Obj_LST_ITEMS obj = new Obj_LST_ITEMS();

            obj.mode = "division";
            obj.id1 = Convert.ToInt32(Session["emp_id"].ToString());

            DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];

            select_div.DataSource = dt;
            select_div.DataTextField = "div_nm";
            select_div.DataValueField = "div_id";
            select_div.DataBind();
            select_div.Items.Insert(0, "Division");

            int CurrentYear = Convert.ToInt32(DateTime.Now.Year);

            select_year.Items.Insert(0, (CurrentYear - 3).ToString());
            select_year.Items.Insert(1, (CurrentYear - 2).ToString());
            select_year.Items.Insert(2, (CurrentYear - 1).ToString());
            select_year.Items.Insert(3, CurrentYear.ToString());
            select_year.Items.Insert(4, (CurrentYear + 1).ToString());
            select_year.Items.Insert(5, (CurrentYear + 2).ToString());
            select_year.Items.Insert(6, (CurrentYear + 3).ToString());
            select_year.SelectedValue = CurrentYear.ToString();
        }


        public void lstEvts(object sender, EventArgs e)
        {
            // execute pr_search(‘div_evt’, @div_id, @year) which returns a multiple row recordset with the following columns: 1. evt_id, 2. evt_descr. Use these values to populate select_evt. 

            if (select_div.SelectedValue != "Division")
            {
                Obj_LST_ITEMS obj = new Obj_LST_ITEMS();
                obj.mode = "div_evts_year";
                obj.id1 = Convert.ToInt32(select_div.SelectedValue);
                obj.str1 = select_year.SelectedValue;
                DataTable dt = DTL_ITEM_Business.Get_LST_ITEMS(obj).Tables[0];


                select_evt.DataSource = dt;
                select_evt.DataTextField = "evt_nm";
                select_evt.DataValueField = "evt_id";
                select_evt.DataBind();
                select_evt.Items.Insert(0, "Event");
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select div first.')", true);
            }
        }
        public void dtlEvt(object sender, EventArgs e)
        {
            //execute pr_dtl_item(‘evt_summary’, @evt_id, @emp_id, ‘summary’) which returns a multiple row recordset containing the following columns. 
            //Use these columns to populate the data grid in div_tours.
            if (select_evt.SelectedValue != "Event")
            {
                Obj_DTL_ITEM obj = new Obj_DTL_ITEM();

                obj.mode = "evt_summary";
                obj.id1 = select_evt.SelectedValue;
                //obj.id1 = "12";
                obj.str1 = Session["emp_id"].ToString();
                obj.str2 = "summary";

                DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                if (!dt.Columns.Contains("rc"))
                {
                    ViewState["dt"] = dt;
                    gv_summary.DataSource = dt;
                    gv_summary.DataBind();
                }
                else
                {
                    gv_summary.DataSource = null;
                    gv_summary.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + dt.Rows[0]["err_msg"].ToString() + "')", true);
                    return;
                }

            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('select evt first.')", true);
            }
        }
        public void prnEvt(object sender, EventArgs e)
        {
            //create a PDF of the current page(as shown below) and download it to the user’s browser

            DataTable dt = (DataTable)ViewState["dt"];

            StringBuilder SB = new StringBuilder();

            SB.Append("<table style=\"border:1px solid;text-align:center; width:100%;\">");
            SB.Append("<tr><td >Event Summary</td></tr>");
            SB.Append("<tr><td >" + select_evt.SelectedItem + "</td></tr>");
            SB.Append("<tr><td >Printed " + DateTime.Now.ToString("dd/MM/yyyy") + "</td></tr>");
            SB.Append("</table>");

            SB.Append("<table style=\"width:100%;\"><tr><td style=\"color: white;\">.</td></tr></table>");
            SB.Append("<table style=\"border:1px solid;text-align:center; width:100%;font-size: 10px;border-collapse:collapse;\" colspan=\"0\" rowspan=\"0\">");
            SB.Append("<thead style=\"background-color: #a9a9a9;\">");
            SB.Append("<tr>");
            SB.Append("<th style=\"border:1px solid;\">CMG-ID</th>");
            SB.Append("<th style=\"border:1px solid;\">Contract</th>");
            SB.Append("<th style=\"border:1px solid;\">Rep</th>");
            SB.Append("<th style=\"border:1px solid;\">Group</th>");
            SB.Append("<th style=\"border:1px solid;\">State</th>");
            SB.Append("<th style=\"border:1px solid;\">Leader</th>");
            SB.Append("<th style=\"border:1px solid;\">POB</th>");
            SB.Append("<th style=\"border:1px solid;\">Free</th>");
            SB.Append("<th style=\"border:1px solid;\">Pax</th>");
            SB.Append("<th style=\"border:1px solid;\">Depart</th>");
            SB.Append("<th style=\"border:1px solid;\">PKG</th>");
            SB.Append("<th style=\"border:1px solid;\">Total Price</th>");
            SB.Append("<th style=\"border:1px solid;\">Paid</th>");
            SB.Append("</tr>");
            SB.Append("</thead>");
            SB.Append("<tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SB.Append("<tr>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["customer_id"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["contract_date"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["producer_nm"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["group_nm"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["state_abbr"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["leader_nm"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["pob_nr"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["free_trip_nr"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["pax_nr"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">" + dt.Rows[i]["start_date"].ToString() + "</td>");
                SB.Append("<td style=\"border:1px solid;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows[i]["cost_amt"].ToString())) + "</td>");
                SB.Append("<td style=\"border:1px solid;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows[i]["total_costs"].ToString())) + "</td>");
                SB.Append("<td style=\"border:1px solid;\">$ " + String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows[i]["total_paid"].ToString())) + " </td>");
                SB.Append("</tr>");
            }
            SB.Append("</tbody>");
            SB.Append("</table>");


            string str_SB = SB.ToString();

            try
            {

                string File_name = "acc_evt_summary.pdf";

                GeneratePDFReport("pdf", File_name, "", str_SB);

                string Path = "~/pdf/" + File_name;
                if (Path != string.Empty)
                {
                    //string wordDocName1 = "../pdf/" + File_name;
                    //Response.Write("<script>window.open('" + wordDocName1 + "','_blank');</script>");
                    WebClient req = new WebClient();
                    HttpResponse response = System.Web.HttpContext.Current.Response;
                    string filePath = Path;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=" + File_name);
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

        protected void gv_summary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "tour_id")
                {
                    string tour_id = Convert.ToString(e.CommandArgument);

                    Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
                    obj.mode = "tour_type";
                    obj.id1 = tour_id;

                    DataTable dt = DTL_ITEM_Business.GetItemDetails(obj).Tables[0];
                    //inv_type_ind = Convert.ToInt32(dt.Rows[0]["inv_type_ind"].ToString());

                    Obj_global_value.div_id = select_div.SelectedValue;
                    Obj_global_value.emp_id = "";
                    Obj_global_value.year_id = select_year.SelectedValue;
                    Obj_global_value.evt_id = select_evt.SelectedValue;

                    Obj_global_value.tour_id = Convert.ToInt32(tour_id);
                    Obj_global_value.active_ind = Convert.ToInt32(dt.Rows[0]["active_ind"].ToString());
                    Obj_global_value.pmt_plan_ind = Convert.ToInt32(dt.Rows[0]["pmt_plan_ind"].ToString());
                    Obj_global_value.pax_ind = Convert.ToInt32(dt.Rows[0]["pax_ind"].ToString());
                    Obj_global_value.flying_ind = Convert.ToInt32(dt.Rows[0]["flying_ind"].ToString());
                    Obj_global_value.cmg_bus_ind = Convert.ToInt32(dt.Rows[0]["cmg_bus_ind"].ToString());
                    Obj_global_value.inv_type_ind = Convert.ToInt32(dt.Rows[0]["inv_type_ind"].ToString());
                    Obj_global_value.final_ind = Convert.ToInt32(dt.Rows[0]["final_ind"].ToString());

                    string RredirectUrl = "";
                    RredirectUrl += "tour_details";
                    Response.Redirect(RredirectUrl);
                }

            }
            catch (Exception ex) {
                string err_msg = "\"" + ex.Message + "\"";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + err_msg + ")", true);
            }
        }
    }
}