using CapaNavDoc.Models;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class UserExtensions
    {
        /// <summary>
        /// Get a User model from a UserEditionViewModel used to edit a user in a popup form.
        /// </summary>
        /// <param name="model">The model used in the popup edition form.</param>
        /// <returns>A User.</returns>
        public static User ToUser(this UserEditionViewModel model)
        {
            return new User
            {
                Id = model.Id.ToInt32(),
                UserName = model.UserName,
                LastName = model.LastName,
                Password = model.Password,
                FirstName = model.FirstName
            };
        }

        /// <summary>
        /// Get a UserDetailsViewModel used to display the users in a grid view from a User model. 
        /// </summary>
        /// <param name="user">The User model.</param>
        /// <returns>A UserDetailsViewModel.</returns>
        public static UserDetailsViewModel ToUserDetailsViewModel(this User user)
        {
            return new UserDetailsViewModel
            {
                Id = user.Id.ToString(),
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
        }

        /// <summary>
        /// Get a UserEditionViewModel used to edit a user in a popup form from a User model.
        /// </summary>
        /// <param name="user">Teh User model.</param>
        /// <param name="editionMode">The edition mode. Could be for updating ('Changer') or inserting ('Ajouter').</param>
        /// <returns>A UserEditionViewModel.</returns>
        public static UserEditionViewModel ToUserEditionViewModel(this User user, string editionMode)
        {
            return new UserEditionViewModel
            {
                Id = user.Id.ToString(),
                LastName = user.LastName,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Password = user.Password,

                EditionMode = editionMode,
            };
        }
    }
}