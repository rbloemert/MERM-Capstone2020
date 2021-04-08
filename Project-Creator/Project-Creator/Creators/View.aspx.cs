using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Project_Creator.Creators
{
    public partial class View : System.Web.UI.Page
    {
        
        Account CreatorAccount;
        public int creatorAccountID;


        protected void Page_Load(object sender, EventArgs e)
        {
            creatorAccountID = Int32.Parse(Request.QueryString["c"]);
            Database db = new Database();
            CreatorAccount = db.GetAccountInfo(creatorAccountID);

            if (!this.IsPostBack)
            {
                PopulateGrid();
            }
            //Creator 
            CreatorUsernameLabel.Text = CreatorAccount.username;
            CreatorDescriptionTextBox.Text = "Created: " + CreatorAccount.account_creation.ToString();
            CreatorIcon.ImageUrl = CreatorAccount.account_image_path;
        }


        private void PopulateGrid()
        {
            //Gets the database connection.
            Database db = new Database();
            //List<Project> projectList = db.GetProjectList(); //AccountID
            List<Project> projectList = db.GetProjectList(CreatorAccount.accountID); //AccountID

            foreach (Project i in projectList)
            {
                if (i.project_visibility == 0)
                {
                    projectList.Remove(i);
                }
            }

            RepeaterRelated.DataSource = projectList;
            RepeaterRelated.DataBind();

        }


        protected void btnContactCreator_Clicked(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "mailto", "parent.location='mailto:richardbloemert10@gmail.com'", true);
            ClientScript.RegisterStartupScript(this.GetType(), "mailto", "parent.location='mailto:" + CreatorAccount.email + "'", true);
        }

       

    }
}