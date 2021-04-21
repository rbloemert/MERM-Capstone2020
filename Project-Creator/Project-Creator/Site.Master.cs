using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator
{

    //! Master page.
    /*!
     *  Inherited by every page to display navigation bar.
     */
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Checks if the user has a session avaiable.
            if(Session["User"] != null)
            {
                LinkLogin.Visible = false;
                LinkSignup.Visible = false;
                LinkAccount.Visible = true;
                LinkLogout.Visible = true;
            }

            //Checks if the page is postback.
            if (IsPostBack)
            {

                //Checks if the textbox is empty.
                if(TextBoxSearch.Text != "")
                {

                    //Sends the user to the search page.
                    Response.Redirect("~/Browse?s=" + TextBoxSearch.Text);

                }

            }

        }

        protected void Search_Click(object sender, ImageClickEventArgs e)
        {

            //Checks if the search box is not empty.
            if(TextBoxSearch.Text.Trim() != "")
            {

                //Redirects to the browsing page with a search.
                Response.Redirect("/Browse?s=" + TextBoxSearch.Text);

            }

        }

        protected void Link_Home(object sender, EventArgs e)
        {
            Response.Redirect("~/Home");
        }

        protected void Link_Browse(object sender, EventArgs e)
        {
            Response.Redirect("/Browse");
        }

        protected void Link_Signup(object sender, EventArgs e)
        {
            Response.Redirect("/Signup");
        }

        protected void Link_Login(object sender, EventArgs e)
        {
            Session["Back"] = Request.Url.ToString();
            Response.Redirect("/Login");
        }

        protected void Link_Account(object sender, EventArgs e)
        {
            Account user = (Account)Session["User"];
            Response.Redirect("/Creators/View?c=" + user.accountID.ToString());
        }
        protected void Link_Logout(object sender, EventArgs e)
        {

            //Clears the session variables.
            Session.Clear();

            //Redirects to the last page.
            Response.Redirect(Request.Url.ToString());

        }

    }

}