using System.Collections.Generic;
using CapaNavDoc.ViewModel.Action;
using CapaNavDoc.ViewModel.Center;

namespace CapaNavDoc.ViewModel.Equipment
{
    public class EquipmentCenterViewModel : MyLayoutViewModel
    {
        public string EquipmentId { get; set; }
        public List<CenterDetailsViewModel> Centers { get; set; }
        public List<ActionDetailsViewModel> Actions { get; set; }
        public bool[][] Selections { get; set; }
        public string TableColumns { get; set; }
    }
}