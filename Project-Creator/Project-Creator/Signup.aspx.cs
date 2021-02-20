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

        protected void Register(object sender, EventArgs e)
        {
            //Defines the signup user object.
            Account signupUser = new Account();

            //Sets the values of the user from the signup page.
            signupUser.username = TextBoxUsername.Text;
            signupUser.password = TextBoxPassword.Text;
            signupUser.firstname = "John";
            signupUser.lastname = "Lad";
            signupUser.email = TextBoxEmail.Text;
            signupUser.isSiteAdministrator = false;

            //Signs the user up in the database.
            Database db = new Database();
            db.CreateAccount(signupUser);

        }
    }
}