using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    //! Account information class.
    /*!
     *  Contains all the information about an account correlated with the database.
     */
    public class Account
    {
        public int accountID;                   /*!< Unique Account ID per user. */
        public SqlDateTime account_creation;    /*!< When this account was created. */
        public string fullname;                 /*!< The name for this account. */
        public string creatordesc;              /*!< The description for this account. */
        public string username;                 /*!< The username for this account. */
        public string password;                 /*!< The password for this account. */
        public string password_salt;            /*!< The salt used for the password. */
        public string email;                    /*!< The email for this account. */
        public bool isSiteAdministrator;        /*!< If this account is an administrator */
        public string account_image_path;       /*!< The File path of the account's profile picture; NULL means none */
        public bool allows_full_name_display;   /*!< Whether this user wants their full name displayed on their profile */
        public bool allows_email_contact;       /*!< Whether this user wants a contact button on their profile with their email */
    }
}