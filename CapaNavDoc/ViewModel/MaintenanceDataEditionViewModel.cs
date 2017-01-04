namespace CapaNavDoc.ViewModel
{
    public class MaintenanceDataEditionViewModel
    {
        public string Id { get; set; }

        public string Type { get; set; }
        public string Sender { get; set; }
        public string Review { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public bool OnCertificate { get; set; }
        public string DocumentLink { get; set; }

        public string EditionMode { get; set; }
    }
}