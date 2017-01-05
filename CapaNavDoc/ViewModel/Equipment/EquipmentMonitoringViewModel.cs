using System.Collections.Generic;

namespace CapaNavDoc.ViewModel.Equipment
{
    public class EquipmentMonitoringViewModel
    {
        public string EquipmentId { get; set; }
        public string Date { get; set; }
        public string SelectedUserCall { get; set; } 
        public List<string> Users { get; set; }
    }
}