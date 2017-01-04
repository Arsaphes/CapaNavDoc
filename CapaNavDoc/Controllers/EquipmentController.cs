using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Extensions.ViewModels;
using CapaNavDoc.Interfaces;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Controllers
{
    public class EquipmentController : Controller
    {
        /// <summary>
        /// Get the default view displaying the Equipment grid view.
        /// </summary>
        /// <returns>A view.</returns>
        public ActionResult Index()
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            List<Equipment> equipments = bl.GetList();
            List<EquipmentDetailsViewModel> equipmentDetails = equipments.Select(equipment => equipment.ToEquipmentDetailsViewModel()).ToList();
            EquipmentListViewModel model = new EquipmentListViewModel { EquipmentsDetails = equipmentDetails };
            return View("Index", model);
        }

        /// <summary>
        /// Insert or update an Equipment.
        /// </summary>
        /// <param name="model">The EquipmentEditionViewModel used to edit an Equipment.</param>
        [HttpPost]
        public void EditEquipment(EquipmentEditionViewModel model)
        {
            BusinessLayer<Equipment> bl;

            switch (model.EditionMode)
            {
                case "Ajouter":
                    bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
                    bl.Insert(model.ToEquipment());
                    break;

                case "Changer":
                    bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
                    bl.Update(model.ToEquipment());
                    break;
            }
        }

        /// <summary>
        /// Delete an Equipment.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        [HttpPost]
        public void DeleteEquipment(ConfirmationViewModel model)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            bl.Delete(model.Id.ToInt32());
        }


        /// <summary>
        /// Get a partial view used to insert an Equipment.
        /// </summary>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetEquipmentInsertView()
        {
            EquipmentEditionViewModel model = new EquipmentEditionViewModel { EditionMode = "Ajouter" };
            return PartialView("EquipmentEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to update an Equipment.
        /// </summary>
        /// <param name="id">The id of the Equipment to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetEquipmentUpdateView(string id)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            Equipment equipment = bl.Get(id.ToInt32());
            EquipmentEditionViewModel model = equipment.ToEquipmentEditionViewModel("Changer");

            return PartialView("EquipmentEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm an Equipment deletation.
        /// </summary>
        /// <param name="id">The id of the Equipment to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            Equipment equipment = bl.Get(id.ToInt32());
            string equipementCall = $"{equipment.Name} (PN: {equipment.PartNumber})";
            ConfirmationViewModel model = new ConfirmationViewModel { ConfirmationMessage = $"Supprimer l'équipement {equipementCall} ?", Id = id, Controler = "Equipment", Action = "DeleteEquipment" };

            return PartialView("ConfirmationView", model);
        }
        

        /// <summary>
        /// Get the datas used to display the Equipment data table.
        /// </summary>
        /// <param name="param">The data table common properties.</param>
        /// <returns>A serialized set of data for the data table.</returns>
        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            List<EquipmentDetailsViewModel> model = new List<EquipmentDetailsViewModel>(bl.GetList().Select(e => e.ToEquipmentDetailsViewModel()));

            model = TableDataAdapter.Search(model, param);
            model = TableDataAdapter.SortList(model, param);
            model = TableDataAdapter.PageList(model, param);

            string[][] data = model.Select(m => new[] { m.Id.ToString(), m.PartNumber, m.Manufacturer, m.Name, m.Type, m.Ata.ToString(), m.ActivityField, m.MechanicsGroup, m.DocumentsReferences, m.DocumentsPartNumber, m.MonitoringDate }).ToArray();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = model.Count,
                iTotalDisplayRecords = param.iDisplayLength,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get a partial view used to update the Equipment monitoring.
        /// </summary>
        /// <param name="id">The Equipment id.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetEquipmentMonitoringView(string id)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            Equipment equipment = bl.Get(id.ToInt32());
            EquipmentMonitoringViewModel model = equipment.ToEquipmentMonitoringViewModel();

            return PartialView("EquipmentMonitoringView", model);
        }

        /// <summary>
        /// Update the Equipment monitoring status.
        /// </summary>
        /// <param name="model">The EquipmentMonitoringViewModel.</param>
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


        /// <summary>
        /// Get a partial view used to link some centers to the Equipment.
        /// </summary>
        /// <param name="id">The Equipment id.</param>
        /// <returns>A partial view.</returns>
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

        /// <summary>
        /// Update the Center/Action group linked to the Equipment.
        /// </summary>
        /// <param name="model">The EquipmentCenterViewModel.</param>
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