using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using Oracle.ManagedDataAccess.Client;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace IndexDocClinicos.Classes
{
    public class EHRimport
    {
        private OracleConnection conn = null;
        private OracleDataReader dataReader = null;

        public EHRimport()
        {
            conn = new OracleConnection();
            conn.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP)(Host=10.84.5.13)(Port=1521))(CONNECT_DATA= (SID=EVFDEV)));User Id=eresults_v2;Password=eresults_v2";
        }

        public void importPatients()
        {
            try
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("select b.doente, (select codigo from er_tipo_doente d where d.tipo_doente_id = b.tipo_doente_id) t_doente, a.*, c.* "+
                                                        "from gr_entidade a "+
                                                        "join gr_doente c on a.entidade_id = c.entidade_id "+
                                                        "join gr_doente_local b on a.entidade_id = b.entidade_id", conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Debug.Write(dataReader["NOME"] + " - " + dataReader["DATA_NASC"] + "\n");
                }
                dataReader.Close();
            }
            catch (OracleException e)
            {
                Debug.Write("Error something: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}