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

        protected void ValidateUsername(object source, ServerValidateEventArgs args)
        {

            //Creates the database object.
            Database db = new Database();

            //Sets the validation value to whether the username exists.
            args.IsValid = !db.AccountExists(TextBoxUsername.Text);

        }

        protected void Access(object sender, EventArgs e)
        {
            //Account login functionality.
        }
    }
}