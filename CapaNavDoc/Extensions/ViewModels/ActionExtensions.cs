using CapaNavDoc.Models;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class ActionExtensions
    {
        /// <summary>
        /// Get an Action model from a ActionEditionViewModel used to edit an Action in a popup form.
        /// </summary>
        /// <param name="model">The model used in the popup edition form.</param>
        /// <returns>An Action.</returns>
        public static Action ToAction(this ActionEditionViewModel model)
        {
            return new Action
            {
                Id = model.Id.ToInt32(),
                Description = model.Description
            };
        }

        /// <summary>
        /// Get a ActionDetailsViewModel used to display the actions in a grid view from a Action model. 
        /// </summary>
        /// <param name="action">The Action model.</param>
        /// <returns>A ActionDetailsViewModel.</returns>
        public static ActionDetailsViewModel ToActionDetailsViewModel(this Action action)
        {
            return new ActionDetailsViewModel
            {
                Id = action.Id.ToString(),
                Description = action.Description
            };
        }

        /// <summary>
        /// Get a ActionEditionViewModel used to edit an Action in a popup form from an Action model.
        /// </summary>
        /// <param name="action">The Action model.</param>
        /// <param name="editionMode">The edition mode. Could be for updating ('Changer') or inserting ('Ajouter').</param>
        /// <returns>A ActionEditionViewModel.</returns>
        public static ActionEditionViewModel ToActionEditionViewModel(this Action action, string editionMode)
        {
            return new ActionEditionViewModel
            {
                Id = action.Id.ToString(),
                Description = action.Description,

                EditionMode = editionMode,
            };
        }

    }
}