using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace IndexDocClinicos.Classes
{
    public class Connection
    {
        private static OracleConnection connOracle;
        private static MySqlConnection connMySQL;
        public static bool isConnectionFree = true;

        public static void initOracle()
        {
            connOracle = new OracleConnection(ConfigurationManager.AppSettings["Eresults_v2_db"]);
        }

        public static OracleConnection getOracleCon()
        {
            return connOracle;
        }

        public static void openOracle(){
            while (!isConnectionFree) ;
            isConnectionFree = false;
            connOracle.Open();
        }

        public static void closeOracle(){
            if (connOracle != null)
            {
                connOracle.Close();
            }
            isConnectionFree = true;
        }

        //-----------------------------------------------

        public static void initMySQL()
        {
            connMySQL = new MySqlConnection(ConfigurationManager.AppSettings["EHR_db"]);
        }

        public static MySqlConnection getMySQLCon()
        {
            return connMySQL;
        }

        public static void openMySQL()
        {
            while (!isConnectionFree) ;
            isConnectionFree = false;
            connMySQL.Open();
        }

        public static void closeMySQL()
        {
            if (connMySQL != null)
            {
                connMySQL.Close();
            }
            isConnectionFree = true;
        }
    }
}