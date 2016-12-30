using System.Collections.Generic;
using System.Linq;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
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

        /// <summary>
        /// Set an Equipment Center/Action groups.
        /// </summary>
        /// <param name="equipment">The Equipment.</param>
        /// <param name="model">The EquipmentCenterViewModel used to edit the Center/Action groups</param>
        public static void SetCenterActionGroups(this Equipment equipment, EquipmentCenterViewModel model)
        {
            string centerActions = "";
            for (int i = 0; i < model.Centers.Count; i++)
                for (int j = 0; j < model.Actions.Count; j++)
                {
                    if (!model.Selections[i][j]) continue;
                    centerActions += (centerActions.Length == 0 ? "" : ";") + $"{model.Centers[i].Id},{model.Actions[j].Id}";
                }
            equipment.EquipmentCenterActionList = centerActions;
        }

        /// <summary>
        /// Get an EquipmentCenterViewModel from an Equipment used to edit the Center/Action groups.
        /// </summary>
        /// <param name="equipment">The Equipment.</param>
        /// <returns>An EquipmentCenterViewModel.</returns>
        public static EquipmentCenterViewModel ToEquipmentCenterViewModel(this Equipment equipment)
        {
            List<CenterActionCouple> couples = equipment.EquipmentCenterActionList.ToCenterActionGroups();
            BusinessLayer<Action> abl = new BusinessLayer<Action>(new CapaNavDocDal());
            BusinessLayer<Center> cbl = new BusinessLayer<Center>(new CapaNavDocDal());
            EquipmentCenterViewModel model = new EquipmentCenterViewModel
            {
                EquipmentId = equipment.Id.ToString(),
                Centers = new List<CenterDetailsViewModel>(cbl.GetList().Select(c => c.ToCenterDetailsViewModel())),
                Actions = new List<ActionDetailsViewModel>(abl.GetList().Select(a => a.ToActionDetailsViewModel()))
            };
            model.Selections = new bool[model.Centers.Count][];
            model.TableColumns = (model.Actions.Count + 1).ToString();

            for (int i = 0; i < model.Centers.Count; i++)
            {
                model.Selections[i] = new bool[model.Actions.Count];
                for (int j = 0; j < model.Actions.Count; j++)
                {
                    int index = couples.FindIndex(g => g.CenterId == model.Centers[i].Id && g.ActionId == model.Actions[j].Id);
                    if(-1== index) continue;
                    model.Selections[i][j] = true;
                }
            }
            return model;
        }

        /// <summary>
        /// Get an EquipmentMonitoringViewModel from an Equipment used to edit the monitoring.
        /// </summary>
        /// <param name="equipment">The Equipment.</param>
        /// <returns>An EquipmentMonitoringViewModel.</returns>
        public static EquipmentMonitoringViewModel ToEquipmentMonitoringViewModel(this Equipment equipment)
        {
            BusinessLayer<User> ubl = new BusinessLayer<User>(new CapaNavDocDal());

            EquipmentMonitoringViewModel model = new EquipmentMonitoringViewModel
            {
                Date = equipment.MonitoringDate.ToString("yyyy-mm-dd"),
                UserId = equipment.MonitoringUserId.ToString(),
                UserCalls = ubl.GetList().Select(u => new UserCallViewModel { UserId = u.Id.ToString(), UserCall = $"{u.FirstName} {u.LastName}" }).ToList(),
                EquipmentId = equipment.Id.ToString()
            };
            return model;
        }
    }
}