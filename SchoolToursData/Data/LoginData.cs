using SchoolToursData.Object;
using SchoolToursFramework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolToursData.Data
{
    public class LoginData
    {
        public static DataSet GetLoginDetail(ObjLogin Obj)
        {
            DataSet objDataSet = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("mode", Obj.mode);
                parameters[1] = new SqlParameter("eMail", Obj.eMail);
                parameters[2] = new SqlParameter("passcode", Obj.passcode);
                parameters[3] = new SqlParameter("tempcode", Obj.tempcode);
                parameters[4] = new SqlParameter("ind_ind", Obj.ind_ind);
                objDataSet = DataManager.ExecuteDataSet("SchoolTours", "PR_LOGIN", parameters.ToArray());
            }
            catch (Exception ex)
            {

            }

            return objDataSet; //Successful
        }
    }
}
