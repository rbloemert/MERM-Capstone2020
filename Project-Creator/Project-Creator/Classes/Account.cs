﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    public class Account
    {
        public int accountID;                   /* accountID: Unique Account ID per user. Auto incremented */
        public SqlDateTime account_creation;    /* account_creation: When this account was created. */
        public string firstname;                /* firstname: The first name for this account. */
        public string lastname;                 /* lastname: The last name for this account. */
        public string username;                 /* username: The username for this account. */
        public string password;                 /* password: The password for this account. */
        public string email;                    /* email: The email for this account. */
        public bool isSiteAdministrator;        /* isSiteAdministrator: If this account is an administrator */
    }
}