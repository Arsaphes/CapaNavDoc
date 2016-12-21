using System.Collections.Generic;
using CapaNavDoc.Models;

namespace CapaNavDoc.ViewModel
{
    public class UserEditionViewModel:LoggedUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CenterId { get; set; }
        public string EditionMode { get; set; }
        public List<Center> Centers { get; set; }
    }
}