namespace CapaNavDoc.ViewModel.User
{
    public class UserEditionViewModel : MyLayoutViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string EditionMode { get; set; }
    }
}