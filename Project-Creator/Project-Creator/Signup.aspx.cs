using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator
{
    public partial class Signup : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

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
                signupUser.fullname = "John Lad";
                signupUser.email = TextBoxEmail.Text;
                signupUser.isSiteAdministrator = false;

                //Signs the user up in the database.
                Database db = new Database();
                db.CreateAccount(signupUser);

            }

        }
    }
}