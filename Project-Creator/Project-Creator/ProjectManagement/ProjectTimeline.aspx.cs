/*
*	FILE : ProjectTimeline.aspx.cs
*	PROJECT : Project Creator
*	PROGRAMMER : Mark Jackson
*	FIRST VERSION : 2021-02-06
*	DESCRIPTION :
*		This webpage displays a short version of all of the project posts.
*		things it does.
*		-build and display details from each update*
*		-build and display some related projects*
*		things to do
*		-get project information via a parameter
*		-connect and read data from the database
*		-differentiate the project owner from viewers
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.ProjectManagement {
    public partial class ProjectTimeline : System.Web.UI.Page {
        string user = "user";   /*potentally gotten from somewhere. maybe the session*/

        /*this is the project that we are viewing. currently has place holder info. should be gotten from the previous screen*/
        /*may be pass as a string. if that is the case we need to reteieve from the database. we will see*/
        private static readonly Project[] sampleProjects = { new Project("Test Project", "Mark", "https://64.media.tumblr.com/avatar_4f1f4a829fdd_128.pnj", "001"), new Project("Second Project", "Mark", "https://ih1.redbubble.net/image.173786715.7034/flat,128x,075,f-pad,128x128,f8f8f8.u4.jpg", "002") };
        public Project project = sampleProjects[0];
        public ArrayList otherProjects = new ArrayList();


        /*
        * METHOD : Page_Load
        * DESCRIPTION :
        *	when the page is first loaded, populate the Update panel with updates for the project
        * PARAMETERS :
        *	object sender : the object that invoked the method
        *   EventArgs e : addional arguments thata may be useful
        * RETURNS :
        *	none
        */
        protected void Page_Load(object sender, EventArgs e) {
            string id = Session["Project"] as string;

            if (id != null) {
                /*read project data from db*/
                /*then populate*/

                if (id == "002") {
                    project = sampleProjects[1];
                }

                /*this next section needs to be moved inside the above if statement when we start linking the pages together*/
                Session["Project"] = project.id;
                projectIcon.ImageUrl = project.icon;    /*set the icon to be the icon stored within the project*/
                lblTitle.Text = project.title;          /*update the title label to the project name*/
                lblAuthor.Text = project.author;        /*update the author label with data from the project object*/

                /*for each of the updates within the project*/
                foreach (Update u in project.updates) {
                    /*create a div using createUpdatePanel and add it to the UpdatePanel section of the screen*/
                    Panel div = createUpdatePanel(u);
                    UpdatePanel.Controls.Add(div);
                }

                /*there are some additional options if the user is the owner of the project, in that case, they can create a new post*/
                /*we need to figure out how the user is logged in.*/
                if (user == "owner") {
                    /*create a new update object with a + logo and add it to the screen*/
                    Update newUpdate = new Update("https://media.istockphoto.com/vectors/black-plus-sign-positive-symbol-vector-id688550958?k=6&m=688550958&s=612x612&w=0&h=nVa-a5Fb79Dgmqk3F00kop9kF4CXFpF4kh7vr91ERGk=", "000", " < h3>New Update</h3> <br />", "<p />", DateTime.Today);
                    Panel p = createUpdatePanel(newUpdate);
                    UpdatePanel.Controls.Add(p);
                }



                /*generate related projects this will come from the db later*/
                /*create 4 projects to show at the bottom of the page*/
                /*they are all just the test project currently*/
                /*format the title to fit nicely on the screen. should probably be done differently. more research is needed*/
                for (int i = 0; i < 4; i++) {
                    Project currentProject;
                    if (i == 1) {
                        currentProject = sampleProjects[1];
                    } else {
                        currentProject = sampleProjects[0];
                    }
                    Project n = new Project("<h3>" + currentProject.title + "</h3>", "<p>" + currentProject.author + "</p>", currentProject.icon, "00" + (i + 1));
                    otherProjects.Add(n);
                }

                //RelatedPanel.Controls.Add
                /*for each of the projects in the related projects list*/
                int panelNum = 1;
                foreach (Project p in otherProjects) {
                    /*create a div using createUpdatePanel and add it to the related panel of the screen*/
                    Panel div = createRelatedPanel(p);
                    div.ID = "related" + panelNum;
                    RelatedPanel.Controls.Add(div);
                    panelNum++;
                }
            } else {
                /*404*/
                Session["Project"] = "001";
                Response.Redirect("ProjectTimeline.aspx");
            }

        }



        /*
        * METHOD : createRelatedPanel
        * DESCRIPTION :
        *	each of the projects in the related panel need to be dynamically generated. that is what this does.
        *	it builds a div with an image, the author of the project and, the title
        * PARAMETERS :
        *	Project project : the project that we want to add
        * RETURNS :
        *	Panel : a div containing all of the data relevant for a related project
        */
        public Panel createRelatedPanel(Project project) {
            Panel div = new Panel();                /*the div we are building*/
            ImageButton image = new ImageButton();  /*this is the image that is used to diplay the project as well as navigate to that project*/
            //HyperLink image = new HyperLink();
            Label title = new Label();              /*the label that will hold the title of the project*/
            Label author = new Label();             /*the label that will hold the author of the project*/

            div.CssClass = "w3-quarter";        /*the styling that is used for formatting the div*/

            image.Click += relatedClick;
            //image.Click += new System.Web.UI.ImageClickEventHandler(relatedClick);
            image.ID = project.id;
            image.ImageUrl = project.icon;      /*set the image we want to use for the hyperlink. it is stored within the Project object*/
            image.CssClass = "related-img";     /*the styling that is used for formatting the image*/

            title.Text = project.title;         /*set the title of the project based on the project object*/
            author.Text = project.author;       /*set the author of the project based on the project object*/

            /*add the objects we created to the div in the order we want them to appear*/
            div.Controls.Add(image);
            div.Controls.Add(title);
            div.Controls.Add(author);
            return (div);
        }



        protected void relatedClick(object sender, EventArgs e) {
            Session["Project"] = ((ImageButton)sender).ID;
            Response.Redirect("ProjectTimeline.aspx");
        }



        /*
        * METHOD : createUpdatePanel
        * DESCRIPTION :
        *	each of the updates in the updates panel need to be dynamically generated. that is what this does.
        *	it builds a div with an image, the update title, a breif update description and, the date of the update
        * PARAMETERS :
        *	Update update : the update we want to add
        * RETURNS :
        *	Panel : a div containing all of the data relevant for a related project
        */
        public Panel createUpdatePanel(Update update) {
            Panel div = new Panel();                /*the div we are building*/
            ImageButton image = new ImageButton();      /*this is the image that is used to diplay the project as well as navigate to that update*/
            //HyperLink image = new HyperLink();
            Label updateTitle = new Label();        /*the label that will hold the title of the update*/
            Label updateDescription = new Label();  /*the label that will hold the description of the update*/
            Label updateDate = new Label();         /*the label that will hold the date of the update*/

            div.CssClass = "w3-quarter";             /*the styling that is used for formatting the div*/
            image.Click += updateClick;
            //image.Click += new System.Web.UI.ImageClickEventHandler(relatedClick);
            //image.NavigateUrl = "ViewUpdate.aspx";
            image.ID = update.updatePage;
            image.ImageUrl = update.iconURL;        /*set the image we want to use for the hyperlink. it is stored within the Update object*/
            image.CssClass = "update-img";          /*the styling that is used for formatting the image*/

            updateTitle.Text = update.title;                /*set the title of the update based on the update object*/
            updateDescription.Text = update.description;    /*set the description of the update based on the update object*/
            updateDate.Text = update.date.ToString("d");    /*set the date of the update based on the update object and format it to be short date ex. 2020-02-02*/

            /*add the objects we created to the div in the order we want them to appear*/
            div.Controls.Add(image);
            div.Controls.Add(updateTitle);
            div.Controls.Add(updateDescription);
            div.Controls.Add(updateDate);

            return (div);
        }



        protected void updateClick(object sender, EventArgs e) {
            Session["Update"] = ((ImageButton)sender).ID;
            Response.Redirect("ViewUpdate.aspx");
        }

    }

}