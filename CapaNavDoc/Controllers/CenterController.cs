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
        /// <summary>
        /// Get the default view displaying the Centers grid view.
        /// </summary>
        /// <returns>A view.</returns>
        public ActionResult Index()
        {
            CenterBusinessLayer bl = new CenterBusinessLayer(new CapaNavDocDal());
            List<Center> centers = bl.GetList();
            List<CenterDetailsViewModel> centerDetails = centers.Select(center => center.ToCenterDetailsViewModel()).ToList();
            CenterListViewModel model = new CenterListViewModel { CentersDetails = centerDetails };
            return View("Index", model);
        }

        /// <summary>
        /// Insert or update a Center.
        /// </summary>
        /// <param name="model">The CenterEditionViewModel used to edit a Center.</param>
        [HttpPost]
        public void EditCenter(CenterEditionViewModel model)
        {
            CenterBusinessLayer bl;

            switch (model.EditionMode)
            {
                case "Ajouter":
                    bl = new CenterBusinessLayer(new CapaNavDocDal());
                    bl.Insert(model.ToCenter());
                    break;

                case "Changer":
                    bl = new CenterBusinessLayer(new CapaNavDocDal());
                    bl.Update(model.ToCenter());
                    break;
            }
        }

        /// <summary>
        /// Delete a Center.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        [HttpPost]
        public void DeleteCenter(ConfirmationViewModel model)
        {
            CenterBusinessLayer bl = new CenterBusinessLayer(new CapaNavDocDal());
            bl.Delete(model.Id.ToInt32());
        }


        /// <summary>
        /// Get a partial view used to insert a Center.
        /// </summary>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetCenterInsertView()
        {
            CenterEditionViewModel model = new CenterEditionViewModel { EditionMode = "Ajouter" };
            return PartialView("CenterEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to update a Center.
        /// </summary>
        /// <param name="id">The id of the Center to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetCenterUpdateView(string id)
        {
            CenterBusinessLayer bl = new CenterBusinessLayer(new CapaNavDocDal());
            Center center = bl.Get(id.ToInt32());
            CenterEditionViewModel model = center.ToCenterEditionViewModel("Changer");

            return PartialView("CenterEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm a Center deletation.
        /// </summary>
        /// <param name="id">The id of the Center to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            CenterBusinessLayer bl = new CenterBusinessLayer(new CapaNavDocDal());
            Center center = bl.Get(id.ToInt32());
            string userCall = $"{center.Name}";
            ConfirmationViewModel model = new ConfirmationViewModel { ConfirmationMessage = $"Supprimer l'atelier {userCall} ?", Id = id, Controler = "Center", Action = "DeleteCenter" };

            return PartialView("ConfirmationView", model);
        }

        /// <summary>
        /// Get the datas used to display the Center data table.
        /// </summary>
        /// <param name="param">The data table common properties.</param>
        /// <returns>A serialized set of data for the data table.</returns>
        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            List<Center> centers = TableDataAdapter.SearchInCenters(param.sSearch).Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            string[][] data = centers.Select(c => new[] {c.Id.ToString(), null, c.Name }).ToArray();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = centers.Count,
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