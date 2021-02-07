using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_Creator.ProjectManagement {
    public partial class ProjectTimeline : System.Web.UI.Page {
        string user = "user";
        protected void Page_Load(object sender, EventArgs e) {
            Project project = new Project("Test Project", "Mark", "https://64.media.tumblr.com/avatar_4f1f4a829fdd_128.pnj", "https://google.com");
            projectIcon.ImageUrl = project.icon;
            lblTitle.Text = project.title;
            lblAuthor.Text = project.author;
            foreach (Update u in project.updates) {
                Panel div = createUpdatePanel(u);
                UpdatePanel.Controls.Add(div);
            }
            if(user == "owner") {
                Update newUpdate = new Update("https://media.istockphoto.com/vectors/black-plus-sign-positive-symbol-vector-id688550958?k=6&m=688550958&s=612x612&w=0&h=nVa-a5Fb79Dgmqk3F00kop9kF4CXFpF4kh7vr91ERGk=", "https://github.com/", " < h3>New Update</h3> <br />", "<p />", DateTime.Today);
                Panel p = createUpdatePanel(newUpdate);
                UpdatePanel.Controls.Add(p);
            }



            /*generate related projects this will come from the db later*/
            project.title = "<h3>" + project.title + "</h3>";
            project.author = "<p>" + project.author + "</p>";
            ArrayList otherProjects = new ArrayList();
            for (int i = 0; i < 4; i++) {
                otherProjects.Add(project);
            }

            foreach(Project p in otherProjects) {
                Panel div = createRelatedPanel(p);
                RelatedPanel.Controls.Add(div);
            }

        }

        public Panel createRelatedPanel(Project project) {
            Panel div = new Panel();
            div.CssClass = "w3-quarter";
            HyperLink image = new HyperLink();
            image.NavigateUrl = project.url;

            //Image image = new Image();
            Label title = new Label();
            Label author = new Label();
            image.ImageUrl = project.icon;
            image.CssClass = "related-img";
            title.Text = project.title;
            author.Text = project.author;
            div.Controls.Add(image);
            div.Controls.Add(title);
            div.Controls.Add(author);
            return (div);
        }

        public Panel createUpdatePanel(Update update) {
            Panel div = new Panel();
            div.CssClass = "w3-quarter";
            HyperLink image = new HyperLink();
            image.NavigateUrl = update.updatePage;
            Label updateTitle = new Label();
            Label updateDescription = new Label();
            Label updateDate = new Label();
            image.ImageUrl = update.iconURL;
            image.CssClass = "update-img";
            updateTitle.Text = update.title;
            updateDescription.Text = update.description;
            updateDate.Text = update.date.ToString("d");
            div.Controls.Add(image);
            div.Controls.Add(updateTitle);
            div.Controls.Add(updateDescription);
            div.Controls.Add(updateDate);
            return (div);
        }

    }

}