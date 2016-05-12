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
            PagesNumberLimit = 5;
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
                return (int)(Math.Round((double)PagesNumberLimit / (double)2));
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
                int pageToReturn = Page - HalfPagesNumberLimit;
                if (pageToReturn >= 0 && Page + HalfPagesNumberLimit <= NumberOfPages-1)
                {
                    return pageToReturn;
                } else if(pageToReturn<0){
                    return 0;
                }
                return NumberOfPages - PagesNumberLimit;
            }
        }

        public int LastPage
        {//First page that appears in the page number
            get
            {
                int pageToReturn = Page + HalfPagesNumberLimit;
                if (pageToReturn <= NumberOfPages-1 && Page - HalfPagesNumberLimit >= 0)
                {
                    return pageToReturn;
                }
                else if (pageToReturn > NumberOfPages-1)
                {
                    return NumberOfPages-1;
                }
                return PagesNumberLimit-1;
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