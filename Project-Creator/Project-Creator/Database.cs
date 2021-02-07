using System;
using System.IO;
using System.Security.Cryptography;
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
        private string key = "EVAO9NR3R920";
        private byte[] salt = { 0x14, 0x64, 0x98, 0x65, 0x24, 0x75, 0x45, 0x12, 0x15, 0x13, 0x18, 0x19, 0x14 };
        private MySqlConnection connection;

        public Database()
        {

            //Gets a connection to the MySQL database.
            connection = new MySqlConnection("server=" + server + ";port=" + port + ";user=" + username + ";password=" + password + ";database=" + database + ";");

            //Opens the connection to the server.
            connection.Open();

        }

        ~Database()
        {

            //Closes the database connection.
            connection.Close();

        }

        public bool Signup(User signupUser)
        {

            //Prepares the sql query.
            var sql = "INSERT INTO users(username, password, firstname, lastname, email) VALUES(@username, @password, @firstname, @lastname, @email)";
            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@username", signupUser.username);
            cmd.Parameters.AddWithValue("@password", Encrypt(signupUser.password));
            cmd.Parameters.AddWithValue("@firstname", signupUser.firstname);
            cmd.Parameters.AddWithValue("@lastname", signupUser.lastname);
            cmd.Parameters.AddWithValue("@email", signupUser.email);
            cmd.Prepare();

            //Executes the insert command.
            int result = cmd.ExecuteNonQuery();

            //Returns if the insert was successful.
            return (result > 0);

        }

        public string Encrypt(string password)
        {

            //Gets the password byte array.
            byte[] passwordBytes = System.Text.Encoding.Unicode.GetBytes(password);

            //Creates an encryptor.
            using(Aes encryptor = Aes.Create())
            {

                //Derives bytes for the password.
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                //Creates a memory stream.
                using(MemoryStream ms = new MemoryStream())
                {

                    //Creates a crypto stream.
                    using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
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
    }
}