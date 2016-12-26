using System.Web.Mvc;
using CapaNavDoc.DataAccessLayer;
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
            ubl.InsertUser(new User
            {
                FirstName = "Anna",
                LastName = "HASINA",
                Password = "123",
                UserName = "anna"
            });
            ubl.InsertUser(new User
            {
                FirstName = "Jean-Marc",
                LastName = "RAYNAUD",
                Password = "123",
                UserName = "jmr"
            });
            ubl.InsertUser(new User
            {
                FirstName = "Jean-François",
                LastName = "SICARD",
                Password = "123",
                UserName = "jfc"
            });
            ubl.InsertUser(new User
            {
                FirstName = "Régis",
                LastName = "VANDENBURIE",
                Password = "123",
                UserName = "regis"
            });

            CenterBusinessLayer cbl = new CenterBusinessLayer();
            cbl.InsertCenter(new Center
            {
                Name = "Biscarrosse"
            });

            cbl.InsertCenter(new Center
            {
                Name = "Grenoble"
            });

            cbl.InsertCenter(new Center
            {
                Name = "Muret"
            });

            cbl.InsertCenter(new Center
            {
                Name = "Carcassonne"
            });

            cbl.InsertCenter(new Center
            {
                Name = "Castelnaudary"
            });

            cbl.InsertCenter(new Center
            {
                Name = "Montpellier"
            });

            BusinessLayer<Action> abl = new BusinessLayer<Action>(new CapaNavDocDal());
            abl.Insert(new Action
            {
                Description = "Réparation"
            });
            abl.Insert(new Action
            {
                Description = "Inspection"
            });
            abl.Insert(new Action
            {
                Description = "Révision Générale"
            });
            abl.Insert(new Action
            {
                Description = "Réglage"
            });

            BusinessLayer<Equipment> ebl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            ebl.Insert(new Equipment
            {
                ActivityField = "Moteurs à pistons",
                Ata = 72,
                MechanicsGroup = "moteurs pistons",
                Name = "Adaptateur Magnéto",
                Manufacturer = "TCM",
                PartNumber = "632653A1",
                DocumentsReferences = "M-16",
            });
            
            return RedirectToAction("Index");
        }
    }
}