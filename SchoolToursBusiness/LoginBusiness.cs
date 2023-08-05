using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolToursData.Object;
using System.Data;
using SchoolToursData.Data;

namespace SchoolToursBusiness
{
    public class LoginBusiness
    {
        public static DataSet GetLoginDetail(ObjLogin Obj)
        {
            DataSet ds = LoginData.GetLoginDetail(Obj);
            if (Obj.mode == "reset" || Obj.mode == "mytour_reset")
            {
                UtilityBusiness UB = new UtilityBusiness();
                string passcode = Convert.ToString(ds.Tables[0].Rows[0]["passcode"]);

                StringBuilder SB = new StringBuilder();

                #region tBody
                SB.Append("<table width=\"100%\">");
                if (Obj.mode == "reset")
                    SB.Append("<tr><td>You requested a password reset for Minimizer. Please use the following code to reset your Temporary password : " + passcode + "</td>");
                else
                    SB.Append("<tr><td>You requested a password reset for MyTour. Please use the following code to reset your Temporary password : " + passcode + "</td>");

                SB.Append("</table>");
                #endregion tBody

                string tBody = SB.ToString();
                string aa = UB.SendMail(Obj.eMail, "Reset password", tBody);
            }
            return ds;
        }
    }
}
