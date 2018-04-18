﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bookish.DataAccess.DataModels;
using Bookish.DataAccess.Services;
using Bookish.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
 
namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (User.Identity.IsAuthenticated)
            {
                var userid = userManager.FindById(User.Identity.GetUserId()).Email;
                var userBooks = BookService.BooksForUser(userid);
                var usersFirstName = UserService.GetFirstName(userid);
                return View(new HomeViewModel{

                    Books = userBooks,
                    FirstName = usersFirstName
                });

            }

            return View(new HomeViewModel
            {
              Books = new List<BookBorrows>  ()
            });

        }

       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Library(int page = 1, int pagesize = 2)
        {
            var title = BookService.ListTitles();
            PagedList<BookTitle> model = new PagedList<BookTitle>(title, page, pagesize);
            return View(model);
        }


        public ActionResult BookDetails(int titleId, string title)
        {
            var copies = BookService.Copies(titleId);
            var borrowed = BookService.BorrowedCopies(titleId);
            return View(new BookViewModel
            {
                Title = title,
                TitleId = titleId,
                Copies = copies,
                Borrowed = borrowed
            });
        }

        public ActionResult RentBook(int titleId)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (User.Identity.IsAuthenticated)
            {
                var bookId = BookService.GetBookId(titleId);
                var userid = userManager.FindById(User.Identity.GetUserId()).Email;

                BookService.BorrowBook(bookId,userid);
            }

            return View();
        }
    }
}
