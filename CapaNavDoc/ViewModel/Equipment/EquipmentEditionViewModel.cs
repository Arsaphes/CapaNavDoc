using System.Collections.Generic;

namespace CapaNavDoc.ViewModel.Equipment
{
    public class EquipmentEditionViewModel
    {
        public string Id { get; set; }
        public string PartNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Ata { get; set; }
        public string ActivityField { get; set; }
        public string MechanicsGroup { get; set; }
        public string MaintenanceDataId { get; set; }
        public List<string> DocumentsReferences { get; set; }

        public string EditionMode { get; set; }
    }
}