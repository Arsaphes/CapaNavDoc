using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CapaNavDoc.Extensions;
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
        /// <param name="editionMode">The edition mode.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult EditUser(UserEditionViewModel model, string editionMode)
        {
            UserBusinessLayer bl;

            switch (editionMode)
            {
                case "Ajouter":
                    bl = new UserBusinessLayer();
                    bl.InsertUser(model.ToUser());
                    return RedirectToAction("Index");

                case "Changer":
                    bl = new UserBusinessLayer();
                    bl.UpdateUser(model.ToUser());
                    return RedirectToAction("Index");

                default:  // Annuler
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="model">The ConfirmationViewModel used to display the dialog box.</param>
        /// <param name="dialogResult">The confirmation result.</param>
        /// <returns>A redirection to the default view.</returns>
        [HttpPost]
        public ActionResult DeleteUser(ConfirmationViewModel model, string dialogResult)
        {
            if(dialogResult == "Non") return RedirectToAction("Index");

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
            UserEditionViewModel model = new UserEditionViewModel {EditionMode = "Ajouter"};
            return PartialView("UserEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to update a user.
        /// </summary>
        /// <param name="userId">The id of the User to update.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetUserUpdateView(string userId)
        {
            UserBusinessLayer ubl = new UserBusinessLayer();
            User user = ubl.GetUser(userId.ToInt32());
            UserEditionViewModel model = user.ToUserEditionViewModel("Changer");

            return PartialView("UserEditionView", model);
        }

        /// <summary>
        /// Get a partial view used to confirm a User deletation.
        /// </summary>
        /// <param name="userId">The id of the User to delete.</param>
        /// <returns>A partial view.</returns>
        [HttpGet]
        public PartialViewResult GetConfirmationView(string userId)
        {
            UserBusinessLayer bl = new UserBusinessLayer();
            User user = bl.GetUser(userId.ToInt32());
            string userCall = $"{user.FirstName} {user.LastName}";
            ConfirmationViewModel model = new ConfirmationViewModel {ConfirmationMessage = $"Supprimer l'utilisateur {userCall} ?", Id = userId};

            return PartialView("ConfirmationView", model);
        }
    }
}