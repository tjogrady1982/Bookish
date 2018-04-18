using System;
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

        public static List<BookTitle> ListTitles()
        {
            var sqlString = "Select * from tblTitle order by Title";
            IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["BookishConnection"].ConnectionString);

            var data = db.Query<BookTitle>(sqlString).ToList();
            return data;
        }

        public static int Copies(int titleId)
        {
            var sqlString = "select TitleID, Count(TitleID) Copies from tblbook group by TitleID having TitleID = '" + titleId + "'";
            IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["BookishConnection"].ConnectionString);
            var data = db.Query<Book>(sqlString).FirstOrDefault()?.Copies ?? 0; //null coalesce

            return data;
        }

        public static List<Book> BorrowedCopies(int titleId)
        {
            var sqlString = "select tblTitle.*, tbb.DueDate, tbb.BookID, tbu.EmailAddress from tblBorrow tbb " +
                            "join tblUsers tbu on tbb.UserID = tbu.UserID " +
                            "join tblBook on tbb.BookID = tblBook.BookID " +
                            "join tblTitle on tblBook.TitleID = tblTitle.TitleID " +
                            $"where tblTitle.TitleID = '{titleId}' and DateReturned is null";

            IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["BookishConnection"].ConnectionString);

            var data = db.Query<Book>(sqlString).ToList();

            return data;
        }

        public static int GetBookId(int titleId)
        {
            var sqlString = "Select TOP 1 BookID from tblBook  " +
                            "join tblTitle on tblBook.TitleID = tblTitle.TitleID " +
                            $"WHERE tblTitle.TitleID = '{titleId}' and BookID not in  " +
                            "(Select bookid from tblBorrow where DateReturned is null) ";
                            
            IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["BookishConnection"].ConnectionString);
            var data = db.Query<Borrow>(sqlString).FirstOrDefault()?.BookId ?? 0; //null coalesce

            return data;
        }

        public static void BorrowBook(int bookId, string emailAddress)
        {
            var today = System.DateTime.Today.Date;
            var returnDay = today.AddMonths(1); 

            var sqlToday = Convert.ToDateTime(today).ToString("yyyy-MM-dd");
            var sqlReturnDay = Convert.ToDateTime(returnDay).ToString("yyyy-MM-dd");

            var sqlString = "INSERT into tblBorrow (UserID, BookID, DateBorrowed, DueDate) " +
                            "Values ((select userid from tblUsers where EmailAddress = '"  +  emailAddress + "')" +
                            ", " + bookId + ", '"  + sqlToday + "', '"  + sqlReturnDay + "')";
            IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["BookishConnection"].ConnectionString);
            db.Query(sqlString);
        }


    }
}
