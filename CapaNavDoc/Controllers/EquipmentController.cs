using System;
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

namespace CapaNavDoc.Controllers
{
    public class EquipmentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //return new DefaultController<Equipment>().Index<EquipmentDetailsViewModel, EquipmentListViewModel>();
            return View("Index");
        }

        [HttpPost]
        public void EditEquipment(EquipmentEditionViewModel model)
        {
            MaintenanceData md = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).GetList().FirstOrDefault(m => m.Name == model.MaintenanceDataId);
            if (md != null) model.MaintenanceDataId = md.Id.ToString();
            new DefaultController<Equipment>().Edit(model);
        }

        [HttpPost]
        public void DeleteEquipment(ConfirmationViewModel model)
        {
            new DefaultController<Equipment>().Delete(model);
        }


        [HttpGet]
        public PartialViewResult GetEquipmentInsertView()
        {
            return PartialView("EquipmentEditionView", new EquipmentEditionViewModel {EditionMode = "Ajouter", DocumentsReferences = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).GetList().Select(md => md.Name).ToList()});
        }

        [HttpGet]
        public PartialViewResult GetEquipmentUpdateView(string id)
        {
            // Todo: Change the way the edition mode is set. Should be by class property, not parameter.
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
            Logger log =  new Logger();
            log.Debug($"[ GetEquipmentCenters({id}) ]");
            
            BusinessLayer<Equipment> ebl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            log.Debug("Equipment Business Layer created.");

            Equipment equipment = ebl.Get(id.ToInt32());
            log.Debug($"Equipment with ID={id} grabbed. Name={equipment.Name}");

            EquipmentCenterViewModel model = equipment.ToEquipmentCenterViewModel();
            log.Debug("EquipmentViewCenterModel created.");

            return PartialView("EquipmentCentersView", model);
        }

        [HttpPost]
        public void UpdateEquipmentCenters(EquipmentCenterViewModel model)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            Equipment equipment = bl.Get(model.EquipmentId.ToInt32());
            equipment.SetCenterActionGroups(model);
            bl.Update(equipment);
        }
    }
}