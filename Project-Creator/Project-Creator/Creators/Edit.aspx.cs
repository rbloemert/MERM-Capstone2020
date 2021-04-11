using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Creator.Classes;

namespace Project_Creator.Creators
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var salt = Password.Salt();
            Debug.WriteLine(salt);
            Debug.WriteLine(Password.Encrypt("zIIMf47EsLd9", salt));
            if (!IsPostBack)
            {
                if (Session["user"] != null) // if this user is logged in
                {
                    Database db = new Database();
                    Session["user"] = db.GetAccountInfo(((Account)Session["user"]).accountID); // refresh account info
                    fullNameTextbox.Text = ((Account)Session["user"]).fullname;
                    creatorDescTextbox.Text = ((Account)Session["user"]).creatordesc;
                    emailTextbox.Text = ((Account)Session["user"]).email;
                    usernameTextbox.Text = ((Account)Session["user"]).username;
                    allowFullnameCheckbox.Checked = ((Account)Session["user"]).allows_full_name_display;
                    allowContactCheckbox.Checked = ((Account)Session["user"]).allows_email_contact;
                }
                else
                {
                    Response.Redirect("/Login"); // redirect to login if not logged in
                }
            }

        }

        protected void MyAccountEditButton_OnClick(object sender, EventArgs e)
        {

        }

        protected void editSubmitButton_OnClick(object sender, EventArgs e)
        {

            //Checks if the page is valid.
            if (Page.IsValid)
            {

                // submits an edit
                if (Session["user"] == null) return; // if this user is not logged in, return

                //Checks if the password field is empty.
                if ((passwordTextbox.Text != "") && (passwordConfirmTextbox.Text != ""))
                {

                    Database db = new Database();
                    var salt = Password.Salt();
                    switch (db.ModifyAccount(((Account)Session["user"]).accountID,
                        new Account
                        {
                            accountID = ((Account)Session["user"]).accountID,
                            account_creation = ((Account)Session["user"]).account_creation,
                            fullname = fullNameTextbox.Text,
                            creatordesc = creatorDescTextbox.Text,
                            username = ((Account)Session["user"]).username,
                            password = Password.Encrypt(passwordTextbox.Text, salt),
                            password_salt = salt,
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
                else
                {

                    Database db = new Database();
                    switch (db.ModifyAccount(((Account)Session["user"]).accountID,
                        new Account
                        {
                            accountID = ((Account)Session["user"]).accountID,
                            account_creation = ((Account)Session["user"]).account_creation,
                            fullname = fullNameTextbox.Text,
                            creatordesc = creatorDescTextbox.Text,
                            username = ((Account)Session["user"]).username,
                            password = ((Account)Session["user"]).password,
                            password_salt = ((Account)Session["user"]).password_salt,
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

            }

        }
    }
}