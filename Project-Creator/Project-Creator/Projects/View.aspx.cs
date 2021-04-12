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
        public int TimelineIndex = 0;
        public int CreatorID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                //Gets the project id from the URL.
                ProjectID = Convert.ToInt32(Request.QueryString["p"]);

                //Gets the database connection.
                Database db = new Database();
                Project project = db.GetProject(ProjectID);
                project.project_author = db.GetProjectAuthor(ProjectID);
                CreatorID = db.GetProjectOwner(ProjectID);

                //Gets the users account information.
                Account user = (Account)Session["User"];

                //Checks if the project should be visible.
                if ((project.project_visibility == 1) || ((Session["User"] != null) && (user.username == project.project_author)))
                {

                    List<int> followers = db.GetFollowers(ProjectID);
                    lblAuthor.Text = project.project_author;
                    lblTitle.Text = project.project_name;
                    lblDescription.Text = project.project_desc;
                    lblDate.Text = project.project_creation.Value.ToString("yyyy-MM-dd");
                    lblFollowers.Text = followers.Count.ToString() + " Followers";
                    ProjectIcon.ImageUrl = project.project_image_path;

                    //Checks if the user is logged in.
                    if (Session["User"] != null)
                    {

                        //Checks if the user doesn't exist in the followers table.
                        if (followers.Contains(user.accountID))
                        {

                            //Enables the already following note.
                            lblFollowing.Visible = true;

                            //Enables the follow button.
                            ButtonFollow.Text = "Unfollow";

                        }

                        //Checks if the user is the project owner.
                        if(user.username == project.project_author)
                        {

                            //Displays an unable to follow message.
                            ButtonFollow.Text = "You own this project";
                            ButtonFollow.Enabled = false;
                            ButtonEdit.Visible = true;

                            //Checks if the project is private.
                            if (project.project_visibility == 0)
                            {

                                //Displays a message saying the project is private.
                                lblFollowing.Text = "(Private)";

                            }
                            else
                            {

                                //Displays a message saying the project is private.
                                lblFollowing.Text = "(Public)";

                            }

                        }
                        else
                        {

                            //Disables the edit button.
                            ButtonEdit.Visible = false;

                        }

                    }
                    else
                    {

                        //Disables the follow button.
                        ButtonFollow.Enabled = false;

                        //Disables the edit button.
                        ButtonEdit.Visible = false;

                    }

                    //Gets a list of all the timelines for the project.
                    List<Timeline> ProjectTimeline = db.GetTimelineList(ProjectID);
                    TimelineIndex = ProjectTimeline.Count - 1;

                    //Sets the list to the timeline repeater.
                    RepeaterTimeline.DataSource = ProjectTimeline;
                    RepeaterTimeline.DataBind();

                }
                else
                {

                    //Redirects the user back to the home page.
                    Response.Redirect("~/Home");

                }

            }

        }

        protected void Follow_Click(object sender, EventArgs e)
        {

            //Gets the database connection.
            Database db = new Database();
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            Account user = (Account)Session["User"];
            List<int> followers = db.GetFollowers(ProjectID);
            var author = db.GetProjectAuthor(ProjectID);

            //Checks if the user doesn't exist in the followers.
            if (!followers.Contains(user.accountID) && (user.username != author))
            {

                //Adds the logged-in account to the project followers.
                db.AddFollower(ProjectID, user);

                //Redirects back to the page.
                Response.Redirect("~/Projects/View?p=" + ProjectID.ToString());

            }
            else
            {

                //Removes the logged-in account from the project followers.
                db.RemoveFollower(ProjectID, user.accountID);

                //Redirects back to the page.
                Response.Redirect("~/Projects/View?p=" + ProjectID.ToString());

            }

        }
    }
}