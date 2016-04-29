using System;
using System.Collections.Generic;
using SolrNet.Attributes;

namespace IndexDocClinicos.Models
{
    public class Document
    {
        [SolrUniqueKey("elemento_id")]
        public int Elemento_id { get; set; }

        [SolrField("cod_versao")]
        public int Cod_Versao { get; set; }

        [SolrField("value")]
        public string Value { get; set; }


        [SolrField("entidade_id")]
        public int Entidade_id { get; set; }

        [SolrField("doente")]
        public int Doente { get; set; }
    }
}