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
            connection.Open();
        }

        ~Database()
        {
            if (IsConnectionOpen()) connection.Close();
        }

        public bool TestConnection() // returns true if success
        {
            Database db = new Database();
            if (db.IsConnectionOpen()) return true;
            return false;
        }

        public bool IsConnectionOpen() {
            return connection != null && !(connection.State == ConnectionState.Broken || connection.State == ConnectionState.Closed);
        }

        public string GetLastSQLError() {
            return lastErr;
        }
        
        public bool AccountExists(string username) {

            //Gets a list of all existing accounts.
            List<Account> accounts = GetAccountList();

            //Default the username does not exist.
            bool exists = false;

            //Loops through each account comparing usernames.
            foreach (Account account in accounts) {

                //Checks if the account usernames are equal.
                if (username.Equals(account.username)) {
                    exists = true;
                }

            }

            //Returns the status of the username.
            return exists;

        }

        public bool EmailExists(string email) {

            //Gets a list of all existing accounts.
            List<Account> accounts = GetAccountList();

            //Default the username does not exist.
            bool exists = false;

            //Loops through each account comparing usernames.
            foreach (Account account in accounts) {

                //Checks if the account usernames are equal.
                if (email.Equals(account.email)) {
                    exists = true;
                }

            }

            //Returns the status of the username.
            return exists;

        }

        public Account GetAccountInfo(int accountID) {
            Account accs = new Account();
            if (!IsConnectionOpen()) return null;

            var sql = "SELECT * FROM account where accountID = @accountID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@accountID", accountID);
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    accs.accountID = Convert.ToInt32(row["accountID"]);
                    accs.account_creation = Convert.ToDateTime(row["account_creation"]);
                    accs.fullname = row["fullname"].ToString();
                    accs.username = row["username"].ToString();
                    accs.password = row["password"].ToString();
                    accs.password_salt = row["password_salt"].ToString();
                    accs.email = row["email"].ToString();
                    accs.isSiteAdministrator = Convert.ToBoolean(row["isSiteAdministrator"]);
                    accs.account_image_path = row["account_image_path"].ToString();
                    accs.creatordesc = row["creatordesc"].ToString();
                    accs.allows_full_name_display = Convert.IsDBNull(row["allows_full_name_display"]) ? false : Convert.ToBoolean(row["allows_full_name_display"]);
                    accs.allows_email_contact = Convert.IsDBNull(row["allows_email_contact"]) ? false : Convert.ToBoolean(row["allows_email_contact"]);
                }
            }
            return accs;
        }

        public List<Account> GetAccountList() {
            List<Account> accs = new List<Account>();
            if (!IsConnectionOpen()) return accs;

            var sql = "SELECT * FROM account";
            using (var cmd = new SqlCommand(sql, connection)) {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    accs.Add(new Account() {
                        accountID = Convert.ToInt32(row["accountID"]),
                        account_creation = Convert.ToDateTime(row["account_creation"]),
                        fullname = row["fullname"].ToString(),
                        username = row["username"].ToString(),
                        password = row["password"].ToString(),
                        password_salt = row["password_salt"].ToString(),
                        email = row["email"].ToString(),
                        isSiteAdministrator = Convert.ToBoolean(row["isSiteAdministrator"]),
                        account_image_path = row["account_image_path"].ToString(),
                        creatordesc = row["creatordesc"].ToString(),
                        allows_full_name_display = Convert.IsDBNull(row["allows_full_name_display"]) ? false : Convert.ToBoolean(row["allows_full_name_display"]),
                        allows_email_contact = Convert.IsDBNull(row["allows_email_contact"]) ? false : Convert.ToBoolean(row["allows_email_contact"]),
                    });
                }
            }
            return accs;
        }

        public List<Account> GetAccountList(string search)
        {
            List<Account> accs = new List<Account>();
            if (!IsConnectionOpen()) return accs;

            var sql = "SELECT * FROM account WHERE CHARINDEX(@search, username) > 0;";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@search", search);
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
                        account_image_path = row["account_image_path"].ToString(),
                        creatordesc = row["creatordesc"].ToString(),
                        allows_full_name_display = Convert.IsDBNull(row["allows_full_name_display"]) ? false : Convert.ToBoolean(row["allows_full_name_display"]),
                        allows_email_contact = Convert.IsDBNull(row["allows_email_contact"]) ? false : Convert.ToBoolean(row["allows_email_contact"]),
                    });
                }
            }
            return accs;
        }

        public QueryResult CreateAccount(Account account) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO account(account_creation, fullname, username, password, password_salt, email, isSiteAdministrator, account_image_path) VALUES(@account_creation, @fullname, @username, @password, @password_salt, @email, @isSiteAdministrator, @account_image_path)";
            using (var cmd = new SqlCommand(sql, connection)) {
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
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }

            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteAccount(int accountID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM account WHERE accountID=@accountID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@accountID", accountID);


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the delete was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteAccountFull(int accountID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            // GET ALL THE PROJECTS, TIMELINES, COMMENTS, AND NOTIFICATIONS related to this user
            foreach (Project p in GetProjectList(accountID))
            {
                // delete any references of timelines
                foreach (Timeline t in GetTimelineList(p.projectID))
                {
                    foreach (Comment c in GetCommentList(t.timelineID))
                    {
                        DeleteCommentLink(c.commentID, t.timelineID, accountID);
                        DeleteComment(c.commentID);
                    }

                    DeleteTimelineLink(t.timelineID, p.projectID);
                    DeleteTimeline(t.timelineID);

                    // delete notifs?
                    DeleteNotification(accountID, t.timelineID);
                }

                DeleteProjectLink(p.projectID, accountID);
                DeleteProject(p.projectID);
            }

            return DeleteAccount(accountID);
        }

        public QueryResult ModifyAccount(int accountID, Account new_account) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE account SET " +
                      "account_creation = @account_creation, " +
                      "fullname = @fullname, " +
                      "username = @username, " +
                      "password = @password, " +
                      "password_salt = @password_salt, " +
                      "email = @email, " +
                      "isSiteAdministrator = @isSiteAdministrator, " +
                      "account_image_path = @account_image_path, " +
                      "creatordesc = @creatordesc, " +
                      "allows_full_name_display = @allows_full_name_display, " +
                      "allows_email_contact = @allows_email_contact " +
                      "WHERE accountID = @accountID ";
            using (var cmd = new SqlCommand(sql, connection)) {
                var salt = Password.Salt();
                cmd.Parameters.AddWithValue("@accountID", accountID);
                cmd.Parameters.AddWithValue("@account_creation", new_account.account_creation);
                cmd.Parameters.AddWithValue("@fullname", new_account.fullname);
                cmd.Parameters.AddWithValue("@username", new_account.username);
                cmd.Parameters.AddWithValue("@password", new_account.password);
                cmd.Parameters.AddWithValue("@password_salt", new_account.password_salt);
                cmd.Parameters.AddWithValue("@email", new_account.email);
                cmd.Parameters.AddWithValue("@isSiteAdministrator", new_account.isSiteAdministrator);
                cmd.Parameters.AddWithValue("@account_image_path", new_account.account_image_path);
                cmd.Parameters.AddWithValue("@creatordesc", new_account.creatordesc);
                cmd.Parameters.AddWithValue("@allows_full_name_display", new_account.allows_full_name_display);
                cmd.Parameters.AddWithValue("@allows_email_contact", new_account.allows_email_contact);

                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public Account AuthenticateAccount(string username, string password) {

            foreach (Account acc in GetAccountList()) {
                if (username == acc.username && Password.ComparePassword(password, acc.password, acc.password_salt)) return acc;
            }

            return null;
        }

        // ACCOUNTS

        // PROJECTS
        public string GetProjectAuthor(int projectID) {
            string author = "";
            if (!IsConnectionOpen()) return null;

            var sql = "select username from project_link inner join account on project_link.project_owner_accountID = account.accountID where projectID = @projectID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@projectID", projectID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    author = row["username"].ToString();
                }
            }

            return author;
        }

        public int GetProjectOwner(int projectID)
        {
            int author = 0;
            if (!IsConnectionOpen()) return 0;

            var sql = "select accountID from project_link inner join account on project_link.project_owner_accountID = account.accountID where projectID = @projectID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    author = Convert.ToInt32(row["accountID"]);
                }
            }

            return author;
        }

        public Project GetProject(int projectID) {
            Project project = new Project();
            if (!IsConnectionOpen()) return null;

            var sql = "SELECT * FROM project where projectID = @projectID";
            using (var cmd = new SqlCommand(sql, connection)) {
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
                    project.project_visibility = Convert.ToInt32(row["project_visibility"]);
                }
            }

            return project;
        }

        public List<Project> GetProjectList() {
            List<Project> projects = new List<Project>();
            if (!IsConnectionOpen()) return projects;

            var sql = "SELECT * FROM project";
            using (var cmd = new SqlCommand(sql, connection)) {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    projects.Add(new Project() {
                        projectID = Convert.ToInt32(row["projectID"]),
                        project_creation = Convert.ToDateTime(row["project_creation"]),
                        project_name = row["project_name"].ToString(),
                        project_desc = row["project_desc"].ToString(),
                        project_image_path = row["project_image_path"].ToString(),
                        project_visibility = Convert.ToInt32(row["project_visibility"])
                    });
                }
            }
            return projects;
        }

        public List<Project> GetProjectList(int accountID) // return projects belonging to a user
        {
            List<Project> projects = new List<Project>();
            if (!IsConnectionOpen()) return projects;

            var sql = "SELECT * " +
                      "FROM project " +
                      "LEFT JOIN project_link ON (project.projectID = project_link.projectID) " +
                      "WHERE project_link.project_owner_accountID = @accountID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@accountID", accountID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    projects.Add(new Project() {
                        projectID = Convert.ToInt32(row["projectID"]),
                        project_creation = Convert.ToDateTime(row["project_creation"]),
                        project_name = row["project_name"].ToString(),
                        project_desc = row["project_desc"].ToString(),
                        project_image_path = row["project_image_path"].ToString(),
                        project_visibility = Convert.ToInt32(row["project_visibility"])
                    });
                }
            }
            return projects;
        }

        public List<Project> GetProjectList(string search, int visibility, int sorting) // return projects with substring in title
        {
            /*
             * SORTING:
             * 1 = DESCENDING
             * 2 = ASCENDING
             * 3 = MOST FOLLOWERS
             * 4 = LEAST FOLLOWERS
             */
            List<Project> projects = new List<Project>();
            if (!IsConnectionOpen()) return projects;

            var sql = "SELECT project.*, (SELECT COUNT(*) FROM follower_link WHERE follower_link.projectID = project.projectID) AS follower_count " +
                      "FROM project " +
                      "INNER JOIN project_link ON project_link.projectID = project.projectID " +
                      "INNER JOIN account ON account.accountID = project_link.project_owner_accountID " +
                      "WHERE CHARINDEX(@search, project.project_name) > 0 " +
                      "OR CHARINDEX(@search, project.project_desc) > 0 " +
                      "OR CHARINDEX(@search, account.username) > 0 ";
            if(search == "")
            {
                sql = "SELECT *, (SELECT COUNT(*) FROM follower_link WHERE follower_link.projectID = project.projectID) AS follower_count " +
                      "FROM project ";
                if (visibility == 1)
                {
                    sql += "WHERE project.project_visibility = @visibility ";
                }
            }
            else
            {
                if (visibility == 1)
                {
                    sql += "AND project.project_visibility = @visibility ";
                }
            }
            switch (sorting)
            {

                case 1:
                    sql += "ORDER BY project.projectID DESC;";
                    break;

                case 2:
                    sql += "ORDER BY project.projectID ASC;";
                    break;

                case 3:
                    sql += "ORDER BY follower_count DESC;";
                    break;

                case 4:
                    sql += "ORDER BY follower_count ASC;";
                    break;
            }

            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@search", search);
                cmd.Parameters.AddWithValue("@visibility", visibility);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    projects.Add(new Project() {
                        projectID = Convert.ToInt32(row["projectID"]),
                        project_creation = Convert.ToDateTime(row["project_creation"]),
                        project_name = row["project_name"].ToString(),
                        project_desc = row["project_desc"].ToString(),
                        project_image_path = row["project_image_path"].ToString(),
                        project_visibility = Convert.ToInt32(row["project_visibility"])
                    });
                }
            }
            return projects;
        }

        public List<Project> GetProjectList(int accountID, string search, int visibility, int sorting) // return projects with substring in title
        {
            /*
             * SORTING:
             * 1 = DESCENDING
             * 2 = ASCENDING
             * 3 = MOST FOLLOWERS
             * 4 = LEAST FOLLOWERS
             */
            List<Project> projects = new List<Project>();
            if (!IsConnectionOpen()) return projects;

            var sql = "SELECT project.*, (SELECT COUNT(*) FROM follower_link WHERE follower_link.projectID = project.projectID) AS follower_count " +
                      "FROM project " +
                      "INNER JOIN project_link ON project_link.projectID = project.projectID " +
                      "INNER JOIN account ON account.accountID = project_link.project_owner_accountID " +
                      "WHERE CHARINDEX(@search, project.project_name) > 0 " +
                      "OR CHARINDEX(@search, project.project_desc) > 0 " +
                      "OR CHARINDEX(@search, account.username) > 0 " +
                      "AND project_link.project_owner_accountID = @account ";
            if (search == "")
            {
                sql = "SELECT *, (SELECT COUNT(*) FROM follower_link WHERE follower_link.projectID = project.projectID) AS follower_count " +
                      "FROM project " +
                      "INNER JOIN project_link ON project_link.projectID = project.projectID " +
                      "WHERE project_link.project_owner_accountID = @account ";
            }
            if (visibility == 1)
            {
                sql += "AND project.project_visibility = @visibility ";
            }
            switch (sorting)
            {

                case 1:
                    sql += "ORDER BY project.projectID DESC;";
                    break;

                case 2:
                    sql += "ORDER BY project.projectID ASC;";
                    break;

                case 3:
                    sql += "ORDER BY follower_count DESC;";
                    break;

                case 4:
                    sql += "ORDER BY follower_count ASC;";
                    break;
            }

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@search", search);
                cmd.Parameters.AddWithValue("@visibility", visibility);
                cmd.Parameters.AddWithValue("@account", accountID);

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
                        project_image_path = row["project_image_path"].ToString(),
                        project_visibility = Convert.ToInt32(row["project_visibility"])
                    });
                }
            }
            return projects;
        }

        public int CreateProject(Project project) {

            //Prepares the sql query.
            var sql = "INSERT INTO project(project_creation, project_name, project_desc, project_image_path, project_visibility) VALUES(@project_creation, @project_name, @project_desc, @project_image_path, @project_visibility) SELECT SCOPE_IDENTITY()";
            using (var cmd = new SqlCommand(sql, connection)) {

                cmd.Parameters.AddWithValue("@project_creation", project.project_creation);
                cmd.Parameters.AddWithValue("@project_name", project.project_name);
                cmd.Parameters.AddWithValue("@project_desc", project.project_desc);
                cmd.Parameters.AddWithValue("@project_image_path", project.project_image_path);
                cmd.Parameters.AddWithValue("@project_visibility", project.project_visibility);

                //Executes the insert command.
                try
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (SqlException except)
                {
                    lastErr = except.Message;
                }

                return 0;

            }

        }

        public QueryResult DeleteProject(int projectID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM project WHERE projectID=@projectID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@projectID", projectID);

                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult ModifyProject(int projectID, Project new_project) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE project SET " +
                      "project_creation = @project_creation, " +
                      "project_name = @project_name, " +
                      "project_desc = @project_desc, " +
                      "project_image_path = @project_image_path, " +
                      "project_visibility = @project_visibility " +
                      "WHERE projectID = @oldProjectID;";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@oldProjectID", projectID);
                cmd.Parameters.AddWithValue("@projectID", new_project.projectID);
                cmd.Parameters.AddWithValue("@project_creation", new_project.project_creation);
                cmd.Parameters.AddWithValue("@project_name", new_project.project_name);
                cmd.Parameters.AddWithValue("@project_desc", new_project.project_desc);
                cmd.Parameters.AddWithValue("@project_image_path", new_project.project_image_path);
                cmd.Parameters.AddWithValue("@project_visibility", new_project.project_visibility);

                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult CreateProjectLink(int projectID, int accountID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO project_link(projectID, project_owner_accountID) VALUES(@projectID, @project_owner_accountID)";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@project_owner_accountID", accountID);

                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteProjectLink(int projectID, int accountID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM project_link WHERE projectID=@projectID AND project_owner_accountID=@project_owner_accountID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@project_owner_accountID", accountID);


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult AddFollower(int projectID, Account account) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO follower_link(projectID, follower_accountID, follow_date) VALUES(@projectID, @follower_accountID, @follow_date)";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@follower_accountID", account.accountID);
                cmd.Parameters.AddWithValue("@follow_date", new SqlDateTime(DateTime.Now));


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult RemoveFollower(int projectID, Account account) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM follower_link WHERE projectID=@projectID AND follower_accountID=@follower_accountID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                cmd.Parameters.AddWithValue("@follower_accountID", account.accountID);


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public List<int> GetFollowers(int projectID)
        {
            List<int> result = new List<int>();
            if (!IsConnectionOpen()) return null;

            //Prepares the sql query.
            var sql = "SELECT * FROM follower_link WHERE projectID=@projectID";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@projectID", projectID);
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                //Adds the follower account IDs to the list.
                foreach(DataRow row in datatable.Rows)
                {
                    result.Add(Convert.ToInt32(row["follower_accountID"]));
                }
            }

            return result;
        }

        // PROJECTS

        // TIMELINE
        public List<Timeline> GetTimelineList() {
            List<Timeline> timelines = new List<Timeline>();
            if (!IsConnectionOpen()) return timelines;

            var sql = "SELECT * FROM timeline";
            using (var cmd = new SqlCommand(sql, connection)) {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    timelines.Add(new Timeline() {
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
                      "LEFT JOIN timeline_link ON (timeline.timelineID = timeline_link.timelineID) " +
                      "WHERE timeline_link.project_owner_projectID = @projectID " +
                      "ORDER BY timeline.timelineID ASC;";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@projectID", projectID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    timelines.Add(new Timeline() {
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

        public bool CheckTimelineInProject(int projectID, int timelineID) // return a specific timeline
        {
            Timeline timeline = new Timeline();
            if (!IsConnectionOpen()) return false;

            var sql = "SELECT * " +
                      "FROM timeline_link " +
                      "WHERE timelineID = @timelineID and project_owner_projectID = @project";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);
                cmd.Parameters.AddWithValue("@project", projectID);
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                if(datatable.Rows.Count > 0) {
                    return true;
                } else {
                    return false;
                }
            }

        }

        public Timeline GetTimeline(int timelineID) // return a specific timeline
        {
            Timeline timeline = new Timeline();
            if (!IsConnectionOpen()) return timeline;

            var sql = "SELECT * " +
                      "FROM timeline " +
                      "WHERE timelineID = @timelineID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    timeline.timelineID = Convert.ToInt32(row["timelineID"]);
                    timeline.timeline_creation = Convert.ToDateTime(row["timeline_creation"]);
                    timeline.timeline_name = row["timeline_name"].ToString();
                    timeline.timeline_desc = row["timeline_desc"].ToString();
                    timeline.timeline_image_path = row["timeline_image_path"].ToString();
                    timeline.timeline_file_path = row["timeline_file_path"].ToString();
                }
            }

            return timeline;
        }

        public int CreateTimeline(Timeline timeline) {
            int result = 0;

            //Prepares the sql query.
            var sql = "INSERT INTO timeline(timeline_creation, timeline_name, timeline_desc, timeline_image_path, timeline_file_path) VALUES(@timeline_creation, @timeline_name, @timeline_desc, @timeline_image_path, @timeline_file_path) SELECT SCOPE_IDENTITY()";
            using (var cmd = new SqlCommand(sql, connection)) {

                cmd.Parameters.AddWithValue("@timeline_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@timeline_name", timeline.timeline_name);
                cmd.Parameters.AddWithValue("@timeline_desc", timeline.timeline_desc);
                cmd.Parameters.AddWithValue("@timeline_image_path", timeline.timeline_image_path);
                cmd.Parameters.AddWithValue("@timeline_file_path", timeline.timeline_file_path);

                //Executes the insert command.
                try {
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (SqlException except) 
                {
                    lastErr = except.Message;
                }
            }

            return result;
        }

        public QueryResult DeleteTimeline(int timelineID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM timeline WHERE timelineID=@timelineID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult ModifyTimeline(int timelineID, Timeline new_timeline) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE timeline SET " +
                      "timeline_creation = @timeline_creation, " +
                      "timeline_name = @timeline_name, " +
                      "timeline_desc = @timeline_desc, " +
                      "timeline_image_path = @timeline_image_path, " +
                      "timeline_file_path = @timeline_file_path " +
                      "WHERE timelineID = @oldTimelineID ";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@oldTimelineID", timelineID);
                cmd.Parameters.AddWithValue("@timeline_creation", new_timeline.timeline_creation);
                cmd.Parameters.AddWithValue("@timeline_name", new_timeline.timeline_name);
                cmd.Parameters.AddWithValue("@timeline_desc", new_timeline.timeline_desc);
                cmd.Parameters.AddWithValue("@timeline_image_path", new_timeline.timeline_image_path);
                cmd.Parameters.AddWithValue("@timeline_file_path", new_timeline.timeline_file_path);


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult CreateTimelineLink(int timelineID, int projectID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO timeline_link(timelineID, project_owner_projectID) VALUES(@timelineID, @project_owner_projectID)";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);
                cmd.Parameters.AddWithValue("@project_owner_projectID", projectID);

                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteTimelineLink(int timelineID, int projectID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM timeline_link WHERE timelineID=@timelineID AND project_owner_projectID=@project_owner_projectID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);
                cmd.Parameters.AddWithValue("@project_owner_projectID", projectID);

                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        // TIMELINE

        // COMMENT

        public List<Comment> GetCommentList() {
            List<Comment> comments = new List<Comment>();
            if (!IsConnectionOpen()) return comments;

            var sql = "SELECT * FROM comment";
            using (var cmd = new SqlCommand(sql, connection)) {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    comments.Add(new Comment() {
                        commentID = Convert.ToInt32(row["commentID"]),
                        comment_creation = Convert.ToDateTime(row["comment_creation"]),
                        comment_text = row["comment_text"].ToString(),
                    });
                }
            }

            return comments;
        }

        public int GetRecentCommentID() {
            int id = 0;
            if (!IsConnectionOpen()) return id;

            var sql = "SELECT IDENT_CURRENT('comment') as current_identity";
            using (var cmd = new SqlCommand(sql, connection)) {
                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    id = Convert.ToInt32(row["current_identity"]);
                }
            }

            return id;
        }

        public List<Comment> GetCommentList(int timelineID) // return comments belonging to a timeline
        {
            List<Comment> comments = new List<Comment>();
            if (!IsConnectionOpen()) return comments;

            var sql = "SELECT * " +
                      "FROM comment " +
                      "LEFT JOIN comment_link ON (comment.commentID = comment_link.commentID) " +
                      "WHERE comment_link.timeline_owner_timelineID = @timelineID ";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@timelineID", timelineID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows) {
                    comments.Add(new Comment() {
                        commentID = Convert.ToInt32(row["commentID"]),
                        comment_creation = Convert.ToDateTime(row["comment_creation"]),
                        comment_text = row["comment_text"].ToString(),
                        comment_owner_accountID = row["comment_owner_accountID"].ToString(),
                    });
                }
            }

            return comments;
        }

        public QueryResult CreateComment(Comment comment) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO comment(comment_creation, comment_text) VALUES(@comment_creation, @comment_text)";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@comment_creation", new SqlDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@comment_text", comment.comment_text);


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteComment(int commentID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM comment WHERE commentID=@commentID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@commentID", commentID);


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult ModifyComment(int commentID, Comment new_comment) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "UPDATE comment SET " +
                      "commentID = @commentID" +
                      "comment_creation = @comment_creation" +
                      "comment_text = @comment_text" +
                      "WHERE commentID = @oldCommentID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@oldCommentID", commentID);
                cmd.Parameters.AddWithValue("@commentID", new_comment.commentID);
                cmd.Parameters.AddWithValue("@comment_creation", new_comment.comment_creation);
                cmd.Parameters.AddWithValue("@comment_text", new_comment.comment_text);


                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult CreateCommentLink(int commentID, int timelineID, int accountID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "INSERT INTO comment_link(commentID, timeline_owner_timelineID, comment_owner_accountID) VALUES(@commentID, @timeline_owner_timelineID, @comment_owner_accountID)";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@commentID", commentID);
                cmd.Parameters.AddWithValue("@timeline_owner_timelineID", timelineID);
                cmd.Parameters.AddWithValue("@comment_owner_accountID", accountID);

                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }

        public QueryResult DeleteCommentLink(int commentID, int timelineID, int accountID) {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Prepares the sql query.
            var sql = "DELETE FROM comment_link WHERE commentID=@commentID AND timeline_owner_timelineID=@timeline_owner_timelineID AND comment_owner_accountID=@comment_owner_accountID";
            using (var cmd = new SqlCommand(sql, connection)) {
                cmd.Parameters.AddWithValue("@commentID", commentID);
                cmd.Parameters.AddWithValue("@timeline_owner_timelineID", timelineID);
                cmd.Parameters.AddWithValue("@comment_owner_accountID", accountID);

                //Executes the insert command.
                try {
                    result = cmd.ExecuteNonQuery();
                } catch (SqlException except) {
                    lastErr = except.Message;
                    return QueryResult.FailedBadQuery;
                }
            }

            //Returns if the insert was successful.
            if (result > 0) {
                return QueryResult.Successful;
            }

            return QueryResult.FailedNoChanges;
        }
        // COMMENT

        // NOTIFICATIONS
        public QueryResult CreateNotifications(int ProjectID, int TimelineID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Gets a list of followers.
            List<int> follower_list = GetFollowers(ProjectID);

            //Gets an SQL statement string.
            var sql = "INSERT INTO notify(notify_account_id, notify_timeline_id) VALUES";

            //Loops through each follower adding them to the query string.
            foreach(var follower in follower_list)
            {

                //Adds the follower entry on to the list.
                sql += "(" + follower + "," + TimelineID + "),";

            }

            //Replaces the last comma with a semi-colon.
            sql = sql.Remove(sql.Length - 1, 1) + ";";

            using (var cmd = new SqlCommand(sql, connection))
            {

                //Executes the sql query.
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

        public List<Timeline> GetNotifications(int AccountID)
        {
            List<Timeline> notifications = new List<Timeline>();
            var sql = "SELECT *, timeline_link.project_owner_projectID " +
                      "FROM timeline " +
                      "INNER JOIN timeline_link ON (timeline.timelineID = timeline_link.timelineID) " +
                      "LEFT JOIN notify ON (timeline.timelineID = notify.notify_timeline_id) " +
                      "WHERE notify.notify_account_id = @accountID " +
                      "ORDER BY timeline.timelineID ASC;";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@accountID", AccountID);

                var adapter = new SqlDataAdapter(cmd);
                var datatable = new DataTable();
                adapter.Fill(datatable);

                foreach (DataRow row in datatable.Rows)
                {
                    notifications.Add(new Timeline()
                    {
                        timelineID = Convert.ToInt32(row["timelineID"]),
                        timeline_creation = Convert.ToDateTime(row["timeline_creation"]),
                        timeline_name = row["timeline_name"].ToString(),
                        timeline_desc = row["timeline_desc"].ToString(),
                        timeline_image_path = row["timeline_image_path"].ToString(),
                        timeline_file_path = row["timeline_file_path"].ToString(),
                        timeline_project = Convert.ToInt32(row["project_owner_projectID"]),
                    });
                }
            }

            return notifications;

        }

        public QueryResult DeleteNotification(int AccountID, int TimelineID)
        {
            int result;
            if (!IsConnectionOpen()) return QueryResult.FailedNotConnected;

            //Replaces the last comma with a semi-colon.
            string sql = "DELETE FROM notify WHERE notify_account_id = @notify_account_id AND notify_timeline_id = @notify_timeline_id;";

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@notify_account_id", AccountID);
                cmd.Parameters.AddWithValue("@notify_timeline_id", TimelineID);

                //Executes the sql query.
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
        
    }
}