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
        protected void Page_Load(object sender, EventArgs e) {

            //Gets the project id from the URL.
            ProjectID = Convert.ToInt32(Request.QueryString["p"]);
            UpdateID = Convert.ToInt32(Request.QueryString["u"]);

            //Gets the database connection.
            Database db = new Database();

            Project project = db.GetProject(ProjectID);
            project.project_author = db.GetAuthor(ProjectID);
            

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
        }
    }
}