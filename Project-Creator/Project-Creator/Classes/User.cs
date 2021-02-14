using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Creator
{
    public enum Type
    {
        user = 0,
        creator = 1,
        admin = 2
    }

    public class User
    {
        public int id;
        public string username;
        public string password;
        public string fullname;
        public string email;
        public int type;
    }
}