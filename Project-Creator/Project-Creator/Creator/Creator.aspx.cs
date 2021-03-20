using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Project_Creator
{
    public partial class Creator : System.Web.UI.Page
    {
        
        Account CreatorAccount;



        string sampleDesc = "Sample Description Text.Paragraphs are the building blocks of papers. Many students define paragraphs in terms of length: a paragraph is a group of at least five sentences, a paragraph is half a page long, etc.In reality, though, the unity and coherence of ideas among sentences is what constitutes a paragraph. A paragraph is defined as “a group of sentences or a single sentence that forms a unit” (Lunsford and Connors 116).";

        protected void Page_Load(object sender, EventArgs e)
        {
            int creatorAccountID = 3;//Convert.ToInt32(Request.QueryString["creatorID"]);
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