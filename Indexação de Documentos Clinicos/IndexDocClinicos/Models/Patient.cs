using System;

namespace IndexDocClinicos.Models
{
    public class Patient
    {
        public string Uid { get; set; }

        public string Doente { get; set; }

        public string Entidade_id { get; set; }

        public string Nome { get; set; }

        public int? N_Contribuinte { get; set; }

        public string Morada { get; set; }

        public string Localidade { get; set; }

        public string Codigo_Postal { get; set; }

        public double? Telefone1 { get; set; }

        public double? Telefone2 { get; set; }

        public double? Fax { get; set; }

        public double? N_Servico_Nacional_Saude { get; set; }

        public string N_Beneficiario { get; set; }

        public double? N_Cartao_Cidadao { get; set; }

        public DateTime Data_Nasc { get; set; }

        public string Sexo_Sigla { get; set; }

        public string Sexo { get; set; }

        public string Estado_Civil { get; set; }
    }
}