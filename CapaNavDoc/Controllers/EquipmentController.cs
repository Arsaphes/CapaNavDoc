using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Extensions.ViewModels;
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
        /// <param name="editionMode">The edition mode.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult EditEquipment(EquipmentEditionViewModel model, string editionMode)
        {
            BusinessLayer<Equipment> bl;

            switch (editionMode)
            {
                case "Ajouter":
                    bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
                    bl.Insert(model.ToEquipment());
                    return RedirectToAction("Index");

                case "Changer":
                    bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
                    bl.Update(model.ToEquipment());
                    return RedirectToAction("Index");

                default:  // Annuler
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Delete an Equipment.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        /// <param name="dialogResult">The confirmation result.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult DeleteEquipment(ConfirmationViewModel model, string dialogResult)
        {
            if (dialogResult == "Non") return RedirectToAction("Index");

            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            bl.Delete(model.Id.ToInt32());
            return RedirectToAction("Index");
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
        /// <param name="equipmentId">The id of the Equipment to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetEquipmentUpdateView(string equipmentId)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            Equipment equipment = bl.Get(equipmentId.ToInt32());
            EquipmentEditionViewModel model = equipment.ToEquipmentEditionViewModel("Changer");

            return PartialView("EquipmentEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm an Equipment deletation.
        /// </summary>
        /// <param name="equipmentId">The id of the Equipment to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string equipmentId)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            Equipment equipment = bl.Get(equipmentId.ToInt32());
            string equipementCall = $"{equipment.Name} (PN: {equipment.PartNumber})";
            ConfirmationViewModel model = new ConfirmationViewModel { ConfirmationMessage = $"Supprimer l'équipement {equipementCall} ?", Id = equipmentId, Controler = "Equipment", Action = "DeleteEquipment" };

            return PartialView("ConfirmationView", model);
        }

        /// <summary>
        /// Get a partial view used to link some centers to the Equipment.
        /// </summary>
        /// <param name="equipmentId">The Equipment id.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetEquipmentCenters(string equipmentId)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            Equipment equipment = bl.Get(equipmentId.ToInt32());
            List<EquipmentCenterViewModel> centers = new List<EquipmentCenterViewModel>();

            centers.AddRange(equipment.GetEquipmentCenterList().Select(c => c.ToEquipmentCenterViewModel()));
            EquipmentCenterListViewModel model = new EquipmentCenterListViewModel { EquipmentId = equipmentId, EquipmentCenters = centers};

            return PartialView("EquipmentCentersView", model);
        }
    }
}