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

        [SolrField("value")]
        public List<string> Value { get; set; }

        [SolrField("first_name")]
        public string First_name { get; set; }

        [SolrField("last_name")]
        public string Last_name { get; set; }

        [SolrField("dob")]
        public DateTime Dob { get; set; }
    }
}