using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SchoolToursFramework.Data
{
    public class DataManager
    {
        public DbCommand dc;

        private string GetConnectionString(string pConn_Name)
        {
            string pConnectionString = "";
            try
            {


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pConnectionString;
        }

        public static IDataReader ExecuteReader(string connectionName, string query)
        {
            Database d = DatabaseFactory.CreateDatabase(connectionName);

            DbCommand dc = d.GetSqlStringCommand(query);
            dc.CommandTimeout = 0;
            return d.ExecuteReader(dc);
        }

        public static SqlDataReader ExecuteReader(string connectionName, string storedProcedureName, System.Data.SqlClient.SqlParameter[] sqlParameters)
        {
            Database d = DatabaseFactory.CreateDatabase(connectionName);

            DbCommand dc = d.GetStoredProcCommand(storedProcedureName);

            for (int x = 0; x <= sqlParameters.Length - 1; x++)
            {
                d.AddInParameter(dc, sqlParameters[x].ParameterName, sqlParameters[x].DbType, sqlParameters[x].Value);
            }
            dc.CommandTimeout = 0;
            return (SqlDataReader)d.ExecuteReader(dc);
        }

        public static IDataReader ExecuteIDataReader(string connectionName, string storedProcedureName, System.Data.SqlClient.SqlParameter[] sqlParameters)
        {
            Database d = DatabaseFactory.CreateDatabase(connectionName);

            DbCommand dc = d.GetStoredProcCommand(storedProcedureName);

            for (int x = 0; x <= sqlParameters.Length - 1; x++)
            {
                d.AddInParameter(dc, sqlParameters[x].ParameterName, sqlParameters[x].DbType, sqlParameters[x].Value);
            }
            dc.CommandTimeout = 0;
            return d.ExecuteReader(dc);
        }

        public static int ExecuteNonQuery(string connectionName, string storedProcedureName, System.Data.SqlClient.SqlParameter[] sqlParameters)
        {
            try
            {
                Database d = DatabaseFactory.CreateDatabase(connectionName);

                DbCommand dc = d.GetStoredProcCommand(storedProcedureName);

                for (int x = 0; x <= sqlParameters.Length - 1; x++)
                {
                    d.AddInParameter(dc, sqlParameters[x].ParameterName, sqlParameters[x].DbType, sqlParameters[x].Value);
                }
                dc.CommandTimeout = 0;
                return d.ExecuteNonQuery(dc);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static Object ExecuteScalar(string connectionName, string storedProcedureName, System.Data.SqlClient.SqlParameter[] sqlParameters)
        {
            Database d = DatabaseFactory.CreateDatabase(connectionName);

            DbCommand dc = d.GetStoredProcCommand(storedProcedureName);

            for (int x = 0; x <= sqlParameters.Length - 1; x++)
            {
                d.AddInParameter(dc, sqlParameters[x].ParameterName, sqlParameters[x].DbType, sqlParameters[x].Value);
            }
            dc.CommandTimeout = 0;
            return d.ExecuteScalar(dc);
        }

        public static Object ExecuteScalar(string connectionName, string query)
        {
            Database d = DatabaseFactory.CreateDatabase(connectionName);

            DbCommand dc = d.GetSqlStringCommand(query);
            dc.CommandTimeout = 0;
            return d.ExecuteScalar(dc);
        }

        public static DataSet ExecuteDataSet(string connectionName, string storedProcedureName, System.Data.SqlClient.SqlParameter[] sqlParameters)
        {
            Database d = DatabaseFactory.CreateDatabase(connectionName);

            DbCommand dc = d.GetStoredProcCommand(storedProcedureName);

            for (int x = 0; x <= sqlParameters.Length - 1; x++)
            {
                d.AddInParameter(dc, sqlParameters[x].ParameterName, sqlParameters[x].DbType, sqlParameters[x].Value);
            }
            dc.CommandTimeout = 0;
            return d.ExecuteDataSet(dc);
        }

        public static DataSet ExecuteDataSet(string connectionName, string storedProcedureName)
        {
            Database d = DatabaseFactory.CreateDatabase(connectionName);

            DbCommand dc = d.GetStoredProcCommand(storedProcedureName);
            dc.CommandTimeout = 0;
            return d.ExecuteDataSet(dc);
        }

        public static string GetStringValue(object dbValue)
        {
            return System.Convert.IsDBNull(dbValue) ? "" : (string)dbValue;
        }

        public static DateTime GetDateTimeValue(object dbValue)
        {
            if (dbValue != null)
            {
                DateTime parsedDate;
                DateTime.TryParse(dbValue.ToString(), out parsedDate);
                return parsedDate;
            }
            else { return new DateTime(); }
        }

        public static int GetIntValue(object dbValue)
        {
            return System.Convert.IsDBNull(dbValue) ? -1 : (int)dbValue;
        }

        public static decimal GetDecimalValue(object dbValue)
        {
            return System.Convert.IsDBNull(dbValue) ? -1 : (decimal)dbValue;
        }
    }
}
