using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.Projects.Updates {
    public partial class Edit : System.Web.UI.Page {
        public int ProjectID = 0;
        public int UpdateID = 0;
        public Project ProjectObject = new Project();
        public Timeline TimelineObject = new Timeline();

        protected void Page_Load(object sender, EventArgs e) {

            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            UpdateID = Convert.ToInt32(Request.QueryString["u"]);

            //Checks if the page is a post back.
            if (!IsPostBack) {

                Database db = new Database();
                string ProjectAuthor = db.GetProjectAuthor(ProjectID);

                //Checks if the project exists.
                if (ProjectID != 0) {

                    UpdateID = Convert.ToInt32(Request.QueryString["u"]);

                    if(UpdateID != 0) {

                        //Disables the sending notifications radio buttons.
                        RadioPublic.Visible = false;
                        RadioPrivate.Visible = false;

                        //Checks if the user is logged in.
                        if (Session["User"] != null) {

                            //Gets the session user object.
                            Account user = (Account)Session["User"];

                            //Checks if the creator is the author of this project.
                            if (user.username != ProjectAuthor) {

                                //Redirects the user to the home page.
                                Response.Redirect("/Home");

                            } else {

                                //Gets the project information.
                                ProjectObject = db.GetProject(ProjectID);
                                ProjectObject.project_author = db.GetProjectAuthor(ProjectID);
                                TimelineObject = db.GetTimeline(UpdateID);

                                //Sets the textbox to the project title.
                                TextBoxUpdate.Text = TimelineObject.timeline_name;
                                lblDate.Text = TimelineObject.timeline_creation.Value.ToString("yyyy-MM-dd"); ;
                                TimelineImage.ImageUrl = TimelineObject.timeline_image_path;
                                txtDesc.Text = TimelineObject.timeline_desc;
                                lblContent.Text = TimelineObject.timeline_file_path;
                            }

                        }

                    }

                } else {

                    //Checks if the user is logged in.
                    if (Session["User"] != null) {

                        //Gets the session user object.
                        Account user = (Account)Session["User"];

                        //Gets a project object.
                        ProjectObject = new Project("New Project", "", user.username, "NULL", 1);

                        //Creates a new project to be editted.
                        ProjectID = db.CreateProject(ProjectObject);

                        //Links the project to the account.
                        db.CreateProjectLink(ProjectID, user.accountID);

                        //Updates the project ID.
                        ProjectObject.projectID = ProjectID;

                    } else {

                        //Redirects the user to the home page.
                        Response.Redirect("~/Home");

                    }
                }

            }

        }

        protected void btnNewImage_Click(object sender, EventArgs e) {
            //TODO: Allow the user to upload an image
        }

        protected void btnNewFile_Click(object sender, EventArgs e) {
            //TODO: Add a way for the user to add content
        }

        protected void btnCancel_Click(object sender, EventArgs e) {

            //Redirects back to the project editing.
            Response.Redirect("~/Projects/Edit?p=" + ProjectID);

        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            UpdateID = Convert.ToInt32(Request.QueryString["u"]);

            if (ImageUploader.HasFile) {
                try {
                    switch (ImageUploader.PostedFile.ContentType) {
                        case ("image/jpeg"):
                        case ("image/png"):
                        case ("image/bmp"):
                            string filename = ProjectID + "" + UpdateID + Path.GetExtension(ImageUploader.PostedFile.FileName);
                            TimelineObject.timeline_image_path = StorageService.UploadFileToStorage(ImageUploader.FileContent, filename, StorageService.timeline_image);
                            if (TimelineObject.timeline_image_path == null) {
                                TimelineObject.timeline_image_path = TimelineImage.ImageUrl;
                            }
                            break;
                    }
                } catch (Exception ex) {

                }
            } else {
                TimelineObject.timeline_image_path = "";
            }
            if (ContentUploader.HasFile) {
                try {
                    switch (ContentUploader.PostedFile.ContentType) {
                        case ("image/jpeg"):
                        case ("image/png"):
                        case ("image/bmp"):
                        case ("application/pdf"):
                        case ("video/mp4"):
                        case ("text/plain"):
                            string filename = ProjectID + "" + UpdateID + Path.GetExtension(ContentUploader.PostedFile.FileName);
                            TimelineObject.timeline_file_path = StorageService.UploadFileToStorage(ContentUploader.FileContent, filename, StorageService.timeline_file);
                            if (TimelineObject.timeline_file_path == null) {
                                TimelineObject.timeline_file_path = lblContent.Text;
                            }
                            break;
                    }
                } catch (Exception ex) {

                }
            } else {
                TimelineObject.timeline_file_path = "";
            }

            //Gets the timeline object values.
            TimelineObject.timeline_name = TextBoxUpdate.Text;
            TimelineObject.timeline_desc = txtDesc.Text;
            
            //Checks if the update has an ID.
            if(UpdateID != 0)
            {

                //This should probably be changed to an actual date value.
                TimelineObject.timeline_creation = Convert.ToDateTime(lblDate.Text);

                //Updates the timeline in the database.
                Database db = new Database();
                if (db.ModifyTimeline(UpdateID, TimelineObject) == Database.QueryResult.Successful)
                {
                    Response.Redirect("~/Projects/Edit?p=" + ProjectID);
                }

            }
            else
            {

                //Creates a new date for the update object.
                TimelineObject.timeline_creation = new System.Data.SqlTypes.SqlDateTime(DateTime.Now);

                //Adds the new update to the project.
                Database db = new Database();
                int TimelineID = db.CreateTimeline(TimelineObject);
                db.CreateTimelineLink(TimelineID, ProjectID);
                if(RadioPublic.Checked == true)
                {
                    db.CreateNotifications(ProjectID, TimelineID);
                }

                //Redirects back to the project editing.
                Response.Redirect("~/Projects/Edit?p=" + ProjectID);

            }

        }

        protected void btnDelete_Click(object sender, EventArgs e) {
            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            UpdateID = Convert.ToInt32(Request.QueryString["u"]);

            Database db = new Database();
            List<Comment> comments = db.GetCommentList(UpdateID);
            foreach(Comment c in comments) {
                db.DeleteCommentLink(c.commentID, UpdateID, Int32.Parse(c.comment_owner_accountID));
                db.DeleteComment(c.commentID);
            }
            //Deletes the timeline link.
            db.DeleteTimelineLink(UpdateID, ProjectID);
            db.DeleteTimeline(UpdateID);

            //Redirects back to the project editing.
            Response.Redirect("~/Projects/Edit?p=" + ProjectID);
        }

    }

}