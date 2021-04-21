using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator
{

    //! Login page.
    /*!
     *  Used for logging into an account on the website.
     */
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            //Checks if the user is logged in.
            if (Session["User"] != null)
            {

                //Redirects to the homepage.
                Response.Redirect("Home.aspx");

            }

        }

        protected void ValidatePassword(object source, ServerValidateEventArgs args)
        {
            //Creates a database object.
            using (Database db = new Database())
            {
                Account user = db.AuthenticateAccount(TextBoxUsername.Text, args.Value);

                //Checks if the user has been authenticated.
                if (user != null)
                {
                    //Sets the validator to be valid.
                    args.IsValid = true;

                    //Login session variables.
                    Session["User"] = user;
                }
                else
                {
                    //Sets the validator to be invalid.
                    args.IsValid = false;
                }
            }
        }

        protected void Access(object sender, EventArgs e)
        {

            //Checks if the page has been validated.
            if (Page.IsValid)
            {

                //Checks if the last page is available.
                if(Session["Back"] != null)
                {

                    //Redirects to the last page.
                    Response.Redirect(Session["Back"].ToString());

                }
                else
                {

                    //Redirects to the homepage.
                    Response.Redirect("/Home");

                }

            }

        }
    }
}