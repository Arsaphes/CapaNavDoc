using System.Web.Mvc;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Controllers
{
    public class WelcomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

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