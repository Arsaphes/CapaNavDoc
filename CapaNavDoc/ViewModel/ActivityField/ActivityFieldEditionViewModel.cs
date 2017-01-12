using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.ViewModel.ActivityField
{
    public class ActivityFieldEditionViewModel : MyLayoutViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Le domaine d'activité est obligatoire.")]
        public string Description { get; set; }

        public string EditionMode { get; set; }
    }
}