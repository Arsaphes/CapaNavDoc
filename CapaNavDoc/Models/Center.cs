using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
    public class Center
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string UserList { get; set; }
    }
}
