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
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace IndexDocClinicos.Classes
{
    public class EhrData
    {
        private OracleConnection connOracle = null;
        private OracleDataReader dataReaderOracle = null;

        private MySqlConnection connMySQL = null;
        private MySqlDataReader dataReaderMySQL = null;

        private string token = "";
        private Organization organization;//organization
        private List<Patient> patients;//patients
        private List<Dictionary<string, string>> map_list = new List<Dictionary<string, string>>();

        //private String []keys = { "",""};

        public EhrData()
        {
            //start connections to dbs
            connOracle = new OracleConnection();
            connOracle.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP)(Host=10.84.5.13)(Port=1521))(CONNECT_DATA= (SID=EVFDEV)));User Id=eresults_v2;Password=eresults_v2";
            connMySQL = new MySqlConnection("server=localhost;port=3306;database=ehrserver;userid=root;password=12345;");

            //init variables
            organization = new Organization
            {
                Version = 0,
                Name = "EVF",
                Number = "2222",
                Uid = getOrganizationUid()/*Guid.NewGuid().ToString()*/
            };
            patients = new List<Patient>();

            //TODO criar organização e utilizador aqui e nao no bootstrap
            Debug.WriteLine("Loging in...");
            login();//login to get token

            Debug.WriteLine("Importing patients...");
            importPatients();//select data from database and store it on patients

            Debug.WriteLine("Committing patients to ehr...");
            commitPersonsPatients();//commit persons to ehr

            Debug.WriteLine("Initializing information to fill xml...");
            fillData();//create a string with file information (xml with data)

            Debug.WriteLine("Filling xml...");
            commitDocument();
        }

        public string getEhrUidForSubject(string patientUid){
            string tempUrl = "format=json";
            tempUrl += "&subjectUid=" + patientUid;
            Request.Get("http://localhost:8090/ehr/rest/ehrForSubject", tempUrl, token);
            //Debug.WriteLine(Request.data["uid"]);
            return Request.data["uid"]+"";
        }

        public string getOrganizationUid()
        {
            string uid = "";
            try
            {
                connMySQL.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ehrserver.organization where number=2222", connMySQL);
                dataReaderMySQL = cmd.ExecuteReader();
                while (dataReaderMySQL.Read())
                {
                    uid = dataReaderMySQL["uid"]+"";
                }
                dataReaderMySQL.Close();
            }
            catch (OracleException e)
            {
                Debug.Write("Error something: {0}", e.ToString());
            }
            finally
            {
                if (connMySQL != null)
                {
                    connMySQL.Close();
                }
            }
            return uid;
        }

        public void login()
        {
            string tempUrl = "username=admin";
            tempUrl += "&password=admin";
            tempUrl += "&organization=2222";
            Request.Post("http://localhost:8090/ehr/rest/login", tempUrl);
            token = Request.data["token"].ToString();
        }

        public void importPatients()
        {
            int row = 0;
            try
            {
                connOracle.Open();

                OracleCommand cmd = new OracleCommand("select b.doente, (select codigo from er_tipo_doente d where d.tipo_doente_id = b.tipo_doente_id) t_doente, a.*, c.*, s.sigla " +
                                                        "from gr_entidade a " +
                                                        "join gr_doente c on a.entidade_id = c.entidade_id " +
                                                        "join gr_doente_local b on a.entidade_id = b.entidade_id " +
                                                        "left outer join er_sexo s on c.sexo_id = s.sexo_id", connOracle);
                dataReaderOracle = cmd.ExecuteReader();
                while (dataReaderOracle.Read())
                {
                    row++;
                    if (row != 177237)
                    {
                        Debug.Write("[" + row + "] = " + dataReaderOracle["DOENTE"] + " - " + dataReaderOracle["ENTIDADE_ID"] + " - " + dataReaderOracle["NOME"] + " \n");

                        Patient patient = new Patient
                        {
                            Doente = Convert.ToInt32(dataReaderOracle["DOENTE"]),
                            Entidade_id = Convert.ToInt32(dataReaderOracle["ENTIDADE_ID"]),
                            Nome = dataReaderOracle["NOME"] + "",
                            Morada = dataReaderOracle["MORADA"] + "",
                            Localidade = dataReaderOracle["LOCALIDADE"] + "",
                            Codigo_Postal = dataReaderOracle["CODIGO_POSTAL"] + "",
                            N_Beneficiario = dataReaderOracle["N_BENEF"] + "",
                            N_Cartao_Cidadao = dataReaderOracle["N_BI"] + "",
                            Data_Nasc = Convert.ToDateTime(dataReaderOracle["DATA_NASC"]),
                            Sexo = dataReaderOracle["SIGLA"] + "",
                            Uid = Guid.NewGuid().ToString()
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
                    if (row >= 100)//REMOVE: just for testing
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

        public void commitPersonsPatients()
        {
            foreach (Patient patient in patients)
            {
                string[] names = patient.Nome.Split(' ');
                string tempUrl = "firstName=" + names[0];
                tempUrl += "&lastName=" + names[names.Length - 1];
                tempUrl += "&dob=" + patient.Data_Nasc.ToString("yyyyMMdd");
                tempUrl += "&role=pat";
                tempUrl += "&sex=" + patient.Sexo;
                tempUrl += "&format";
                tempUrl += "&createEhr=true";
                tempUrl += "&organizationUid=" + organization.Uid;
                tempUrl += "&uid=" + patient.Uid;
                Request.Post("http://localhost:8090/ehr/rest/createPerson", tempUrl, token, "application/json");
            }
        }

        public void fillData()
        {

            foreach(Patient patient in patients){
                map_list.Add(new Dictionary<string, string>());
                map_list[map_list.Count-1].Add("CONTRIBUTION", Guid.NewGuid().ToString());
                map_list[map_list.Count-1].Add("COMMITTER_NAME", "João Correia");
                map_list[map_list.Count-1].Add("TIME_COMMITTED", DateTime.Now.ToString("yyyyMMdd"));
                map_list[map_list.Count-1].Add("VERSION_ID", Guid.NewGuid().ToString());
                map_list[map_list.Count-1].Add("COMPOSITION", Guid.NewGuid().ToString());
                map_list[map_list.Count-1].Add("COMPOSER_NAME", "João Correia");
                map_list[map_list.Count-1].Add("COMPOSITION_DATE", DateTime.Now.ToString("yyyyMMdd"));
                map_list[map_list.Count-1].Add("NAME", patient.Nome);
                map_list[map_list.Count-1].Add("DOB", patient.Data_Nasc.ToString("yyyyMMdd"));
                map_list[map_list.Count-1].Add("SEX", patient.Sexo);
                map_list[map_list.Count-1].Add("ADDRESS", patient.Morada);
                map_list[map_list.Count-1].Add("POST_CODE", patient.Codigo_Postal);
                map_list[map_list.Count-1].Add("LOCAL", patient.Localidade);
                map_list[map_list.Count-1].Add("TELEPHONE1", patient.Telefone1+"");
                map_list[map_list.Count-1].Add("TELEPHONE2", patient.Telefone2+"");
                map_list[map_list.Count-1].Add("FAX", patient.Fax+"");
                map_list[map_list.Count-1].Add("CC", patient.N_Cartao_Cidadao);
                map_list[map_list.Count-1].Add("CONTR", patient.N_Contribuinte+"");
                map_list[map_list.Count-1].Add("BENEF", patient.N_Beneficiario);
                map_list[map_list.Count-1].Add("SNS", patient.N_Servico_Nacional_Saude+"");
                map_list[map_list.Count-1].Add("uid", patient.Uid);
            }
        }

        public void commitDocument()
        {
            foreach (Dictionary<string, string> patient in map_list)
            {
                string text = System.IO.File.ReadAllText("C:\\Users\\Joaogcorreia\\Desktop\\EHR + Solr + IndexDocClinicos\\Indexacao-de-Documentos-Clinicos\\xml_arquetipos_templates\\demographic_patient (right).xml");
                foreach (var item in patient)
                {
                    string pattern = @"\[\[:::"+item.Key+@":::\]\]";
                    Regex rgx = new Regex(pattern);
                    string result = rgx.Replace(text, item.Value.Equals("")?"0":item.Value);
                    text = result;
                }
                //Debug.WriteLine(text);

                /*using (FileStream fs = File.Create("C:\\Users\\Joaogcorreia\\Desktop\\versions_test\\" + patient["VERSION_ID"] + ".xml"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(text);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }*/

                //Post
                string tempUrl = "ehrUid=" + getEhrUidForSubject(patient["uid"]);
                tempUrl += "&auditSystemId=popo";
                tempUrl += "&auditCommitter=Joao";
                Request.Post("http://localhost:8090/ehr/rest/commit", tempUrl, token, "application/json", text);
                Debug.WriteLine(Request.dataXML);

                //break;
            }
        }

    }
}