using CapaNavDoc.Classes;

namespace CapaNavDoc.ViewModel
{
    public class ActionDetailsViewModel
    {
        public string Id { get; set; }

        [Column(Column = 1)]
        public string Description { get; set; } 
    }
}