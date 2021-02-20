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

            // Please have a look at the new Account class and change this function accordingly
            // I commented it out because it caused build errors due to the changes I made,
            // and I'm not sure how to fix it to work with the website's content
            // - Max




            ////Defines the signup user object.
            //Account signupUser = new Account();

            ////Sets the values of the user from the signup page.
            //signupUser.username = TextBoxUsername.Text;
            //signupUser.password = TextBoxPassword.Text;
            //signupUser.fullname = TextBoxFullName.Text;
            //signupUser.email = TextBoxEmail.Text;

            ////Checks if the user is a creator or user.
            //if(RadioCreator.Checked == true)
            //{
            //    signupUser.type = (int)Type.creator;
            //}
            //else
            //{
            //    signupUser.type = (int)Type.user;
            //}

            ////Signs the user up in the database.
            //Database db = new Database();
            //db.Register(signupUser);

        }
    }
}