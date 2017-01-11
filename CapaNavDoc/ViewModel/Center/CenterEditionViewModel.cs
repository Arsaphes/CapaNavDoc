using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.ViewModel.Center
{
    public class CenterEditionViewModel : MyLayoutViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "L'atelier est obligatoire.")]
        public string Name { get; set; }

        public string EditionMode { get; set; }        
    }
}