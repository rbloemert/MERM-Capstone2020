using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    public class Project2
    {
        public int projectID;                   /* projectID: Unique ID of this project. */
        public SqlDateTime project_creation;    /* project_creation: When this project was created. */
        public string project_name;             /* project_name: Name of this project. */
        public string project_desc;             /* project_desc: Description of this project. */

        public static Database.QueryResult RegisterProject()
        {
            // to be completed
            return Database.QueryResult.FailedNoChanges;
        }
    }
}