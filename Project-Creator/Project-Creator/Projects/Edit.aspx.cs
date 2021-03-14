﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.Projects
{
    public partial class Edit : System.Web.UI.Page
    {
        public int ProjectID;
        public Project ProjectObject;

        protected void Page_Load(object sender, EventArgs e)
        {

            //Checks if the page is a post back.
            if (!IsPostBack)
            {

                //Gets the project id from the URL.
                ProjectID = Convert.ToInt32(Request.QueryString["p"]);
                ProjectObject = new Project();

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
                            Response.Redirect("~/Home");

                        }
                        else
                        {

                            //Gets the project information.
                            ProjectObject = db.GetProject(ProjectID);
                            ProjectObject.project_author = db.GetProjectAuthor(ProjectID);

                            //Sets the textbox to the project title.
                            TextBoxTitle.Text = ProjectObject.project_name;
                            TextBoxDescription.Text = ProjectObject.project_desc;
                            lblAuthor.Text = ProjectObject.project_author;
                            lblDate.Text = ProjectObject.project_creation.ToString();

                            //Gets a list of all the timelines for the project.
                            List<Timeline> ProjectTimeline = db.GetTimelineList(ProjectID);

                            //Sets the list to the timeline repeater.
                            RepeaterTimeline.DataSource = ProjectTimeline;
                            RepeaterTimeline.DataBind();

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
                        Response.Redirect("~/Home");

                    }
                }

            }

        }

        protected void AddUpdate_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Complete_Click(object sender, EventArgs e)
        {

            //Creates a database connection.
            Database db = new Database();

            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            ProjectObject = new Project();
            ProjectObject = db.GetProject(ProjectID);
            ProjectObject.project_author = db.GetProjectAuthor(ProjectID);

            //Creates a new project with the project changes.
            Project proj = new Project();
            proj.project_name = TextBoxTitle.Text;
            proj.project_desc = TextBoxDescription.Text;
            proj.project_author = ProjectObject.project_author;
            proj.project_creation = ProjectObject.project_creation;
            proj.project_image_path = "NULL";

            //Attempts to save and publish project changes.
            db.ModifyProject(ProjectID, proj);

            //Sends the creator to view their page.
            Response.Redirect("~/Projects/View?p=" + ProjectID.ToString());

        }
    }

}