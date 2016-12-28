using System;
using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class EquipmentMonitoringViewModel
    {
        public string EquipmentId { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public List<UserCallViewModel> UserCalls { get; set; }
    }
}