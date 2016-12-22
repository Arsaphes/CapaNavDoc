using System.Web.Mvc;

namespace CapaNavDoc.Controllers
{
    public class WelcomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}