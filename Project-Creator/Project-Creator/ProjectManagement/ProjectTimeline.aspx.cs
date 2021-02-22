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

        /*this is the project that we are viewing. currently has place holder info. should be gotten from the previous screen*/
        /*may be pass as a string. if that is the case we need to reteieve from the database. we will see*/
        public Project2 project = new Project2();
        public List<Project2> otherProjects = new List<Project2>();


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
                int projectID = Int32.Parse(id);

                Database d = new Database();
                project = d.GetProject(projectID);
                if(project != null) {
                    project.getUpdates();

                    lblTitle.Text = project.project_name;
                    lblDesc.Text = project.project_desc;

                    /*for each of the updates within the project*/
                    foreach (Timeline t in project.project_timeline) {
                        /*create a div using createUpdatePanel and add it to the UpdatePanel section of the screen*/
                        Panel div = createUpdatePanel(t);
                        UpdatePanel.Controls.Add(div);
                    }


                    ////probably are going to get rid of this
                    ///*there are some additional options if the user is the owner of the project, in that case, they can create a new post*/
                    ///*we need to figure out how the user is logged in.*/
                    //if (user == "owner") {
                    //    /*create a new update object with a + logo and add it to the screen*/
                    //    Update newUpdate = new Update("https://media.istockphoto.com/vectors/black-plus-sign-positive-symbol-vector-id688550958?k=6&m=688550958&s=612x612&w=0&h=nVa-a5Fb79Dgmqk3F00kop9kF4CXFpF4kh7vr91ERGk=", "000", " < h3>New Update</h3> <br />", "<p />", DateTime.Today);
                    //    Panel p = createUpdatePanel(newUpdate);
                    //    UpdatePanel.Controls.Add(p);
                    //}

                    otherProjects = d.GetProjectList();
                    /*for each of the projects in the related projects list*/
                    int panelNum = 1;
                    foreach (Project2 p in otherProjects) {
                        /*create a div using createUpdatePanel and add it to the related panel of the screen*/
                        Panel div = createRelatedPanel(p);
                        div.ID = "related" + panelNum;
                        RelatedPanel.Controls.Add(div);
                        panelNum++;
                        if(panelNum > 4) {
                            break;
                        }
                    }
                }
            } else {
                /*404*/
                Session["Project"] = "-1";
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
        public Panel createRelatedPanel(Project2 project) {
            Panel div = new Panel();                /*the div we are building*/
            ImageButton image = new ImageButton();  /*this is the image that is used to diplay the project as well as navigate to that project*/
            Label title = new Label();              /*the label that will hold the title of the project*/
            Label desc = new Label();             /*the label that will hold the author of the project*/

            div.CssClass = "timeline-content";        /*the styling that is used for formatting the div*/

            image.Click += relatedClick;
            image.ID = project.projectID.ToString();
            image.ImageUrl = project.project_icon_path;      /*set the image we want to use for the hyperlink. it is stored within the Project object*/
            image.CssClass = "related-img";     /*the styling that is used for formatting the image*/

            title.Text = "<h3>" + project.project_name + "</h3>";         /*set the title of the project based on the project object*/
            desc.Text = "<p>" + project.project_desc + "</p>";       /*set the author of the project based on the project object*/

            /*add the objects we created to the div in the order we want them to appear*/
            div.Controls.Add(image);
            div.Controls.Add(title);
            div.Controls.Add(desc);
            return (div);
        }



        /*
        * METHOD : relatedClick
        * DESCRIPTION :
        *	when a related project is clicked, navigate to that page
        * PARAMETERS :
        *	object sender : the object that invoked the method
        *   EventArgs e : addional arguments thata may be useful
        * RETURNS :
        *	none
        *	we are navigated to another page
        */
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
        public Panel createUpdatePanel(Timeline update) {
            Panel div = new Panel();                /*the div we are building*/
            ImageButton image = new ImageButton();      /*this is the image that is used to diplay the project as well as navigate to that update*/
            Label updateTitle = new Label();        /*the label that will hold the title of the update*/
            Label updateDescription = new Label();  /*the label that will hold the description of the update*/
            Label updateDate = new Label();         /*the label that will hold the date of the update*/

            div.CssClass = "timeline-content";             /*the styling that is used for formatting the div*/
            image.Click += updateClick;
            image.ID = update.timelineID.ToString();
            image.ImageUrl = update.timeline_image_path;        /*set the image we want to use for the hyperlink. it is stored within the Update object*/
            image.CssClass = "update-img";          /*the styling that is used for formatting the image*/

            updateTitle.Text = "<h3>"+update.timeline_name+"</h3>";                /*set the title of the update based on the update object*/
            updateDescription.Text = "<p>"+update.timeline_desc;    /*set the description of the update based on the update object*/
            updateDate.Text = "<br>"+update.timeline_creation.ToString()+"</p>";    /*set the date of the update based on the update object and format it to be short date ex. 2020-02-02*/

            /*add the objects we created to the div in the order we want them to appear*/
            div.Controls.Add(image);
            div.Controls.Add(updateTitle);
            div.Controls.Add(updateDescription);
            div.Controls.Add(updateDate);

            return (div);
        }



        /*
        * METHOD : updateClick
        * DESCRIPTION :
        *	when an update icon is clicked on, navigate to that updates page
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