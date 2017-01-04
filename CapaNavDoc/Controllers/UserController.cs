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

namespace CapaNavDoc.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        /// <summary>
        /// Get the default view displaying the users grid view.
        /// </summary>
        /// <returns>A view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            UserBusinessLayer ubl = new UserBusinessLayer();
            List<User> users = ubl.GetUsers();
            List<UserDetailsViewModel> usersDetails = users.Select(user => user.ToModel<UserDetailsViewModel>()).ToList();
            UserListViewModel model = new UserListViewModel {UsersDetails = usersDetails};
            return View("Index", model);
        }

        /// <summary>
        /// Insert or update a user.
        /// </summary>
        /// <param name="model">The UserEditionViewModel used to edit a user.</param>
        [HttpPost]
        public void EditUser(UserEditionViewModel model)
        {
            UserBusinessLayer bl;

            switch (model.EditionMode)
            {
                case "Ajouter":
                    bl = new UserBusinessLayer();
                    bl.InsertUser(model.ToModel<User>());
                    break;

                case "Changer":
                    bl = new UserBusinessLayer();
                    bl.UpdateUser(model.ToModel<User>());
                    break;
            }
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        [HttpPost]
        public void DeleteUser(ConfirmationViewModel model)
        {
            UserBusinessLayer bl = new UserBusinessLayer();
            bl.DeleteUser(model.Id.ToInt32());
        }


        /// <summary>
        /// Get a partial view used to insert a User.
        /// </summary>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetUserInsertView()
        {
            UserEditionViewModel model = new UserEditionViewModel { EditionMode = "Ajouter" };
            return PartialView("UserEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to update a User.
        /// </summary>
        /// <param name="id">The id of the User to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetUserUpdateView(string id)
        {
            UserBusinessLayer ubl = new UserBusinessLayer();
            User user = ubl.GetUser(id.ToInt32());
            UserEditionViewModel model = user.ToModel<UserEditionViewModel>("Changer");

            return PartialView("UserEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm a User deletation.
        /// </summary>
        /// <param name="id">The id of the User to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string id)
        {
            UserBusinessLayer bl = new UserBusinessLayer();
            User user = bl.GetUser(id.ToInt32());
            string call = $"{user.FirstName} {user.LastName}";
            ConfirmationViewModel model = new ConfirmationViewModel {ConfirmationMessage = $"Supprimer l'utilisateur {call} ?", Id = id, Controler = "User", Action = "DeleteUser"};

            return PartialView("ConfirmationView", model);
        }


        /// <summary>
        /// Get the datas used to display the User data table.
        /// </summary>
        /// <param name="param">The data table common properties.</param>
        /// <returns>A serialized set of data for the data table.</returns>
        [HttpGet]
        public ActionResult AjaxHandler(JQueryDataTableParam param)
        {
            BusinessLayer<User> bl = new BusinessLayer<User>(new CapaNavDocDal());
            List<UserDetailsViewModel> model = new List<UserDetailsViewModel>(bl.GetList().Select(u => u.ToModel<UserDetailsViewModel>()));

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