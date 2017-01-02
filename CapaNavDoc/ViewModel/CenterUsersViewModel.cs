using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class CenterUsersViewModel : MyLayoutViewModel
    {
        public string CenterId { get; set; }
        public List<CenterUserViewModel> CenterUsersDetails { get; set; }
    }
}