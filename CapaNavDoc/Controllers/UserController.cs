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
        [HttpGet]
        public ActionResult Index()
        {
            UserBusinessLayer ubl = new UserBusinessLayer();
            CenterBusinessLayer cbl = new CenterBusinessLayer();
            List<User> users = ubl.GetUsers();
            List<UserDetailsViewModel> usersDetails = users.Select(user => user.ToUserDetailsViewModel(cbl.GetCenter(user.CenterId).Name)).ToList();
            UserListViewModel model = new UserListViewModel {UsersDetails = usersDetails};
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult EditUser(UserEditionViewModel model, string editionMode, string centerId)
        {
            UserBusinessLayer bl;

            switch (editionMode)
            {
                case "Ajouter":
                    bl = new UserBusinessLayer();
                    bl.InsertUser(model.ToUser(centerId));
                    return RedirectToAction("Index");

                case "Changer":
                    bl = new UserBusinessLayer();
                    bl.UpdateUser(model.ToUser(centerId));
                    return RedirectToAction("Index");

                default:  // Annuler
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(ConfirmationViewModel model, string dialogResult)
        {
            if(dialogResult == "Non") return RedirectToAction("Index");

            UserBusinessLayer bl = new UserBusinessLayer();
            bl.DeleteUser(model.Id.ToInt32());
            return RedirectToAction("Index");
        }


        [HttpGet]
        public PartialViewResult GetUserInsertView()
        {
            UserEditionViewModel model = new UserEditionViewModel {EditionMode = "Ajouter"};
            CenterBusinessLayer bl = new CenterBusinessLayer();
            model.Centers = bl.GetCenters();

            return PartialView("UserEditionView", model);
        }

        [HttpGet]
        public PartialViewResult GetUserUpdateView(string userId)
        {
            CenterBusinessLayer cbl = new CenterBusinessLayer();
            UserBusinessLayer ubl = new UserBusinessLayer();
            User user = ubl.GetUser(userId.ToInt32());
            UserEditionViewModel model = user.ToUserEditionViewModel(cbl.GetCenters(), "Changer");

            return PartialView("UserEditionView", model);
        }

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