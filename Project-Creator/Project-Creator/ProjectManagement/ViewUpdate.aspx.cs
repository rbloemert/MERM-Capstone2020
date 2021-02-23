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
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Project_Creator.ProjectManagement {
    public partial class ViewUpdate : System.Web.UI.Page {
        private Timeline currentUpdate;   /*the information related to the update we want to display is stored here*/
        private List<Timeline> timelines;
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
                /*temp redirect for testing*/
                Session["Project"] = "-2";
                Session["Update"] = "1";
                Response.Redirect("ViewUpdate.aspx");
            } else {
                int updateID = Int32.Parse(update) - 1;
                /*get from db and populate page*/
                Database db = new Database();
                timelines = db.GetTimelineList(Int32.Parse(project));
                currentUpdate = timelines[updateID];

                /*build elements to add to the content section*/
                Label title = new Label();
                title.Text = "<h1>" + currentUpdate.timeline_name +"</h1>";
                title.CssClass = "update-title";
                Label content = new Label();
                content.Text = "<p>" + currentUpdate.timeline_desc + "</p>";

                /*add the content to the page*/
                updateData.Controls.Add(title);
                updateData.Controls.Add(content);

                /*make the sidebar image the one from the update*/
                sidebarImage.BackImageUrl = currentUpdate.timeline_image_path;

                /*add the current update to the update panel*/
                Panel div = new Panel();
                div.CssClass = "timeline-content";
                Timeline nextUpdate;
                Timeline prevUpdate;
                try {
                    prevUpdate = timelines[updateID - 1];
                } catch {
                    prevUpdate = null;
                }
                try {
                    nextUpdate = timelines[updateID + 1];
                } catch {
                    nextUpdate = null;
                }
                 
                if (prevUpdate == null) {
                    RelatedPanel.Controls.Add(div);
                } else {
                    RelatedPanel.Controls.Add(createOtherPanel(prevUpdate, updateID));
                }
                if (nextUpdate == null) {
                    RelatedPanel.Controls.Add(div);
                } else {
                    div = createOtherPanel(nextUpdate, updateID + 2);
                    div.CssClass += " next-update";
                    RelatedPanel.Controls.Add(div);
                }
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
        public Panel createOtherPanel(Timeline update, int updateNumber) {
            Panel div = new Panel();                /*the div we are building*/
            ImageButton image = new ImageButton();      /*this is the image that is used to diplay the project as well as navigate to that update*/
            Label nextPrev = new Label();
            Label updateTitle = new Label();        /*the label that will hold the title of the update*/

            div.CssClass = "timeline-content";             /*the styling that is used for formatting the div*/

            image.Click += new System.Web.UI.ImageClickEventHandler(updateClick);
            image.ID = updateNumber.ToString();
            image.ImageUrl = update.timeline_image_path;        /*set the image we want to use for the hyperlink. it is stored within the Update object*/
            image.CssClass = "related-img";          /*the styling that is used for formatting the image*/

            if(update.timeline_creation > currentUpdate.timeline_creation) {
                nextPrev.Text = "<br>Next Update<br>";
            } else {
                nextPrev.Text = "<br>Prev Update<br>";
            }
            updateTitle.Text = update.timeline_name;                /*set the title of the update based on the update object*/

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
            Response.Redirect("ViewUpdate.aspx");
        }
    }
}