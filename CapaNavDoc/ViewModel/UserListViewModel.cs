using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class UserListViewModel : LoggedUserViewModel
    {
        public List<UserDetailsViewModel> UsersDetails { get; set; }
    }
}
