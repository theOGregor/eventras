using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolToursData.Object;
using SchoolToursFramework.Data;
using System.Data;
using System.Data.SqlClient;

namespace SchoolToursData.Data
{
    public class DTL_ITEM_Data
    {
        public static DataSet GetItemDetails(Obj_DTL_ITEM Obj)
        {
            DataSet objDataSet = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("mode", Obj.mode);
                parameters[1] = new SqlParameter("id1", Obj.id1);
                parameters[2] = new SqlParameter("str1", Obj.str1);
                parameters[3] = new SqlParameter("str2", Obj.str2);
                objDataSet = DataManager.ExecuteDataSet("SchoolTours", "PR_DTL_ITEM", parameters.ToArray());
            }
            catch (Exception ex)
            {

            }

            return objDataSet; //Successful
        }
        public static DataSet Get_LST_ITEMS(Obj_LST_ITEMS Obj)
        {
            DataSet objDataSet = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("mode", Obj.mode);
                parameters[1] = new SqlParameter("id1", Obj.id1);
                parameters[2] = new SqlParameter("str1", Obj.str1);
                parameters[3] = new SqlParameter("str2", Obj.str2);
                parameters[4] = new SqlParameter("str3", Obj.str3);

                objDataSet = DataManager.ExecuteDataSet("SchoolTours", "PR_LST_ITEMS", parameters.ToArray());
            }
            catch (Exception ex)
            {

            }

            return objDataSet; //Successful
        }
        public static DataSet Get_PR_SEARCH(Obj_PR_SEARCH Obj)
        {
            DataSet objDataSet = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("mode", Obj.mode);
                parameters[1] = new SqlParameter("id1", Obj.id1);
                parameters[2] = new SqlParameter("id2", Obj.id2);
                parameters[3] = new SqlParameter("id3", Obj.id3);
                parameters[4] = new SqlParameter("str1", Obj.str1);
                parameters[5] = new SqlParameter("str2", Obj.str2);
                parameters[6] = new SqlParameter("str3", Obj.str3);
                parameters[7] = new SqlParameter("str4", Obj.str4);

                objDataSet = DataManager.ExecuteDataSet("SchoolTours", "PR_SEARCH", parameters.ToArray());
            }
            catch (Exception ex)
            {

            }

            return objDataSet; //Successful
        }

        public static int Put_SET_ITEM(Obj_SET_ITEM obj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[15];
                parameters[0] = new SqlParameter("mode", obj.mode);
                parameters[1] = new SqlParameter("id1", obj.id1);
                parameters[2] = new SqlParameter("id2", obj.id2);
                parameters[3] = new SqlParameter("id3", obj.id3);
                parameters[4] = new SqlParameter("id4", obj.id4);
                parameters[5] = new SqlParameter("str1", obj.str1);
                parameters[6] = new SqlParameter("str2", obj.str2);
                parameters[7] = new SqlParameter("str3", obj.str3);
                parameters[8] = new SqlParameter("str4", obj.str4);
                parameters[9] = new SqlParameter("str5", obj.str5);
                parameters[10] = new SqlParameter("str6", obj.str6);
                parameters[11] = new SqlParameter("str7", obj.str7);
                parameters[12] = new SqlParameter("str8", obj.str8);
                parameters[13] = new SqlParameter("str9", obj.str9);
                parameters[14] = new SqlParameter("str10", obj.str10);

                int result = Convert.ToInt32(DataManager.ExecuteScalar("SchoolTours", "PR_SET_ITEM", parameters.ToArray()));
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static DataSet Put_SET_ITEM_DS(Obj_SET_ITEM obj)
        {
            DataSet objDataSet = null;
            try
            {


                SqlParameter[] parameters = new SqlParameter[15];
                parameters[0] = new SqlParameter("mode", obj.mode);
                parameters[1] = new SqlParameter("id1", obj.id1);
                parameters[2] = new SqlParameter("id2", obj.id2);
                parameters[3] = new SqlParameter("id3", obj.id3);
                parameters[4] = new SqlParameter("id4", obj.id4);
                parameters[5] = new SqlParameter("str1", obj.str1);
                parameters[6] = new SqlParameter("str2", obj.str2);
                parameters[7] = new SqlParameter("str3", obj.str3);
                parameters[8] = new SqlParameter("str4", obj.str4);
                parameters[9] = new SqlParameter("str5", obj.str5);
                parameters[10] = new SqlParameter("str6", obj.str6);
                parameters[11] = new SqlParameter("str7", obj.str7);
                parameters[12] = new SqlParameter("str8", obj.str8);
                parameters[13] = new SqlParameter("str9", obj.str9);
                parameters[14] = new SqlParameter("str10", obj.str10);

                objDataSet = (DataManager.ExecuteDataSet("SchoolTours", "PR_SET_ITEM", parameters.ToArray()));
                return objDataSet;
            }
            catch (Exception ex)
            {

            }
            return objDataSet; //Successful

        }

        public static int del_DEL_ITEM(Obj_DEL_ITEM obj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("mode", obj.mode);
                parameters[1] = new SqlParameter("id1", obj.id1);
                parameters[2] = new SqlParameter("id2", obj.id2);
                parameters[3] = new SqlParameter("id3", obj.id3);

                int result = Convert.ToInt32(DataManager.ExecuteScalar("SchoolTours", "PR_DEL_ITEM", parameters.ToArray()));
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static DataSet del_DEL_ITEM_DS(Obj_DEL_ITEM obj)
        {
            DataSet objDataSet = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("mode", obj.mode);
                parameters[1] = new SqlParameter("id1", obj.id1);
                parameters[2] = new SqlParameter("id2", obj.id2);
                parameters[3] = new SqlParameter("id3", obj.id3);

                objDataSet = DataManager.ExecuteDataSet("SchoolTours", "PR_DEL_ITEM", parameters.ToArray());
                return objDataSet;
            }
            catch (Exception ex)
            {

            }

            return objDataSet;

        }
        public static DataSet Get_PR_ACCOUNTING(Obj_PR_ACCOUNTING Obj)
        {
            DataSet objDataSet = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("mode", Obj.mode);
                parameters[1] = new SqlParameter("id1", Obj.id1);
                parameters[2] = new SqlParameter("id2", Obj.id2);
                parameters[3] = new SqlParameter("id3", Obj.id3);
                parameters[4] = new SqlParameter("id4", Obj.id4);
                parameters[5] = new SqlParameter("str1", Obj.str1);
                parameters[6] = new SqlParameter("str2", Obj.str2);
                parameters[7] = new SqlParameter("str3", Obj.str3);
                parameters[8] = new SqlParameter("str4", Obj.str4);

                objDataSet = DataManager.ExecuteDataSet("SchoolTours", "PR_ACCOUNTING", parameters.ToArray());
            }
            catch (Exception ex)
            {

            }

            return objDataSet; //Successful
        }
        public static DataSet Get_PR_MAILER(Obj_PR_MAILER Obj)
        {
            DataSet objDataSet = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("mode", Obj.mode);
                parameters[1] = new SqlParameter("id1", Obj.id1);
                parameters[2] = new SqlParameter("id2", Obj.id2);
                parameters[3] = new SqlParameter("str1", Obj.str1);
                parameters[4] = new SqlParameter("str2", Obj.str2);

                objDataSet = DataManager.ExecuteDataSet("SchoolTours", "PR_MAILER", parameters.ToArray());
            }
            catch (Exception ex)
            {

            }

            return objDataSet; //Successful
        }
    }
}
