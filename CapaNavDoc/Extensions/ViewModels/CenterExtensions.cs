using CapaNavDoc.Models;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class CenterExtensions
    {
        /// <summary>
        /// Get a Center model from a CenterEditionViewModel used to edit a Center in a popup form.
        /// </summary>
        /// <param name="model">The model used in the popup edition form.</param>
        /// <returns>A Center.</returns>
        public static Center ToCenter(this CenterEditionViewModel model)
        {
            return new Center
            {
                Id = model.Id.ToInt32(),
                Name = model.Name
            };
        }

        /// <summary>
        /// Get a CenterDetailsViewModel used to display the Centers in a grid view from a Center model. 
        /// </summary>
        /// <param name="center">The Center model.</param>
        /// <returns>A CenterDetailsViewModel.</returns>
        public static CenterDetailsViewModel ToCenterDetailsViewModel(this Center center)
        {
            return new CenterDetailsViewModel
            {
                Id = center.Id.ToString(),
                Name = center.Name
            };
        }

        /// <summary>
        /// Get a CenterEditionViewModel used to edit a Center in a popup form from an Center model.
        /// </summary>
        /// <param name="center">The Center model.</param>
        /// <param name="editionMode">The edition mode. Could be for updating ('Changer') or inserting ('Ajouter').</param>
        /// <returns>A CenterEditionViewModel.</returns>
        public static CenterEditionViewModel ToCenterEditionViewModel(this Center center, string editionMode)
        {
            return new CenterEditionViewModel
            {
                Id = center.Id.ToString(),
                Name = center.Name,

                EditionMode = editionMode,
            };
        }
    }
}