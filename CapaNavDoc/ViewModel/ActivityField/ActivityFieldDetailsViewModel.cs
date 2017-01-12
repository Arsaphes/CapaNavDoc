using CapaNavDoc.Classes;

namespace CapaNavDoc.ViewModel.ActivityField
{
    public class ActivityFieldDetailsViewModel
    {
        public string Id { get; set; }

        [Column(Column = 1)]
        public string Description { get; set; }
    }
}