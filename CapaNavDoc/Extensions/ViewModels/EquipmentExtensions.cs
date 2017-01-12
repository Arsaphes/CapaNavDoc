using System.Linq;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel.Equipment;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class EquipmentExtensions
    {
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

        public static EquipmentEditionViewModel ToEquipmentEditionViewModel(this Equipment equipment)
        {
            BusinessLayer<MaintenanceData> bl = new BusinessLayer<MaintenanceData>(new CapaNavDocDal());
            BusinessLayer<ActivityField> afbl = new BusinessLayer<ActivityField>(new CapaNavDocDal());

            return new EquipmentEditionViewModel
            {
                Id = equipment.Id.ToString(),
                Name = equipment.Name,
                Type = equipment.Type,
                Ata = equipment.Ata.ToString(),
                PartNumber = equipment.PartNumber,
                ActivityFieldDescriptions = afbl.GetList().Select(a=>a.Description).ToList(),
                ActivityFieldId = equipment.ActivityFieldId == 0 ? "" : afbl.Get(equipment.ActivityFieldId).Description,
                Manufacturer = equipment.Manufacturer,
                MechanicsGroup = equipment.MechanicsGroup,
                DocumentsReferences = bl.GetList().Select(md => md.Name).ToList(),
                MaintenanceDataId = equipment.MaintenanceDataId == 0 ? "" : bl.Get(equipment.MaintenanceDataId).Name,
                EditionMode = EditionMode.Update
            };
        }
    }
}