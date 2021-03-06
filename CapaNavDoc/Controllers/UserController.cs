﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Extensions;
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
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditUser(UserEditionViewModel model)
        {
            //System.Threading.Thread.Sleep(80000);
            if (!ModelState.IsValid) return PartialView("UserEditionView", model);
            new DefaultController<User>().Edit(model);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DeleteUser(ConfirmationViewModel model)
        {
            new DefaultController<User>().Delete(model);
            return Json(new { success = true });
        }


        [HttpGet]
        public PartialViewResult GetUserInsertView()
        {
            return PartialView("UserEditionView", new UserEditionViewModel { EditionMode = EditionMode.Insert });
        }

        [HttpGet]
        public PartialViewResult GetUserUpdateView(string id)
        {
            return PartialView("UserEditionView", new BusinessLayer<User>(new CapaNavDocDal()).Get(id.ToInt32()).ToModel(new UserEditionViewModel(), EditionMode.Update));
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