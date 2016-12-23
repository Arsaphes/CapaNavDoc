using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class CenterUsersViewModel : MyLayoutViewModel
    {
        public string CenterId { get; set; }
        public string CenterName { get; set; }  
        public List<UserDetailsViewModel> CenterUsersDetails { get; set; }
        public List<UserDetailsViewModel> UsersDetails { get; set; }
    }
}