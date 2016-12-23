using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
    public class Center
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
