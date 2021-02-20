using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Project_Creator.Classes
{
    // I haven't had a chance to try these out yet

    public class Database
    {
        public enum QueryResult
        {
            FailedNotConnected = 0, // Connection not established
            FailedNoChanges,        // Query OK but no changes made
            FailedBadQuery,         // Query not OK
            FailedNotQualified,     // On funcs involving a check (eg IDs), qualifier did not pass
            Successful              // Success
        }

        //Defines the database connection variables.
        private string lastErr = "";
        private string key = "EVAO9NR3R920";
        private byte[] salt = { 0x14, 0x64, 0x98, 0x65, 0x24, 0x75, 0x45, 0x12, 0x15, 0x13, 0x18, 0x19, 0x14 };
        private SqlConnection connection;

        public Database()
        {
            connection = new SqlConnection("Server=tcp:projectcreator.database.windows.net,1433;Initial Catalog=projectcreatordb;Persist Security Info=False;User ID=creatoradmin;Password=ProjectCreator1233;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            connection.Open();
        }

        ~Database()
        {
            connection.Close();
        }

        public bool TestConnection() // returns true if success
        {
            Database db = new Database();
            if (db.IsConnectionOpen()) return true;
            return false;
        }

        public bool IsConnectionOpen()
        {
            return connection != null && connection.State == ConnectionState.Open;
        }

        public string GetLastSQLError()
        {
            return lastErr;
        }

        //public bool InsertIntoTable(string table, List<Tuple<string, object>> info)
        //{
        //    if (!IsConnectionOpen()) return false;

        //    string sql = "INSERT INTO " + table;

        //    sql += "(";
        //    foreach (Tuple<string, object> tuple in info)
        //    {
        //        if (tuple.Equals(info.Last()))
        //        {
        //            sql += tuple.Item1;
        //        }
        //        else
        //        {
        //            sql += (tuple.Item1 + ", ");
        //        }
        //    }
        //    sql += ") ";

        //    sql += "VALUES(";
        //    foreach (Tuple<string, object> tuple in info)
        //    {
        //        if (tuple.Equals(info.Last()))
        //        {
        //            sql += ("@" + tuple.Item1);
        //        }
        //        else
        //        {
        //            sql += ("@" + tuple.Item1 + ", ");
        //        }
        //    }
        //    sql += ")";

        //    var cmd = new SqlCommand(sql, connection);

        //    foreach (Tuple<string, object> tuple in info)
        //    {
        //        cmd.Parameters.AddWithValue("@" + tuple.Item1, tuple.Item2);
        //    }

        //    cmd.Prepare();

        //    return cmd.ExecuteNonQuery() > 0;
        //}

        public string Encrypt(string password)
        {

            //Gets the password byte array.
            byte[] passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);

            //Creates an encryptor.
            using (Aes encryptor = Aes.Create())
            {

                //Derives bytes for the password.
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                //Creates a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {

                    //Creates a crypto stream.
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {

                        //Writes the password to the stream.
                        cs.Write(passwordBytes, 0, passwordBytes.Length);
                        cs.Close();
                    }

                    //Sets the password to the encrypted password.
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }

            //Returns the encrypted password.
            return password;
        }

        public string Decrypt(string password)
        {
            //Gets the password byte array.
            byte[] passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);

            //Creates an encryptor.
            using (Aes encryptor = Aes.Create())
            {

                //Derives bytes for the password.
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                //Creates a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    //Creates a crypto stream.
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        //Writes the password to the stream.
                        cs.Write(passwordBytes, 0, passwordBytes.Length);
                        cs.Close();
                    }

                    //Sets the password to the encrypted password.
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }

            //Returns the encrypted password.
            return password;
        }

        // ACCOUNTS

        public List<Account> GetAccountList()
        {
            List<Account> accs = new List<Account>();
            if (!IsConnectionOpen()) return accs;

            var sql = "SELECT * FROM account";
            using (var cmd = new SqlCommand(sql, connection))
            {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    accs.Add(new Account()
                    {
                        accountID = Convert.ToInt32(row["accountID"]),
                        account_creation = Convert.ToDateTime(row["account_creation"]),
                        firstname = row["firstname"].ToString(),
                        lastname = row["lastname"].ToString(),
                        username = row["username"].ToString(),
                        password = Decrypt(row["password"].ToString()),
                        email = row["email"].ToString(),
                        isSiteAdministrator = Convert.ToBoolean(row["isSiteAdministrator"])
                    });
                }
            }

            return accs;
        }

        public QueryResult CreateAccount(Account account)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO account(account_creation, firstname, lastname, username, password, email, isSiteAdministrator) VALUES(@account_creation, @firstname, @lastname, @username, @password, @email, @isSiteAdministrator)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@account_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@firstname", account.firstname);
                cmd.Parameters.AddWithValue("@lastname", account.lastname);
                cmd.Parameters.AddWithValue("@username", account.username);
                cmd.Parameters.AddWithValue("@password", Encrypt(account.password));
                cmd.Parameters.AddWithValue("@email", account.email);
                cmd.Parameters.AddWithValue("@isSiteAdministrator", account.isSiteAdministrator);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
                
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteAccount(int accountID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM account WHERE accountID=@accountID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@accountID", accountID);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult ModifyAccount(int accountID, Account new_account)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE account SET " +
                      "accountID = @accountID" +
                      "account_creation = @account_creation" +
                      "firstname = @firstname" +
                      "lastname = @lastname" +
                      "username = @username" +
                      "password = @password" +
                      "email = @email" +
                      "isSiteAdministrator = @isSiteAdministrator" + 
                      "WHERE accountID = @oldAccountID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@oldAccountID", accountID);
                cmd.Parameters.AddWithValue("@accountID", new_account.accountID);
                cmd.Parameters.AddWithValue("@account_creation", new_account.account_creation);
                cmd.Parameters.AddWithValue("@firstname", new_account.firstname);
                cmd.Parameters.AddWithValue("@lastname", new_account.lastname);
                cmd.Parameters.AddWithValue("@username", new_account.username);
                cmd.Parameters.AddWithValue("@password", Encrypt(new_account.password));
                cmd.Parameters.AddWithValue("@email", new_account.email);
                cmd.Parameters.AddWithValue("@isSiteAdministrator", new_account.isSiteAdministrator);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public Account AuthenticateAccount(string username, string password)
        {
            string encPwd = Encrypt(password);

            foreach (Account acc in GetAccountList())
            {
                if (username == acc.username && encPwd == acc.password) return acc;
            }

            return new Account();
        }

        // ACCOUNTS

        // PROJECTS

        public List<Project2> GetProjectList()
        {
            List<Project2> projects = new List<Project2>();
            if (!IsConnectionOpen()) return projects;

            var sql = "SELECT * FROM project";
            using (var cmd = new SqlCommand(sql, connection))
            {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    projects.Add(new Project2()
                    {
                        projectID = Convert.ToInt32(row["projectID"]),
                        project_creation = Convert.ToDateTime(row["project_creation"]),
                        project_name = row["project_name"].ToString(),
                        project_desc = row["project_desc"].ToString(),
                    });
                }
            }

            return projects;
        }

        public List<Project2> GetProjectList(int accountID) // return projects belonging to a user
        {
            List<Project2> projects = new List<Project2>();
            if (!IsConnectionOpen()) return projects;

            var sql = "SELECT * " +
                      "FROM project" +
                      "LEFT JOIN project_link ON (project.projectID = project_link.projectID)" +
                      "WHERE project_link.project_owner_accountID = @accountID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@accountID", accountID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    projects.Add(new Project2()
                    {
                        projectID = Convert.ToInt32(row["projectID"]),
                        project_creation = Convert.ToDateTime(row["project_creation"]),
                        project_name = row["project_name"].ToString(),
                        project_desc = row["project_desc"].ToString(),
                    });
                }
            }

            return projects;
        }

        public QueryResult CreateProject(Project2 project)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO project(project_creation, project_name, project_desc) VALUES(@project_creation, @project_name, @project_desc)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@project_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@project_name", project.project_name);
                cmd.Parameters.AddWithValue("@project_desc", project.project_desc);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteProject(int projectID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM project WHERE projectID=@projectID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult ModifyProject(int projectID, Project2 new_project)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE project SET " +
                      "projectID = @projectID" +
                      "project_creation = @project_creation" +
                      "project_name = @project_name" +
                      "project_desc = @project_desc" +
                      "WHERE projectID = @oldProjectID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@oldProjectID", projectID);
                cmd.Parameters.AddWithValue("@projectID", new_project.projectID);
                cmd.Parameters.AddWithValue("@project_creation", new_project.project_creation);
                cmd.Parameters.AddWithValue("@project_name", new_project.project_name);
                cmd.Parameters.AddWithValue("@project_desc", new_project.project_desc);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult AddFollower(int projectID, Account account)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO follower_link(projectID, follower_accountID, follow_date) VALUES(@projectID, @follower_accountID, @follow_date)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@follower_accountID", account.accountID);
                cmd.Parameters.AddWithValue("@follow_date", new SqlDateTime(DateTime.Now));
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult RemoveFollower(int projectID, Account account)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM follower_link WHERE projectID=@projectID AND follower_accountID=@follower_accountID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@follower_accountID", account.accountID);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        // PROJECTS

        // TIMELINE

        public List<Timeline> GetTimelineList()
        {
            List<Timeline> timelines = new List<Timeline>();
            if (!IsConnectionOpen()) return timelines;

            var sql = "SELECT * FROM timeline";
            using (var cmd = new SqlCommand(sql, connection))
            {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    timelines.Add(new Timeline()
                    {
                        timelineID = Convert.ToInt32(row["timelineID"]),
                        timeline_creation = Convert.ToDateTime(row["timeline_creation"]),
                        timeline_name = row["timeline_name"].ToString(),
                        timeline_desc = row["timeline_desc"].ToString(),
                    });
                }
            }

            return timelines;
        }

        public List<Timeline> GetTimelineList(int projectID) // return timelines belonging to a project
        {
            List<Timeline> timelines = new List<Timeline>();
            if (!IsConnectionOpen()) return timelines;

            var sql = "SELECT * " +
                      "FROM timeline" +
                      "LEFT JOIN timeline_link ON (timeline.timelineID = timeline_link.timelineID)" +
                      "WHERE timeline_link.project_owner_projectID = @projectID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    timelines.Add(new Timeline()
                    {
                        timelineID = Convert.ToInt32(row["timelineID"]),
                        timeline_creation = Convert.ToDateTime(row["timeline_creation"]),
                        timeline_name = row["timeline_name"].ToString(),
                        timeline_desc = row["timeline_desc"].ToString(),
                    });
                }
            }

            return timelines;
        }

        public QueryResult CreateTimeline(Timeline timeline)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO timeline(timeline_creation, timeline_name, timeline_desc) VALUES(@timeline_creation, @timeline_name, @timeline_desc)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@timeline_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@timeline_name", timeline.timeline_name);
                cmd.Parameters.AddWithValue("@timeline_desc", timeline.timeline_desc);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteTimeline(int timelineID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM timeline WHERE timelineID=@timelineID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult ModifyTimeline(int timelineID, Timeline new_timeline)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE timeline SET " +
                      "timelineID = @timelineID" +
                      "timeline_creation = @timeline_creation" +
                      "timeline_name = @timeline_name" +
                      "timeline_desc = @timeline_desc" +
                      "WHERE timelineID = @oldTimelineID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@oldTimelineID", timelineID);
                cmd.Parameters.AddWithValue("@timelineID", new_timeline.timelineID);
                cmd.Parameters.AddWithValue("@timeline_creation", new_timeline.timeline_creation);
                cmd.Parameters.AddWithValue("@timeline_name", new_timeline.timeline_name);
                cmd.Parameters.AddWithValue("@timeline_desc", new_timeline.timeline_desc);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        // TIMELINE

        // COMMENT

        public List<Comment> GetCommentList()
        {
            List<Comment> comments = new List<Comment>();
            if (!IsConnectionOpen()) return comments;

            var sql = "SELECT * FROM comment";
            using (var cmd = new SqlCommand(sql, connection))
            {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    comments.Add(new Comment()
                    {
                        commentID = Convert.ToInt32(row["commentID"]),
                        comment_creation = Convert.ToDateTime(row["comment_creation"]),
                        comment_text = row["comment_text"].ToString(),
                    });
                }
            }

            return comments;
        }

        public List<Comment> GetCommentList(int timelineID) // return comments belonging to a timeline
        {
            List<Comment> comments = new List<Comment>();
            if (!IsConnectionOpen()) return comments;

            var sql = "SELECT * " +
                      "FROM comment" +
                      "LEFT JOIN comment_link ON (comment.commentID = comment_link.commentID)" +
                      "WHERE comment_link.timeline_owner_timelineID = @timelineID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    comments.Add(new Comment()
                    {
                        commentID = Convert.ToInt32(row["commentID"]),
                        comment_creation = Convert.ToDateTime(row["comment_creation"]),
                        comment_text = row["comment_text"].ToString(),
                    });
                }
            }

            return comments;
        }

        public QueryResult CreateComment(Comment comment)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO comment(comment_creation, comment_text) VALUES(@comment_creation, @comment_text)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@comment_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@comment_text", comment.comment_text);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteComment(int commentID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM comment WHERE commentID=@commentID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@commentID", commentID);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult ModifyComment(int commentID, Comment new_comment)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE comment SET " +
                      "commentID = @commentID" +
                      "comment_creation = @comment_creation" +
                      "comment_text = @comment_text" +
                      "WHERE commentID = @oldCommentID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@oldCommentID", commentID);
                cmd.Parameters.AddWithValue("@commentID", new_comment.commentID);
                cmd.Parameters.AddWithValue("@comment_creation", new_comment.comment_creation);
                cmd.Parameters.AddWithValue("@comment_text", new_comment.comment_text);
                cmd.Prepare();

                //Executes the insert command.
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        // COMMENT
    }
}