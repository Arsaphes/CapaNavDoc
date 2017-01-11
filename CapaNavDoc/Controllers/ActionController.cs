using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;
using CapaNavDoc.ViewModel.Action;

namespace CapaNavDoc.Controllers
{
    public class ActionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return new DefaultController<Action>().Index<ActionDetailsViewModel, ActionListViewModel>();
        }

        [HttpPost]
        public ActionResult EditAction(ActionEditionViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("ActionEditionView", model);
            new DefaultController<Action>().Edit(model);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteAction(ConfirmationViewModel model)
        {
            new DefaultController<Action>().Delete(model);
            return Json(new { success = true });
        }


        [HttpGet]
        public PartialViewResult GetActionInsertView()
        {
            return PartialView("ActionEditionView", new ActionEditionViewModel { EditionMode = EditionMode.Insert });
        }

        [HttpGet]
        public PartialViewResult GetActionUpdateView(string id)
        {
            return PartialView("ActionEditionView", new BusinessLayer<Action>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel(new ActionEditionViewModel(), EditionMode.Update));
        }

        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            Action t = new BusinessLayer<Action>(new CapaNavDocDal()).Get(id.ToInt32());
            return PartialView("ConfirmationView", new ConfirmationViewModel
            {
                Id = id,
                ConfirmationMessage = $"Supprimer l'action {t.Description} ?",
                Controler = "Action",
                Action = "DeleteAction"
            });
        }


        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            List<ActionDetailsViewModel> model = new List<ActionDetailsViewModel>(bl.GetList().Select(a => (ActionDetailsViewModel)a.ToModel(new ActionDetailsViewModel())));

            model = TableDataAdapter.Search(model, param);
            model = TableDataAdapter.SortList(model, param);
            model = TableDataAdapter.PageList(model, param);

            string[][] data = model.Select(m => new[] { m.Id.ToString(), m.Description }).ToArray();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = model.Count,
                iTotalDisplayRecords = param.iDisplayLength,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }
    }
}