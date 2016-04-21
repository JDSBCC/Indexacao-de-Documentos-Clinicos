using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndexDocClinicos.Models
{
    public class Organization
    {
        public int Version { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string Uid { get; set; }
    }
}