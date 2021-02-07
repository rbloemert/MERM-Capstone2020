/*
*	FILE : Project.cs
*	PROJECT : Project Creator
*	PROGRAMMER : Mark Jackson
*	             Eric Emerson
*	FIRST VERSION : 2021-02-06
*	DESCRIPTION :
*		This class contains the defintion of a project based on the requirements of Project Creator.
*		Currentlty, a project contains a title, author, an icon, a navigation url and, a list of updates
*		This will definately change in the future as the scope changes and especially once we have a 
*		better grasp on inter page navigation
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Creator {
    public class Project {
        public string title { get; set; }       /*the name of the project*/
        public string author { get; set; }      /*who the project belongs to*/
        public string icon { get; set; }        /*an image describing the project*/
        public string url { get; set; }         /*the navigation url for the project. currently used when a project is in the recommended section*/
        public ArrayList updates;               /*a list containing all of the Update objects associated with the project*/


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
        public Project(string t, string a, string i, string URL, ArrayList u) {
            title = t;
            author = a;
            icon = i;
            updates = u;
            url = URL;
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
        public Project(string t, string a, string i, string URL) {
            title = t;
            author = a;
            icon = i;
            url = URL;

            /*create a list for the updates and get them from the db*/
            updates = new ArrayList();
            getUpdates();
        }



        /*
        * METHOD : getUpdates
        * DESCRIPTION :
        *	fetch the updates for the project from the database and put them into the updates data member
        * PARAMETERS :
        *	none
        * RETURNS :
        *	bool : true if the data was sucessfully retrieved
        *	       false if retrieval failed
        *	updates data member is changed
        */
        public bool getUpdates() {
            bool success = false;

            /*this whole method needs to change when we connect it to the database*/

            updates.Clear();    /*remove all of the contents in the array list
            /*get from db*/

            /*temp data for now*/
            for (int i = 0; i < 10; i++) {
                Update u = new Update("https://cdn.escapistmagazine.com/media/global/images/library/deriv/1400/1400821.jpg", "https://google.com", "<h3>Project Update Title</h3>", "<p>a description about the update which summarises the complete description that is displayed upon clicking on the card for more information.</p><br />", DateTime.Today);
                updates.Add(u);
            }
            success = true;
            return (success);
        }
    }
}