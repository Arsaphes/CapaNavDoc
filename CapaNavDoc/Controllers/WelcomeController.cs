using System.Web.Mvc;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Controllers
{
    public class WelcomeController : Controller
    {
        /// <summary>
        /// Get the default view.
        /// </summary>
        /// <returns>A view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Initialize the database with some dump values.
        /// </summary>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult InitializeDatabase()
        {
            UserBusinessLayer ubl = new UserBusinessLayer();
            ubl.InsertUser(new User
            {
                LastName = "THOMAS",
                UserName = "cthomas",
                FirstName = "Christophe",
                Password = "123"
            });

            return RedirectToAction("Index");
        }
    }
}