using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
        //Todo: Implement IsActive.
        public bool IsActive { get; set; }
    }
}
