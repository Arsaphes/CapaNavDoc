using CapaNavDoc.Models;
using CapaNavDoc.ViewModel.User;

namespace CapaNavDoc.Extensions.ViewModels
{
    public static class UserExtensions
    {
        public static UserCallViewModel ToUserCallViewModel(this User user)
        {
            if (user == null) return new UserCallViewModel();

            return new UserCallViewModel
            {
                UserId = user.Id.ToString(),
                UserCall = $"{user.FirstName} {user.LastName}"
            };
        }
    }
}