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
        [HttpGet]
        public ActionResult Index()
        {
            return new DefaultController<Action>().Index<ActionDetailsViewModel, ActionListViewModel>();
        }

        [HttpPost]
        public void EditAction(ActionEditionViewModel model)
        {
            new DefaultController<Action>().Edit(model);
        }

        [HttpPost]
        public void DeleteAction(ConfirmationViewModel model)
        {
            new DefaultController<Action>().Delete(model);
        }


        [HttpGet]
        public PartialViewResult GetActionInsertView()
        {
            return PartialView("ActionEditionView", new ActionEditionViewModel { EditionMode = "Ajouter" });
        }

        [HttpGet]
        public PartialViewResult GetActionUpdateView(string id)
        {
            ActionEditionViewModel model = new BusinessLayer<Action>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel<ActionEditionViewModel>("Changer");

            return PartialView("ActionEditionView", model);
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
            List<ActionDetailsViewModel> model = new List<ActionDetailsViewModel>(bl.GetList().Select(a => a.ToModel<ActionDetailsViewModel>()));

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