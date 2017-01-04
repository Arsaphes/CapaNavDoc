using System.Web.Mvc;
using System.Web.Security;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", new LoginUserViewModel());
        }

        [HttpPost]
        public ActionResult DoLogin(LoginUserViewModel loginUser, string submitAction)
        {
            switch (submitAction)
            {
                case "Login":
                    UserBusinessLayer bl = new UserBusinessLayer();
                    User user = bl.GetUser(loginUser.UserName, loginUser.Password);
                    if (user == null) return View("Index", loginUser);
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "User");

                default:
                    return View("Index", new LoginUserViewModel());
            }
        }
    }
}