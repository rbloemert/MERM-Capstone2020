using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator
{
    public partial class Account1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                MyAccountFullNameLabel.Text = "Full Name: " + ((Account)Session["user"]).fullname;
                MyAccountUsernameLabel.Text = "Username: " + ((Account)Session["user"]).username;
                MyAccountEmailLabel.Text = "Email: " + ((Account)Session["user"]).email;

                // projects
                Database db = new Database();
                List<Project> pr = db.GetProjectList(((Account)Session["user"]).accountID);
                MyAccountProjectsOwnedLabel.Text = "Projects Owned: " + pr.Count;
                MyAccountProjectRepeater.DataSource = pr;
            }
            else
            {
                MyAccountFullNameLabel.Text = "Not logged in!";
                MyAccountProjectsOwnedLabel.Text = "Not logged in!";
            }
            
        }
    }
}