using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.Projects
{
    public partial class Edit : System.Web.UI.Page
    {
        public int ProjectID;
        public int TimelineIndex = 0;
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
                            lblDate.Text = ProjectObject.project_creation.Value.ToString("yyyy-MM-dd");
                            RadioPrivate.Checked = (ProjectObject.project_visibility == 0);
                            RadioPublic.Checked = (ProjectObject.project_visibility == 1);

                            //Gets a list of all the timelines for the project.
                            List<Timeline> ProjectTimeline = db.GetTimelineList(ProjectID);
                            TimelineIndex = ProjectTimeline.Count - 1;

                            //Sets the list to the timeline repeater.
                            RepeaterTimeline.DataSource = ProjectTimeline;
                            RepeaterTimeline.DataBind();

                        }

                    }
                    else
                    {

                        //Redirects the user to the home page.
                        Response.Redirect("~/Home");

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
                        ProjectObject = new Project("New Project", "", user.username, "NULL", 0);

                        //Creates a new project to be editted.
                        ProjectID = db.CreateProject(ProjectObject);

                        //Links the project to the account.
                        db.CreateProjectLink(ProjectID, user.accountID);

                        //Updates the project ID.
                        ProjectObject.projectID = ProjectID;

                        //Redirects to the project edit page.
                        Response.Redirect("~/Projects/Edit?p=" + ProjectID.ToString());

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

            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);

            //Redirects to the project update edit page.
            Response.Redirect("~/Projects/Updates/Edit?p=" + ProjectID.ToString());

        }

        protected void Save_Click(object sender, EventArgs e)
        {

            //Creates a database connection.
            Database db = new Database();

            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            ProjectObject = new Project();
            ProjectObject = db.GetProject(ProjectID);
            ProjectObject.project_author = db.GetProjectAuthor(ProjectID);

            HttpPostedFile file = Request.Files["ImageUploader"];

            //Creates a new project with the project changes.
            Project proj = new Project();
            proj.project_name = TextBoxTitle.Text;
            proj.project_desc = TextBoxDescription.Text;
            proj.project_author = ProjectObject.project_author;
            proj.project_creation = ProjectObject.project_creation;
            if (file != null && file.ContentLength > 0) {
                try {
                    switch (file.ContentType) {
                        case ("image/jpeg"):
                        case ("image/png"):
                        case ("image/bmp"):
                            string filename = ProjectID + Path.GetExtension(file.FileName);
                            proj.project_image_path = StorageService.UploadFileToStorage(file.InputStream, filename, StorageService.project_image, file.ContentType);
                            break;
                    }
                } catch (Exception ex) {

                }
            } else {
                proj.project_image_path = ProjectObject.project_image_path;
            }
            proj.project_visibility = Convert.ToInt32(RadioPublic.Checked);

            //Attempts to save and publish project changes.
            db.ModifyProject(ProjectID, proj);

            //Sends the creator to view their page.
            Response.Redirect("~/Projects/View?p=" + ProjectID.ToString());

        }

        protected void Delete_Click(object sender, EventArgs e)
        {

            //Creates a database connection.
            Database db = new Database();

            //Gets the project ID.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            int AccountID = db.GetProjectOwner(ProjectID);

            //Gets the list of project timelines.
            List<Timeline> ProjectTimeline = db.GetTimelineList(ProjectID);

            //Loops through each timeline of the project.
            foreach(var update in ProjectTimeline)
            {

                //Deletes the timeline link.
                db.DeleteTimelineLink(update.timelineID, ProjectID);
                db.DeleteTimeline(update.timelineID);

            }

            //Deletes the project and link.
            db.DeleteProjectLink(ProjectID, AccountID);
            db.DeleteProject(ProjectID);

            //Sends the creator to their account page.
            Response.Redirect("~/Home");


        }


    }

}