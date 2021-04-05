﻿using System;
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
            //Checks if the creator is defined.
            if (Request.QueryString["c"] != null)
            {

                creatorAccountID = Int32.Parse(Request.QueryString["c"]);
                Database db = new Database();
                CreatorAccount = db.GetAccountInfo(creatorAccountID);

                //Creator 
                lblUsername.Text = CreatorAccount.username;
                lblDate.Text = "Created: " + CreatorAccount.account_creation.Value.ToString("yyyy-MM-dd");
                CreatorIcon.ImageUrl = CreatorAccount.account_image_path;

                int visibility = 1;

                //Checks if the user is logged in.
                if (Session["User"] != null)
                {

                    //Gets the user object.
                    Account user = (Account)Session["User"];

                    //Checks if the user is the project owner.
                    if (user.username == CreatorAccount.username)
                    {

                        //Displays an unable to follow message.
                        ButtonEdit.Visible = true;
                        ButtonAddProject.Visible = true;
                        visibility = 0;

                    }
                    else
                    {

                        //Disables the edit button.
                        ButtonEdit.Visible = false;
                        ButtonAddProject.Visible = false;

                    }

                }
                else
                {

                    //Disables the edit button.
                    ButtonEdit.Visible = false;
                    ButtonAddProject.Visible = false;

                }

                //Gets the database connection.
                db = new Database();

                //List<Project> projectList = db.GetProjectList(); //AccountID
                List<Project> projectList = new List<Project>();

                //Checks if the search is defined.
                if (Request.QueryString["s"] != null)
                {

                    //Gets the search string.
                    string search = Request.QueryString["s"];

                    //Checks if the search option is defined.
                    if (Request.QueryString["o"] != null)
                    {

                        //Gets the search option.
                        int option = Convert.ToInt32(Request.QueryString["o"]);

                        //Checks if the option is valid.
                        if (option != 0)
                        {

                            //Gets the project list.
                            projectList = db.GetProjectList(creatorAccountID, search, visibility, option);

                        }
                        else
                        {

                            //Gets the project list.
                            projectList = db.GetProjectList(creatorAccountID, search, visibility, 3);

                        }

                    }
                    else
                    {

                        //Gets the project list.
                        projectList = db.GetProjectList(creatorAccountID, search, visibility, 3);

                    }

                }
                else
                {

                    //Gets the project list.
                    projectList = db.GetProjectList(creatorAccountID, "", visibility, 3);

                }

                RepeaterProject.DataSource = projectList;
                RepeaterProject.DataBind();

            }
            
        }


        protected void btnContactCreator_Clicked(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "mailto", "parent.location='mailto:richardbloemert10@gmail.com'", true);
            ClientScript.RegisterStartupScript(this.GetType(), "mailto", "parent.location='mailto:" + CreatorAccount.email + "'", true);
        }

        protected void Search_Click(object sender, EventArgs e)
        {

            //Redirects to the account project search.
            Response.Redirect("~/Creators/View?c=" + creatorAccountID.ToString() + "&s=" + SearchBox.Text + "&o=" + DropDownSort.SelectedValue.ToString());

        }

        protected void AddProject_Click(object sender, ImageClickEventArgs e)
        {

            //Redirects to project creation page.
            Response.Redirect("~/Projects/Edit");

        }
    }
}