using System.Collections.Generic;
using System.Linq;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class EquipmentCenterExtensions
    {
        public static EquipmentCenterViewModel ToEquipmentCenterViewModel(this EquipmentCenter equipmentCenter)
        {
            BusinessLayer<Center> bl = new BusinessLayer<Center>(new CapaNavDocDal());
            List<EquipmentCenterActionViewModel> actions = new List<EquipmentCenterActionViewModel>();
            actions.AddRange(equipmentCenter.GetEquipmentCenterActions().Select(a => a.ToEquipmentCenterActionViewModel()));

            return new EquipmentCenterViewModel
            {
                CenterId = equipmentCenter.CenterId.ToString(),
                CenterName = bl.Get(equipmentCenter.CenterId).Name,
                EquipmentCenterId = equipmentCenter.Id.ToString(),
                CenterActions = actions
            };
        }

        public static List<Action> GetEquipmentCenterActions(this EquipmentCenter equipmentCenter)
        {
            List<Action> actions = new List<Action>();
            if (string.IsNullOrEmpty(equipmentCenter.ActionList)) return actions;

            string[] ids = equipmentCenter.ActionList.Split(';');
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            actions.AddRange(ids.Select(id => bl.Get(id.ToInt32())));

            return actions;
        }


    }
}