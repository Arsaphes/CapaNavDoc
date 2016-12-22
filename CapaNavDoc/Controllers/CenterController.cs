using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Extensions;
using CapaNavDoc.Extensions.ViewModels;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Controllers
{
    public class CenterController : Controller
    {
        /// <summary>
        /// Get the default view displaying the Centers grid view.
        /// </summary>
        /// <returns>A view.</returns>
        public ActionResult Index()
        {
            CenterBusinessLayer bl = new CenterBusinessLayer();
            List<Center> centers = bl.GetCenters();
            List<CenterDetailsViewModel> centerDetails = centers.Select(center => center.ToCenterDetailsViewModel()).ToList();
            CenterListViewModel model = new CenterListViewModel { CentersDetails = centerDetails };
            return View("Index", model);
        }

        /// <summary>
        /// Insert or update a Center.
        /// </summary>
        /// <param name="model">The CenterEditionViewModel used to edit a Center.</param>
        /// <param name="editionMode">The edition mode.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult EditCenter(CenterEditionViewModel model, string editionMode)
        {
            CenterBusinessLayer bl;

            switch (editionMode)
            {
                case "Ajouter":
                    bl = new CenterBusinessLayer();
                    bl.InsertCenter(model.ToCenter());
                    return RedirectToAction("Index");

                case "Changer":
                    bl = new CenterBusinessLayer();
                    bl.UpdateCenter(model.ToCenter());
                    return RedirectToAction("Index");

                default:  // Annuler
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Delete a Center.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        /// <param name="dialogResult">The confirmation result.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult DeleteCenter(ConfirmationViewModel model, string dialogResult)
        {
            if (dialogResult == "Non") return RedirectToAction("Index");

            CenterBusinessLayer bl = new CenterBusinessLayer();
            bl.DeleteCenter(model.Id.ToInt32());
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Get a partial view used to insert a Center.
        /// </summary>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetCenterInsertView()
        {
            CenterEditionViewModel model = new CenterEditionViewModel { EditionMode = "Ajouter" };
            return PartialView("CenterEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to update a Center.
        /// </summary>
        /// <param name="centerId">The id of the Center to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetCenterUpdateView(string centerId)
        {
            CenterBusinessLayer bl = new CenterBusinessLayer();
            Center center = bl.GetCenter(centerId.ToInt32());
            CenterEditionViewModel model = center.ToCenterEditionViewModel("Changer");

            return PartialView("CenterEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm a Center deletation.
        /// </summary>
        /// <param name="CenterId">The id of the Center to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string CenterId)
        {
            CenterBusinessLayer bl = new CenterBusinessLayer();
            Center center = bl.GetCenter(CenterId.ToInt32());
            string userCall = $"{center.Name}";
            ConfirmationViewModel model = new ConfirmationViewModel { ConfirmationMessage = $"Supprimer l'utilisateur {userCall} ?", Id = CenterId, Controler = "Center", Action = "DeleteCenter" };

            return PartialView("ConfirmationView", model);
        }
    }
}