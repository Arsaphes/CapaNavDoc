using System.Web.Mvc;
using System.Web.Security;
using CapaNavDoc.Models;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", new LoggedUserViewModel());
        }

        [HttpPost]
        public ActionResult DoLogin(LoggedUserViewModel ud, string submitButton)
        {
            switch (submitButton)
            {
                case "Login":
                    UserBusinessLayer ubl = new UserBusinessLayer();
                    User user = ubl.GetUser(ud.UserName, ud.Password);
                    if (user == null) return View("Index", ud);
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "User");

                default:
                    return View("Index", new LoggedUserViewModel());
            }
        }
    }
}