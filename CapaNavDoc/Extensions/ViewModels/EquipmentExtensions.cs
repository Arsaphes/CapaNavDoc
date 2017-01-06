using System.Collections.Generic;
using System.Linq;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel.Action;
using CapaNavDoc.ViewModel.Center;
using CapaNavDoc.ViewModel.Equipment;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class EquipmentExtensions
    {
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
            Logger log = new Logger();
            log.Debug($"[ ToEquipmentCenterViewModel(ID={equipment.Id}) ]");

            List<CenterActionCouple> couples = equipment.EquipmentCenterActionList.ToCenterActionGroups();
            log.Debug($"CenterActionCouple list created. Count={couples.Count}");

            BusinessLayer<Action> abl = new BusinessLayer<Action>(new CapaNavDocDal());
            BusinessLayer<Center> cbl = new BusinessLayer<Center>(new CapaNavDocDal());
            log.Debug("Action and Center Business Layers created.");

            EquipmentCenterViewModel model = new EquipmentCenterViewModel
            {
                EquipmentId = equipment.Id.ToString(),
                Centers = new List<CenterDetailsViewModel>(cbl.GetList().Select(c => (CenterDetailsViewModel)c.ToModel(new CenterDetailsViewModel()))),
                Actions = new List<ActionDetailsViewModel>(abl.GetList().Select(a => (ActionDetailsViewModel)a.ToModel(new ActionDetailsViewModel())))
            };
            model.Selections = new bool[model.Centers.Count][];
            model.TableColumns = (model.Actions.Count + 1).ToString();

            for (int i = 0; i < model.Centers.Count; i++)
            {
                model.Selections[i] = new bool[model.Actions.Count];
                for (int j = 0; j < model.Actions.Count; j++)
                {
                    int index = couples.FindIndex(g => g.CenterId == model.Centers[i].Id && g.ActionId == model.Actions[j].Id);
                    if (-1 == index) continue;
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
            BusinessLayer<User> bl = new BusinessLayer<User>(new CapaNavDocDal());

            return new EquipmentMonitoringViewModel
            {
                EquipmentId = equipment.Id.ToString(),
                SelectedUserCall = bl.GetList().FirstOrDefault(u => u.Id == equipment.MonitoringUserId).ToUserCallViewModel().UserCall,
                Date = equipment.MonitoringDate.ToString("dd-mm-yyyy"),
                Users = bl.GetList().Select(u => u.ToUserCallViewModel().UserCall).ToList()
            };
        }
    }
}