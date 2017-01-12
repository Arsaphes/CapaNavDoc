using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;
using CapaNavDoc.ViewModel.Center;

namespace CapaNavDoc.Controllers
{
    public class CenterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditCenter(CenterEditionViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("CenterEditionView", model);
            new DefaultController<Center>().Edit(model);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteCenter(ConfirmationViewModel model)
        {
            new DefaultController<Center>().Delete(model);
            return Json(new { success = true });
        }


        [HttpGet]
        public PartialViewResult GetCenterInsertView()
        {
            return PartialView("CenterEditionView", new CenterEditionViewModel { EditionMode = EditionMode.Insert });
        }

        [HttpGet]
        public PartialViewResult GetCenterUpdateView(string id)
        {
            return PartialView("CenterEditionView", new BusinessLayer<Center>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel(new CenterEditionViewModel(), EditionMode.Update));
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
            List<CenterDetailsViewModel> model = new List<CenterDetailsViewModel>(bl.GetList().Select(c => (CenterDetailsViewModel)c.ToModel(new CenterDetailsViewModel())));

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
            BusinessLayer<Center> cbl = new BusinessLayer<Center>(new CapaNavDocDal());

            List<User> centerUsers = cbl.Get(id.ToInt32()).UserList.GetList<User>().OrderBy(u=>u.FirstName).ToList();
            List<User> otherUsers = ubl.GetList().Except(centerUsers, (u1, u2) => u1.Id == u2.Id).OrderBy(u => u.FirstName).ToList();

            List<CenterUserViewModel> centerUsersVm = new List<CenterUserViewModel>();
            centerUsersVm.AddRange(centerUsers.Select(u => new CenterUserViewModel { FirstName = u.FirstName, LastName = u.LastName, Id = u.Id.ToString(), Selected = true }));
            centerUsersVm.AddRange(otherUsers.Select(u => new CenterUserViewModel { FirstName = u.FirstName, LastName = u.LastName, Id = u.Id.ToString(), Selected = false }));

            return PartialView("CenterUsersView", new CenterUserListViewModel { CenterId = id, CenterUsersDetails = centerUsersVm});
        }

        [HttpPost]
        public void SetCenterUsers(CenterUserListViewModel model)
        {
            BusinessLayer<Center> bl = new BusinessLayer<Center>(new CapaNavDocDal());
            Center center = bl.Get(model.CenterId.ToInt32());
            center.UserList = model.CenterUsersDetails.Where(user => user.Selected).Aggregate("", (current, user) => current.AddId(user.Id.ToInt32()));
            bl.Update(center);
        }
    }
}