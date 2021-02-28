using Project_Creator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.Posts {
    public partial class View : System.Web.UI.Page {
        public int ProjectID = 0;
        public int UpdateID = 0;
        public bool loggedIn = false;
        protected void Page_Load(object sender, EventArgs e) {
            //check if we are logged in
            if (!loggedIn) {
                btnSubmitComment.Enabled = false;
                txtNewComment.Enabled = false;
            }

            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            UpdateID = Convert.ToInt32(Request.QueryString["u"]);

            //Gets the database connection.
            Database db = new Database();
            Project project = db.GetProject(ProjectID);
            project.project_author = db.GetProjectAuthor(ProjectID);
            

            //Gets a list of all the timelines for the project.
            List<Timeline> ProjectTimeline = db.GetTimelineList(ProjectID);
            Timeline currentTimeline = new Timeline();
            foreach(Timeline t in ProjectTimeline) {
                if(t.timelineID == UpdateID) {
                    currentTimeline = t;
                    break;
                }
            }

            lblUpdate.Text = currentTimeline.timeline_name;
            lblTitle.Text = project.project_name;
            lblDate.Text = currentTimeline.timeline_creation.ToString();
            ProjectIcon.ImageUrl = project.project_image_path;
            TimelineImage.ImageUrl = currentTimeline.timeline_image_path;
            lblDesc.Text = currentTimeline.timeline_desc;

            //Sets the list to the timeline repeater.
            RepeaterTimeline.DataSource = ProjectTimeline;
            RepeaterTimeline.DataBind();

            List<Comment> rawComments = db.GetCommentList(currentTimeline.timelineID);
            List<Comment2> comments = new List<Comment2>();
            foreach(Comment c in rawComments) {
                Comment2 newComment = new Comment2(c);
                comments.Add(newComment);
            }

            RepeaterComment.DataSource = comments;
            RepeaterComment.DataBind();

        }

        protected void btnSubmitComment_Click(object sender, EventArgs e) {
            if (loggedIn) {
                //submit comment
            }
        }
    }
}