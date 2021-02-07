using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    public class Project
    {
        public string title { get; set; }
        public string author { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
        public ArrayList updates;

        public Project(string t, string a, string i, string URL, ArrayList u)
        {
            title = t;
            author = a;
            icon = i;
            updates = u;
            url = URL;
        }
        public Project(string t, string a, string i, string URL)
        {
            title = t;
            author = a;
            icon = i;
            url = URL;
            updates = new ArrayList();
            getUpdates();
        }

        public bool getUpdates()
        {
            bool success = false;
            updates.Clear();
            /*get from db*/

            /*temp data for now*/
            for (int i = 0; i < 10; i++)
            {
                Update u = new Update("https://cdn.escapistmagazine.com/media/global/images/library/deriv/1400/1400821.jpg", "https://google.com", "<h3>Project Update Title</h3>", "<p>a description about the update which summarises the complete description that is displayed upon clicking on the card for more information.</p><br />", DateTime.Today);
                updates.Add(u);
            }
            success = true;
            return (success);
        }
    }
}