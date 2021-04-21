using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    //! Timeline information class.
    /*!
     *  Contains all the information about a timeline correlated with the database.
     */
    public class Timeline
    {
        public int timelineID;                   /*!< Unique timeline ID */
        public int timeline_project;             /*!< The project which has the timeline. */
        public SqlDateTime timeline_creation;    /*!< When this timeline was created. */
        public string timeline_name;             /*!< The name of the timeline. */
        public string timeline_desc;             /*!< The description of the timeline. */
        public string timeline_image_path;       /*!< The File path of the timeline's picture; NULL means none */
        public string timeline_file_path;        /*!< The File path of the timeline's file; NULL means none */
    }
}