using PandaTechEShop.Services;
using PandaTechEShop.ViewModels.Base;

namespace PandaTechEShop.ViewModels.Account
{
    public class CreateAccountPageViewModel : BaseViewModel
    {
        public CreateAccountPageViewModel(IBaseService baseService)
            : base(baseService)
        {
            Title = "Welcome to Xamarin.Forms with PRISM!";
        }
    }
}
