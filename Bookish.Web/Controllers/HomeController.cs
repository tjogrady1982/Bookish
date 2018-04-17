using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bookish.DataAccess.DataModels;
using Bookish.DataAccess.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (User.Identity.IsAuthenticated)
            {
                var userBooks = BookService.BooksForUser(userManager.FindById(User.Identity.GetUserId()).Email);
                return View(userBooks);
            }

            return View(new List<BookBorrows>());
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
    }
}
