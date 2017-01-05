using System;
using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
    public class MaintenanceData
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
        public string Sender { get; set; }
        public string DocumentReference { get; set; }
        public string DocumentPartNumber { get; set; }
        public string Review { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public bool OnCertificate { get; set; }
        public byte[] Document { get; set; }

        public DateTime MonitoringDate { get; set; }
        public int MonitoringUserId { get; set; }
    }
}