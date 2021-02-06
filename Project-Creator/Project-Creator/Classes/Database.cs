using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Project_Creator.Classes
{
    public class Database
    {

        //Defines the database connection variables.
        private string server = "us-cdbr-east-03.cleardb.com";
        private string port = "3306";
        private string username = "b657cf55589ae9";
        private string password = "a6d3be7b";
        private string database = "heroku_57d61cda850cb7a";
        private MySqlConnection connection;

        public Database()
        {
            connection = new MySqlConnection("server=" + server + ";port=" + port + ";user=" + username + ";password=" + password + ";database=" + database + ";");
        }

        public bool Signup(string username, string password, string firstname, string lastname, string email)
        {

        }
    }
}