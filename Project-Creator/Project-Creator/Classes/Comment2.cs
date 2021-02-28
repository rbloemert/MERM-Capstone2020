using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Creator {
    public class Comment2 : Comment {
        public string comment_account_name { get; set; }
        public string account_image_path { get; set; }

        public Comment2(Comment comment) {
            commentID = comment.commentID;
            comment_creation = comment.comment_creation;
            comment_text = comment.comment_text;
            Database db = new Database();
            Account a = db.GetAccountInfo(Int32.Parse(comment.comment_owner_accountID));
            comment_account_name = a.username;
            account_image_path = a.account_image_path;
        }
    }
}