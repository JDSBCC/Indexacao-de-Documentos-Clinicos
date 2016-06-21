using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace IndexDocClinicos.Classes
{
    public class Connection
    {
        private static OracleConnection connOracle;
        private static MySqlConnection connMySQL;

        public Connection() {
            initOracle();
            initMySQL();
        }

        public static void initOracle()
        {
            connOracle = new OracleConnection(ConfigurationManager.AppSettings["Eresults_v2_db"]);
        }

        public static OracleConnection getOracleCon()
        {
            return connOracle;
        }

        public static bool openOracle(){
            //TaskControl.waitDB();
            try {
                if (connOracle.State != ConnectionState.Open)
                    connOracle.Open();
                return true;
            } catch (Exception ex) {
                Debug.WriteLine("Error: " + ex);
                return false;
            }
        }

        public static bool closeOracle(){
            try {
                if (connOracle.State != ConnectionState.Closed) {
                    connOracle.Close();
                    //TaskControl.releaseDB();
                }
                return true;
            } catch (Exception ex) {
                Debug.WriteLine("Error: " + ex);
                return false;
            }
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

        public static bool openMySQL()
        {
            //TaskControl.waitDB();
            try {
                if (connMySQL.State != ConnectionState.Open)
                    connMySQL.Open();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                return false;
            }
        }

        public static bool closeMySQL()
        {
            try {
                if (connMySQL.State != ConnectionState.Closed){
                    connMySQL.Close();
                    //TaskControl.releaseDB();
                }
                return true;
            } catch (Exception ex) {
                Debug.WriteLine("Error: " + ex);
                return false;
            }
        }
    }
}