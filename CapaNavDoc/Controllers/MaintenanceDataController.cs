using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Extensions.ViewModels;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;
using CapaNavDoc.ViewModel.MaintenanceData;
using CapaNavDoc.ViewModel.User;

namespace CapaNavDoc.Controllers
{
    public class MaintenanceDataController : Controller
    {
        [HttpGet]
        public ActionResult Index(string name)
        {
            return View("Index", new MaintenanceDataIndexViewModel {MaintenanceDataName = name});
        }

        [HttpPost]
        public ActionResult EditMaintenanceData(MaintenanceDataEditionViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("MaintenanceDataEditionView", model);
            MaintenanceData md = new DefaultController<MaintenanceData>().Edit(model);
            if (model.FileUpload == null) return Json(new { success = true }); ;

            byte[] buffer = new byte[32*1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = model.FileUpload.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                MaintenanceData m = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).Get(md.Id);
                m.Document = ms.ToArray();
                new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).Update(m);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteMaintenanceData(ConfirmationViewModel model)
        {
            new DefaultController<MaintenanceData>().Delete(model);
            return Json(new { success = true });
        }


        [HttpGet]
        public PartialViewResult GetMaintenanceDataInsertView()
        {
            return PartialView("MaintenanceDataEditionView", new MaintenanceDataEditionViewModel {EditionMode = EditionMode.Insert });
        }

        [HttpGet]
        public PartialViewResult GetMaintenanceDataUpdateView(string id)
        {
            return PartialView("MaintenanceDataEditionView", new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel(new MaintenanceDataEditionViewModel(), EditionMode.Update));
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
            List<MaintenanceDataDetailsViewModel> model = new List<MaintenanceDataDetailsViewModel>(bl.GetList().Select(d => (MaintenanceDataDetailsViewModel)d.ToModel(new MaintenanceDataDetailsViewModel())));
            
            model = TableDataAdapter.Search(model, param);
            model = TableDataAdapter.SortList(model, param);
            model = TableDataAdapter.PageList(model, param);

            string[][] data = model.Select(m => new[] {m.Id, m.Type, m.Sender, m.DocumentReference, m.DocumentPartNumber, m.Review, m.Date, m.Name, m.OnCertificate, m.MonitoringDate, bl.Get(m.Id.ToInt32()).Document == null ? null : ""}).ToArray();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = model.Count,
                iTotalDisplayRecords = param.iDisplayLength,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetPdfDocument(string id)
        {
            byte[] byteArray = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).Get(id.ToInt32()).Document;
            if (byteArray == null || byteArray.Length == 0) return null;
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }


        [HttpGet]
        public PartialViewResult GetEquipmentMonitoringView(string id)
        {
            MaintenanceData md = new BusinessLayer<MaintenanceData>(new CapaNavDocDal()).Get(id.ToInt32());
            MaintenanceDataMonitoringViewModel model = md.ToMaintenanceDataMonitoringViewModel();

            return PartialView("MaintenanceDataMonitoringView", model);
        }

        // Todo: Add return value.
        [HttpPost]
        public void UpdateMaintenanceDataMonitoring(MaintenanceDataMonitoringViewModel model)
        {
            BusinessLayer<MaintenanceData> mdbl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            BusinessLayer<User> ubl = new BusinessLayer<User>(new CapaNavDocDal());
            MaintenanceData md = mdbl.Get(model.MaintenanceDataId.ToInt32());
            UserCallViewModel userCallViewModel = ubl.GetList().Select(u => u.ToUserCallViewModel()).FirstOrDefault(u => u.UserCall == model.SelectedUserCall);
            if (userCallViewModel == null) return;

            md.MonitoringUserId = userCallViewModel.UserId.ToInt32();
            md.MonitoringDate = DateTime.ParseExact(model.Date, "dd-mm-yyyy", CultureInfo.InvariantCulture);
            mdbl.Update(md);
        }
    }
}