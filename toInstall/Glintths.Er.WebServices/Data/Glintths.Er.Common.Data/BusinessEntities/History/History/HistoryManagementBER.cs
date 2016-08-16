using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{
    public class HistoryManagementBER
    {
        protected Microsoft.Practices.EnterpriseLibrary.Data.Database dal;
        private static HistoryManagementBER instance = new HistoryManagementBER();
        public static HistoryManagementBER Instance
        {
            get { return instance; }
        }

        private HistoryManagementBER()
        {

        }

        public string GetTreeLevelsForEresults(string companyDB, string entId, string globalFilters, string docsSession, string servsSession, string userName, string userAnaRes)
        {
            IDataReader reader = GetTreeLevelsForEresultsDB(companyDB, entId, globalFilters, docsSession, servsSession, userName, userAnaRes);
            string xml = "";
            while (reader.Read())
            {
                try
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        switch (reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture))
                        {
                            case "XML":
                                if (!reader.IsDBNull(i)) xml = reader.GetString(i);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Quick Start is configured so that the Propagate Policy will
                    // log the exception and then recommend a rethrow.
                    bool rethrow = ExceptionPolicy.HandleException(ex, "Business Entities Exception Policy");
                    if (rethrow)
                    {
                        throw;
                    }
                }
            }
            reader.Close();
            return xml;
        }

        protected virtual string GetTreeLevelsForEresultsDBMethod(string companyDB)
        {
            string proc = GetTreeLevelsForEresultsDBMethodName;
            string package = GetTreeLevelsForEresultsDBPackageName;

            dal = CPCHS.Common.Database.Database.GetDatabase("HistoryWCF", companyDB);
            proc = GetDBMethod(dal, proc, package);
            //if (dal is Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase)
            //    proc = package + "." + proc;
            //else
            //    proc = package + "_" + proc;

            return proc;
        }

        protected virtual string GetTreeLevelsForEresultsDBMethodName
        {
            get { return "GetPatientDocsHistoryMulti"; }

        }

        protected virtual string GetTreeLevelsForEresultsDBPackageName
        {
            get { return "PCK_DOCUMENTS_DOCUMENT"; }
        }

        protected virtual IDataReader GetTreeLevelsForEresultsDB(string companyDB, string entIds, string globalFilters, string docsSession, string servsSession, string userName, string userAnaRes)
        {
            IDataReader ret = null;
            try
            {
                string dbMethod = GetTreeLevelsForEresultsDBMethod(companyDB);
                ret = dal.ExecuteReader(dbMethod, entIds, globalFilters, docsSession, servsSession, userName, userAnaRes, DBNull.Value);
            }
            catch (Exception ex)
            {
                // Quick Start is configured so that the Propagate Policy will
                // log the exception and then recommend a rethrow.
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Exception Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            return ret;
        }

        public string GetNodeCellsForEresults(string companyDB, string mode, string entId, Nullable<DateTime> dateBegin, Nullable<DateTime> dateEnd, string globalFilters, string docsSession, string servsSession, string userName, string userAnaRes)
        {
            IDataReader reader = GetNodeCellsForEresultsDB(companyDB, mode, entId, dateBegin, dateEnd, globalFilters, docsSession, servsSession, userName, userAnaRes);
            string xml = "";
            while (reader.Read())
            {
                try
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        switch (reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture))
                        {
                            case "XML":
                                if (!reader.IsDBNull(i)) xml = reader.GetString(i);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Quick Start is configured so that the Propagate Policy will
                    // log the exception and then recommend a rethrow.
                    bool rethrow = ExceptionPolicy.HandleException(ex, "Business Entities Exception Policy");
                    if (rethrow)
                    {
                        throw;
                    }
                }
            }
            reader.Close();
            //xml = "string de teste";
            return xml;
        }

        protected virtual string GetNodeCellsForEresultsDBMethod(string companyDB)
        {
            string proc = GetNodeCellsForEresultsDBMethodName;
            string package = GetNodeCellsForEresultsDBPackageName;

            dal = CPCHS.Common.Database.Database.GetDatabase("HistoryWCF", companyDB);
            proc = GetDBMethod(dal, proc, package);
            /*if (dal is Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase)
                proc = package + "." + proc;
            else
                proc = package + "_" + proc;*/

            return proc;
        }

        protected static string GetDBMethod(Microsoft.Practices.EnterpriseLibrary.Data.Database dal, string proc, string package)
        {
            if (dal is Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase || dal is EntLibContrib.Data.OdpNet.OracleDatabase)
                proc = package + "." + proc; /*IGNORE_STRING*/
            else
                proc = package + "$" + proc; /*IGNORE_STRING*/

            return proc;
        }

        protected virtual string GetNodeCellsForEresultsDBMethodName
        {
            get { return "GetPatDocsHistoryCellsMulti"; }

        }

        protected virtual string GetNodeCellsForEresultsDBPackageName
        {
            get { return "PCK_DOCUMENTS_DOCUMENT"; }
        }

        protected virtual IDataReader GetNodeCellsForEresultsDB(string companyDB, string mode, string entId, Nullable<DateTime> dateBegin, Nullable<DateTime> dateEnd, string globalFilters, string docsSession, string servsSession, string userName, string userAnaRes)
        {
            IDataReader ret = null;
            try
            {
                string dbMethod = GetNodeCellsForEresultsDBMethod(companyDB);
                ret = dal.ExecuteReader(dbMethod, entId, dateBegin, dateEnd, mode, globalFilters, docsSession, servsSession, userName, userAnaRes, DBNull.Value);
            }
            catch (Exception ex)
            {
                // Quick Start is configured so that the Propagate Policy will
                // log the exception and then recommend a rethrow.
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Exception Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            return ret;
        }

    }
}






