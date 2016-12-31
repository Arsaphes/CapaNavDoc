using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Classes;
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
        [HttpPost]
        public void EditAction(ActionEditionViewModel model)
        {
            BusinessLayer<Action> bl;

            switch (model.EditionMode)
            {
                case "Ajouter":
                    bl = new BusinessLayer<Action>(new CapaNavDocDal());
                    bl.Insert(model.ToAction());
                    break;

                case "Changer":
                    bl = new BusinessLayer<Action>(new CapaNavDocDal());
                    bl.Update(model.ToAction());
                    break;
            }
        }

        /// <summary>
        /// Delete an Action.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        [HttpPost]
        public void DeleteAction(ConfirmationViewModel model)
        {
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            bl.Delete(model.Id.ToInt32());
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
        /// <param name="id">The id of the Action to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetActionUpdateView(string id)
        {
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            Action action = bl.Get(id.ToInt32());
            ActionEditionViewModel model = action.ToActionEditionViewModel("Changer");

            return PartialView("ActionEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm an Action deletation.
        /// </summary>
        /// <param name="id">The id of the Action to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            Action action = bl.Get(id.ToInt32());
            string call = $"{action.Description}";
            ConfirmationViewModel model = new ConfirmationViewModel { ConfirmationMessage = $"Supprimer l'action {call} ?", Id = id, Controler = "Action", Action = "DeleteAction" };

            return PartialView("ConfirmationView", model);
        }

        /// <summary>
        /// Get the datas used to display the Action data table.
        /// </summary>
        /// <param name="param">The data table common properties.</param>
        /// <returns>A serialized set of data for the data table.</returns>
        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            List<Action> actions = TableDataAdapter.SearchInActions(param.sSearch).Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            string[][] data = actions.Select(a => new[] { a.Id.ToString(), a.Description }).ToArray();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = actions.Count,
                iTotalDisplayRecords = param.iDisplayLength,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }
    }
}