using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
    public class ActivityField
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
    }
}