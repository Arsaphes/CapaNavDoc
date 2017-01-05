using System.Collections.Generic;

namespace CapaNavDoc.ViewModel.MaintenanceData
{
    public class MaintenanceDataListViewModel : MyLayoutViewModel
    {
        public List<MaintenanceDataDetailsViewModel> MaintenanceDataDetails { get; set; }
    }
}