using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolToursBusiness;
using SchoolToursData.Object;
using System.Data;

namespace SchoolTours.ApplicationsSettings
{
    public partial class bounced : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void img_delete_Click(object sender, ImageClickEventArgs e)
        {
            //del_eMail() The user will paste a comma separated list into the textbox. When the user clicks the Delete button, the application should parse the list into separate items using the comma as the delimiter.
            //Each eMail address should also be trimmed of white space. For each item in the resulting array, the app should execute pr_mailer(‘bounced’, null, null, @eMail_address). 
            //    The recordset will be 1 if the record was found and deleted, or 0 if no matching address was found. Build an array of any eMail addresses that were not deleted and place 
            //    this list back in the eMail_string textbox. Pop up a message saying either “All eMail addresses were deleted,” or “The following eMail addresses were not deleted.”
            string Response = "All eMail addresses were deleted";
            string email = input_eMail_string.Text.Trim();
            if (email != "")
            {
                string[] email_split = email.Split(',');
                string NotfoundEmail = "";
                for (int i = 0; i < email_split.Length; i++)
                {
                    Obj_PR_MAILER obj = new Obj_PR_MAILER();
                    obj.mode = "bounced";
                    obj.str1 = email_split[i].Trim();
                    DataTable dt = DTL_ITEM_Business.Get_PR_MAILER(obj).Tables[0];
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        // Response = "The following eMail addresses were not deleted";
                        NotfoundEmail += email_split[i].Trim() + ",";
                    }
                    else
                    {

                    }
                }
                if (NotfoundEmail != "")
                {
                    NotfoundEmail = NotfoundEmail.Remove(NotfoundEmail.Length - 1, 1);
                    Response = "The following eMail addresses were not deleted: " + NotfoundEmail;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Response + "')", true);
            }
            else {

            }
        }
    }
}