using System;
using System.Collections.Generic;
using System.Configuration;

namespace IndexDocClinicos.Models
{
    public class SearchView
    {

        public SearchView()
        {
            Page = 0;
            Rows = Convert.ToInt32(ConfigurationManager.AppSettings["ResultsPerPage"]);//3;
            TotalResults = 0;
            SearchTerm = "";
            PagesNumberLimit = Convert.ToInt32(ConfigurationManager.AppSettings["LimitOfPages"]);
        }

        public string SearchTerm { get; set; }

        public int Page { get; set; }

        public int PreviousPage
        {
            get
            {
                return Page;
            }
        }

        public int NextPage
        {
            get
            {
                return Page+2;
            }
        }

        public int Rows { get; set; }

        public int TotalResults { get; set; }

        public int PagesNumberLimit { get; set; }

        public int HalfPagesNumberLimit
        {
            get
            {
                return PagesNumberLimit / 2;
            }
        }

        public int NumberOfPages
        {
            get
            {
                return (int)(Math.Round((double)TotalResults / (double)Rows));
            }
        }

        public int Start
        {//number of the result that will appear in first placa on the page
            get
            {
                return Page * Rows;
            }
        }

        public int StartPage
        {//First page that appears in the page number
            get
            {
                return (Page / PagesNumberLimit)*PagesNumberLimit;
            }
        }

        public int LastPage
        {//First page that appears in the page number
            get
            {
                int var = StartPage + PagesNumberLimit - 1;
                return var>NumberOfPages-1?NumberOfPages-1:var; 
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