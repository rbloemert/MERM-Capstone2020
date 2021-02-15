/*
*	FILE : ViewUpdate.aspx.cs
*	PROJECT : Project Creator
*	PROGRAMMER : Mark Jackson
*	FIRST VERSION : 2021-02-07
*	DESCRIPTION :
*		This webpage displays all of the information related to a singular update
*		-it shows the update icon on the left
*		-it shows the update information of the right
*		-it shows links to the next and previous updates as well
*/
using System;
using System.Web.UI.WebControls;

namespace Project_Creator.ProjectManagement {
    public partial class ViewUpdate : System.Web.UI.Page {
        private Update currentUpdate;   /*the information related to the update we want to display is stored here*/


        /*
        * METHOD : Page_Load
        * DESCRIPTION :
        *	when the page is first loaded, populate the update with information from the database
        *	also draw the previous and next update links
        * PARAMETERS :
        *	object sender : the object that invoked the method
        *   EventArgs e : addional arguments thata may be useful
        * RETURNS :
        *	none
        */
        protected void Page_Load(object sender, EventArgs e) {
            string update = Session["Update"] as string;        /*the update we want to load up. it is passed from another screen*/
            string project = Session["Project"] as string;      /*the project the update is from. it is passed from another screen*/

            /*/if we did not get the information passed to us*/
            if (update == null || project == null) {
                /*404*/
            } else {
                /*get from db and populate page*/

                /*since db doesn't exist yet generate update information to display*/
                string img = "";
                if (project == "002") {
                    img = "https://pbs.twimg.com/profile_images/656441231560585216/NWOReGD5_400x400.jpg";
                } else {
                    img = "https://cdn.escapistmagazine.com/media/global/images/library/deriv/1400/1400821.jpg";
                }
                currentUpdate = new Update(img, "https://google.com", "<h3>Project Update Title</h3>", "<p>a description about the update which summarises the complete description that is displayed upon clicking on the card for more information.</p><br />", DateTime.Today);

                /*build elements to add to the content section*/
                Label title = new Label();
                title.Text = currentUpdate.title;
                title.CssClass = "update-title";
                Label content = new Label();
                content.Text = currentUpdate.description;

                /*add the content to the page*/
                updateData.Controls.Add(title);
                updateData.Controls.Add(content);

                /*make the sidebar image the one from the update*/
                sidebarImage.BackImageUrl = currentUpdate.iconURL;

                /*add the current update to the update panel*/
                RelatedPanel.Controls.Add(createOtherPanel(currentUpdate));
                Panel div = new Panel();
                div.CssClass = "timeline-content";
                RelatedPanel.Controls.Add(div);
                RelatedPanel.Controls.Add(div);
                div = createOtherPanel(currentUpdate);
                div.CssClass += " next-update";
                RelatedPanel.Controls.Add(div);
            }
        }



        /*
       * METHOD : createOtherPanel
       * DESCRIPTION :
       *	create the objects for the updates at the bottom of the screen
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

            div.CssClass = "timeline-content";             /*the styling that is used for formatting the div*/

            image.Click += new System.Web.UI.ImageClickEventHandler(updateClick);
            image.ID = update.updatePage;
            image.ImageUrl = update.iconURL;        /*set the image we want to use for the hyperlink. it is stored within the Update object*/
            image.CssClass = "related-img";          /*the styling that is used for formatting the image*/

            if(update.date > currentUpdate.date) {
                nextPrev.Text = "<br>Next Update";
            } else {
                nextPrev.Text = "<br>Prev Update";
            }
            updateTitle.Text = update.title;                /*set the title of the update based on the update object*/

            /*add the objects we created to the div in the order we want them to appear*/
            div.Controls.Add(image);
            div.Controls.Add(nextPrev);
            div.Controls.Add(updateTitle);

            return (div);
        }



        /*
        * METHOD : updateClick
        * DESCRIPTION :
        *	when prev/next update is pressed this method is invoked
        *	it navigates to somewhere. the current version is just a placeholder
        * PARAMETERS :
        *	object sender : the object that invoked the method
        *   EventArgs e : addional arguments thata may be useful
        * RETURNS :
        *	none
        *	we are navigated to another page
        */
        protected void updateClick(object sender, EventArgs e) {
            Session["Update"] = ((ImageButton)sender).ID;
            Response.Redirect("ProjectTimeline.aspx");
        }
    }
}