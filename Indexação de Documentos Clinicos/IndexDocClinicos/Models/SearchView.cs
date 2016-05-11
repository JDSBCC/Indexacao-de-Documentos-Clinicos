using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace IndexDocClinicos.Models
{
    public class SearchView
    {

        public SearchView()
        {
            Page = 0;
            Rows = 3;
            TotalResults = 0;
            SearchTerm = "";
        }

        public string SearchTerm { get; set; }

        public int Page { get; set; }

        public int Rows { get; set; }

        public int TotalResults { get; set; }

        public int NumberOfPages
        {
            get
            {
                return (int)(Math.Round((double)TotalResults / (double)Rows));
            }
        }

        public int StartPage
        {
            get
            {
                return Page * Rows;
            }
        }

        public List<Dictionary<string, string>> Results { get; set; }

        public void Reset()
        {
            Page = 0;
            TotalResults = 0;
            SearchTerm = "";
        }
    }
}