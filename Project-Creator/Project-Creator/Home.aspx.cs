using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator
{
    public partial class Home : System.Web.UI.Page
    {
        public int TimelineIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                //Gets a database connection.
                Database db = new Database();

                //Checks if the user is signed into the website.
                if (Session["User"] != null)
                {

                    //Enables the timeline divider.
                    WelcomeMessage.Visible = false;
                    Notifications.Visible = true;

                    //Gets the user object from the session.
                    Account user = (Account)Session["User"];

                    //Updates the profile labels.
                    lblUsername.Text = user.username;
                    lblFullname.Text = "(" + user.fullname + ")";
                    lblEmail.Text = user.email;
                    lblDate.Text = user.account_creation.Value.ToString("yyyy-MM-dd");
                    AccountIcon.ImageUrl = user.account_image_path;
                    lblFullname.Visible = user.allows_full_name_display;
                    divEmail.Visible = user.allows_email_contact;

                    //Gets all the current notifications for the user.
                    List<Timeline> ProjectTimeline = db.GetNotifications(user.accountID);

                    //Binds the data to the project timeline.
                    TimelineIndex = ProjectTimeline.Count - 1;
                    RepeaterTimeline.DataSource = ProjectTimeline;
                    RepeaterTimeline.DataBind();

                }
                else
                {

                    //Disables the timelinder divider.
                    Notifications.Visible = false;
                    WelcomeMessage.Visible = true;


                }

            }
            
        }
    }
}