using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Bookish.DataAccess.DataModels;
using Dapper;

namespace Bookish.DataAccess.Services
{
    public class BookService
    {
        public static List<BookBorrows> BooksForUser(string userEmail)
        {
            var sqlString = "select tblTitle.*, tbb.DueDate from tblBorrow tbb " +
                "join tblUsers tbu on tbb.UserID = tbu.UserID " +
                "join tblBook on tbb.BookID = tblBook.BookID " +
                "join tblTitle on tblBook.TitleID = tblTitle.TitleID " +
                $"where tbu.EmailAddress = '{userEmail}'";

            // TODO avoid making a new connection per request
            IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["BookishConnection"].ConnectionString);

            var data = db.Query<BookBorrows>(sqlString).ToList();
            //var books = new List<BookTitle>();
            //foreach (var row in data)
            //{
            //    books.Add((BookTitle)row);   
            //}

            return data;
        }
    }
}
