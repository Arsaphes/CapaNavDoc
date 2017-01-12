using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.ViewModel.Equipment
{
    public class EquipmentEditionViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Le Part Number est obligatoire.")]
        public string PartNumber { get; set; }

        [Required(ErrorMessage = "Le constructeur est obligatoire.")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "La désignation est obligatoire.")]
        public string Name { get; set; }

        public string Type { get; set; }

        [Required(ErrorMessage = "Le code ATA est obligatoire.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Le code ATA doit être un nombre.")]
        public string Ata { get; set; }

        public string ActivityFieldId { get; set; }
        public List<string> ActivityFieldDescriptions { get; set; }

        [Required(ErrorMessage = "La famille de technicien est obligatoire.")]
        public string MechanicsGroup { get; set; }

        public string MaintenanceDataId { get; set; }
        public List<string> DocumentsReferences { get; set; }

        public string EditionMode { get; set; }
    }
}