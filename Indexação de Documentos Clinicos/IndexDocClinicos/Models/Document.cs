using System;
using System.Collections.Generic;

namespace IndexDocClinicos.Models
{
    public class Document
    {
        public int Elemento_id { get; set; }

        public int Cod_Versao { get; set; }

        public string Content { get; set; }

        public int Entidade_id { get; set; }

        public int Doente { get; set; }
    }
}