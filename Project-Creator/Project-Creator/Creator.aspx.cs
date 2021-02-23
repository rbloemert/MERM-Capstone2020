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
        List<Project2> projectList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                PopulateGrid();
            }
        }


        private void PopulateGrid()
        {
            projectList = new List<Project2>();


            for(int i = 0; i < 5; i++)
            {
                string x = "Project: " + (i + 1);
                projectList.Add(new Project2(i, x, "me", "Unavailable"));

            }


            //creatorProjectGrid.DataSource = projectList;
            //creatorProjectGrid.DataBind();

        }


        protected void btnSelectProject_Clicked(object sender, GridViewCommandEventArgs e) //e is the position of the project in the list
        {
            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = creatorProjectGrid.Rows[index];




        }

        protected void btnFollowCreator_Clicked(object sender, EventArgs e)
        {


        }


    }
}