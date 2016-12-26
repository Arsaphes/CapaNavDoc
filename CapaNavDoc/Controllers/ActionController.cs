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
    public class ActionController : Controller
    {
        /// <summary>
        /// Get the default view displaying the actions grid view.
        /// </summary>
        /// <returns>A view.</returns>
        public ActionResult Index()
        {
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            List<Action> actions = bl.GetList();
            List<ActionDetailsViewModel> actionDetails = actions.Select(action => action.ToActionDetailsViewModel()).ToList();
            ActionListViewModel model = new ActionListViewModel {ActionsDetails = actionDetails};
            return View("Index", model);
        }

        /// <summary>
        /// Insert or update an Action.
        /// </summary>
        /// <param name="model">The ActionEditionViewModel used to edit an Action.</param>
        /// <param name="editionMode">The edition mode.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult EditAction(ActionEditionViewModel model, string editionMode)
        {
            BusinessLayer<Action> bl;

            switch (editionMode)
            {
                case "Ajouter":
                    bl = new BusinessLayer<Action>(new CapaNavDocDal());
                    bl.Insert(model.ToAction());
                    return RedirectToAction("Index");

                case "Changer":
                    bl = new BusinessLayer<Action>(new CapaNavDocDal());
                    bl.Update(model.ToAction());
                    return RedirectToAction("Index");

                default:  // Annuler
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Delete an Action.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        /// <param name="dialogResult">The confirmation result.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult DeleteAction(ConfirmationViewModel model, string dialogResult)
        {
            if (dialogResult == "Non") return RedirectToAction("Index");

            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            bl.Delete(model.Id.ToInt32());
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Get a partial view used to insert an Action.
        /// </summary>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetActionInsertView()
        {
            ActionEditionViewModel model = new ActionEditionViewModel { EditionMode = "Ajouter" };
            return PartialView("ActionEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to update an Action.
        /// </summary>
        /// <param name="actionId">The id of the Action to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetActionUpdateView(string actionId)
        {
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            Action action = bl.Get(actionId.ToInt32());
            ActionEditionViewModel model = action.ToActionEditionViewModel("Changer");

            return PartialView("ActionEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm an Action deletation.
        /// </summary>
        /// <param name="actionId">The id of the Action to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string actionId)
        {
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            Action action = bl.Get(actionId.ToInt32());
            string userCall = $"{action.Description}";
            ConfirmationViewModel model = new ConfirmationViewModel { ConfirmationMessage = $"Supprimer l'utilisateur {userCall} ?", Id = actionId, Controler = "Action", Action = "DeleteAction" };

            return PartialView("ConfirmationView", model);
        }
    }
}