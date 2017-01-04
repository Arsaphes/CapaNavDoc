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
        /// <summary>
        /// Get the default view displaying the MaintenanceData grid view.
        /// </summary>
        /// <returns>A view.</returns>
        public ActionResult Index()
        {
            BusinessLayer<MaintenanceData> bl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            List<MaintenanceData> mDatas = bl.GetList();
            List<MaintenanceDataDetailsViewModel> mDataDetails = mDatas.Select(d => d.ToMaintenanceDataDetailsViewModel()).ToList();
            MaintenanceDataListViewModel model = new MaintenanceDataListViewModel {MaintenanceDataDetails = mDataDetails};
            return View("Index", model);
        }

        /// <summary>
        /// Insert or update a MaintenanceData.
        /// </summary>
        /// <param name="model">The MaintenanceDataEditionViewModel used to edit a MaintenanceData.</param>
        [HttpPost]
        public void EditMaintenanceData(MaintenanceDataEditionViewModel model)
        {
            BusinessLayer<MaintenanceData> bl;

            switch (model.EditionMode)
            {
                case "Ajouter":
                    bl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
                    bl.Insert(model.ToMaintenanceData());
                    break;

                case "Changer":
                    bl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
                    bl.Update(model.ToMaintenanceData());
                    break;
            }
        }

        /// <summary>
        /// Delete a MaintenanceData.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        [HttpPost]
        public void DeleteMaintenanceData(ConfirmationViewModel model)
        {
            BusinessLayer<MaintenanceData> bl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            bl.Delete(model.Id.ToInt32());
        }


        /// <summary>
        /// Get a partial view used to insert a MaintenanceData.
        /// </summary>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetMaintenanceDataInsertView()
        {
            MaintenanceDataEditionViewModel model = new MaintenanceDataEditionViewModel { EditionMode = "Ajouter" };
            return PartialView("MaintenanceDataEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to update a MaintenanceData.
        /// </summary>
        /// <param name="id">The id of the MaintenanceData to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetMaintenanceDataUpdateView(string id)
        {
            BusinessLayer<MaintenanceData> ubl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            MaintenanceData md = ubl.Get(id.ToInt32());
            MaintenanceDataEditionViewModel model = md.ToMaintenanceDataEditionViewModel("Changer");

            return PartialView("MaintenanceDataEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm a MaintenanceData deletion.
        /// </summary>
        /// <param name="id">The id of the MaintenanceData to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            BusinessLayer<MaintenanceData> bl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            MaintenanceData md = bl.Get(id.ToInt32());
            string call = $"{md.Name}";
            ConfirmationViewModel model = new ConfirmationViewModel { ConfirmationMessage = $"Supprimer les données d'entretien {call} ?", Id = id, Controler = "MaintenanceData", Action = "DeleteMaintenanceData" };

            return PartialView("ConfirmationView", model);
        }


        /// <summary>
        /// Get the datas used to display the MaintenanceData data table.
        /// </summary>
        /// <param name="param">The data table common properties.</param>
        /// <returns>A serialized set of data for the data table.</returns>
        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<MaintenanceData> bl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            List<MaintenanceDataDetailsViewModel> model = new List<MaintenanceDataDetailsViewModel>(bl.GetList().Select(d => d.ToMaintenanceDataDetailsViewModel()));

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