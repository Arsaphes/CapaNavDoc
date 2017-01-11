using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CapaNavDoc.ViewModel.MaintenanceData
{
    public class MaintenanceDataEditionViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Le type est obligatoire.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "L'émetteur est obligatoire.")]
        public string Sender { get; set; }

        [Required(ErrorMessage = "La référence document est obligatoire.")]
        public string DocumentReference { get; set; }

        public string DocumentPartNumber { get; set; }

        [Required(ErrorMessage = "La révision est obligatoire.")]
        public string Review { get; set; }

        [Required(ErrorMessage = "La date est obligatoire.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public string Date { get; set; }

        [Required(ErrorMessage = "La désignation est obligatoire.")]
        public string Name { get; set; }

        public string OnCertificate { get; set; }

        public HttpPostedFileBase FileUpload { get; set; }

        public string EditionMode { get; set; }
    }
}