/*
*	FILE : Project2.cs
*	PROJECT : Project Creator
*	PROGRAMMER : Mark Jackson
*	             Eric Emerson
*	             Max Mikheev
*	FIRST VERSION : 2021-02-06
*	DESCRIPTION :
*		This class contains the defintion of a project based on the requirements of Project Creator.
*		Currentlty, a project contains a title, author, an icon, a navigation url and, a list of updates
*		This will definately change in the future as the scope changes and especially once we have a 
*		better grasp on inter page navigation
*/
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

using System.Web;

namespace Project_Creator
{
    // This class is to be merged with the actual Project class at some point
    // I just didn't want to break any existing components

    public class Project2
    {
        public int projectID { get; set; }                   /* projectID: Unique ID of this project. */
        public SqlDateTime project_creation { get; set; }    /* project_creation: When this project was created. */
        public string project_name { get; set; }             /* project_name: Name of this project. */
        public string project_desc { get; set; }             /* project_desc: Description of this project. */
        public string project_icon_path { get; set; }        /* project_icon_path:  path to the image used as the icon */
        public List<Timeline> project_timeline { get; set; } /* project_timeline: list of the related timeline elements */

        public static Database.QueryResult RegisterProject(Account project_owner, Project2 new_project)
        {
            // work in progress
            // TODO: implement proper error handling using QueryResult, handle ID correctly...
            Database db = new Database();
            db.CreateProject(new_project);
            db.CreateProjectLink(new_project.projectID, project_owner.accountID);
            return Database.QueryResult.Successful;
        }

        /*
        * METHOD : getUpdates
        * DESCRIPTION :
        *	fetch the updates for the project from the database and put them into the updates data member
        * PARAMETERS :
        *	none
        * RETURNS :
        *	none
        */
        public void getUpdates() {
            if(project_timeline == null) {
                project_timeline = new List<Timeline>();
            } else {
                project_timeline.Clear();    /*remove all of the contents in the array list*/
            }
            Database d = new Database();
            project_timeline = d.GetTimelineList(projectID);
        }

        /*
        * METHOD : Project -- Constructor
        * DESCRIPTION :
        *	create a new project with no attributes specified
        * PARAMETERS :
        *	none
        * RETURNS :
        *	none
        *	A project object is created
        */
        public Project2() {
        }

        /*
        * METHOD : Project -- Constructor
        * DESCRIPTION :
        *	create a new project with all of the members specified by the caller
        * PARAMETERS :
        *	string t : the title of the project
        *   string a : the author of the project
        *   string i : the URL for the icon
        *   string URL : the URL that the project exists on
        *   ArrayList u : an arraylist that contains all of the updates for the project
        * RETURNS :
        *	none
        *	A project object is created
        */
        public Project2(int id, string title, string desc, string iconUrl, List<Timeline> updates) {
            project_name = title;
            project_desc = desc;
            project_icon_path = iconUrl;
            project_timeline = updates;
            projectID = id;
        }

        /*
        * METHOD : Project -- Constructor
        * DESCRIPTION :
        *	create a new project with all without the update list but with all other menbers
        * PARAMETERS :
        *	string t : the title of the project
        *   string a : the author of the project
        *   string i : the URL for the icon
        *   string URL : the URL that the project exists on
        * RETURNS :
        *	none
        *	A project object is created
        */
        public Project2(int id, string title, string desc, string iconUrl) {
            project_name = title;
            project_desc = desc;
            project_icon_path = iconUrl;
            projectID = id;
            /*create a list for the updates*/
            project_timeline = new List<Timeline>();
        }
    }
}