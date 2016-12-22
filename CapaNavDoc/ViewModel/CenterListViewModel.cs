using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class CenterListViewModel : LoggedUserViewModel
    {
        public List<CenterDetailsViewModel> CentersDetails { get; set; }
    }
}