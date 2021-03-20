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
        private int creatorAccountID;
        
        
        string sampleDesc = "Sample Description Text.Paragraphs are the building blocks of papers. Many students define paragraphs in terms of length: a paragraph is a group of at least five sentences, a paragraph is half a page long, etc.In reality, though, the unity and coherence of ideas among sentences is what constitutes a paragraph. A paragraph is defined as “a group of sentences or a single sentence that forms a unit” (Lunsford and Connors 116).";

        protected void Page_Load(object sender, EventArgs e)
        {
            creatorAccountID = Convert.ToInt32(Request.QueryString["creatorID"]);

            if (!this.IsPostBack)
            {
                PopulateGrid();
            }
            //Creator 
            CreatorUsernameLabel.Text = "Sample Username";
            CreatorDescriptionTextBox.Text = sampleDesc;
            CreatorIcon.ImageUrl = "/images/Test.png";
        }


        private void PopulateGrid()
        {
            //Gets the database connection.
            Database db = new Database();
            List<Project> projectList = db.GetProjectList(); //AccountID
            //List<Project> projectList = db.GetProjectList(creatorAccountID); //AccountID


            RepeaterRelated.DataSource = projectList;
            RepeaterRelated.DataBind();

        }


        protected void btnContactCreator_Clicked(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Contact Creator Clicked')</script>");

        }

        protected void btnSelectProject_Clicked(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Select Project Clicked')</script>");


        }


    }
}