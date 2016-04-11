using System;
using System.Collections.Generic;
using SolrNet.Attributes;

namespace IndexDocClinicos.Models
{
    public class Document
    {
        [SolrUniqueKey("elemento_id")]
        public int Elemento_id { get; set; }

        [SolrField("text")]
        public int Text { get; set; }
    }
}