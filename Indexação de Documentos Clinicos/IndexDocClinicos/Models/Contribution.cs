using System;
using System.Collections.Generic;
using SolrNet.Attributes;

namespace IndexDocClinicos.Models
{
    public class Contribution
    {

        [SolrUniqueKey("uid")]
        public string Uid { get; set; }//version uid

        [SolrField("value")]
        public List<string> Value { get; set; }

        [SolrField("dates")]
        public List<DateTime> Dates { get; set; }

        [SolrField("first_name")]
        public string First_name { get; set; }

        [SolrField("last_name")]
        public string Last_name { get; set; }

        [SolrField("dob")]
        public DateTime Dob { get; set; }

        [SolrField("elemento_id")]
        public int Elemento_id { get; set; }//id do ficheiro

        [SolrField("documento_id")]
        public int Documento_id { get; set; }//nº do doente

        [SolrField("content")]
        public string Content { get; set; }//conteúdo do ficheiro

        [SolrField("entidade_id")]
        public int Entidade_id { get; set; }//nº do paciente

        [SolrField("doente")]
        public int Doente { get; set; }//nº do doente
    }
}