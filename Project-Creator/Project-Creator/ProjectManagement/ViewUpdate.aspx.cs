using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.ProjectManagement {
    public partial class ViewUpdate : System.Web.UI.Page {
        private Update u;
        protected void Page_Load(object sender, EventArgs e) {
            string update = Session["Update"] as string;
            string project = Session["Project"] as string;

            if (update == null || project == null) {
                /*404*/
            } else {
                /*get from db and populate page*/
                string img = "";
                if (update == "002") {
                    img = "https://pbs.twimg.com/profile_images/656441231560585216/NWOReGD5_400x400.jpg";
                } else {
                    img = "https://cdn.escapistmagazine.com/media/global/images/library/deriv/1400/1400821.jpg";
                }
                u = new Update(img, "https://google.com", "<h3>Project Update Title</h3>", "<p>a description about the update which summarises the complete description that is displayed upon clicking on the card for more information.</p><br />", DateTime.Today);

                RelatedPanel.Controls.Add(createOtherPanel(u));
                Panel div = new Panel();
                div.CssClass = "w3-quarter";
                RelatedPanel.Controls.Add(div);
                RelatedPanel.Controls.Add(div);
                RelatedPanel.Controls.Add(createOtherPanel(u));

            }
        }



        /*
       * METHOD : createOtherPanel
       * DESCRIPTION :
       *	
       * PARAMETERS :
       *	Update update : the update that we want to add
       * RETURNS :
       *	Panel : a div containing all of the data relevant for a related project
       */
        public Panel createOtherPanel(Update update) {
            Panel div = new Panel();                /*the div we are building*/
            ImageButton image = new ImageButton();      /*this is the image that is used to diplay the project as well as navigate to that update*/
            Label nextPrev = new Label();
            Label updateTitle = new Label();        /*the label that will hold the title of the update*/

            div.CssClass = "w3-quarter";             /*the styling that is used for formatting the div*/

            image.Click += new System.Web.UI.ImageClickEventHandler(updateClick);
            image.ID = update.updatePage;
            image.ImageUrl = update.iconURL;        /*set the image we want to use for the hyperlink. it is stored within the Update object*/
            image.CssClass = "update-img";          /*the styling that is used for formatting the image*/

            if(update.date > u.date) {
                nextPrev.Text = "Next Update";
            } else {
                nextPrev.Text = "Prev Update";
            }
            updateTitle.Text = update.title;                /*set the title of the update based on the update object*/

            /*add the objects we created to the div in the order we want them to appear*/
            div.Controls.Add(image);
            div.Controls.Add(nextPrev);
            div.Controls.Add(updateTitle);

            return (div);
        }

        protected void updateClick(object sender, EventArgs e) {
            Session["Update"] = ((ImageButton)sender).ID;
            Response.Redirect("ProjectTimeline.aspx");
        }
    }
}