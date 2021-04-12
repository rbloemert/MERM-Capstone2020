using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator
{
    public partial class Signup : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            //Checks if the user is logged in.
            if (Session["User"] != null)
            {

                //Redirects to the homepage.
                Response.Redirect("/Home");

            }

        }

        protected void ValidateUsername(object source, ServerValidateEventArgs args)
        {

            //Creates the database object.
            Database db = new Database();

            //Sets the validation value to whether the username exists.
            args.IsValid = !db.AccountExists(args.Value);

        }

        protected void ValidateEmail(object source, ServerValidateEventArgs args)
        {

            //Creates the database object.
            Database db = new Database();

            //Sets the validation value to whether the username exists.
            args.IsValid = !db.EmailExists(args.Value);

        }

        protected void Register(object sender, EventArgs e)
        {

            //Checks if the page is valid.
            if(Page.IsValid == true)
            {

                //Defines the signup user object.
                Account signupUser = new Account();

                //Sets the values of the user from the signup page.
                signupUser.username = TextBoxUsername.Text;
                signupUser.password = TextBoxPassword.Text;
                signupUser.fullname = TextBoxFullName.Text;
                signupUser.email = TextBoxEmail.Text;
                signupUser.isSiteAdministrator = false;
                signupUser.account_image_path = "~/Images/Account_Placeholder.png";

                //Signs the user up in the database.
                Database db = new Database();
                db.CreateAccount(signupUser);

                Response.Redirect("~/Login");

            }

        }
    }
}