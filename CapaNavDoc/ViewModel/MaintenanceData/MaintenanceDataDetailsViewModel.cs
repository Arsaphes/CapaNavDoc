using System.ComponentModel.DataAnnotations;
using CapaNavDoc.Classes;

namespace CapaNavDoc.ViewModel.MaintenanceData
{
    public class MaintenanceDataDetailsViewModel
    {
        [Key]
        public string Id { get; set; }

        [Column(Column = 1)]
        public string Type { get; set; }

        [Column(Column = 2)]
        public string Sender { get; set; }

        [Column(Column = 3)]
        public string DocumentReference { get; set; }

        [Column(Column = 4)]
        public string DocumentPartNumber { get; set; }

        [Column(Column = 5)]
        public string Review { get; set; }

        public string Date { get; set; }

        [Column(Column = 7)]
        public string Name { get; set; }

        [Column(Column = 8)]
        public string OnCertificate { get; set; }

        public string MonitoringDate { get; set; }
    }
}