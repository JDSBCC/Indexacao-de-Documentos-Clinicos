using IndexDocClinicos.Models;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

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

        public EhrData(List<Patient> patients)
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
            this.patients = patients;//UPDATE não deve ser feita esta igualdade

            //TODO criar organização e utilizador aqui e nao no bootstrap
            Debug.WriteLine("Loging in...");
            login();//login to get token

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

        public void commitPersonsPatients()
        {
            foreach (Patient patient in patients)
            {
                string[] names = patient.Nome.Split(' ');
                string tempUrl = "firstName=" + names[0];
                tempUrl += "&lastName=" + names[names.Length - 1];
                tempUrl += "&dob=" + patient.Data_Nasc.ToString("yyyyMMdd");
                tempUrl += "&role=pat";
                tempUrl += "&sex=" + patient.Sexo_Sigla;
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
                map_list[map_list.Count-1].Add("CIVIL", patient.Estado_Civil+"");
                map_list[map_list.Count-1].Add("uid", patient.Uid);
            }
        }

        public void commitDocument()
        {
            foreach (Dictionary<string, string> patient in map_list)
            {
                string text = System.IO.File.ReadAllText("C:\\Users\\Joaogcorreia\\Desktop\\EHR + Solr + IndexDocClinicos\\Indexacao-de-Documentos-Clinicos\\xml_arquetipos_templates\\demographic_patient.xml");
                foreach (var item in patient)
                {
                    string pattern = @"\[\[:::"+item.Key+@":::\]\]";
                    Regex rgx = new Regex(pattern);
                    string result = rgx.Replace(text, item.Value.Equals("")?"-":item.Value);
                    text = result;
                }

                //Post
                string tempUrl = "ehrUid=" + getEhrUidForSubject(patient["uid"]);
                tempUrl += "&auditSystemId=popo";
                tempUrl += "&auditCommitter=Joao";
                Request.Post("http://localhost:8090/ehr/rest/commit", tempUrl, token, "application/json", text);
                //Debug.WriteLine(Request.dataXML);
            }
        }

    }
}