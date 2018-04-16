using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace Bookish.DataAccess
{
    public class Users
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public static void RegisterUser(string emailAddress, string password, string firstname, string lastname)
        {
            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BookishConnection"].ConnectionString);
            string sqlstring = "Insert into tblUsers (EmailAddress, Password, FirstName, Surname) VALUES ('" + emailAddress + "', '" + password + "', '" + firstname + "', '" + lastname + "')";
            db.Query(sqlstring);
        }
    }

}
