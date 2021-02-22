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
        private List<Project2> projectList;
        private int creatorID = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            projectList = new List<Project2>();
            projectList = PopulateList();
            if (!this.IsPostBack)
            {
                // Checks to see if creator ID is passed through
                int tmp = Convert.ToInt32(Server.UrlDecode(Request.QueryString["Parameter"]));
                if (tmp >= 0)
                {
                    creatorID = tmp;
                }


                creatorProjectGrid.DataSource = projectList;
                creatorProjectGrid.DataBind();
                // Populate Creator information
                CreatorUsernameLabel.Text = "Rick The Creator Test";
                CreatorDescriptionLabel.Text = "\n\nBio:\nsadflkasjdlkjfasdl;jflsadjfl;safjs;lfsjdasljfls;ajsljfsaljflsajasfl;jasdl;flas";
            }


        }


        private List<Project2> PopulateList()
        {
            Database db = new Database();
            if(creatorID != -1) // Returns creator's projects given the creator ID
            {
                return db.GetProjectList(creatorID);
            }
            else
            {
                return db.GetProjectList();
            }
        }


        protected void btnSelectProject_Clicked(object sender, GridViewCommandEventArgs e) //e is the position of the project in the list
        {
            
            int index = Convert.ToInt32(e.CommandArgument);
            int focusedProjectID = projectList[index].projectID;



            Response.Redirect("Project?Parameter=" + Server.UrlEncode(focusedProjectID.ToString()));

            /*
             * For Project.aspx to read the Parameter passes projectID, to read refreshed data from db?:
             *      int focusedProjectID = Server.UrlDecord(Request.QueryString["Parameter"].ToInt());
             */



        }

        protected void btnFollowCreator_Clicked(object sender, EventArgs e)
        {


        }


    }
}