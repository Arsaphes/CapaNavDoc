using System;
using System.Data.SqlTypes;
using System.Globalization;
using CapaNavDoc.Models;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class MaintenanceDataExtensions
    {
        /// <summary>
        /// Get a MaintenanceData model from a MaintenanceDataEditionViewModel used to edit a MaintenanceData in a popup form.
        /// </summary>
        /// <param name="model">The model used in the popup edition form.</param>
        /// <returns>A MaintenanceData.</returns>
        public static MaintenanceData ToMaintenanceData(this MaintenanceDataEditionViewModel model)
        {
            return new MaintenanceData
            {
                Id = model.Id.ToInt32(),
                Name = model.Name,
                Type = model.Type,
                Sender = model.Sender,
                Review = model.Review,
                DocumentLink = model.DocumentLink,
                OnCertificate = model.OnCertificate,
                Date = string.IsNullOrEmpty(model.Date) ? SqlDateTime.MinValue.Value : DateTime.ParseExact(model.Date, "dd-mm-yyyy", CultureInfo.InvariantCulture)
            };
        }

        /// <summary>
        /// Get a MaintenanceDataDetailsViewModel used to display the maintenance datas in a grid view from a MaintenanceData model. 
        /// </summary>
        /// <param name="maintenanceData">The MaintenanceData model.</param>
        /// <returns>A UserDetailsViewModel.</returns>
        public static MaintenanceDataDetailsViewModel ToMaintenanceDataDetailsViewModel(this MaintenanceData maintenanceData)
        {
            return new MaintenanceDataDetailsViewModel
            {
                Id = maintenanceData.Id.ToString(),
                Name = maintenanceData.Name,
                Type = maintenanceData.Type,
                Date = maintenanceData.Date.ToString("dd-mm-yyyy"),
                DocumentLink = maintenanceData.DocumentLink,
                OnCertificate = maintenanceData.OnCertificate ? "Oui":"Non",
                Review = maintenanceData.Review,
                Sender = maintenanceData.Sender
            };
        }

        /// <summary>
        /// Get a MaintenanceDataEditionViewModel used to edit a MaintenanceData in a popup form from a MaintenanceDataEditionViewModel model.
        /// </summary>
        /// <param name="md">Teh MaintenanceData model.</param>
        /// <param name="editionMode">The edition mode. Could be for updating ('Changer') or inserting ('Ajouter').</param>
        /// <returns>A MaintenanceDataEditionViewModel.</returns>
        public static MaintenanceDataEditionViewModel ToMaintenanceDataEditionViewModel(this MaintenanceData md, string editionMode)
        {
            return new MaintenanceDataEditionViewModel
            {
                Id = md.Id.ToString(),
                Type = md.Type,
                Sender = md.Sender,
                Review = md.Review,
                Date = md.Date.ToString("dd-mm-yyyy"),
                Name = md.Name,
                OnCertificate = md.OnCertificate,
                DocumentLink = md.DocumentLink,
                EditionMode = editionMode,
            };
        }
    }
}