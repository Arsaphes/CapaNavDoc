namespace CapaNavDoc.ViewModel.Equipment
{
    public class EquipmentCenterActionViewModel
    {
        public string EquipmentId { get; set; }
        public string[] CenterNames { get; set; }
        public string[] ActionNames { get; set; }
        public bool[][] Selections { get; set; }
    }
}