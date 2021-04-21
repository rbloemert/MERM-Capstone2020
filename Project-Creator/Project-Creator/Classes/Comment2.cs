using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Creator {

    //! Comment display class.
    /*!
     *  Used for support in displaying Comment objects on web pages.
     */
    public class Comment2 : Comment {
        public string comment_account_name { get; set; } //!< The username of the commentor's account.
        public string account_image_path { get; set; } //!< The profile picture link accosiated with the commentor's account.

        /*!
         *  A constructor.
         *  Creates a new Comment2 object with the information from a Comment object.
         */
        public Comment2(Comment comment) {
            commentID = comment.commentID;
            comment_creation = comment.comment_creation;
            comment_text = comment.comment_text;
            using (Database db = new Database())
            {
                Account a = db.GetAccountInfo(Int32.Parse(comment.comment_owner_accountID));
                comment_account_name = a.username;
                comment_owner_accountID = comment.comment_owner_accountID;
                account_image_path = a.account_image_path;
            }
        }
    }
}