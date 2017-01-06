using System.Linq;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;
using CapaNavDoc.ViewModel.MaintenanceData;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class MaintenanceDataExtensions
    {
        public static MaintenanceDataMonitoringViewModel ToMaintenanceDataMonitoringViewModel(this MaintenanceData md)
        {
            BusinessLayer<User> bl = new BusinessLayer<User>(new CapaNavDocDal());

            return new MaintenanceDataMonitoringViewModel
            {
                MaintenanceDataId = md.Id.ToString(),
                SelectedUserCall = bl.GetList().FirstOrDefault(u => u.Id == md.MonitoringUserId).ToUserCallViewModel().UserCall,
                Date = md.MonitoringDate.ToString("dd-mm-yyyy"),
                Users = bl.GetList().Select(u => u.ToUserCallViewModel().UserCall).ToList()
            };
        }
    }
}