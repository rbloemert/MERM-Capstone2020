using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.Creators
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null) // if this user is logged in
            {
                Database db = new Database();
                Session["user"] = db.GetAccountInfo(((Account)Session["user"]).accountID); // refresh account info
                MyAccountFullNameLabel.Text = "Full Name: " + ((Account)Session["user"]).fullname;
                MyAccountDescLabel.Text = "Desc: " + ((Account)Session["user"]).creatordesc;
                MyAccountUsernameLabel.Text = "Username: " + ((Account)Session["user"]).username;
                MyAccountEmailLabel.Text = "Email: " + ((Account)Session["user"]).email;

                // projects MIGRATE TO HOME PAGE
                //Database db = new Database();
                //List<Project> pr = db.GetProjectList(((Account)Session["user"]).accountID);
                //MyAccountProjectsOwnedLabel.Text = "Projects Owned: " + pr.Count;
                //MyAccountProjectRepeater.DataSource = pr;
            }
            else
            {
                Response.Redirect("/Login"); // redirect to login if not logged in
            }

        }

        protected void MyAccountEditButton_OnClick(object sender, EventArgs e)
        {
            if (Session["user"] == null) return; // if this user is not logged in, return

            MyAccountEditButton.Visible = false;
            myAccountEditPanel.Visible = true;

            fullNameTextbox.Text = ((Account)Session["user"]).fullname;
            creatorDescTextbox.Text = ((Account)Session["user"]).creatordesc;
            emailTextbox.Text = ((Account)Session["user"]).email;
            usernameTextbox.Text = ((Account)Session["user"]).username;
            // password
            allowFullnameCheckbox.Checked = ((Account)Session["user"]).allows_full_name_display;
            allowContactCheckbox.Checked = ((Account)Session["user"]).allows_email_contact;
        }

        protected void editSubmitButton_OnClick(object sender, EventArgs e)
        {
            // submits an edit
            if (Session["user"] == null) return; // if this user is not logged in, return

            Database db = new Database();
            switch (db.ModifyAccount(((Account)Session["user"]).accountID,
                new Account
                {
                    accountID = ((Account)Session["user"]).accountID,
                    account_creation = ((Account)Session["user"]).account_creation,
                    fullname = fullNameTextbox.Text,
                    creatordesc = creatorDescTextbox.Text,
                    username = usernameTextbox.Text,
                    password = passwordTextbox.Text,
                    password_salt = "",
                    email = emailTextbox.Text,
                    isSiteAdministrator = ((Account)Session["user"]).isSiteAdministrator,
                    account_image_path = ((Account)Session["user"]).account_image_path,
                    allows_full_name_display = allowFullnameCheckbox.Checked,
                    allows_email_contact = allowContactCheckbox.Checked,
                }))
            {
                case Database.QueryResult.Successful:
                    editErrorLabel.Text = "Successfully modified account.";
                    editErrorLabel.ForeColor = Color.Green;
                    editErrorLabel.Visible = true;
                    break;

                default:
                    editErrorLabel.Text = "Failed to modify account: " + db.GetLastSQLError();
                    editErrorLabel.ForeColor = Color.Red;
                    editErrorLabel.Visible = true;
                    break;
            }


        }

        protected void editCancelButton_OnClick(object sender, EventArgs e)
        {
            MyAccountEditButton.Visible = true;
            myAccountEditPanel.Visible = false;
        }
    }
}