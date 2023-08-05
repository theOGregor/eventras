using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using SchoolToursData.Object;
using System.Data;
using SchoolToursBusiness;

namespace SchoolTours
{
    public partial class SiteMaster : MasterPage
    {

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
            //if (Session.Keys.Count == 0)
            //{
            //    try
            //    {
            //        Response.Redirect("../login.aspx");
            //    }
            //    catch (Exception ex)
            //    {
            //        Response.Redirect("/login.aspx");
            //    }
            //}
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                }
                catch (Exception ex) { }
                if (Session["emp_id"] == null)
                {
                    Response.Redirect("/Login.aspx");
                }
                if (!string.IsNullOrEmpty(Session["given_nm"] as string))
                {
                    lbl_given_nm.Text = (Session["given_nm"].ToString());
                }

                checkRole();
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }


        public void checkRole()
        {
            Obj_DTL_ITEM obj = new Obj_DTL_ITEM();
            string Url = HttpContext.Current.Request.RawUrl;
            try
            {
                string[] splitUrl = Url.Split('/');
                Url = splitUrl[splitUrl.Length - 1] + ".html";
                if (Url.ToLower().IndexOf('?') != -1)
                {
                    Url = Url.Substring(0, Url.LastIndexOf("?") + 0) + ".html";
                }

            }
            catch (Exception ex)
            {

            }



            obj.id1 = Session["emp_id"].ToString();
            obj.mode = "validate";
            obj.str1 = Url;

            DataSet ds = DTL_ITEM_Business.GetItemDetails(obj);

            div_footer_applicationsetting.Visible = true;
            div_applicationsetting_panel.Visible = true;
            div_footer_Back.Visible = false;
            lbl_page_name.Text = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                int Res = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                if (Url == "bounced.html")
                {

                }
                else if (Res == 0)
                {
                    Response.Redirect("/Login.aspx");
                }
            }
            else {
                Response.Redirect("/Login.aspx");
            }

            if (Url == "rev_lookup.html")
            {
                div_menu_section.Visible = false;

                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Reverse Lookup";

            }
            else if (Url == "rpt_sales_prod.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Sales Productivity Report";
            }
            else if (Url == "lst_gen.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "List Generator";
            }
            else if (Url == "mass_mailer.html")
            {
                div_footer_applicationsetting.Visible = false;
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                div_footer_Back.Visible = true;
                lbl_BackPageName.Text = "LIST GENERATOR";
                lbl_page_name.Text = "Mass Mailer";
            }
            else if (Url == "mailer.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Mailer";

                div_footer_applicationsetting.Visible = false;
                div_footer_Back.Visible = true;

                string prevPage = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();
                string[] splitUrl = prevPage.Split('/');
                Url = splitUrl[splitUrl.Length - 1];
                lbl_BackPageName.Text = Url;

            }
            else if (Url == "app_profile.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "profile";

                div_footer_applicationsetting.Visible = true;
                div_applicationsetting_panel.Visible = false;
                div_footer_Back.Visible = false;
            }
            else if (Url == "app_profile.html" || Url == "app_emp_div.html" || Url == "app_tags.html" || Url == "app_ref.html" || Url == "app_page_role.html" || Url == "app_evt.html" || Url == "app_emp_role.html" || Url == "bounced.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "APPLICATION SETTINGS";

                div_footer_applicationsetting.Visible = true;
                div_applicationsetting_panel.Visible = false;
                div_footer_Back.Visible = false;
            }

            else if (Url == "ops.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Ops Central";
            }
            else if (Url == "Defect.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                div_footer_applicationsetting.Visible = false;
                div_footer_Back.Visible = true;

                lbl_page_name.Text = "Defect Tracking";
            }
            #region Operation
            else if (Url == "tour_details.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;

                lbl_page_name.Text = "Ops Central -" + "Details";

                return;
            }
            else if (Url == "pax.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Ops Central -" + "pax";
                return;
            }
            else if (Url == "contract.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Ops Central -" + "DEADLINES";
                return;
            }
            else if (Url == "cost_grp.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Ops Central -" + "Group Costs";
                return;
            }
            else if (Url == "cost_ind.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Ops Central -" + "Individual Costs";
                return;
            }
            else if (Url == "billing_grp.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Ops Central -" + "billing";
                return;
            }
            else if (Url == "billing_ind.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Ops Central -" + "billing";
                return;
            }
            else if (Url == "acc_evt_summary.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Event Summary";
                return;
            }
            else if (Url == "acc_invpmt_summary.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Invoice/Payment Summary";
                return;
            }
            #endregion Operation

            #region Accounting
            else if (Url == "pmt_finder.html")
            {
                div_menu_section.Visible = false;
                div_clock_only.Visible = true;
                div_clock.Visible = false;
                lbl_page_name.Text = "Payment Finder";
                return;
            }
            #endregion Accounting

        }

        protected void img_footer_Back_Click(object sender, ImageClickEventArgs e)
        {

            string Url = HttpContext.Current.Request.RawUrl;
            try
            {
                string[] splitUrl = Url.Split('/');
                Url = splitUrl[splitUrl.Length - 1] + ".html";
                if (Url.ToLower().IndexOf('?') != -1)
                {
                    Url = Url.Substring(0, Url.LastIndexOf("?") + 0) + ".html";
                }
            }
            catch (Exception ex)
            {

            }
            object refUrl = ViewState["RefUrl"];
            if (refUrl == null)
            {
                Response.Redirect("~/dashboard");
            }
            if (Url == "Defect.html")
            {
                Response.Redirect((string)refUrl);
            }
            else if (Url == "mass_mailer.html")
            {
                Response.Redirect("~/Sales/lst_gen");
            }
            else if (Url == "mailer.html")
            {
                // if (refUrl != null)
                Response.Redirect((string)refUrl);
            }
        }

        //protected void btn_enterKey_Click(object sender, EventArgs e)
        //{
        //    Sales.sales obj = new Sales.sales();
        //    obj.tagEntityCreate();
        //    obj.tagPersonCreate();
        //}
    }

}