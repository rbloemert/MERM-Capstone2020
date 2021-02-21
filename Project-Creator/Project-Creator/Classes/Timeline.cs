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
        public SqlDateTime timeline_creation;    /* timeline_creation: When this timeline was created. */
        public string timeline_image_path;       /* timeline_image_path: Path of timeline image */
        public string timeline_name;             /* timelinetitle: The name of the timeline. */
        public string timeline_desc;             /* timelinedesc: The description of the timeline. */
    }
}