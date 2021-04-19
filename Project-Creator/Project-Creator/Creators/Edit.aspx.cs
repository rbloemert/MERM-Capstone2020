using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Creator.Classes;

namespace Project_Creator.Creators
{
    public partial class Edit : System.Web.UI.Page
    {
        public string imagePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var salt = Password.Salt();
            Debug.WriteLine(salt);
            Debug.WriteLine(Password.Encrypt("zIIMf47EsLd9", salt));
            if (!IsPostBack)
            {
                if (Session["user"] != null) // if this user is logged in
                {
                    using (Database db = new Database())
                    {
                        Session["user"] = db.GetAccountInfo(((Account)Session["user"]).accountID); // refresh account info
                        fullNameTextbox.Text = ((Account)Session["user"]).fullname;
                        creatorDescTextbox.Text = ((Account)Session["user"]).creatordesc;
                        emailTextbox.Text = ((Account)Session["user"]).email;
                        usernameTextbox.Text = ((Account)Session["user"]).username;
                        allowFullnameCheckbox.Checked = ((Account)Session["user"]).allows_full_name_display;
                        allowContactCheckbox.Checked = ((Account)Session["user"]).allows_email_contact;
                        imagePath = ((Account)Session["user"]).account_image_path;
                    }
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
                    using (Database db = new Database())
                    {
                        var salt = Password.Salt();
                        HttpPostedFile file = Request.Files["ImageUploader"];
                        if (file != null && file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(imagePath);
                            if (fileName != null && fileName.ToUpper() != "NULL" && fileName.ToUpper() != "")
                            {
                                try
                                {
                                    StorageService.DeleteFileFromStorage(fileName, StorageService.account_image);
                                }
                                catch
                                {

                                }
                            }
                            try
                            {
                                switch (file.ContentType)
                                {
                                    case ("image/jpeg"):
                                    case ("image/png"):
                                    case ("image/bmp"):
                                        string accountID = ((Account)Session["user"]).accountID.ToString();
                                        int ID = int.Parse(accountID);
                                        Random rnd = new Random();
                                        if (ID == 0)
                                        {
                                            ID = rnd.Next(int.MinValue, int.MaxValue);
                                        }
                                        string filename = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ID.ToString())) + Path.GetExtension(file.FileName);
                                        imagePath = StorageService.UploadFileToStorage(file.InputStream, filename, StorageService.account_image, file.ContentType);
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            imagePath = ((Account)Session["user"]).account_image_path;
                        }

                        var user = new Account
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
                            account_image_path = imagePath,
                            allows_full_name_display = allowFullnameCheckbox.Checked,
                            allows_email_contact = allowContactCheckbox.Checked,
                        };

                        switch (db.ModifyAccount(((Account)Session["user"]).accountID, user))
                        {
                            case Database.QueryResult.Successful:
                                Session["user"] = user;
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
                else
                {
                    using (Database db = new Database())
                    {
                        HttpPostedFile file = Request.Files["ImageUploader"];
                        if (file != null && file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(imagePath);
                            if (fileName != null && fileName.ToUpper() != "NULL" && fileName.ToUpper() != "")
                            {
                                try
                                {
                                    StorageService.DeleteFileFromStorage(fileName, StorageService.account_image);
                                }
                                catch
                                {

                                }
                            }
                            try
                            {
                                switch (file.ContentType)
                                {
                                    case ("image/jpeg"):
                                    case ("image/png"):
                                    case ("image/bmp"):
                                        string accountID = ((Account)Session["user"]).accountID.ToString();
                                        string filename = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(accountID)) + Path.GetExtension(file.FileName);
                                        imagePath = StorageService.UploadFileToStorage(file.InputStream, filename, StorageService.account_image, file.ContentType);
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            imagePath = ((Account)Session["user"]).account_image_path;
                        }

                        var user = new Account
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
                            account_image_path = imagePath,
                            allows_full_name_display = allowFullnameCheckbox.Checked,
                            allows_email_contact = allowContactCheckbox.Checked,
                        };

                        switch (db.ModifyAccount(((Account)Session["user"]).accountID, user))
                        {
                            case Database.QueryResult.Successful:
                                Session["user"] = user;
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

        protected void DeleteAccount_Click(object sender, EventArgs e)
        {
            // some form of safeguard goes here?

            using (Database db = new Database())
            {
                Database.QueryResult res = db.DeleteAccountFull(((Account)Session["user"]).accountID);
                switch (res)
                {
                    case Database.QueryResult.Successful:
                        Session.Clear(); // logout
                        Response.Redirect("~/Home");
                        break;

                    default:
                        // this is what happens if there's an error... I don't know how you'd like to display errors here so

                        //editErrorLabel.Text = "Failed to delete account: " + db.GetLastSQLError() + " (" + res + ")";
                        //editErrorLabel.ForeColor = Color.Red;
                        //editErrorLabel.Visible = true;
                        break;
                }
            }
        }
    }
}