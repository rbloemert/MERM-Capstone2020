using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Project_Creator.Projects.Updates {
    public partial class Edit : System.Web.UI.Page {
        public int ProjectID = 0;
        public int UpdateID = 0;
        public Project ProjectObject = new Project();
        public Timeline TimelineObject = new Timeline();
        public string FileLink = "";
        public Stream ImageStream;
        public string FileType = "";

        protected void Page_Load(object sender, EventArgs e) {

            //Checks if not a postback.
            if (!Page.IsPostBack)
            {

                //Gets the project id from the URL.
                ProjectID = Convert.ToInt32(Request.QueryString["p"]);
                UpdateID = Convert.ToInt32(Request.QueryString["u"]);

                Database db = new Database();
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;

                //Checks if the project exists.
                if (ProjectID != 0)
                {

                    string ProjectAuthor = db.GetProjectAuthor(ProjectID);

                    if (UpdateID != 0)
                    {

                        //Checks if the user is logged in.
                        if (Session["User"] != null)
                        {

                            //Disables the sending notifications radio buttons.
                            RadioPublic.Visible = false;
                            RadioPrivate.Visible = false;

                            //Gets the session user object.
                            Account user = (Account)Session["User"];

                            //Checks if the creator is the author of this project.
                            if (user.username != ProjectAuthor)
                            {

                                //Redirects the user to the home page.
                                Response.Redirect("/Home");

                            }
                            else
                            {

                                if (db.CheckTimelineInProject(ProjectID, UpdateID))
                                {

                                    //Gets the project information.
                                    ProjectObject = db.GetProject(ProjectID);
                                    ProjectObject.project_author = db.GetProjectAuthor(ProjectID);
                                    TimelineObject = db.GetTimeline(UpdateID);

                                    //Sets the textbox to the project title.
                                    TextBoxUpdate.Text = TimelineObject.timeline_name;
                                    lblDate.Text = TimelineObject.timeline_creation.Value.ToString("yyyy-MM-dd"); ;
                                    //TimelineImage.ImageUrl = TimelineObject.timeline_image_path;
                                    txtDesc.Text = TimelineObject.timeline_desc;
                                    FileLink = TimelineObject.timeline_file_path;
                                    FileType = ti.ToTitleCase(System.IO.Path.GetExtension(FileLink).Replace(".", ""));

                                    //Checks the extension of the uploaded file.
                                    switch (System.IO.Path.GetExtension(FileLink))
                                    {
                                        case ".png":
                                        case ".jpg":
                                        case ".jpeg":
                                        case ".bmp":
                                            //Displays the artwork showcase.
                                            FileImage.Style["display"] = "block";
                                            break;
                                        case ".mp4":
                                            //Displays the video player.
                                            FileVideo.Style["display"] = "block";
                                            break;
                                        case ".pdf":
                                            FilePDF.Style["display"] = "block";
                                            break;
                                        case ".txt":
                                            FileText.Style["display"] = "block";
                                            var webRequest = WebRequest.Create(@FileLink);
                                            using (var response = webRequest.GetResponse())
                                            using (var content = response.GetResponseStream())
                                            using (var reader = new StreamReader(content))
                                            {
                                                var strContent = reader.ReadToEnd();
                                                strContent = strContent.Replace("&", "&amp");
                                                strContent = strContent.Replace("<", "&lt");
                                                strContent = strContent.Replace(">", "&gt");
                                                //strContent = strContent.Replace("\n", "<br>");
                                                FileTextContent.Text = "<br>" + strContent;
                                            }
                                            break;
                                        case ".zip":
                                            FileZip.Style["display"] = "block";
                                            break;
                                    }
                                }
                                else
                                {
                                    //Redirects the user to the home page.
                                    Response.Redirect("~/Projects/Edit?p=");
                                }
                            }

                        }

                    }
                    else
                    {
                        TimelineObject.timeline_image_path = "~/Images/Update_Placeholder.png";
                        TimelineObject.timeline_file_path = "";
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
                        ProjectObject = new Project("New Project", "", user.username, "NULL", 1);

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

        protected void btnCancel_Click(object sender, EventArgs e) {

            //Redirects back to the project editing.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            Response.Redirect("~/Projects/Edit?p=" + ProjectID);

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                //Gets the project id from the URL.
                ProjectID = Convert.ToInt32(Request.QueryString["p"]);
                UpdateID = Convert.ToInt32(Request.QueryString["u"]);
                Database db = new Database();
                TimelineObject = db.GetTimeline(UpdateID);

                HttpPostedFile file = Request.Files["ImageUploader"]; 
                Random rnd = new Random();

                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(TimelineObject.timeline_image_path);
                    if (fileName != null && fileName.ToUpper() != "NULL" && fileName.ToUpper() != "")
                    {
                        try
                        {
                            StorageService.DeleteFileFromStorage(fileName, StorageService.timeline_image);
                        }
                        catch
                        {

                        }
                    }
                    try
                    {
                        switch (file.ContentType)
                        {
                            case ("image/jpeg"):
                            case ("image/png"):
                            case ("image/bmp"):
                                int ID = UpdateID;
                                if(ID == 0) {
                                    ID = rnd.Next(int.MinValue, int.MaxValue);
                                }
                                string id = ProjectID + "" + ID;
                                string filename = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(id)) + Path.GetExtension(file.FileName);
                                TimelineObject.timeline_image_path = StorageService.UploadFileToStorage(file.InputStream, filename, StorageService.timeline_image, file.ContentType);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(TimelineObject.timeline_image_path))
                    {
                        TimelineObject.timeline_image_path = "~/Images/Update_Placeholder.png";
                    }
                }

                file = Request.Files["ContentUploader"];
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(TimelineObject.timeline_file_path);
                    if (fileName != null && fileName.ToUpper() != "NULL" && fileName.ToUpper() != "")
                    {
                        try
                        {
                            StorageService.DeleteFileFromStorage(fileName, StorageService.timeline_file);
                        }
                        catch
                        {

                        }
                    }
                    try
                    {
                        switch (file.ContentType)
                        {
                            case ("image/jpeg"):
                            case ("image/png"):
                            case ("image/bmp"):
                            case ("application/pdf"):
                            case ("video/mp4"):
                            case ("text/plain"):
                            case ("application/x-zip-compressed"):
                                int ID = UpdateID;
                                if (ID == 0) {
                                    ID = rnd.Next(int.MinValue, int.MaxValue);
                                }
                                string id = ProjectID + "" + ID;
                                string filename = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(id)) + Path.GetExtension(file.FileName);
                                TimelineObject.timeline_file_path = StorageService.UploadFileToStorage(file.InputStream, filename, StorageService.timeline_file, file.ContentType);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(TimelineObject.timeline_file_path))
                    {
                        TimelineObject.timeline_file_path = "";
                    }
                }

                //Gets the timeline object values.
                TimelineObject.timeline_name = TextBoxUpdate.Text;
                TimelineObject.timeline_desc = txtDesc.Text;

                //Checks if the update has an ID.
                if (UpdateID != 0)
                {

                    //This should probably be changed to an actual date value.
                    TimelineObject.timeline_creation = Convert.ToDateTime(lblDate.Text);

                    //Updates the timeline in the database.
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
                    int TimelineID = db.CreateTimeline(TimelineObject);
                    db.CreateTimelineLink(TimelineID, ProjectID);
                    if (RadioPublic.Checked == true)
                    {
                        db.CreateNotifications(ProjectID, TimelineID);
                    }

                    //Redirects back to the project editing.
                    Response.Redirect("~/Projects/Edit?p=" + ProjectID);

                }

            }

        }

        protected void btnDelete_Click(object sender, EventArgs e) {
            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            UpdateID = Convert.ToInt32(Request.QueryString["u"]);

            Database db = new Database();
            List<Comment> comments = db.GetCommentList(UpdateID);
            foreach (Comment c in comments) {
                db.DeleteCommentLink(c.commentID, UpdateID, Int32.Parse(c.comment_owner_accountID));
                db.DeleteComment(c.commentID);
            }
            db.DeleteAllNotifications(UpdateID);
            //Deletes the timeline link.
            db.DeleteTimelineLink(UpdateID, ProjectID);
            db.DeleteTimeline(UpdateID);

            //Redirects back to the project editing.
            Response.Redirect("~/Projects/Edit?p=" + ProjectID);
        }

    }

}