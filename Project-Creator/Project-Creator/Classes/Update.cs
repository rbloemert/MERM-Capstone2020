using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    public class Update
    {
        public string iconURL { get; set; }
        public string updatePage { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }

        public Update(string iconUrl, string pageUrl, string t, string desc, DateTime d)
        {
            iconURL = iconUrl;
            updatePage = pageUrl;
            title = t;
            description = desc;
            date = d;
        }
    }
}