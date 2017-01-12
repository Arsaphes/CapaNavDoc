using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;
using CapaNavDoc.ViewModel.ActivityField;

namespace CapaNavDoc.Controllers
{
    public class ActivityFieldController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditActivityField(ActivityFieldEditionViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("ActivityFieldEditionView", model);
            new DefaultController<ActivityField>().Edit(model);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteActivityField(ConfirmationViewModel model)
        {
            new DefaultController<ActivityField>().Delete(model);
            return Json(new { success = true });
        }


        [HttpGet]
        public PartialViewResult GetActivityFieldInsertView()
        {
            return PartialView("ActivityFieldEditionView", new ActivityFieldEditionViewModel { EditionMode = EditionMode.Insert });
        }

        [HttpGet]
        public PartialViewResult GetActivityFieldUpdateView(string id)
        {
            return PartialView("ActivityFieldEditionView", new BusinessLayer<ActivityField>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel(new ActivityFieldEditionViewModel(), EditionMode.Update));
        }

        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            ActivityField t = new BusinessLayer<ActivityField>(new CapaNavDocDal()).Get(id.ToInt32());
            return PartialView("ConfirmationView", new ConfirmationViewModel
            {
                Id = id,
                ConfirmationMessage = $"Supprimer le domaine d'activité {t.Description} ?",
                Controler = "ActivityField",
                Action = "DeleteActivityField"
            });
        }


        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<ActivityField> bl = new BusinessLayer<ActivityField>(new CapaNavDocDal());
            List<ActivityFieldDetailsViewModel> model = new List<ActivityFieldDetailsViewModel>(bl.GetList().Select(a => (ActivityFieldDetailsViewModel)a.ToModel(new ActivityFieldDetailsViewModel())));

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