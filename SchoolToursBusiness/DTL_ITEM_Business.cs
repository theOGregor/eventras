using SchoolToursData.Data;
using SchoolToursData.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolToursBusiness
{
    public class DTL_ITEM_Business
    {
        public static DataSet GetItemDetails(Obj_DTL_ITEM Obj)
        {
            return DTL_ITEM_Data.GetItemDetails(Obj);
        }
        public static DataSet Get_LST_ITEMS(Obj_LST_ITEMS Obj)
        {
            return DTL_ITEM_Data.Get_LST_ITEMS(Obj);
        }
        public static DataSet Get_PR_SEARCH(Obj_PR_SEARCH Obj)
        {
            return DTL_ITEM_Data.Get_PR_SEARCH(Obj);
        }
        public static int Put_SET_ITEM(Obj_SET_ITEM obj)
        {
            return DTL_ITEM_Data.Put_SET_ITEM(obj);
        }
        public static DataSet Put_SET_ITEM_DS(Obj_SET_ITEM obj)
        {
            return DTL_ITEM_Data.Put_SET_ITEM_DS(obj);
        }
        public static int del_DEL_ITEM(Obj_DEL_ITEM obj)
        {
            return DTL_ITEM_Data.del_DEL_ITEM(obj);
        }
        public static DataSet del_DEL_ITEM_DS(Obj_DEL_ITEM obj)
        {
            return DTL_ITEM_Data.del_DEL_ITEM_DS(obj);
        }
        public static DataSet Get_PR_ACCOUNTING(Obj_PR_ACCOUNTING Obj)
        {
            return DTL_ITEM_Data.Get_PR_ACCOUNTING(Obj);
        }
        public static DataSet Get_PR_MAILER(Obj_PR_MAILER Obj)
        {
            return DTL_ITEM_Data.Get_PR_MAILER(Obj);
        }
    }
}
