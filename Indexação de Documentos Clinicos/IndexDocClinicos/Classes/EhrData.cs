using IndexDocClinicos.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace IndexDocClinicos.Classes
{
    public class EhrData
    {
        private static string token = "";
        private static Organization organization;//organization
        private List<Patient> patients;//patients
        private List<Dictionary<string, string>> map_list;
        private List<string> patientUids;

        public EhrData()
        {

            patients = new List<Patient>();
            map_list = new List<Dictionary<string, string>>();
            patientUids = new List<string>();
        }

        public List<string> getPatientUids() {
            return patientUids;
        }

        public void setOrganization()
        {
            organization = new Organization
            {
                Version = 0,
                Name = "EVF",
                Number = "2222",
                Uid = getOrganizationUid()
            };
        }

        public void setPatients(List<Patient> patients)
        {
            foreach(var patient in patients){
                this.patients.Add(patient);
            }
        }

        public string getEhrUidForSubject(string patientUid){
            string tempUrl = "format=json";
            tempUrl += "&subjectUid=" + patientUid;
            Request.Get(ConfigurationManager.AppSettings["EHR_rest"] + "/ehrForSubject", tempUrl, token);
            //Debug.WriteLine(Request.data["uid"]);
            return Request.data["uid"]+"";
        }

        public string getOrganizationUid()
        {
            string uid = "";
            try
            {
                Connection.openMySQL();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ehrserver.organization where number=2222", Connection.getMySQLCon());
                MySqlDataReader dataReaderMySQL = cmd.ExecuteReader();
                while (dataReaderMySQL.Read())
                {
                    uid = dataReaderMySQL["uid"]+"";
                }
                dataReaderMySQL.Close();
            }
            catch (MySqlException e)
            {
                Debug.Write("Error: {0}", e.ToString());
            }
            finally
            {
                Connection.closeMySQL();
            }
            return uid;
        }

        public void login()
        {
            string tempUrl = "username=admin";
            tempUrl += "&password=admin";
            tempUrl += "&organization=2222";
            Request.Post(ConfigurationManager.AppSettings["EHR_rest"] + "/login", tempUrl);
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
                tempUrl += "&sex=" + (patient.Sexo_Sigla==null ? "U" : patient.Sexo_Sigla);
                tempUrl += "&format";
                tempUrl += "&createEhr=true";
                tempUrl += "&organizationUid=" + organization.Uid;
                tempUrl += "&uid=" + patient.Uid;
                Debug.WriteLine("------->" + tempUrl);
                Request.Post(ConfigurationManager.AppSettings["EHR_rest"] + "/createPerson", tempUrl, token, "application/json");
            }
        }

        public void fillData()
        {
            foreach(Patient patient in patients){
                map_list.Add(new Dictionary<string, string>());
                map_list[map_list.Count-1].Add("CONTRIBUTION", Guid.NewGuid().ToString());
                map_list[map_list.Count-1].Add("COMMITTER_NAME", "João Correia");
                map_list[map_list.Count-1].Add("TIME_COMMITTED", DateTime.Now.ToString("yyyyMMdd"));
                map_list[map_list.Count-1].Add("VERSION_ID", patient.Version_Uid);
                map_list[map_list.Count-1].Add("COMPOSITION", Guid.NewGuid().ToString());
                map_list[map_list.Count-1].Add("COMPOSER_NAME", "João Correia");
                map_list[map_list.Count-1].Add("COMPOSITION_DATE", DateTime.Now.ToString("yyyyMMdd"));
                map_list[map_list.Count-1].Add("NAME", patient.Nome);
                map_list[map_list.Count-1].Add("DOB", patient.Data_Nasc.ToString("yyyyMMdd"));
                map_list[map_list.Count-1].Add("SEX", patient.Sexo==null?"Indefinido":patient.Sexo);
                map_list[map_list.Count-1].Add("ADDRESS", patient.Morada==null?"":patient.Morada);
                map_list[map_list.Count-1].Add("POST_CODE", patient.Codigo_Postal==null?"":patient.Codigo_Postal);
                map_list[map_list.Count-1].Add("LOCAL", patient.Localidade==null?"":patient.Localidade);
                map_list[map_list.Count-1].Add("TELEPHONE1", patient.Telefone1+"");
                map_list[map_list.Count-1].Add("TELEPHONE2", patient.Telefone2+"");
                map_list[map_list.Count-1].Add("FAX", patient.Fax+"");
                map_list[map_list.Count-1].Add("CC", patient.N_Cartao_Cidadao+"");
                map_list[map_list.Count-1].Add("CONTR", patient.N_Contribuinte+"");
                map_list[map_list.Count-1].Add("BENEF", patient.N_Beneficiario+"");
                map_list[map_list.Count-1].Add("SNS", patient.N_Servico_Nacional_Saude+"");
                map_list[map_list.Count-1].Add("CIVIL", patient.Estado_Civil+"");
                map_list[map_list.Count-1].Add("ELEMID", patient.Elemento_id+"");
                map_list[map_list.Count-1].Add("DOCID", patient.Documento_id+"");
                map_list[map_list.Count-1].Add("ENTID", patient.Entidade_id+"");
                map_list[map_list.Count-1].Add("DOEID", patient.Doente+"");
                map_list[map_list.Count-1].Add("DOCDATE", patient.DocDate.ToString("yyyyMMdd"));
                map_list[map_list.Count-1].Add("uid", patient.Uid);
                patientUids.Add(patient.Uid);
            }
        }

        public void commitDocument()
        {
            foreach (Dictionary<string, string> patient in map_list)
            {
                string text = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["demographic_template"]);
                foreach (var item in patient)
                {
                    string pattern = @"\[\[:::"+item.Key+@":::\]\]";
                    Regex rgx = new Regex(pattern);
                    //Debug.WriteLine("[" + item.Key + "] = " + (item.Value.Equals("") ? "999999999" : item.Value));
                    string result = rgx.Replace(text, item.Value.Equals("")?"0":item.Value);
                    text = result;
                }

                //Post
                string tempUrl = "ehrUid=" + getEhrUidForSubject(patient["uid"]);
                tempUrl += "&auditSystemId=popo";
                tempUrl += "&auditCommitter=Joao";
                Request.Post(ConfigurationManager.AppSettings["EHR_rest"] + "/commit", tempUrl, token, "application/json", text);
                //Debug.WriteLine(Request.dataXML);
            }
        }

        public void freeMemory()
        {
            patients.Clear();
            map_list.Clear();
            patientUids.Clear();
        }

    }
}