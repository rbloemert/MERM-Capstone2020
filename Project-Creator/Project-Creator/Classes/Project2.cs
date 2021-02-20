using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    // This class is to be merged with the actual Project class at some point
    // I just didn't want to break any existing components

    public class Project2
    {
        public int projectID;                   /* projectID: Unique ID of this project. */
        public SqlDateTime project_creation;    /* project_creation: When this project was created. */
        public string project_name;             /* project_name: Name of this project. */
        public string project_desc;             /* project_desc: Description of this project. */

        public static Database.QueryResult RegisterProject(Account project_owner, Project2 new_project)
        {
            // work in progress
            // TODO: implement proper error handling using QueryResult, handle ID correctly...
            Database db = new Database();
            db.CreateProject(new_project);
            db.CreateProjectLink(new_project.projectID, project_owner.accountID);
            return Database.QueryResult.Successful;
        }
    }
}