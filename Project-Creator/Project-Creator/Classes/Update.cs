/*
*	FILE : Updates.cs
*	PROJECT : Project Creator
*	PROGRAMMER : Mark Jackson
*	             Eric Emerson
*	FIRST VERSION : 2021-02-06
*	DESCRIPTION :
*		This class contains the defintion of a project update as it relates to projects within Project Creator.
*		Currently, an update contains an image, a link to the full update page, a title, a description and, the
*		date that the update took place.
*		This will change in the future as the project scope changes and other elements get defined
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Creator {
    public class Update {
        public string iconURL { get; set; }     /*the image describing the update*/
        public string updatePage { get; set; }  /*the link to the full update page*/
        public string title { get; set; }       /*the title of the update*/
        public string description { get; set; } /*a description of the update*/
        public DateTime date { get; set; }      /*when the update took place*/



        /*
        * METHOD : Update -- Constructor
        * DESCRIPTION :
        *	create a new update with all of the members specified by the caller
        * PARAMETERS :
        *	string iconUrl : the image of the update
        *   string pageUrl : the location of the full update page
        *   string t : the title of the update
        *   string desc : the desciption of the update
        *   DateTime d : when the update took place
        * RETURNS :
        *	none
        *	A update object is created
        */
        public Update(string iconUrl, string pageUrl, string t, string desc, DateTime d) {
            iconURL = iconUrl;
            updatePage = pageUrl;
            title = t;
            description = desc;
            date = d;
        }
    }
}