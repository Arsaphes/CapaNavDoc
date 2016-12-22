using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Extensions;
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
            ActionBusinessLayer bl = new ActionBusinessLayer();
            List<Action> actions = bl.GetActions();
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
            ActionBusinessLayer bl;

            switch (editionMode)
            {
                case "Ajouter":
                    bl = new ActionBusinessLayer();
                    bl.InsertAction(model.ToAction());
                    return RedirectToAction("Index");

                case "Changer":
                    bl = new ActionBusinessLayer();
                    bl.UpdateAction(model.ToAction());
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

            ActionBusinessLayer bl = new ActionBusinessLayer();
            bl.DeleteAction(model.Id.ToInt32());
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
            ActionBusinessLayer ub = new ActionBusinessLayer();
            Action action = ub.GetAction(actionId.ToInt32());
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
            ActionBusinessLayer bl = new ActionBusinessLayer();
            Action action = bl.GetAction(actionId.ToInt32());
            string userCall = $"{action.Description}";
            ConfirmationViewModel model = new ConfirmationViewModel { ConfirmationMessage = $"Supprimer l'utilisateur {userCall} ?", Id = actionId, Controler = "Action", Action = "DeleteAction" };

            return PartialView("ConfirmationView", model);
        }
    }
}