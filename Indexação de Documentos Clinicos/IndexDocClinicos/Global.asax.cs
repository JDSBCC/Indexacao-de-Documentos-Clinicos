using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using SolrNet;
using SolrNet.Exceptions;
using SolrNet.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace IndexDocClinicos
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var connection = new SolrConnection("http://localhost:8983/solr/ehr");
            Startup.Init<Contribution>(connection);
            AddInitialDocumentsFromDatabase();
            Testing();
        }

        private void Testing()
        {
            OracleConnection conn = null;
            OracleDataReader dataReader = null;
            try
            {
                conn = new OracleConnection();
                conn.ConnectionString = "Data Source=(DESCRIPTION= (ADDRESS= (PROTOCOL=TCP)(Host=10.84.5.13)(Port=1521))(CONNECT_DATA= (SID=EVFDEV)));User Id=eresults_v2;Password=eresults_v2";
                conn.Open();

                OracleCommand cmd = new OracleCommand("select * from er_ficheiro where elemento_id=13706193", conn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /*byte[] buffer = (byte[])dataReader["file_stream"];
                    File.WriteAllBytes("C:\\Users\\Joaogcorreia\\Desktop\\foo.pdf", buffer);*/

                    /*using (MemoryStream stream = new MemoryStream((byte[])dataReader["file_stream"]))
                    {
                        var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
                        ExtractParameters extract = new ExtractParameters(stream, "doc1", dataReader["nome_original"] + "")
                        {
                            ExtractOnly = true,
                            ExtractFormat = ExtractFormat.Text/*,
                            StreamType = "application/pdf"
                        };
                        var response = solr.Extract(extract);
                        Debug.WriteLine("\n+++++++++++++++++++++++++++++++ " + response.Content);
                    }*/

                }
                dataReader.Close();
            }catch(OracleException e){
                Debug.Write("Error something: {0}", e.ToString());
            }finally {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void AddInitialDocumentsFromDatabase()
        {
            try
            {
                var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
                solr.Delete(SolrQuery.All);
                var connection = ServiceLocator.Current.GetInstance<ISolrConnection>();

                List<Dictionary<string, object>> docs = QueryingEHR();

                //var cd = new Countdown(docs.Count);
                foreach (var partition in Partition.PartitionBySize(docs, 2))
                {
                    Parallel.ForEach(partition, (doc) =>
                    {
                        solr.Add(new Contribution
                            {
                                Id = Convert.ToInt32(doc["id"]),
                                Ehr_id = Convert.ToInt32(doc["ehr_id"]),
                                Archetype_id = ((List<object>)doc["archetype_id"]).Cast<string>().ToList(),
                                Template_id = doc["template_id"] + "",
                                Value = ((List<object>)doc["value"]).Cast<string>().ToList(),
                                First_name = doc["first_name"] + "",
                                Last_name = doc["last_name"] + "",
                                Dob = Convert.ToDateTime(doc["dob"])
                            });
                    });
                }
                    /*foreach (var doc in partition)
                    {
                        ThreadPool.QueueUserWorkItem(y =>
                        {
                            var captured = doc;
                            solr.Add(new Contribution
                            {
                                Id = Convert.ToInt32(doc["id"]),
                                Ehr_id = Convert.ToInt32(doc["ehr_id"]),
                                Archetype_id = ((List<object>)doc["archetype_id"]).Cast<string>().ToList(),
                                Template_id = doc["template_id"] + "",
                                Value = ((List<object>)doc["value"]).Cast<string>().ToList(),
                                First_name = doc["first_name"] + "",
                                Last_name = doc["last_name"] + "",
                                Dob = Convert.ToDateTime(doc["dob"])
                            });
                            cd.Signal();
                        });
                    }
                }
                cd.Wait();*/

                solr.Commit();
                solr.BuildSpellCheckDictionary();
            }
            catch (SolrConnectionException e)
            {
                //throw new Exception(string.Format("Couldn't connect to Solr. Please make sure that Solr is running on '{0}' or change the address in your web.config, then restart the application.", solrURL), e);
            }
        }

        private List<Dictionary<string, object>> QueryingEHR()
        {

            List<Dictionary<string, object>> docs = new List<Dictionary<string, object>>();
            string cs = @"server=localhost;port=3306;database=ehrserver;
            userid=root;password=12345;";

            MySqlConnection conn = null;
            MySqlDataReader dataReader = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                //contribution
                List<int> id = new List<int>();
                MySqlCommand cmd1 = new MySqlCommand("SELECT cont.id, cont.ehr_id, ci.id as comp_id " +
                                                    "FROM contribution cont, version v, composition_index ci " +
                                                    "WHERE cont.id=v.contribution_id AND v.data_id=ci.id AND ci.last_version=1", conn);
                dataReader = cmd1.ExecuteReader();
                while (dataReader.Read()) {//for each contribution
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("id", dataReader["id"]);
                    dict.Add("ehr_id", dataReader["ehr_id"]);
                    id.Add(Convert.ToInt32(dataReader["comp_id"]));
                    docs.Add(dict);
                }
                dataReader.Close();
                
                //data_value_index
                Dictionary<int, List<int>> data_value_ids = new Dictionary<int, List<int>>();
                for (int i = 0; i < id.Count; i++)
                {
                    docs[i].Add("archetype_id", new List<object>());
                    data_value_ids.Add(id[i], new List<int>());
                    string query = "SELECT id, archetype_id, template_id " +
                                "FROM data_value_index " +
                                "WHERE owner_id=@id";
                    MySqlCommand cmd2 = new MySqlCommand(query, conn);
                    cmd2.Prepare();
                    cmd2.Parameters.AddWithValue("@id", id[i]);
                    dataReader = cmd2.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ((List<object>)docs[i]["archetype_id"]).Add(dataReader["archetype_id"]);
                        docs[i]["template_id"] = dataReader["template_id"];
                        data_value_ids[id[i]].Add(Convert.ToInt32(dataReader["id"]));
                    }
                    dataReader.Close();
                }

                //dv_text_index
                for (int i = 0; i < id.Count; i++)
                {
                    docs[i].Add("value", new List<object>());
                    for (int j = 0; j < data_value_ids[id[i]].Count; j++)
                    {
                        string query = "SELECT value FROM dv_text_index WHERE id=@id";
                        MySqlCommand cmd3 = new MySqlCommand(query, conn);
                        cmd3.Prepare();
                        cmd3.Parameters.AddWithValue("@id", data_value_ids[id[i]][j]);
                        dataReader = cmd3.ExecuteReader();
                        while (dataReader.Read())
                        {
                            ((List<object>)docs[i]["value"]).Add(dataReader["value"]);
                        }
                        dataReader.Close();
                    }
                }

                //person
                for (int i = 0; i < id.Count; i++)
                {
                    string query = "SELECT first_name, last_name, dob " +
                                "FROM ehr, patient_proxy pp, person p " +
                                "WHERE ehr.subject_id=pp.id AND pp.value=p.uid AND ehr.id=@id";
                    MySqlCommand cmd4 = new MySqlCommand(query, conn);
                    cmd4.Prepare();
                    cmd4.Parameters.AddWithValue("@id", docs[i]["ehr_id"]);
                    dataReader = cmd4.ExecuteReader();
                    while (dataReader.Read())
                    {
                        docs[i].Add("first_name", dataReader["first_name"]);
                        docs[i].Add("last_name", dataReader["last_name"]);
                        docs[i].Add("dob", dataReader["dob"]);
                    }
                    dataReader.Close();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Debug.Write("Error: {0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return docs;
        }
    }
}
