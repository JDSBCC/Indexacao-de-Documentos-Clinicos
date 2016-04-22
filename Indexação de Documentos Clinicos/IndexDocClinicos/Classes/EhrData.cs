using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace IndexDocClinicos.Classes
{
    public class EhrData
    {
        private OracleConnection connOracle = null;
        private OracleDataReader dataReaderOracle = null;

        private MySqlConnection connMySQL = null;
        private MySqlDataReader dataReaderMySQL = null;

        private Organization organization;//organization
        private List<Patient> patients;//patients

        public EhrData()
        {
            organization = new Organization
            {
                Version = 0,
                Name = "EVF",
                Number = "EVF",
                Uid = Guid.NewGuid().ToString()
            };
            patients = new List<Patient>();


            connOracle = new OracleConnection();
            connOracle.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP)(Host=10.84.5.13)(Port=1521))(CONNECT_DATA= (SID=EVFDEV)));User Id=eresults_v2;Password=eresults_v2";

            connMySQL = new MySqlConnection("server=localhost;port=3306;database=ehrserver;userid=root;password=12345;");
        }

        public void createOrganization()
        {
            try
            {
                connMySQL.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO `ehrserver`.`organization` (`version`, `name`, `number`, `uid`) VALUES 
                                                    ('" + organization.Version + "', '" + organization.Name + "', '" + organization.Number
                                                        + "', '" + organization.Uid + "');", connMySQL);
                dataReaderMySQL = cmd.ExecuteReader();
                dataReaderMySQL.Close();
            }
            catch (MySqlException ex)
            {
            }
            finally
            {
                if (connMySQL != null)
                {
                    connMySQL.Close();
                }
            }
        }

        public void importPatients()
        {
            int row = 0;
            try
            {
                connOracle.Open();

                OracleCommand cmd = new OracleCommand("select b.doente, (select codigo from er_tipo_doente d where d.tipo_doente_id = b.tipo_doente_id) t_doente, a.*, c.* " +
                                                        "from gr_entidade a " +
                                                        "join gr_doente c on a.entidade_id = c.entidade_id " +
                                                        "join gr_doente_local b on a.entidade_id = b.entidade_id", connOracle);
                dataReaderOracle = cmd.ExecuteReader();
                while (dataReaderOracle.Read())
                {
                    row++;
                    if (row != 177237)
                    {
                        Debug.Write("[" + row + "] = " + dataReaderOracle["DOENTE"] + " - " + dataReaderOracle["ENTIDADE_ID"] + " - " + dataReaderOracle["NOME"] + " \n");

                        Patient patient = new Patient
                        {
                            Entidade_id = Convert.ToInt32(dataReaderOracle["ENTIDADE_ID"]),
                            Nome = dataReaderOracle["NOME"] + "",
                            Morada = dataReaderOracle["MORADA"] + "",
                            Localidade = dataReaderOracle["LOCALIDADE"] + "",
                            Codigo_Postal = dataReaderOracle["CODIGO_POSTAL"] + "",
                            N_Beneficiario = dataReaderOracle["N_BENEF"] + "",
                            N_Cartao_Cidadao = dataReaderOracle["N_BI"] + "",
                            Data_Nasc = Convert.ToDateTime(dataReaderOracle["DATA_NASC"]),
                            Sexo = dataReaderOracle["SEXO_ID"] + ""
                        };

                        if (!Convert.IsDBNull(dataReaderOracle["N_CONTRIBUINTE"]))
                            patient.N_Contribuinte = Convert.ToInt32(dataReaderOracle["N_CONTRIBUINTE"]);
                        if (!Convert.IsDBNull(dataReaderOracle["TELEFONE1"]))
                            patient.Telefone1 = Convert.ToDouble(dataReaderOracle["TELEFONE1"]);
                        if (!Convert.IsDBNull(dataReaderOracle["TELEFONE2"]))
                            patient.Telefone2 = Convert.ToDouble(dataReaderOracle["TELEFONE2"]);
                        if (!Convert.IsDBNull(dataReaderOracle["FAX"]))
                            patient.Fax = Convert.ToDouble(dataReaderOracle["FAX"]);
                        if (!Convert.IsDBNull(dataReaderOracle["N_SNS"]))
                            patient.N_Servico_Nacional_Saude = Convert.ToDouble(dataReaderOracle["N_SNS"]);

                        patients.Add(patient);
                    }
                    if (row >= 2000)//REMOVE: just for testing
                    {
                        break;
                    }
                }
                dataReaderOracle.Close();
            }
            catch (OracleException e)
            {
                Debug.Write("Error something: {0}", e.ToString());
            }
            finally
            {
                if (connOracle != null)
                {
                    connOracle.Close();
                }
            }
        }

        public void createPersons()
        {
                foreach (Patient patient in patients)
                {

                    string[] names = patient.Nome.Split(' ');
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8090/ehr/rest/createPerson");
                    request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiZXh0cmFkYXRhIjp7Im9yZ2FuaXphdGlvbiI6IjY2NjYifSwiaXNzdWVkX2F0IjoiMjAxNi0wNC0yMlQxMzo0ODoyOS40ODRaIn0=.Fztrd8LR/FkBY2CJLjnl/qYYYjz1/vr4g01e8yTad/0=");
                    request.Accept = "application/json";

                    string tempUrl = "firstName=" + names[0];
                    tempUrl += "&lastName=" + names[names.Length - 1];
                    tempUrl += "&dob=" + patient.Data_Nasc;
                    tempUrl += "&role=pat";
                    tempUrl += "&sex=" + "M";
                    tempUrl += "&organizationUid=" + organization.Uid;
                    var data = Encoding.ASCII.GetBytes(tempUrl);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
        }

    }
}