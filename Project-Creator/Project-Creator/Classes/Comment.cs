using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    public class Comment
    {
        public int commentID;                   /* commentID: Unique ID of this comment. */
        public SqlDateTime comment_creation;    /* comment_creation: When this comment was created. */
        public string comment_text;             /* comment_text: The text of this comment. */
        public string comment_owner_accountID;
    }
}