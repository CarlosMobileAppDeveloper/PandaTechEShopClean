using System;
using PropertyChanged;

namespace PandaTechEShop.ViewModels.Popups
{
    [AddINotifyPropertyChangedInterface]
    public class ToastPopupViewModel
    {
        public ToastPopupViewModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}