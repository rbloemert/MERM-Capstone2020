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
            
        }

        protected void Access(object sender, EventArgs e)
        {

            //Creates a database object.
            Database db = new Database();
            
            //Checks if the account is authenticated correctly.
            if(db.AuthenticateAccount(TextBoxUsername.Text, TextBoxPassword.Text) != null)
            {
                //Login session variables.
            }
        }
    }
}