using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.Projects
{
    public partial class View : System.Web.UI.Page
    {
        public int ProjectID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);

            //Gets the database connection.
            Database db = new Database();

            Project project = db.GetProject(ProjectID);
            project.project_author = db.GetAuthor(ProjectID);
            lblAuthor.Text = project.project_author;
            lblTitle.Text = project.project_name;
            lblDate.Text = project.project_creation.ToString();
            ProjectIcon.ImageUrl = project.project_image_path;

            //Gets a list of all the timelines for the project.
            List<Timeline> ProjectTimeline = db.GetTimelineList(ProjectID);

            //Sets the list to the timeline repeater.
            RepeaterTimeline.DataSource = ProjectTimeline;
            RepeaterTimeline.DataBind();

            List<Project> projects = db.GetProjectList();
            List<Project> otherProjects = new List<Project>();
           /*for each of the projects in the related projects list*/
           int panelNum = 1;
            foreach (Project p in projects) {
                if(p.projectID != ProjectID) {
                    panelNum++;
                    otherProjects.Add(p);
                    if (panelNum > 4) {
                        break;
                    }
                }
            }
            RepeaterRelated.DataSource = otherProjects;
            RepeaterRelated.DataBind();
        }
    }
}