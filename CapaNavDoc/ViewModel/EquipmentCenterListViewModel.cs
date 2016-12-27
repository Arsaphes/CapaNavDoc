using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class EquipmentCenterListViewModel : MyLayoutViewModel
    {
        public string EquipmentId { get; set; }

        public List<EquipmentCenterViewModel> EquipmentCenters { get; set; }
    }
}