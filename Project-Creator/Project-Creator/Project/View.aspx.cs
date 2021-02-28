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
        protected void Page_Load(object sender, EventArgs e)
        {

            //Gets the project id from the URL.
            int ProjectID = Convert.ToInt32(Request.QueryString["p"]);

            //Gets the database connection.
            Database db = new Database();

            //Gets a list of all the timelines for the project.
            List<Timeline> ProjectTimeline = db.GetTimelineList(ProjectID);

            //Sets the list to the timeline repeater.
            RepeaterTimeline.DataSource = ProjectTimeline;
            RepeaterTimeline.DataBind();

        }
    }
}