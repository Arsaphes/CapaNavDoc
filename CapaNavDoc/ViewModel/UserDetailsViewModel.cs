using CapaNavDoc.Extensions;

namespace CapaNavDoc.ViewModel
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
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