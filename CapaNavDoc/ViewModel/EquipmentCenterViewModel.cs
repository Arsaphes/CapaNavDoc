using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class EquipmentCenterViewModel
    {
        public string EquipmentCenterId { get; set; }
        public string CenterId { get; set; }
        public string CenterName { get; set; }
        public List<EquipmentCenterActionViewModel> CenterActions { get; set; }
    }
}