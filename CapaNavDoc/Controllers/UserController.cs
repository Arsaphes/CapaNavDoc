using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Extensions;
using CapaNavDoc.Extensions.ViewModels;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.ViewModel.User;

namespace CapaNavDoc.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return new DefaultController<User>().Index<UserDetailsViewModel, UserListViewModel>();
        }

        [HttpPost]
        public void EditUser(UserEditionViewModel model)
        {
            new DefaultController<User>().Edit(model);
        }

        [HttpPost]
        public void DeleteUser(ConfirmationViewModel model)
        {
            new DefaultController<User>().Delete(model);
        }


        [HttpGet]
        public PartialViewResult GetUserInsertView()
        {
            return PartialView("UserEditionView", new UserEditionViewModel { EditionMode = "Ajouter" });
        }

        [HttpGet]
        public PartialViewResult GetUserUpdateView(string id)
        {
            return PartialView("UserEditionView", new BusinessLayer<User>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel(new UserEditionViewModel(), "Changer"));
        }

        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            User t = new BusinessLayer<User>(new CapaNavDocDal()).Get(id.ToInt32());
            return PartialView("ConfirmationView", new ConfirmationViewModel
            {
                Id = id,
                ConfirmationMessage = $"Supprimer l'utilisateur {t.FirstName} {t.LastName} ?",
                Controler = "User",
                Action = "DeleteUser"
            });
        }


        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<User> bl = new BusinessLayer<User>(new CapaNavDocDal());
            List<UserDetailsViewModel> model = new List<UserDetailsViewModel>(bl.GetList().Select(u => (UserDetailsViewModel)u.ToModel(new UserDetailsViewModel())));

            model = TableDataAdapter.Search(model, param);
            model = TableDataAdapter.SortList(model, param);
            model = TableDataAdapter.PageList(model, param);

            string[][] data = model.Select(m => new[] {m.Id.ToString(), m.FirstName, m.LastName, m.UserName, m.Password }).ToArray();
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