using System;
using System.Text;
using System.Security.Cryptography;

namespace Project_Creator.Classes
{
    //! Password encryption and decryption class.
    /*!
     *  Used for encrypting and decrypting password to be stored and read from the database.
     */
    public static class Password
    {

        //Defines the password salt and hash variables.
        public const int SALT_SIZE = 24; //!< The size of the byte array for generating salt.
        public const int HASH_SIZE = 24; //!< The size of the hash to be generated.
        public const int ITERATIONS = 1000; //!< How many iterations the password should be hashed through.

        /*!
         *  Generates a random salt string for encryption.
         *  @return the salt generated for password encryption
         */
        public static string Salt()
        {

            //Generates a random salt for the password.
            var salt = new byte[SALT_SIZE];
            new RNGCryptoServiceProvider().GetBytes(salt);
            return Convert.ToBase64String(salt);

        }

        /*!
         *  Encrypts a password with the password string and salt provided.
         *  @param password the password string to encrypt
         *  @param salt the salt to support the password encryption
         *  @return the hashed password generated with the password string and salt
         */
        public static string Encrypt(string password, string salt)
        {

            //Hashes the password with the salt.
            var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(salt), ITERATIONS);
            var hash = pbkdf2.GetBytes(HASH_SIZE);

            //Returns the password hash.
            return Convert.ToBase64String(hash);

        }

        /*!
         *  Compares the stored password hash with the newly generated password hash.
         *  @param password the password the encrypt and compare with the hash
         *  @param hash the hash to compare with the final encrypted password
         *  @param salt the salt to has the password string with for comparison
         *  @return true of the hashes are equal otherwise false
         */
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