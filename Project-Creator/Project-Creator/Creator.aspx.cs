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
        List<Project> projectList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                PopulateGrid();
            }
        }


        private void PopulateGrid()
        {
            projectList = new List<Project>();


            for(int i = 0; i < 5; i++)
            {
                projectList.Add(new Project("proj1", "me", null, i.ToString(), null));

            }


            creatorProjectGrid.DataSource = projectList;
            creatorProjectGrid.DataBind();

        }




    }
}