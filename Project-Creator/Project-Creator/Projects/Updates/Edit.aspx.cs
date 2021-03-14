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
        public string tempImagePath = "";

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
                        //Gets the database connection.
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
                                txtUpdate.Text = TimelineObject.timeline_name;
                                lblDate.Text = TimelineObject.timeline_creation.ToString();
                                TimelineImage.ImageUrl = TimelineObject.timeline_image_path;
                                txtDesc.Text = TimelineObject.timeline_desc;
                                lblDescCounter.Text = txtDesc.Text.Length + " of 255";
                                //txtContent.Text = TimelineObject.timeline_file_path;
                            }

                        }

                    } else {

                        //Redirects the user to the home page.
                        Response.Redirect("/Home");

                    }

                } else {

                    //Checks if the user is logged in.
                    if (Session["User"] != null) {

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

                    } else {

                        //Redirects the user to the home page.
                        Response.Redirect("/Home");

                    }
                }

            }

        }

        protected void btnNewImage_Click(object sender, EventArgs e) {
            //TODO: Allow the user to upload an image
            tempImagePath = "./Images/" + ProjectID + UpdateID + Path.GetExtension(ImageUploader.PostedFile.FileName);
            ImageUploader.SaveAs(tempImagePath);
            TimelineImage.ImageUrl = tempImagePath;
        }

        protected void btnNewFile_Click(object sender, EventArgs e) {
            //TODO: Add a way for the user to add content
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("View?p=" + ProjectID + "&u=" + UpdateID);
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            if (ImageUploader.HasFile) {
                try {
                    switch (ImageUploader.PostedFile.ContentType) {
                        case ("image/jpeg"):
                        case ("image/png"):
                        case ("image/bmp"):
                            string filename = "timeline_image/" + ProjectID + UpdateID + Path.GetExtension(ImageUploader.PostedFile.FileName);
                            StorageService.UploadFileToStorage(ImageUploader.FileContent, filename);
                            if (StorageService.DoesFileExistOnStorage(filename)) {
                                TimelineObject.timeline_image_path = "https://projectcreatorstorage.file.core.windows.net/projectcreator/" + filename;
                            }
                            break;
                    }
                } catch (Exception ex) {

                }
            } else {
                TimelineObject.timeline_image_path = TimelineImage.ImageUrl;
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
                            string filename = "timeline_file/" + ProjectID + UpdateID + Path.GetExtension(ImageUploader.PostedFile.FileName);
                            StorageService.UploadFileToStorage(ImageUploader.FileContent, filename);
                            if (StorageService.DoesFileExistOnStorage(filename)) {
                                TimelineObject.timeline_file_path = "https://projectcreatorstorage.file.core.windows.net/projectcreator/" + filename;
                            }
                            break;
                    }
                } catch (Exception ex) {

                }
            } else {
                TimelineObject.timeline_file_path = lblContent.Text;
            }

            TimelineObject.timeline_desc = txtDesc.Text;
            TimelineObject.timeline_name = txtUpdate.Text;
            TimelineObject.timeline_creation = Convert.ToDateTime(lblDate.Text);
            TimelineObject.timeline_name = txtUpdate.Text;
            Database db = new Database();
            if(db.ModifyTimeline(UpdateID, TimelineObject) == Database.QueryResult.Successful) {
                Response.Redirect("View?p=" + ProjectID + "&u=" + UpdateID);
            }
        }
    }

}