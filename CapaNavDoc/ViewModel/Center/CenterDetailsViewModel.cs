using CapaNavDoc.Classes;

namespace CapaNavDoc.ViewModel.Center
{
    public class CenterDetailsViewModel
    {
        public string Id { get; set; }

        [Column(Column = 2)]
        public string Name { get; set; }
    }
}