using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using SolrNet;
using SolrNet.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IndexDocClinicos.Classes
{
    public class Data
    {
        private ISolrOperations<Contribution> solr;

        private OracleConnection connOracle = null;
        private OracleDataReader dataReaderOracle = null;

        private MySqlConnection connMySQL = null;
        private MySqlDataReader dataReaderMySQL = null;

        private List<Document> documents;//documents pdf
        private List<Patient> patients;//patients
        
        public static bool isConnectionFree = true;
        private string contQuery = "";

        public Data()
        {
            //initializing data lists
            documents = new List<Document>();
            patients = new List<Patient>();
        }

        public List<Patient> getPatients()
        {
            return patients;
        }

        public void clearPatients()
        {
            patients.Clear();
        }

        public void freeMemory()
        {
            documents.Clear();
            patients.Clear();
        }

        public void queryingEresults(string condition)
        {
            while (!isConnectionFree) ;
            isConnectionFree = false;
            try
            {
                connOracle = new OracleConnection();
                connOracle.ConnectionString = ConfigurationManager.AppSettings["Eresults_v2_db"];
                connOracle.Open();
                OracleCommand cmd = new OracleCommand("select d.documento_id, f.*, dl.doente, ge.*, c.*, s.codigo, s.descricao, ec.descricao as estado_civil from er_ficheiro f " +
                    "join er_elemento e on e.elemento_id=f.elemento_id and e.versao_activa='S' and e.cod_versao=f.cod_versao " +
                    "join er_documento d on d.documento_id=e.documento_id " +
                    "join gr_visita_documento vd on d.documento_id=vd.documento_id " +
                    "join gr_visita v on vd.visita_id=v.visita_id " +
                    "join gr_entidade ge on v.entidade_pai_id=ge.entidade_id " +
                    "join gr_doente c on ge.entidade_id = c.entidade_id " +
                    "join gr_doente_local dl on v.entidade_pai_id=dl.entidade_id  and dl.activo='S' " +
                    "left join er_sexo s on c.sexo_id=s.sexo_id " +
                    "left join er_estado_civil ec on ec.estado_civil_id=c.estado_civil_id " +
                    "where " + condition, connOracle);//REMOVE restriçao de elemento_id
                //cmd.CommandTimeout = 900;
                dataReaderOracle = cmd.ExecuteReader();
                while (dataReaderOracle.Read())
                {
                    Debug.Write("[PATIENT] = " + dataReaderOracle["elemento_id"] + " - " + dataReaderOracle["documento_id"] + " - " + dataReaderOracle["NOME"] + " - " + dataReaderOracle["DATA_NASC"] + " \n");
                    //save document content in docInfo
                    saveDocContent();

                    //add patient metadata to ehr
                    saveMetadataContent();
                }
                dataReaderOracle.Close();
            }
            catch (OracleException e)
            {
                Debug.Write("Error: {0}", e.ToString());
                if (connOracle != null) {
                    connOracle.Close();
                }
                freeMemory();
                isConnectionFree = true;
                queryingEresults(condition);
            }
            catch (TimeoutException te) {
                if (connOracle != null){
                    connOracle.Close();
                }
                freeMemory();
                isConnectionFree = true;
                queryingEresults(condition);
            }
            finally
            {
                if (connOracle != null)
                {
                    connOracle.Close();
                    isConnectionFree = true;
                }
            }
        }

        private void saveDocContent()
        {
            using (MemoryStream stream = new MemoryStream((byte[])dataReaderOracle["file_stream"]))
            {
                solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
                ExtractParameters extract = new ExtractParameters(stream, dataReaderOracle["elemento_id"] + "", dataReaderOracle["nome_original"] + "")
                {
                    ExtractOnly = true,
                    ExtractFormat = ExtractFormat.Text
                };
                string response;
                try {
                    response = solr.Extract(extract).Content;
                } catch (SolrConnectionException) {
                    response = "";
                }

                Document doc = new Document
                {
                    Elemento_id = Convert.ToInt32(dataReaderOracle["elemento_id"]),
                    Documento_id = Convert.ToInt32(dataReaderOracle["documento_id"]),
                    Content = response.Replace("\n", " "),
                    Entidade_id = Convert.ToInt32(dataReaderOracle["entidade_id"]),
                    Doente = Convert.ToInt32(dataReaderOracle["doente"])
                };
                documents.Add(doc);


                stream.Close();
            }
        }

        private void saveMetadataContent()
        {
            //Debug.Write("[PATIENT] = " + dataReaderOracle["elemento_id"] + " - " + dataReaderOracle["documento_id"] + " - " + dataReaderOracle["NOME"] + " - " + dataReaderOracle["DATA_NASC"] + " \n");

            Patient patient = new Patient
            {
                Doente = dataReaderOracle["DOENTE"]+"",
                Entidade_id = dataReaderOracle["ENTIDADE_ID"]+"",
                Nome = dataReaderOracle["NOME"] + "",
                Uid = Guid.NewGuid().ToString()
            };

            if (!Convert.IsDBNull(dataReaderOracle["MORADA"]))
                patient.Morada = dataReaderOracle["MORADA"] + "";
            if (!Convert.IsDBNull(dataReaderOracle["LOCALIDADE"]))
                patient.Localidade = dataReaderOracle["LOCALIDADE"] + "";
            if (!Convert.IsDBNull(dataReaderOracle["CODIGO_POSTAL"]))
                patient.Codigo_Postal = dataReaderOracle["CODIGO_POSTAL"] + "";
            if (!Convert.IsDBNull(dataReaderOracle["N_BENEF"]))
                patient.N_Beneficiario = dataReaderOracle["N_BENEF"] + "";
            if (!Convert.IsDBNull(dataReaderOracle["DATA_NASC"]))
                patient.Data_Nasc = Convert.ToDateTime(dataReaderOracle["DATA_NASC"] + "");
            if (!Convert.IsDBNull(dataReaderOracle["CODIGO"]))
                patient.Sexo_Sigla = dataReaderOracle["CODIGO"] + "";
            if (!Convert.IsDBNull(dataReaderOracle["DESCRICAO"]))
                patient.Sexo = dataReaderOracle["DESCRICAO"] + "";
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
            if (!Convert.IsDBNull(dataReaderOracle["ESTADO_CIVIL"]))
                patient.Estado_Civil = dataReaderOracle["ESTADO_CIVIL"] + "";
            if (!Convert.IsDBNull(dataReaderOracle["N_BI"]))
                patient.N_Cartao_Cidadao = Convert.ToDouble(dataReaderOracle["N_BI"]);

            patients.Add(patient);
        }

        public void commitDataSolr()
        {
            List<Dictionary<string, object>> docs;
            while ((docs=QueryingEHR())==null) ;
            solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();

            if (documents.Count == docs.Count) {
                for (int i = 0; i < documents.Count; i++) {
                    solr.Add(new Contribution
                    {
                        Uid = docs[i]["uid"] + "",
                        Value = (List<string>)docs[i]["value"],
                        Dates = (List<DateTime>)docs[i]["dates"],
                        First_name = docs[i]["first_name"] + "",
                        Last_name = docs[i]["last_name"] + "",
                        Dob = Convert.ToDateTime(docs[i]["dob"]),
                        Elemento_id = documents[i].Elemento_id,
                        Documento_id = documents[i].Documento_id,
                        Content = documents[i].Content,
                        Entidade_id = documents[i].Entidade_id,
                        Doente = documents[i].Doente
                    });
                }
                solr.Commit();
            }
        }

        public void setNumContQuery(List<string> patientsUids)
        {
            contQuery = "AND (pp.value='" + patientsUids.First() + "'";
            foreach (var uid in patientsUids.Skip(1))
            {
                contQuery += " or pp.value='" + uid + "'";
            }
            contQuery += ")";
        }

        private List<Dictionary<string, object>> QueryingEHR()
        {
            while (!isConnectionFree) ;
            isConnectionFree = false;

            List<Dictionary<string, object>> docs = new List<Dictionary<string, object>>();

            try
            {
                connMySQL = new MySqlConnection(ConfigurationManager.AppSettings["EHR_db"]);
                connMySQL.Open();

                //contribution
                List<int> id = new List<int>();
                List<int> ehr_id = new List<int>();
                MySqlCommand cmd1 = new MySqlCommand("SELECT cont.id, cont.ehr_id, v.uid, ci.id as comp_id " +
                                                    "FROM contribution cont, version v, composition_index ci, patient_proxy pp, ehr e " +
                                                    "WHERE cont.id=v.contribution_id AND v.data_id=ci.id AND ci.last_version=1 AND cont.ehr_id=e.id AND e.subject_id=pp.id "+
                                                    contQuery, connMySQL);

                //cmd1.CommandTimeout = 900;
                dataReaderMySQL = cmd1.ExecuteReader();
                while (dataReaderMySQL.Read())
                {//for each contribution
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("uid", dataReaderMySQL["uid"]);
                    id.Add(Convert.ToInt32(dataReaderMySQL["comp_id"]));
                    ehr_id.Add(Convert.ToInt32(dataReaderMySQL["ehr_id"]));
                    docs.Add(dict);
                }
                dataReaderMySQL.Close();

                //data_value_index
                Dictionary<int, List<int>> data_value_ids = new Dictionary<int, List<int>>();
                for (int i = 0; i < id.Count; i++)
                {
                    docs[i].Add("archetype_id", new List<object>());
                    data_value_ids.Add(id[i], new List<int>());
                    string query = "SELECT id " +
                                "FROM data_value_index " +
                                "WHERE owner_id=@id";
                    MySqlCommand cmd2 = new MySqlCommand(query, connMySQL);
                    //cmd2.CommandTimeout = 900;
                    cmd2.Prepare();
                    cmd2.Parameters.AddWithValue("@id", id[i]);
                    dataReaderMySQL = cmd2.ExecuteReader();
                    while (dataReaderMySQL.Read())
                    {
                        data_value_ids[id[i]].Add(Convert.ToInt32(dataReaderMySQL["id"]));
                    }
                    dataReaderMySQL.Close();
                }

                //dv_text_index
                for (int i = 0; i < id.Count; i++)
                {
                    docs[i].Add("value", new List<string>());
                    if (data_value_ids[id[i]].Count==0) {
                        if (connMySQL != null) {
                            connMySQL.Close();
                        }
                        isConnectionFree = true;
                        return null;
                    }
                    for (int j = 0; j < data_value_ids[id[i]].Count; j++)
                    {
                        string query = "SELECT value FROM dv_text_index WHERE id=@id";
                        MySqlCommand cmd3 = new MySqlCommand(query, connMySQL);
                        //cmd3.CommandTimeout = 900;
                        cmd3.Prepare();
                        cmd3.Parameters.AddWithValue("@id", data_value_ids[id[i]][j]);
                        dataReaderMySQL = cmd3.ExecuteReader();
                        while (dataReaderMySQL.Read())
                        {
                            ((List<string>)docs[i]["value"]).Add(dataReaderMySQL["value"]+"");
                        }
                        dataReaderMySQL.Close();
                    }
                }

                //dv_date_time_index
                for (int i = 0; i < id.Count; i++)
                {
                    docs[i].Add("dates", new List<DateTime>());
                    for (int j = 0; j < data_value_ids[id[i]].Count; j++)
                    {
                        string query = "SELECT value FROM dv_date_time_index WHERE id=@id";
                        MySqlCommand cmd4 = new MySqlCommand(query, connMySQL);
                        //cmd4.CommandTimeout = 900;
                        cmd4.Prepare();
                        cmd4.Parameters.AddWithValue("@id", data_value_ids[id[i]][j]);
                        dataReaderMySQL = cmd4.ExecuteReader();
                        while (dataReaderMySQL.Read())
                        {
                            ((List<DateTime>)docs[i]["dates"]).Add(Convert.ToDateTime(dataReaderMySQL["value"]));
                        }
                        dataReaderMySQL.Close();
                    }
                }

                //person
                for (int i = 0; i < id.Count; i++)
                {
                    string query = "SELECT first_name, last_name, dob " +
                                "FROM ehr, patient_proxy pp, person p " +
                                "WHERE ehr.subject_id=pp.id AND pp.value=p.uid AND ehr.id=@id";
                    MySqlCommand cmd5 = new MySqlCommand(query, connMySQL);
                    //cmd5.CommandTimeout = 900;
                    cmd5.Prepare();
                    cmd5.Parameters.AddWithValue("@id", ehr_id[i]);
                    dataReaderMySQL = cmd5.ExecuteReader();
                    while (dataReaderMySQL.Read())
                    {
                        docs[i].Add("first_name", dataReaderMySQL["first_name"]);
                        docs[i].Add("last_name", dataReaderMySQL["last_name"]);
                        docs[i].Add("dob", dataReaderMySQL["dob"]);
                    }
                    dataReaderMySQL.Close();
                }
            }
            catch (MySqlException ex)
            {
                Debug.Write("Error: {0}", ex.ToString());
                if (connMySQL != null) {
                    connMySQL.Close();
                }
                isConnectionFree = true;
                return null;
            }
            catch (TimeoutException te) {
                if (connMySQL != null) {
                    connMySQL.Close();
                }
                isConnectionFree = true;
                return null;
            } finally {
                if (connMySQL != null)
                {
                    connMySQL.Close();
                    isConnectionFree = true;
                }
            }
            return docs;
        }
    }
}