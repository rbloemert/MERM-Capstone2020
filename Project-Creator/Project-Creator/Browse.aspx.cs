using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Creator.Classes;

namespace Project_Creator
{
    public partial class Browse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Gets a connection to the database.
            Database db = new Database();

            //Gets a list of projects.
            List<Project> projects = new List<Project>();

            //Adds the projects from the database to the list.
            projects = db.GetProjectList();

            //Sets the repeater data source to the project list.
            RepeaterProject.DataSource = projects;

        }
    }
}