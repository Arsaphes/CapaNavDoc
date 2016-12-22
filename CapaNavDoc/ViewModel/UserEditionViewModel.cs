namespace CapaNavDoc.ViewModel
{
    public class UserEditionViewModel : LoggedUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EditionMode { get; set; }
    }
}