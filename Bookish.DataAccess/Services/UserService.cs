using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Bookish.DataAccess.Services
{
    public class UserService
    {
        public static void RegisterUser(string emailAddress, string password, string firstname, string lastname)
        {
            IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["BookishConnection"].ConnectionString);
            string sqlstring = "Insert into tblUsers (EmailAddress, Password, FirstName, Surname) VALUES ('" +
                               emailAddress + "', '" + password + "', '" + firstname + "', '" + lastname + "')";
            db.Query(sqlstring);
        }
    }
}
