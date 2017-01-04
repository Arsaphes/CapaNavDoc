using System.Data.SqlTypes;
using System.Web.Mvc;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Controllers
{
    public class WelcomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InitializeDatabase()
        {
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
                MonitoringDate = SqlDateTime.MinValue.Value
            });
            ebl.Insert(new Equipment
            {
                ActivityField = "(CO7) Moteur",
                Ata = 74,
                MechanicsGroup = "Magnétos Unison",
                Name = "Magnétos Unison",
                Manufacturer = "Unison (ex Slick)",
                PartNumber = "4270",
                DocumentsReferences = "Overhaul Manual?",
                DocumentsPartNumber = "OL L1037 RevH Séries 4200 - 6200",
                MonitoringDate = SqlDateTime.MinValue.Value
            });

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

            CenterBusinessLayer cbl = new CenterBusinessLayer(new CapaNavDocDal());
            cbl.Insert(new Center
            {
                Name = "Biscarrosse"
            });

            cbl.Insert(new Center
            {
                Name = "Grenoble"
            });

            cbl.Insert(new Center
            {
                Name = "Muret"
            });

            cbl.Insert(new Center
            {
                Name = "Carcassonne"
            });

            cbl.Insert(new Center
            {
                Name = "Castelnaudary"
            });

            cbl.Insert(new Center
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

            return RedirectToAction("Index");
        }
    }
}