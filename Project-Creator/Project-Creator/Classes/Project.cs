/*
*	FILE : Project.cs
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

    public class Project
    {
        public int projectID { get; set; }                   /* projectID: Unique ID of this project. */
        public SqlDateTime project_creation { get; set; }    /* project_creation: When this project was created. */
        public string project_name { get; set; }             /* project_name: Name of this project. */
        public string project_desc { get; set; }
        public string project_author { get; set; }           /* project_author: Author of this project. */
        public string project_image_path { get; set; }       /* project_image_path: The File path of the project's picture; NULL means none */

        public static Database.QueryResult RegisterProject(Account project_owner, Project new_project)
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
        *	fetch the updates for the project from the database
        * PARAMETERS :
        *	none
        * RETURNS :
        *	none
        */
        public List<Timeline> getUpdates()
        {
            Database d = new Database();
            List<Timeline> timelines = d.GetTimelineList(projectID);
            return timelines;
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
        public Project() 
        {

        }

        public Project(int id, string title, string desc, string iconUrl) {

        }

        // Changes were made to the database,
        // commented out for now

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
        //public Project(int id, string title, string author, string iconUrl, List<Timeline> updates) {
        //    project_name = title;
        //    project_author = author;
        //    project_icon_path = iconUrl;
        //    project_timeline = updates;
        //    projectID = id;
        //}

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
        //public Project(int id, string title, string author, string iconUrl) {
        //    project_name = title;
        //    project_author = author;
        //    project_icon_path = iconUrl;
        //    projectID = id;
        //    /*create a list for the updates*/
        //    project_timeline = new List<Timeline>();
        //}
    }
}