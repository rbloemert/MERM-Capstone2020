using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    //! Comment information class.
    /*!
     *  Contains all the information about a comment correlated with the database.
     */
    public class Comment
    {
        public int commentID;                   /*!< Unique ID of this comment. */
        public SqlDateTime comment_creation;    /*!< When this comment was created. */
        public string comment_text;             /*!< The text of this comment. */
        public string comment_owner_accountID;  /*!< The id of the comment poster's account. */
    }
}