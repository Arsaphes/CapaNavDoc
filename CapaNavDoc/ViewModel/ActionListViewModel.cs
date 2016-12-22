using System.Collections.Generic;

namespace CapaNavDoc.ViewModel
{
    public class ActionListViewModel : LoggedUserViewModel
    {
        public List<ActionDetailsViewModel> ActionsDetails { get; set; }
    }
}