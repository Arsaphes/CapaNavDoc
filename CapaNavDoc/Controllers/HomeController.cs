using System.Web.Mvc;
using System.Web.Security;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Get the default view.
        /// </summary>
        /// <returns>A view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", new LoginUserViewModel());
        }

        /// <summary>
        /// Log the user provided valid credentials.
        /// </summary>
        /// <param name="loginUser">The LoginUserViewModel used to log the user.</param>
        /// <param name="submitAction">The action to perform.</param>
        /// <returns>The default view if login does not success, a redirection if login success.</returns>
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