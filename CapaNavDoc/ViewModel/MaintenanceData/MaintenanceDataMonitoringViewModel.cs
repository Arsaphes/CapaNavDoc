using System.Collections.Generic;

namespace CapaNavDoc.ViewModel.MaintenanceData
{
    public class MaintenanceDataMonitoringViewModel
    {
        public string MaintenanceDataId { get; set; }
        public string Date { get; set; }
        public string SelectedUserCall { get; set; }
        public List<string> Users { get; set; }
    }
}