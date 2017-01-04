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
    public class MaintenanceDataController : Controller
    {
        public ActionResult Index()
        {
            return new DefaultController<MaintenanceData>().Index<MaintenanceDataDetailsViewModel, MaintenanceDataListViewModel>();
        }

        [HttpPost]
        public void EditMaintenanceData(MaintenanceDataEditionViewModel model)
        {
            new DefaultController<MaintenanceData>().Edit(model);
        }

        [HttpPost]
        public void DeleteMaintenanceData(ConfirmationViewModel model)
        {
            new DefaultController<MaintenanceData>().Delete(model);
        }


        [HttpGet]
        public PartialViewResult GetMaintenanceDataInsertView()
        {
            return PartialView("MaintenanceDataEditionView", new MaintenanceDataEditionViewModel { EditionMode = "Ajouter" });
        }

        [HttpGet]
        public PartialViewResult GetMaintenanceDataUpdateView(string id)
        {
            return PartialView("MaintenanceDataEditionView", new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel<MaintenanceDataEditionViewModel>("Changer"));
        }

        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            MaintenanceData t = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).Get(id.ToInt32());
            return PartialView("ConfirmationView", new ConfirmationViewModel
            {
                Id = id,
                ConfirmationMessage = $"Supprimer les données d'entretien {t.Name} ?",
                Controler = "MaintenanceData",
                Action = "DeleteMaintenanceData"
            });
        }


        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<MaintenanceData> bl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            List<MaintenanceDataDetailsViewModel> model = new List<MaintenanceDataDetailsViewModel>(bl.GetList().Select(d => d.ToModel<MaintenanceDataDetailsViewModel>()));

            model = TableDataAdapter.Search(model, param);
            model = TableDataAdapter.SortList(model, param);
            model = TableDataAdapter.PageList(model, param);

            string[][] data = model.Select(m => new[] { m.Id.ToString(), m.Type, m.Sender, m.Review, m.Date, m.Name, m.OnCertificate, m.DocumentLink }).ToArray();
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