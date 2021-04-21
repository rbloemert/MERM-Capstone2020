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
    //! Project information class.
    /*!
     *  Contains all the information about a project correlated with the database.
     */
    public class Project
    {
        public int projectID { get; set; }                   /*!< Unique ID of this project. */
        public SqlDateTime project_creation { get; set; }    /*!< When this project was created. */
        public string project_name { get; set; }             /*!< Name of this project. */
        public string project_desc { get; set; }             /*!< The description about the project. */
        public string project_author { get; set; }           /*!< Author of this project. */
        public string project_image_path { get; set; }       /*!< The File path of the project's picture; NULL means none */
        public int project_visibility { get; set; }          /*!< The visibility status of the project (0: hidden, 1: visible) */
        public int project_followers = 0;                    /*!< The number of followers the project has. */

        /*!
         *  A constructor.
         *  Default Project class constructor.
         */
        public Project() { }

        /*!
         *  A constructor.
         *  Creates a new Project object with the specified information.
         */
        public Project(string project_name, string project_desc, string project_author, string project_image_path, int project_visibility)
        {
            this.project_creation = new SqlDateTime(DateTime.Now);
            this.project_name = project_name;
            this.project_desc = project_desc;
            this.project_author = project_author;
            this.project_image_path = project_image_path;
            this.project_visibility = project_visibility;
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