using System.Collections.Generic;
using CapaNavDoc.Models;
using CapaNavDoc.ViewModel;

namespace CapaNavDoc.Extensions
{
    public static class ViewModelsExtensions
    {
        public static User ToUser(this UserEditionViewModel model, string centerId)
        {
            return new User
            {
                Id = model.Id.ToInt32(),
                UserName = model.UserName,
                LastName = model.LastName,
                Password = model.Password,
                FirstName = model.FirstName,
                CenterId = centerId.ToInt32()
            };
        }

        public static UserDetailsViewModel ToUserDetailsViewModel(this User user, string centerName)
        {
            return new UserDetailsViewModel
            {
                Id = user.Id.ToString(),
                Password = user.Password,
                CenterName = centerName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
        }

        public static UserEditionViewModel ToUserEditionViewModel(this User user, List<Center> centers, string editionMode)
        {
            return new UserEditionViewModel
            {
                Id = user.Id.ToString(),
                LastName = user.LastName,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Password = user.Password,
                Centers = centers,
                EditionMode = editionMode,
                CenterId = user.CenterId.ToString()
            };
        }
    }
}
