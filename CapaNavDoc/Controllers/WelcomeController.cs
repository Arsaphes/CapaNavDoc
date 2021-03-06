﻿using System.Data.SqlTypes;
using System.Web.Mvc;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using Action = CapaNavDoc.Models.Action;

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
                Ata = 72,
                MechanicsGroup = "moteurs pistons",
                Name = "Adaptateur Magnéto",
                Manufacturer = "TCM",
                PartNumber = "632653A1",
                MonitoringDate = SqlDateTime.MinValue.Value
            });
            ebl.Insert(new Equipment
            {
                Ata = 74,
                MechanicsGroup = "Magnétos Unison",
                Name = "Magnétos Unison",
                Manufacturer = "Unison (ex Slick)",
                PartNumber = "4270",
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

            BusinessLayer<Center> cbl = new BusinessLayer<Center>(new CapaNavDocDal());
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

            BusinessLayer<ActivityField> afbl = new BusinessLayer<ActivityField>(new CapaNavDocDal());
            afbl.Insert(new ActivityField
            {
                Description = "Moteur"
            });
            afbl.Insert(new ActivityField
            {
                Description = "Cellule"
            });
            afbl.Insert(new ActivityField
            {
                Description = "Radio"
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public void GeneratePdfTest()
        {
        }

    }
}