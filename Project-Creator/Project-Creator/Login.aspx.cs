using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLoginFeedback.Text = "";
        }


        protected void btnLogin_Clicked(object sender, EventArgs e)
        {
            if(validatePassword())
            {
                // Basic validation grants allowance into this block.

            }
            else
            {
                lblLoginFeedback.Text = "Please try again.";
            }




        }

        protected bool validatePassword()
        {
            bool boolResponse = false;
            if (string.IsNullOrWhiteSpace(txtLoginUsername.Text) || string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                boolResponse = false;
            }
            else { boolResponse = true; }
            return boolResponse;
        }

        protected void btnClear_Clicked(object sender, EventArgs e)
        {
            lblLoginFeedback.Text = "";
            txtLoginUsername.Text = "";
            txtLoginPassword.Text = "";
        }
    }
}