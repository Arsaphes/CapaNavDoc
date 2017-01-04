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
    public class CenterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return new DefaultController<Center>().Index<CenterDetailsViewModel, CenterListViewModel>();
        }

        [HttpPost]
        public void EditCenter(CenterEditionViewModel model)
        {
            new DefaultController<Center>().Edit(model);
        }

        [HttpPost]
        public void DeleteCenter(ConfirmationViewModel model)
        {
            new DefaultController<Center>().Delete(model);
        }


        [HttpGet]
        public PartialViewResult GetCenterInsertView()
        {
            return PartialView("CenterEditionView", new CenterEditionViewModel { EditionMode = "Ajouter" });
        }

        [HttpGet]
        public PartialViewResult GetCenterUpdateView(string id)
        {
            return PartialView("CenterEditionView", new BusinessLayer<Center>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel<CenterEditionViewModel>("Changer"));
        }

        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            Center t = new BusinessLayer<Center>(new CapaNavDocDal()).Get(id.ToInt32());
            return PartialView("ConfirmationView", new ConfirmationViewModel
            {
                Id = id,
                ConfirmationMessage = $"Supprimer l'atelier {t.Name} ?",
                Controler = "Center",
                Action = "DeleteCenter"
            });
        }


        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<Center> bl = new BusinessLayer<Center>(new CapaNavDocDal());
            List<CenterDetailsViewModel> model = new List<CenterDetailsViewModel>(bl.GetList().Select(c => c.ToModel<CenterDetailsViewModel>()));

            model = TableDataAdapter.Search(model, param);
            model = TableDataAdapter.SortList(model, param);
            model = TableDataAdapter.PageList(model, param);

            string[][] data = model.Select(m => new[] {m.Id.ToString(), null, m.Name }).ToArray();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = model.Count,
                iTotalDisplayRecords = param.iDisplayLength,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public PartialViewResult GetCenterUsersView(string id)
        {
            BusinessLayer<User> ubl = new BusinessLayer<User>(new CapaNavDocDal());
            CenterBusinessLayer cbl = new CenterBusinessLayer(new CapaNavDocDal());
            List<User> centerUsers = cbl.GetCenterUsers(id.ToInt32());
            List<User> users = ubl.GetList();

            List<CenterUserViewModel> centerUsersVm = users.Select(u =>
                centerUsers.Contains(u) ?
                    new CenterUserViewModel {Id = u.Id.ToString(), FirstName = u.FirstName, LastName = u.LastName, Selected = true} :
                    new CenterUserViewModel {FirstName = u.FirstName, Id = u.Id.ToString(), LastName = u.LastName, Selected = false}).ToList();

            CenterUsersViewModel model = new CenterUsersViewModel
            {
                CenterId = id,
                CenterUsersDetails = centerUsersVm
            };
            return PartialView("CenterUsersView", model);
        }

        [HttpPost]
        public void SetCenterUsers(CenterUsersViewModel model)
        {
            CenterBusinessLayer bl = new CenterBusinessLayer(new CapaNavDocDal());
            Center center = bl.Get(model.CenterId.ToInt32());
            center.UserList = model.CenterUsersDetails.Where(user => user.Selected).Aggregate("", (current, user) => current.AddId(user.Id.ToInt32()));
            bl.Update(center);
        }
    }
}