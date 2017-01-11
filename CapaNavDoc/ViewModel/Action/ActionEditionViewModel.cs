using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.ViewModel.Action
{
    public class ActionEditionViewModel : MyLayoutViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "L'atelier est obligatoire.")]
        public string Description { get; set; }

        public string EditionMode { get; set; }
    }
}