using System.ComponentModel.DataAnnotations;
using CapaNavDoc.Validation;

namespace CapaNavDoc.ViewModel.User
{
    public class UserEditionViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [ClonedUser(FirstNameProperty = "FirstName", LastNameProperty = "LastName", ErrorMessage = "Cet utilisateur existe déjà.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "L'identifiant est obligatoire.")]
        [ClonedUserName(ErrorMessage = "Cet identifiant existe déjà.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        public string Password { get; set; }

        public string EditionMode { get; set; }
    }
}