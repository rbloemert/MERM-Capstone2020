using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.Projects
{
    public partial class Edit : System.Web.UI.Page
    {
        public int ProjectID = 0;
        public Project ProjectObject = new Project();

        protected void Page_Load(object sender, EventArgs e)
        {

            //Checks if the page is a post back.
            if (!IsPostBack)
            {

                //Gets the project id from the URL.
                ProjectID = Convert.ToInt32(Request.QueryString["p"]);

                //Gets the database connection.
                Database db = new Database();
                string ProjectAuthor = db.GetProjectAuthor(ProjectID);

                //Checks if the project exists.
                if (ProjectID != 0)
                {

                    //Checks if the user is logged in.
                    if (Session["User"] != null)
                    {

                        //Gets the session user object.
                        Account user = (Account)Session["User"];

                        //Checks if the creator is the author of this project.
                        if (user.username != ProjectAuthor)
                        {

                            //Redirects the user to the home page.
                            Response.Redirect("../Home");

                        }
                        else
                        {

                            //Gets the project information.
                            ProjectObject = db.GetProject(ProjectID);

                            //Sets the textbox to the project title.
                            TextBoxTitle.Text = ProjectObject.project_name;

                        }

                    }
                    else
                    {

                        //Redirects the user to the home page.
                        Response.Redirect("../Home");

                    }

                }
                else
                {

                    //Checks if the user is logged in.
                    if (Session["User"] != null)
                    {

                        //Gets the session user object.
                        Account user = (Account)Session["User"];

                        //Gets a project object.
                        ProjectObject = new Project("New Project", "", user.username, "NULL");

                        //Creates a new project to be editted.
                        ProjectID = db.CreateProject(ProjectObject);

                        //Links the project to the account.
                        db.CreateProjectLink(ProjectID, user.accountID);

                        //Updates the project ID.
                        ProjectObject.projectID = ProjectID;

                    }
                    else
                    {

                        //Redirects the user to the home page.
                        Response.Redirect("../Home");

                    }
                }

            }

        }

    }

}