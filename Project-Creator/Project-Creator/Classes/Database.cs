using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Project_Creator.Classes;

namespace Project_Creator
{
    // I haven't had a chance to try these out yet
    // Note: Methods in the DB class are for internal use; I will provide funcs within the individual classes to offer functionality

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
        private SqlConnection connection;
        private string connectionString = "Data Source=tcp:projectcreator.database.windows.net,1433;Initial Catalog=projectcreatordb;User Id=creatoradmin@projectcreator;Password=ProjectCreator1233";

        public Database()
        {
            connection = new SqlConnection(connectionString);
            //connection = new SqlConnection("Server=tcp:projectcreator.database.windows.net,1433;Initial Catalog=projectcreatordb;Persist Security Info=False;User ID=creatoradmin;Password=ProjectCreator1233;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            connection.Open();
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

        //    

        //    return cmd.ExecuteNonQuery() > 0;
        //}

        // ACCOUNTS

        public bool AccountExists(string username)
        {

            //Gets a list of all existing accounts.
            List<Account> accounts = GetAccountList();

            //Default the username does not exist.
            bool exists = false;

            //Loops through each account comparing usernames.
            foreach (Account account in accounts)
            {

                //Checks if the account usernames are equal.
                if (username.Equals(account.username))
                {
                    exists = true;
                }

            }

            //Returns the status of the username.
            return exists;

        }

        public bool EmailExists(string email)
        {

            //Gets a list of all existing accounts.
            List<Account> accounts = GetAccountList();

            //Default the username does not exist.
            bool exists = false;

            //Loops through each account comparing usernames.
            foreach (Account account in accounts)
            {

                //Checks if the account usernames are equal.
                if (email.Equals(account.email))
                {
                    exists = true;
                }

            }

            //Returns the status of the username.
            return exists;

        }

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
                        fullname = row["fullname"].ToString(),
                        username = row["username"].ToString(),
                        password = row["password"].ToString(),
                        password_salt = row["password_salt"].ToString(),
                        email = row["email"].ToString(),
                        isSiteAdministrator = Convert.ToBoolean(row["isSiteAdministrator"]),
                        account_image_path = row["account_image_path"].ToString()
                    });
                }
            }
            connection.Close();
            return accs;
        }

        public QueryResult CreateAccount(Account account)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO account(account_creation, fullname, username, password, password_salt, email, isSiteAdministrator, account_image_path) VALUES(@account_creation, @fullname, @username, @password, @password_salt, @email, @isSiteAdministrator, account_image_path)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                var salt = Password.Salt();
                cmd.Parameters.AddWithValue("@account_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@fullname", account.fullname);
                cmd.Parameters.AddWithValue("@username", account.username);
                cmd.Parameters.AddWithValue("@password", Password.Encrypt(account.password, salt));
                cmd.Parameters.AddWithValue("@password_salt", salt);
                cmd.Parameters.AddWithValue("@email", account.email);
                cmd.Parameters.AddWithValue("@isSiteAdministrator", account.isSiteAdministrator);
                cmd.Parameters.AddWithValue("@account_image_path", "NULL");

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
            connection.Close();

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
            connection.Close();

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
                      "fullname = @fullname" +
                      "username = @username" +
                      "password = @password" +
                      "password_salt = @password_salt" +
                      "email = @email" +
                      "isSiteAdministrator = @isSiteAdministrator" +
                      "account_image_path = @account_image_path" +
                      "WHERE accountID = @oldAccountID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                var salt = Password.Salt();
                cmd.Parameters.AddWithValue("@oldAccountID", accountID);
                cmd.Parameters.AddWithValue("@accountID", new_account.accountID);
                cmd.Parameters.AddWithValue("@account_creation", new_account.account_creation);
                cmd.Parameters.AddWithValue("@fullname", new_account.fullname);
                cmd.Parameters.AddWithValue("@username", new_account.username);
                cmd.Parameters.AddWithValue("@password", Password.Encrypt(new_account.password, salt));
                cmd.Parameters.AddWithValue("@password_salt", salt);
                cmd.Parameters.AddWithValue("@email", new_account.email);
                cmd.Parameters.AddWithValue("@isSiteAdministrator", new_account.isSiteAdministrator);
                cmd.Parameters.AddWithValue("@account_image_path", new_account.account_image_path);

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
            connection.Close();

            //Returns if the insert was successful.
            if (result > 0)
            {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public Account AuthenticateAccount(string username, string password)
        {

            foreach (Account acc in GetAccountList())
            {
                if (username == acc.username && Password.ComparePassword(password, acc.password, acc.password_salt)) return acc;
            }

            return null;
        }

        // ACCOUNTS

        // PROJECTS

        public Project GetProject(int projectID) {
            Project project = new Project();
            if (!IsConnectionOpen()) return null;

            var sql = "SELECT * FROM project where projectID = @projectID";
            using (var cmd = new SqlCommand(sql, connection)) 
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    project.projectID = Convert.ToInt32(row["projectID"]);
                    project.project_creation = Convert.ToDateTime(row["project_creation"]);
                    project.project_name = row["project_name"].ToString();
                    project.project_desc = row["project_desc"].ToString();
                    project.project_image_path = row["project_image_path"].ToString();
                }
            }

            return project;
        }

        public List<Project> GetProjectList()
        {
            List<Project> projects = new List<Project>();
            if (!IsConnectionOpen()) return projects;

            var sql = "SELECT * FROM project";
            using (var cmd = new SqlCommand(sql, connection))
            {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    projects.Add(new Project()
                    {
                        projectID = Convert.ToInt32(row["projectID"]),
                        project_creation = Convert.ToDateTime(row["project_creation"]),
                        project_name = row["project_name"].ToString(),
                        project_desc = row["project_desc"].ToString(),
                        project_image_path = row["project_image_path"].ToString()
                });
                }
            }
            connection.Close();
            return projects;
        }

        public List<Project> GetProjectList(int accountID) // return projects belonging to a user
        {
            List<Project> projects = new List<Project>();
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
                    projects.Add(new Project()
                    {
                        projectID = Convert.ToInt32(row["projectID"]),
                        project_creation = Convert.ToDateTime(row["project_creation"]),
                        project_name = row["project_name"].ToString(),
                        project_desc = row["project_desc"].ToString(),
                        project_image_path = row["project_image_path"].ToString()
                    });
                }
            }
            connection.Close();
            return projects;
        }

        public List<Project> GetProjectList(string search) // return projects with substring in title
        {
            List<Project> projects = new List<Project>();
            if (!IsConnectionOpen()) return projects;

            var sql = "SELECT * " +
                      "FROM project" +
                      "WHERE CHARINDEX('@search', project.project_name) > 0";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@search", search);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    projects.Add(new Project()
                    {
                        projectID = Convert.ToInt32(row["projectID"]),
                        project_creation = Convert.ToDateTime(row["project_creation"]),
                        project_name = row["project_name"].ToString(),
                        project_desc = row["project_desc"].ToString(),
                        project_image_path = row["project_image_path"].ToString()
                    });
                }
            }
            connection.Close();
            return projects;
        }

        public QueryResult CreateProject(Project project)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO project(project_creation, project_name, project_desc, project_image_path) VALUES(@project_creation, @project_name, @project_desc, @project_image_path)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@project_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@project_name", project.project_name);
                cmd.Parameters.AddWithValue("@project_desc", project.project_desc);
                cmd.Parameters.AddWithValue("@project_image_path", "NULL");

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

        public QueryResult ModifyProject(int projectID, Project new_project)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE project SET " +
                      "projectID = @projectID" +
                      "project_creation = @project_creation" +
                      "project_name = @project_name" +
                      "project_desc = @project_desc" +
                      "project_image_path = @project_image_path" +
                      "WHERE projectID = @oldProjectID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@oldProjectID", projectID);
                cmd.Parameters.AddWithValue("@projectID", new_project.projectID);
                cmd.Parameters.AddWithValue("@project_creation", new_project.project_creation);
                cmd.Parameters.AddWithValue("@project_name", new_project.project_name);
                cmd.Parameters.AddWithValue("@project_desc", new_project.project_desc);
                cmd.Parameters.AddWithValue("@project_image_path", new_project.project_image_path);

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

        public QueryResult CreateProjectLink(int projectID, int accountID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO project_link(projectID, project_owner_accountID) VALUES(@projectID, @project_owner_accountID)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@project_owner_accountID", accountID);

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

        public QueryResult DeleteProjectLink(int projectID, int accountID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM project_link WHERE projectID=@projectID AND project_owner_accountID=@project_owner_accountID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@project_owner_accountID", accountID);


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
                        timeline_image_path = row["timeline_image_path"].ToString(),
                        timeline_file_path = row["timeline_file_path"].ToString()
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
                      "FROM timeline " +
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
                        timeline_image_path = row["timeline_image_path"].ToString(),
                        timeline_file_path = row["timeline_file_path"].ToString()
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
            var sql = "INSERT INTO timeline(timeline_creation, timeline_name, timeline_desc, timeline_image_path, timeline_file_path) VALUES(@timeline_creation, @timeline_name, @timeline_desc, @timeline_image_path, @timeline_file_path)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@timeline_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@timeline_name", timeline.timeline_name);
                cmd.Parameters.AddWithValue("@timeline_desc", timeline.timeline_desc);
                cmd.Parameters.AddWithValue("@timeline_image_path", "NULL");
                cmd.Parameters.AddWithValue("@timeline_file_path", "NULL");

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
                      "timeline_image_path = @timeline_image_path" +
                      "timeline_file_path = @timeline_file_path" +
                      "WHERE timelineID = @oldTimelineID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@oldTimelineID", timelineID);
                cmd.Parameters.AddWithValue("@timelineID", new_timeline.timelineID);
                cmd.Parameters.AddWithValue("@timeline_creation", new_timeline.timeline_creation);
                cmd.Parameters.AddWithValue("@timeline_name", new_timeline.timeline_name);
                cmd.Parameters.AddWithValue("@timeline_desc", new_timeline.timeline_desc);
                cmd.Parameters.AddWithValue("@timeline_image_path", new_timeline.timeline_image_path);
                cmd.Parameters.AddWithValue("@timeline_file_path", new_timeline.timeline_file_path);


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

        public QueryResult CreateTimelineLink(int timelineID, int projectID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO timeline_link(timelineID, project_owner_projectID) VALUES(@timelineID, @project_owner_projectID)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);
                cmd.Parameters.AddWithValue("@project_owner_projectID", projectID);

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

        public QueryResult DeleteTimelineLink(int timelineID, int projectID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM timeline_link WHERE timelineID=@timelineID AND project_owner_projectID=@project_owner_projectID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);
                cmd.Parameters.AddWithValue("@project_owner_projectID", projectID);

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

        public QueryResult CreateCommentLink(int commentID, int timelineID, int accountID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO comment_link(commentID, timeline_owner_timelineID, comment_owner_accountID) VALUES(@commentID, @timeline_owner_timelineID, @comment_owner_accountID)";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@commentID", commentID);
                cmd.Parameters.AddWithValue("@timeline_owner_timelineID", timelineID);
                cmd.Parameters.AddWithValue("@comment_owner_accountID", accountID);

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

        public QueryResult DeleteCommentLink(int commentID, int timelineID, int accountID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM comment_link WHERE commentID=@commentID AND timeline_owner_timelineID=@timeline_owner_timelineID AND comment_owner_accountID=@comment_owner_accountID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@commentID", commentID);
                cmd.Parameters.AddWithValue("@timeline_owner_timelineID", timelineID);
                cmd.Parameters.AddWithValue("@comment_owner_accountID", accountID);

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