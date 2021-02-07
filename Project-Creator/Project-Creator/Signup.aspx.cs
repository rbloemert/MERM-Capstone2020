using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project_Creator.Classes;

namespace Project_Creator
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Signup(object sender, EventArgs e)
        {

            //Defines the signup user object.
            User signupUser = new User();

            //Sets the values of the user from the signup page.
            signupUser.username = TextBoxUsername.Text;
            signupUser.password = TextBoxPassword.Text;
            signupUser.firstname = TextBoxFirstName.Text;
            signupUser.lastname = TextBoxLastName.Text;
            signupUser.email = TextBoxEmail.Text;

            //Signs the user up in the database.
            Database db = new Database();
            db.Signup(signupUser);

        }
    }
}