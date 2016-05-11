using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using SolrNet;
using SolrNet.Exceptions;
using System;
using System.Collections.Generic;
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
        private List<Contribution> contributions;

        public Data()
        {
            //initializing data lists
            documents = new List<Document>();
            patients = new List<Patient>();
            contributions = new List<Contribution>();

            //querying eresulst to get data
            Debug.WriteLine("Querying eresults and saving their results...");
            queryingEresults();

            //commiting data do ehr server
            EhrData metaInfo = new EhrData(patients);

            //indexing ehr data in solr
            Debug.WriteLine("Indexing data in solr...");
            addToSolr();
        }

        private void queryingEresults()
        {
            try
            {
                connOracle = new OracleConnection();
                connOracle.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP)(Host=10.84.5.13)(Port=1521))(CONNECT_DATA= (SID=EVFDEV)));User Id=eresults_v2;Password=eresults_v2";
                connOracle.Open();

                OracleCommand cmd = new OracleCommand("select f.*, dl.doente, ge.*, c.*, s.sigla, s.descricao, ec.descricao as estado_civil from er_ficheiro f " +
                    "join (select elemento_id, max(cod_versao) as max_versao from er_ficheiro group by elemento_id) fic on fic.elemento_id=f.elemento_id and fic.max_versao=f.cod_versao " +
                    "join er_elemento e on e.elemento_id=f.elemento_id " +
                    "join er_documento d on d.documento_id=e.documento_id " +
                    "join gr_visita_documento vd on d.documento_id=vd.documento_id " +
                    "join gr_visita v on vd.visita_id=v.visita_id " +
                    "join gr_entidade ge on v.entidade_pai_id=ge.entidade_id " +
                    "join gr_doente c on ge.entidade_id = c.entidade_id " +
                    "join gr_doente_local dl on v.entidade_pai_id=dl.entidade_id " +
                    "left join er_sexo s on c.sexo_id=s.sexo_id " +
                    "left join er_estado_civil ec on ec.estado_civil_id=c.estado_civil_id " +
                    "where f.elemento_id>13706193 AND f.elemento_id<13708193", connOracle);//REMOVE restriçao de elemento_id
                dataReaderOracle = cmd.ExecuteReader();
                while (dataReaderOracle.Read())
                {
                    //save document content in docInfo
                    saveDocContent();

                    //add patient metadata to ehr
                    saveMetadataContent();
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
                var response = solr.Extract(extract);

                Document doc = new Document
                {
                    Elemento_id = Convert.ToInt32(dataReaderOracle["elemento_id"]),
                    Cod_Versao = Convert.ToInt32(dataReaderOracle["cod_versao"]),
                    Content = response.Content.Replace("\n", " "),
                    Entidade_id = Convert.ToInt32(dataReaderOracle["entidade_id"]),
                    Doente = Convert.ToInt32(dataReaderOracle["doente"]),
                    File_Stream = Convert.ToBase64String((byte[])dataReaderOracle["file_stream"])
                };
                documents.Add(doc);


                stream.Close();
            }
        }

        private void saveMetadataContent()
        {
            Debug.Write("[PATIENT] = " + dataReaderOracle["DOENTE"] + " - " + dataReaderOracle["ENTIDADE_ID"] + " - " + dataReaderOracle["NOME"] + " \n");

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
                Sexo_Sigla = dataReaderOracle["SIGLA"] + "",
                Sexo = dataReaderOracle["DESCRICAO"] + "",
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
            if (!Convert.IsDBNull(dataReaderOracle["ESTADO_CIVIL"]))
            {
                patient.Estado_Civil = dataReaderOracle["ESTADO_CIVIL"] + "";
            }
            else
            {
                patient.Estado_Civil = "-";
            }

            patients.Add(patient);
        }

        private void addDocsToContributions()
        {
            //add documents to contributions
            foreach (Document doc in documents)
            {
                contributions.Add(new Contribution
                {
                    Elemento_id = doc.Elemento_id,
                    Cod_Versao = doc.Cod_Versao,
                    Content = doc.Content,
                    Entidade_id = doc.Entidade_id,
                    Doente = doc.Doente,
                    File_Stream = doc.File_Stream
                });
            }
        }

        private bool addEHRToContributions(List<Dictionary<string, object>> docs)
        {
            //add ehr data to contributions
            docs = QueryingEHR();//UPDATE verificar se contributions.Count==docs.Count
            int value = docs.Count-contributions.Count;//REMOVE so para teste

            for (int i = 0; i < contributions.Count; i++)
            {
                try
                {
                    contributions[i].Id = Convert.ToInt32(docs[i+value]["id"]);
                    contributions[i].Ehr_id = Convert.ToInt32(docs[i + value]["ehr_id"]);
                    contributions[i].Archetype_id = ((List<object>)docs[i + value]["archetype_id"]).Cast<string>().ToList();
                    contributions[i].Template_id = docs[i + value]["template_id"] + "";
                    contributions[i].Uid = docs[i + value]["uid"] + "";
                    contributions[i].Value = ((List<object>)docs[i + value]["value"]).Cast<string>().ToList();
                    contributions[i].First_name = docs[i + value]["first_name"] + "";
                    contributions[i].Last_name = docs[i + value]["last_name"] + "";
                    contributions[i].Dob = Convert.ToDateTime(docs[i + value]["dob"]);
                }
                catch (KeyNotFoundException ex)
                {
                    return false;
                }
            }
            return true;
        }

        private void addToSolr()
        {
            List<Dictionary<string, object>> docs = new List<Dictionary<string, object>>();

            addDocsToContributions();

            while (true)
            {
                if (!addEHRToContributions(docs))
                {
                    //Debug.WriteLine("################KeyNotFoundException#################");
                    docs.Clear();
                    Thread.Sleep(10000);
                    docs = QueryingEHR();
                }
                else
                {
                    break;
                }
            }

            //commit data to solr
            commitDataSolr(docs);
        }

        private bool commitDataSolr(List<Dictionary<string, object>> docs)
        {

            solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            solr.Delete(SolrQuery.All);

            bool success = true;
            foreach (var partition in Partition.PartitionBySize(contributions, 2))
            {
                Parallel.ForEach(partition, (contribution) =>
                {
                    try
                    {
                        solr.Add(contribution);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        success = false;
                    }
                });
            }

            solr.Commit();
            solr.BuildSpellCheckDictionary();

            return success;
        }

        private List<Dictionary<string, object>> QueryingEHR()
        {

            List<Dictionary<string, object>> docs = new List<Dictionary<string, object>>();
            string cs = @"server=localhost;port=3306;database=ehrserver;
            userid=root;password=12345;";

            try
            {
                connMySQL = new MySqlConnection(cs);
                connMySQL.Open();

                //contribution
                List<int> id = new List<int>();
                MySqlCommand cmd1 = new MySqlCommand("SELECT cont.id, cont.ehr_id, v.uid, ci.id as comp_id " +
                                                    "FROM contribution cont, version v, composition_index ci " +
                                                    "WHERE cont.id=v.contribution_id AND v.data_id=ci.id AND ci.last_version=1", connMySQL);
                dataReaderMySQL = cmd1.ExecuteReader();
                while (dataReaderMySQL.Read())
                {//for each contribution
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("id", dataReaderMySQL["id"]);
                    dict.Add("ehr_id", dataReaderMySQL["ehr_id"]);
                    dict.Add("uid", dataReaderMySQL["uid"]);
                    id.Add(Convert.ToInt32(dataReaderMySQL["comp_id"]));
                    docs.Add(dict);
                }
                dataReaderMySQL.Close();

                //data_value_index
                Dictionary<int, List<int>> data_value_ids = new Dictionary<int, List<int>>();
                for (int i = 0; i < id.Count; i++)
                {
                    docs[i].Add("archetype_id", new List<object>());
                    data_value_ids.Add(id[i], new List<int>());
                    string query = "SELECT id, archetype_id, template_id " +
                                "FROM data_value_index " +
                                "WHERE owner_id=@id";
                    MySqlCommand cmd2 = new MySqlCommand(query, connMySQL);
                    cmd2.Prepare();
                    cmd2.Parameters.AddWithValue("@id", id[i]);
                    dataReaderMySQL = cmd2.ExecuteReader();
                    while (dataReaderMySQL.Read())
                    {
                        ((List<object>)docs[i]["archetype_id"]).Add(dataReaderMySQL["archetype_id"]);
                        docs[i]["template_id"] = dataReaderMySQL["template_id"];
                        data_value_ids[id[i]].Add(Convert.ToInt32(dataReaderMySQL["id"]));
                    }
                    dataReaderMySQL.Close();
                }

                //dv_text_index
                for (int i = 0; i < id.Count; i++)
                {
                    docs[i].Add("value", new List<object>());
                    for (int j = 0; j < data_value_ids[id[i]].Count; j++)
                    {
                        string query = "SELECT value FROM dv_text_index WHERE id=@id";
                        MySqlCommand cmd3 = new MySqlCommand(query, connMySQL);
                        cmd3.Prepare();
                        cmd3.Parameters.AddWithValue("@id", data_value_ids[id[i]][j]);
                        dataReaderMySQL = cmd3.ExecuteReader();
                        while (dataReaderMySQL.Read())
                        {
                            ((List<object>)docs[i]["value"]).Add(dataReaderMySQL["value"]);
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
                    MySqlCommand cmd4 = new MySqlCommand(query, connMySQL);
                    cmd4.Prepare();
                    cmd4.Parameters.AddWithValue("@id", docs[i]["ehr_id"]);
                    dataReaderMySQL = cmd4.ExecuteReader();
                    while (dataReaderMySQL.Read())
                    {
                        docs[i].Add("first_name", dataReaderMySQL["first_name"]);
                        docs[i].Add("last_name", dataReaderMySQL["last_name"]);
                        docs[i].Add("dob", dataReaderMySQL["dob"]);
                    }
                    dataReaderMySQL.Close();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Debug.Write("Error: {0}", ex.ToString());
            }
            finally
            {
                if (connMySQL != null)
                {
                    connMySQL.Close();
                }
            }
            return docs;
        }
    }
}