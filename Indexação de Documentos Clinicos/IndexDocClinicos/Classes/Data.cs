﻿using IndexDocClinicos.Models;
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

namespace IndexDocClinicos.Classes
{
    public class Data
    {
        private ISolrOperations<Contribution> solr;

        private List<DocContent> documents;//documents pdf
        private List<Patient> patients;//patients
        
        private MySqlDataReader dataReaderMySQL;
        private OracleDataReader dataReaderOracle;
        public static long time = 0;//REMOVE

        public Data()
        {
            //initializing data lists
            documents = new List<DocContent>();
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
            try
            {
                dataReaderOracle = null;
                Connection.openOracle();
                OracleCommand cmd = new OracleCommand("Select * from " +
                    "(select rownum as rn, NVL(e.DT_ACT, e.DT_CRI) as doc_date, d.documento_id, f.*, dl.doente, ge.*, " +
                    "c.n_proc, c.n_sns, c.n_benef, c.n_bi, c.data_nasc, s.codigo, s.descricao, ec.descricao as estado_civil from er_ficheiro f " +
                    "join er_elemento e on e.elemento_id=f.elemento_id and e.versao_activa='S' and e.cod_versao=f.cod_versao " +
                    "join er_documento d on d.documento_id=e.documento_id " +
                    "join gr_visita_documento vd on d.documento_id=vd.documento_id " +
                    "join gr_visita v on vd.visita_id=v.visita_id " +
                    "join gr_entidade ge on v.entidade_pai_id=ge.entidade_id " +
                    "join gr_doente c on ge.entidade_id = c.entidade_id " +
                    "join gr_doente_local dl on v.entidade_pai_id=dl.entidade_id  and dl.activo='S' " +
                    "left join er_sexo s on c.sexo_id=s.sexo_id " +
                    "left join er_estado_civil ec on ec.estado_civil_id=c.estado_civil_id) " +
                    "where " + condition, Connection.getOracleCon());
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
            } catch (OracleException e) {
                Debug.Write("Error: {0}", e.ToString());
                Connection.closeOracle();
                OracleConnection.ClearAllPools();
                documents = new List<DocContent>();
                patients = new List<Patient>();
                queryingEresults(condition);
            } catch (InvalidOperationException e) {
                Debug.Write("Error: {0}", e.ToString());
                Connection.closeOracle();
                OracleConnection.ClearAllPools();
                documents = new List<DocContent>();
                patients = new List<Patient>();
                queryingEresults(condition);
            } finally {
                Connection.closeOracle();
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

                DocContent doc = new DocContent
                {
                    Content = response
                };
                documents.Add(doc);


                stream.Close();
            }
        }

        private void saveMetadataContent()
        {
            Patient patient = new Patient
            {
                Nome = dataReaderOracle["NOME"] + "",
                Uid = Guid.NewGuid().ToString(),
                Entidade_id = Convert.ToInt32(dataReaderOracle["entidade_id"]),
                Doente = Convert.ToInt32(dataReaderOracle["doente"]),
                Elemento_id = Convert.ToInt32(dataReaderOracle["elemento_id"]),
                Documento_id = Convert.ToInt32(dataReaderOracle["documento_id"]),
                DocDate = Convert.ToDateTime(dataReaderOracle["doc_date"]),
                Version_Uid = Guid.NewGuid().ToString()
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

        public void commitDataSolr(DateTime time_committed)
        {
            List<Dictionary<string, object>> docs;
            while ((docs=QueryingEHR(time_committed))==null) ;
            solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();


            Stopwatch stopwatch = Stopwatch.StartNew(); //REMOVE
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
                        Elemento_id = Convert.ToInt32(((List<int>)docs[i]["ids"])[0]),
                        Documento_id = Convert.ToInt32(((List<int>)docs[i]["ids"])[1]),
                        Version_Uid = docs[i]["version_uid"] + "",
                        Content = documents[i].Content
                    });
                }
                solr.Commit();
            }
            stopwatch.Stop();//REMOVE
            time += stopwatch.ElapsedMilliseconds;
        }

        private List<Dictionary<string, object>> QueryingEHR(DateTime time_committed)
        {
            List<Dictionary<string, object>> docs = new List<Dictionary<string, object>>();

            try
            {
                Connection.openMySQL();

                //contribution
                MySqlCommand cmd = new MySqlCommand("SELECT cont.uid, dti.value as dv_text, ddti.value as dv_date, magnitude as dv_count, " +
                                                    "first_name, last_name, dob, v.uid as version_uid " +
                                                    "FROM contribution cont " +
                                                    "JOIN version v ON cont.id=v.contribution_id " +
                                                    "JOIN composition_index ci ON ci.id=v.data_id AND ci.last_version=1 " +
                                                    "JOIN ehr e ON cont.ehr_id=e.id " +
                                                    "JOIN patient_proxy pp ON e.subject_id=pp.id " +
                                                    "JOIN data_value_index dvi ON dvi.owner_id=ci.id " +
                                                    "LEFT JOIN dv_text_index dti ON dvi.id=dti.id " +
                                                    "LEFT JOIN dv_date_time_index ddti ON dvi.id=ddti.id " +
                                                    "LEFT JOIN dv_count_index dci ON dvi.id=dci.id AND " +
                                                    "(dvi.archetype_path='/items[at0017]/value' OR dvi.archetype_path='/items[at0018]/value') " +
                                                    "JOIN person p ON p.uid=pp.value, " +
                                                    "audit_details ad " +
                                                    "WHERE v.commit_audit_id = ad.id " + 
                                                    "AND ad.time_committed >= '"+time_committed.AddHours(-1).ToString("yyyyMMddHHmmss") +"' " +
                                                    "GROUP BY cont.uid, dti.value, ddti.value, dci.magnitude", Connection.getMySQLCon());
                dataReaderMySQL = null;
                dataReaderMySQL = cmd.ExecuteReader();
                while (dataReaderMySQL.Read())
                {
                    if (docs.Count != 0 && (dataReaderMySQL["uid"] + "").Equals(docs[docs.Count - 1]["uid"]))
                    {
                        if (!Convert.IsDBNull(dataReaderMySQL["dv_text"]))
                            ((List<string>)docs[docs.Count - 1]["value"]).Add(dataReaderMySQL["dv_text"] + "");
                        if (!Convert.IsDBNull(dataReaderMySQL["dv_date"]))
                            ((List<DateTime>)docs[docs.Count - 1]["dates"]).Add(Convert.ToDateTime(dataReaderMySQL["dv_date"]));
                        if (!Convert.IsDBNull(dataReaderMySQL["dv_count"]))
                            ((List<int>)docs[docs.Count - 1]["ids"]).Add(Convert.ToInt32(dataReaderMySQL["dv_count"]));
                    }
                    else
                    {
                        docs.Add(new Dictionary<string, object>());
                        docs[docs.Count - 1].Add("uid", dataReaderMySQL["uid"]);
                        docs[docs.Count - 1].Add("first_name", dataReaderMySQL["first_name"]);
                        docs[docs.Count - 1].Add("last_name", dataReaderMySQL["last_name"]);
                        docs[docs.Count - 1].Add("dob", dataReaderMySQL["dob"]);
                        docs[docs.Count - 1].Add("version_uid", dataReaderMySQL["version_uid"]);
                        docs[docs.Count - 1].Add("ids", new List<int>());
                        docs[docs.Count - 1].Add("value", new List<string>());
                        docs[docs.Count - 1].Add("dates", new List<DateTime>());
                        if (!Convert.IsDBNull(dataReaderMySQL["dv_text"]))
                            ((List<string>)docs[docs.Count - 1]["value"]).Add(dataReaderMySQL["dv_text"] + "");
                        if (!Convert.IsDBNull(dataReaderMySQL["dv_date"]))
                            ((List<DateTime>)docs[docs.Count - 1]["dates"]).Add(Convert.ToDateTime(dataReaderMySQL["dv_date"]));
                        if (!Convert.IsDBNull(dataReaderMySQL["dv_count"]))
                            ((List<int>)docs[docs.Count - 1]["ids"]).Add(Convert.ToInt32(dataReaderMySQL["dv_count"]));
                    }
                }
                dataReaderMySQL.Close();
            }
            catch (MySqlException ex)
            {
                Debug.Write("Error: {0}", ex.ToString());
            }
            catch (InvalidOperationException)
            {
                OracleConnection.ClearAllPools();
                return null;
            }
            finally
            {
                Connection.closeMySQL();
            }

            if (docs.Count == 0 || docs.Count!= documents.Count)
                return null;
            return docs;
        }
    }
}