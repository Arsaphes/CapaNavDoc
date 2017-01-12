using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.ViewModel.MaintenanceData
{
    public class MaintenanceDataMonitoringViewModel
    {
        public string MaintenanceDataId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La date est obligatoire.")]
        public string Date { get; set; }

        public string SelectedUserCall { get; set; }

        public List<string> Users { get; set; }
    }
}