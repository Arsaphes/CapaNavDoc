using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
    public class Action
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
    }
}