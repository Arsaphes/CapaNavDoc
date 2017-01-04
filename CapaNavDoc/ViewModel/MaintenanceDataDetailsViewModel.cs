using System.ComponentModel.DataAnnotations;
using CapaNavDoc.Classes;

namespace CapaNavDoc.ViewModel
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
        public string Review { get; set; }

        public string Date { get; set; }

        [Column(Column = 5)]
        public string Name { get; set; }

        [Column(Column = 6)]
        public string OnCertificate { get; set; }

        [Column(Column = 7)]
        public string DocumentLink { get; set; }

    }
}