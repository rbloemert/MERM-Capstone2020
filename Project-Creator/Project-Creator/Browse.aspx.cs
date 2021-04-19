using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Creator.Classes;

namespace Project_Creator
{
    public partial class Browse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Checks if not postback.
            if (!IsPostBack)
            {

                //Gets the search term from the search bar.
                string search = "";

                //Checks if the quer string is valid.
                if (Request.QueryString["s"] != null)
                {
                    search = Request.QueryString["s"].ToString();
                }

                //Gets a connection to the database.
                using (Database db = new Database())
                {
                    //Gets a list of creators.
                    List<Account> accounts = new List<Account>();
                    if (search != "")
                    {
                        accounts = db.GetAccountList(search);
                    }
                    else
                    {
                        accounts = db.GetAccountList();
                    }

                    //Checks if the accounts list is not empty.
                    if (accounts.Count > 0)
                    {
                        //Binds the data to the repeater.
                        RepeaterCreator.DataSource = accounts;
                        RepeaterCreator.DataBind();
                    }
                    else
                    {
                        //Hides the creator listings.
                        SearchCreator.Visible = false;
                    }

                    //Gets a list of projects.
                    List<Project> projects = new List<Project>();

                    //Checks if the option is set.
                    if (Request.QueryString["o"] != null)
                    {
                        //Gets the integer of the option value.
                        int option = Convert.ToInt32(Request.QueryString["o"]);

                        //Checks if the option is valid.
                        if (option != 0)
                        {
                            //Adds the projects from the database to the list.
                            projects = db.GetProjectList(search, 1, option);
                        }
                    }
                    else
                    {
                        //Adds the projects from the database to the list.
                        projects = db.GetProjectList(search, 1, 3);
                    }

                    //Gets the project authors for each project.
                    for (var p = 0; p < projects.Count; p++)
                    {
                        projects[p].project_author = db.GetProjectAuthor(projects[p].projectID);
                    }

                    //Sets the repeater data source to the project list.
                    RepeaterProject.DataSource = projects;
                    RepeaterProject.DataBind();
                }
            }

        }

        protected void Search_Click(object sender, EventArgs e)
        {

            //Posts back to the browsing with search options.
            Response.Redirect("~/Browse?s=" + SearchBox.Text + "&o=" + DropDownSort.SelectedValue);

        }
    }
}