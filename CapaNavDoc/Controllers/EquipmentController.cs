﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Extensions.ViewModels;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;
using CapaNavDoc.ViewModel.Equipment;
using CapaNavDoc.ViewModel.User;
using Action = CapaNavDoc.Models.Action;

namespace CapaNavDoc.Controllers
{
    public class EquipmentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditEquipment(EquipmentEditionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DocumentsReferences = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).GetList().Select(m => m.Name).ToList();
                return PartialView("EquipmentEditionView", model);
            }
            MaintenanceData md = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).GetList().FirstOrDefault(m => m.Name == model.MaintenanceDataId);
            if (md != null) model.MaintenanceDataId = md.Id.ToString();
            new DefaultController<Equipment>().Edit(model);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteEquipment(ConfirmationViewModel model)
        {
            new DefaultController<Equipment>().Delete(model);
            return Json(new { success = true });
        }


        [HttpGet]
        public PartialViewResult GetEquipmentInsertView()
        {
            return PartialView("EquipmentEditionView", new EquipmentEditionViewModel
            {
                EditionMode = EditionMode.Insert,
                DocumentsReferences = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).GetList().Select(md => md.Name).ToList()
            });
        }

        [HttpGet]
        public PartialViewResult GetEquipmentUpdateView(string id)
        {
            return PartialView("EquipmentEditionView", new BusinessLayer<Equipment>(new CapaNavDocDal()).Get(id.ToInt32()).ToEquipmentEditionViewModel());
        }

        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            Equipment t = new BusinessLayer<Equipment>(new CapaNavDocDal()).Get(id.ToInt32());
            return PartialView("ConfirmationView", new ConfirmationViewModel
            {
                Id = id,
                ConfirmationMessage = $"Supprimer l'équipement {t.PartNumber} ?",
                Controler = "Equipment",
                Action = "DeleteEquipment"
            });
        }


        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            BusinessLayer<MaintenanceData> mbl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            List<EquipmentDetailsViewModel> model = new List<EquipmentDetailsViewModel>(bl.GetList().Select(e => (EquipmentDetailsViewModel)e.ToModel(new EquipmentDetailsViewModel())));

            model = TableDataAdapter.Search(model, param);
            model = TableDataAdapter.SortList(model, param);
            model = TableDataAdapter.PageList(model, param);
            
            string[][] data = model.Select(m => new[] {m.Id.ToString(), m.PartNumber, m.Manufacturer, m.Name, m.Type,
                m.Ata.ToString(), m.ActivityField, m.MechanicsGroup,
                m.MaintenanceDataId.ToInt32() == 0 ? "" : mbl.Get(m.MaintenanceDataId.ToInt32()).Name,
                m.MonitoringDate, m.MaintenanceDataId  }).ToArray();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = model.Count,
                iTotalDisplayRecords = param.iDisplayLength,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public PartialViewResult GetEquipmentMonitoringView(string id)
        {
            Equipment equipment = new BusinessLayer<Equipment>(new CapaNavDocDal()).Get(id.ToInt32());
            EquipmentMonitoringViewModel model = equipment.ToEquipmentMonitoringViewModel();

            return PartialView("EquipmentMonitoringView", model);
        }

        [HttpPost]
        public void UpdateEquipmentMonitoring(EquipmentMonitoringViewModel model)
        {
            BusinessLayer<Equipment> ebl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            BusinessLayer<User> ubl = new BusinessLayer<User>(new CapaNavDocDal());
            Equipment equipment = ebl.Get(model.EquipmentId.ToInt32());
            UserCallViewModel userCallViewModel = ubl.GetList().Select(u => u.ToUserCallViewModel()).FirstOrDefault(u => u.UserCall == model.SelectedUserCall);
            if (userCallViewModel == null) return;

            equipment.MonitoringUserId = userCallViewModel.UserId.ToInt32();
            equipment.MonitoringDate = DateTime.ParseExact(model.Date, "dd-mm-yyyy", CultureInfo.InvariantCulture);
            ebl.Update(equipment);
        }


        [HttpGet]
        public PartialViewResult GetEquipmentCenters(string id)
        {
            List<Center> centers = new BusinessLayer<Center>(new CapaNavDocDal()).GetList();
            List<Action> actions = new BusinessLayer<Action>(new CapaNavDocDal()).GetList();
            string[] couples = new BusinessLayer<Equipment>(new CapaNavDocDal()).Get(id.ToInt32()).EquipmentCenterActionList?.Split(';') ?? new string[0];
            EquipmentCenterActionViewModel model = new EquipmentCenterActionViewModel
            {
                EquipmentId = id,
                ActionNames = actions.Select(a => a.Description).ToArray(),
                CenterNames = centers.Select(c => c.Name).ToArray(),
                Selections = ArrayHelper.GetInitialized(centers.Count, actions.Count, (i, j) => couples.Contains($"{centers[i].Id},{actions[j].Id}"))
            };
            return PartialView("EquipmentCentersView", model);
        }

        [HttpPost]
        public void UpdateEquipmentCenters(EquipmentCenterActionViewModel model)
        {
            List<Center> centers = new BusinessLayer<Center>(new CapaNavDocDal()).GetList();
            List<Action> actions = new BusinessLayer<Action>(new CapaNavDocDal()).GetList();
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            Equipment equipment = bl.Get(model.EquipmentId.ToInt32());
            equipment.EquipmentCenterActionList = null;

            for (int i = 0; i < centers.Count; i++)
                equipment.EquipmentCenterActionList = actions.Where((t, j) => model.Selections[i][j]).Aggregate(equipment.EquipmentCenterActionList, (current, t) => current.AddIdCouple(centers[i].Id, t.Id));
            bl.Update(equipment);
        }
    }
}