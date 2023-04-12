using SocialNetwork.ViewModels.Account;

namespace SocialNetwork.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel LoginModel { get; set; }
        public RegisterViewModel RegisterModel { get; set; }

        public MainViewModel()
        {
            LoginModel = new LoginViewModel();
            RegisterModel = new RegisterViewModel();
        }
    }

}
