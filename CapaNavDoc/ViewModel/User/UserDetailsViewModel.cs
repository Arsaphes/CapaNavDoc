using CapaNavDoc.Classes;
using CapaNavDoc.Extensions;

namespace CapaNavDoc.ViewModel.User
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; } 

        [Column(Column = 1)]
        public string FirstName { get; set; }

        [Column(Column = 2)]
        public string LastName { get; set; }

        [Column(Column = 3)]
        public string UserName { get; set; }

        public string Password { get; set; }


        public override bool Equals(object obj)
        {
            UserDetailsViewModel vm = (UserDetailsViewModel)obj;
            return vm != null && vm.Id == Id;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.ToInt32();
        }
    }
}