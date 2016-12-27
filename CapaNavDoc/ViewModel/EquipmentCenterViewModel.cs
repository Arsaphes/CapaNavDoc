using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class EquipmentCenterViewModel
    {
        public string EquipmentId { get; set; }
        public List<string> CenterNames { get; set; }
        public List<string> ActionDescriptions { get; set; }
    }
}