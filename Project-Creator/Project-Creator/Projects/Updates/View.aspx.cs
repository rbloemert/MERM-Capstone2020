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
        public int TimelineIndex = 0;
        public bool loggedIn = false;
        public Account user = null;
        public Project project;
        List<Comment2> comments;
        protected void Page_Load(object sender, EventArgs e) {
            txtNewComment.Rows = 4;

            //check if the user is logged in
            if (Session["User"] != null) {
                //Gets the session user object.
                user = (Account)Session["User"];
                loggedIn = true;
                LoggedInUserImage.ImageUrl = user.account_image_path;
                lblNewCommentUser.Text = user.username;
            } else {
                txtNewComment.Enabled = false;
                txtNewComment.Text = "You must be signed in order to leave a comment";
            }

            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            UpdateID = Convert.ToInt32(Request.QueryString["u"]);

            if (IsPostBack) {

            } else {
                //Checks if the project and update is set.
                if ((ProjectID != 0) && (UpdateID != 0)) {

                    //Gets the database connection.
                    Database db = new Database();
                    project = db.GetProject(ProjectID);
                    project.project_author = db.GetProjectAuthor(ProjectID);

                    //Gets a list of all the timelines for the project.
                    List<Timeline> ProjectTimeline = db.GetTimelineList(ProjectID);
                    Timeline currentTimeline = new Timeline();
                    var counter = 0;
                    foreach (Timeline t in ProjectTimeline) {
                        if (t.timelineID == UpdateID) {
                            currentTimeline = t;
                            break;
                        }
                        counter++;
                    }
                    TimelineIndex = counter;

                    lblUpdate.Text = currentTimeline.timeline_name;
                    lblDate.Text = currentTimeline.timeline_creation.Value.ToString("yyyy-MM-dd");
                    TimelineImage.ImageUrl = currentTimeline.timeline_image_path;
                    lblDesc.Text = currentTimeline.timeline_desc;
                    lblContent.Text = currentTimeline.timeline_file_path;

                    //Sets the list to the timeline repeater.
                    RepeaterTimeline.DataSource = ProjectTimeline;
                    RepeaterTimeline.DataBind();

                    List<Comment> rawComments = db.GetCommentList(currentTimeline.timelineID);
                     comments = new List<Comment2>();
                    foreach (Comment c in rawComments) {
                        Comment2 newComment = new Comment2(c);
                        comments.Add(newComment);
                    }

                    RepeaterComment.DataSource = comments;
                    RepeaterComment.DataBind();

                } else {
                    Response.Redirect("~/Home");
                }
            }
        }

        protected void btnSubmitComment_Click(object sender, EventArgs e) {
            if (loggedIn) {
                //submit comment
 
                DateTime now = DateTime.Now;
                Comment comment = new Comment();
                comment.comment_creation = now;
                comment.comment_owner_accountID = user.accountID.ToString(); 
                comment.comment_text = txtNewComment.Text; //needs to be filtered for security reasons.
                Database db = new Database();
                db.CreateComment(comment);
                int id = db.GetRecentCommentID();
                db.CreateCommentLink(id, UpdateID, user.accountID);

                Response.Redirect("View?p="+ ProjectID + "&u=" + UpdateID);
            } else {
                Response.Redirect("/Login");
            }
        }

        protected void RepeaterComment_ItemCommand(object source, RepeaterCommandEventArgs e) {
            if (e.CommandName == "delete_comment") {
                if (loggedIn) {
                    string allParams = Convert.ToString(e.CommandArgument);
                    string[] paramaters = new string[2];
                    char splitter = ',';
                    paramaters = allParams.Split(splitter);
                    int commentID = Int32.Parse(paramaters[0]);
                    int comment_accountID = Int32.Parse(paramaters[1]);
                    Database db = new Database();
                    int projectOwner = db.GetProjectOwner(ProjectID);
                    if (user.accountID == comment_accountID || user.isSiteAdministrator || user.accountID == projectOwner) {
                        db.DeleteCommentLink(commentID, UpdateID, comment_accountID);
                        db.DeleteComment(commentID);
                        Response.Redirect("View?p=" + ProjectID + "&u=" + UpdateID);
                    }
                }
            }
        }
    }
}