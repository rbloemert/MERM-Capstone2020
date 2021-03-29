using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    public class Timeline
    {
        public int timelineID;                   /* timelineID: Unique timeline ID */
        public int timeline_project;             /* timeline_project: The project which has the timeline. */
        public SqlDateTime timeline_creation;    /* timeline_creation: When this timeline was created. */
        public string timeline_name;             /* timelinetitle: The name of the timeline. */
        public string timeline_desc;             /* timelinedesc: The description of the timeline. */
        public string timeline_image_path;       /* timeline_image_path: The File path of the timeline's picture; NULL means none */
        public string timeline_file_path;        /* timeline_file_path: The File path of the timeline's file; NULL means none */
    }
}