using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Extensions;
using CapaNavDoc.Extensions.ViewModels;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;

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
            List<UserDetailsViewModel> usersDetails = users.Select(user => user.ToUserDetailsViewModel()).ToList();
            UserListViewModel model = new UserListViewModel {UsersDetails = usersDetails};
            return View("Index", model);
        }

        /// <summary>
        /// Insert or update a user.
        /// </summary>
        /// <param name="model">The UserEditionViewModel used to edit a user.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult EditUser(UserEditionViewModel model)
        {
            UserBusinessLayer bl;

            switch (model.EditionMode)
            {
                case "Ajouter":
                    bl = new UserBusinessLayer();
                    bl.InsertUser(model.ToUser());
                    break;

                case "Changer":
                    bl = new UserBusinessLayer();
                    bl.UpdateUser(model.ToUser());
                    break;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult DeleteUser(ConfirmationViewModel model)
        {
            UserBusinessLayer bl = new UserBusinessLayer();
            bl.DeleteUser(model.Id.ToInt32());
            return RedirectToAction("Index");
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
            UserEditionViewModel model = user.ToUserEditionViewModel("Changer");

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
            string userCall = $"{user.FirstName} {user.LastName}";
            ConfirmationViewModel model = new ConfirmationViewModel {ConfirmationMessage = $"Supprimer l'utilisateur {userCall} ?", Id = id, Controler = "User", Action = "DeleteUser"};

            return PartialView("ConfirmationView", model);
        }
    }
}