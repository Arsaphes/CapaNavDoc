using CapaNavDoc.Models;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class EquipmentExtensions
    {
        /// <summary>
        /// Get an Equipment model from an EquipmentEditionViewModel used to edit an Equipment in a popup form.
        /// </summary>
        /// <param name="model">The model used in the popup edition form.</param>
        /// <returns>An Equipment.</returns>
        public static Equipment ToEquipment(this EquipmentEditionViewModel model)
        {
            return new Equipment
            {
                Id = model.Id.ToInt32(),
                Name = model.Name,
                PartNumber = model.PartNumber,
                DocumentsPartNumber = model.DocumentsPartNumber,
                ActivityField = model.ActivityField,
                Ata = model.Ata.ToInt32(),
                DocumentsReferences = model.DocumentsReferences,
                Manufacturer = model.Manufacturer,
                MechanicsGroup = model.MechanicsGroup,
                Type = model.Type
            };
        }

        /// <summary>
        /// Get an EquipmentDetailsViewModel used to display the Equipments in a grid view from an Equipment model. 
        /// </summary>
        /// <param name="equipment">The Equipment model.</param>
        /// <returns>An EquipmentDetailsViewModel.</returns>
        public static EquipmentDetailsViewModel ToEquipmentDetailsViewModel(this Equipment equipment)
        {
            return new EquipmentDetailsViewModel
            {
                Id = equipment.Id.ToString(),
                Name = equipment.Name,
                DocumentsPartNumber = equipment.DocumentsPartNumber,
                PartNumber = equipment.PartNumber,
                Ata = equipment.Ata.ToString(),
                ActivityField = equipment.ActivityField,
                MechanicsGroup = equipment.MechanicsGroup,
                Type = equipment.Type,
                Manufacturer = equipment.Manufacturer,
                DocumentsReferences = equipment.DocumentsReferences
            };
        }

        /// <summary>
        /// Get an EquipmentEditionViewModel used to edit an Equipment in a popup form from an Equipment model.
        /// </summary>
        /// <param name="equipment">The Equipment model.</param>
        /// <param name="editionMode">The edition mode. Could be for updating ('Changer') or inserting ('Ajouter').</param>
        /// <returns>An EquipmentEditionViewModel.</returns>
        public static EquipmentEditionViewModel ToEquipmentEditionViewModel(this Equipment equipment, string editionMode)
        {
            return new EquipmentEditionViewModel
            {
                Id = equipment.Id.ToString(),
                Name = equipment.Name,
                PartNumber = equipment.PartNumber,
                ActivityField = equipment.ActivityField,
                Ata = equipment.Ata.ToString(),
                DocumentsPartNumber = equipment.DocumentsPartNumber,
                DocumentsReferences = equipment.DocumentsReferences,
                Manufacturer = equipment.Manufacturer,
                MechanicsGroup = equipment.MechanicsGroup,
                Type = equipment.Type,

                EditionMode = editionMode,
            };
        }
    }
}