using System.Web;

namespace CapaNavDoc.ViewModel.MaintenanceData
{
    public class MaintenanceDataEditionViewModel
    {
        public string Id { get; set; }

        public string Type { get; set; }
        public string Sender { get; set; }
        public string Review { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string OnCertificate { get; set; }
        public string DocumentLink { get; set; }
        public HttpPostedFileBase FileUpload { get; set; }

        public string EditionMode { get; set; }
    }
}