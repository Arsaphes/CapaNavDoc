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


        public override bool Equals(object obj)
        {
            User u = obj as User;
            return u?.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
