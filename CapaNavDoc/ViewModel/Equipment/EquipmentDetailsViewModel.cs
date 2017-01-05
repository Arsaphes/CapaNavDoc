using CapaNavDoc.Classes;

namespace CapaNavDoc.ViewModel.Equipment
{
    public class EquipmentDetailsViewModel
    {
        public string Id { get; set; }

        [Column(Column = 1)]
        public string PartNumber { get; set; }

        [Column(Column = 2)]
        public string Manufacturer { get; set; }

        [Column(Column = 3)]
        public string Name { get; set; }

        [Column(Column = 4)]
        public string Type { get; set; }

        [Column(Column = 5)]
        public string Ata { get; set; }

        [Column(Column = 6)]
        public string ActivityField { get; set; }

        [Column(Column = 7)]
        public string MechanicsGroup { get; set; }

        [Column(Column = 8)]
        public string DocumentsReferences { get; set; }

        [Column(Column = 9)]
        public string DocumentsPartNumber { get; set; }

        //public string CenterName { get; set; }

        public string MonitoringDate { get; set; }
    }
}