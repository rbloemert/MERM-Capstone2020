using System;
using System.Text;
using System.Security.Cryptography;

namespace Project_Creator.Classes
{
    public static class Password
    {

        //Defines the password salt and hash variables.
        public const int SALT_SIZE = 24;
        public const int HASH_SIZE = 24;
        public const int ITERATIONS = 1000;

        public static string Salt()
        {

            //Generates a random salt for the password.
            var salt = new byte[SALT_SIZE];
            new RNGCryptoServiceProvider().GetBytes(salt);
            return Convert.ToBase64String(salt);

        }

        public static string Encrypt(string password, string salt)
        {

            //Hashes the password with the salt.
            var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(salt), ITERATIONS);
            var hash = pbkdf2.GetBytes(HASH_SIZE);

            //Returns the password hash.
            return Convert.ToBase64String(hash);

        }

        public static bool ComparePassword(string password, string hash, string salt)
        {

            //Hashes the password with the salt.
            var newHash = Encrypt(password, salt);

            //Checks if the hased password are equal.
            if (newHash == hash)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}