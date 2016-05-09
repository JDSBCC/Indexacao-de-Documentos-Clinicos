using System;
using System.Collections.Generic;
using SolrNet.Attributes;

namespace IndexDocClinicos.Models
{
    public class Contribution
    {
        [SolrUniqueKey("id")]
        public int Id { get; set; }

        [SolrField("ehr_id")]
        public int Ehr_id { get; set; }

        [SolrField("archetype_id")]
        public List<string> Archetype_id { get; set; }

        [SolrField("template_id")]
        public string Template_id { get; set; }

        [SolrField("uid")]
        public string Uid { get; set; }//version uid

        [SolrField("value")]
        public List<string> Value { get; set; }

        [SolrField("first_name")]
        public string First_name { get; set; }

        [SolrField("last_name")]
        public string Last_name { get; set; }

        [SolrField("dob")]
        public DateTime Dob { get; set; }

        [SolrField("elemento_id")]
        public int Elemento_id { get; set; }//id do ficheiro

        [SolrField("cod_versao")]
        public int Cod_Versao { get; set; }//versão do ficheiro

        [SolrField("content")]
        public string Content { get; set; }//conteúdo do ficheiro

        [SolrField("entidade_id")]
        public int Entidade_id { get; set; }//nº do paciente

        [SolrField("doente")]
        public int Doente { get; set; }//nº do doente

        [SolrField("file_stream")]
        public string File_Stream { get; set; }
    }
}