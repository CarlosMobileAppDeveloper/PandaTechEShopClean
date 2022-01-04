using System;
using System.Threading.Tasks;
using PandaTechEShop.Constants;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Models.Complaint;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Complaint;
using PandaTechEShop.ViewModels.Base;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.ContactUs
{
    public class ContactUsFormPageViewModel : BaseViewModel
    {
        private readonly IComplaintService _complaintService;

        public ContactUsFormPageViewModel(
            IBaseService baseService,
            IComplaintService complaintService)
            : base(baseService)
        {
            Title = "Contact Us";
            _complaintService = complaintService;
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            SendComplaintCommand = new AsyncCommand(ExecuteSendComplaintCommandAsync, allowsMultipleExecutions: false);
        }

        public IAsyncCommand NavigateBackCommand { get; }
        public IAsyncCommand SendComplaintCommand { get; }

        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ComplaintTitle { get; set; }
        public string ComplaintMessage { get; set; }

        private async Task ExecuteSendComplaintCommandAsync()
        {
            FullName?.Trim();
            EmailAddress?.Trim();
            PhoneNumber?.Trim();
            ComplaintTitle?.Trim();
            ComplaintMessage?.Trim();

            if (!string.IsNullOrEmpty(FullName)
                && !string.IsNullOrEmpty(EmailAddress)
                && !string.IsNullOrEmpty(PhoneNumber)
                && !string.IsNullOrEmpty(ComplaintTitle)
                && !string.IsNullOrEmpty(ComplaintMessage))
            {
                var complaint = new ComplaintInfo
                {
                    FullName = FullName,
                    Email = EmailAddress,
                    PhoneNumber = PhoneNumber,
                    Title = ComplaintTitle,
                    Description = ComplaintMessage,
                };

                var response = await _complaintService.RegisterComplaintAsync(complaint);

                if (response)
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Complaint Sent"));
                    await Task.Delay(500);
                    await NavigationService.NavigateAsync($"{NavigationConstants.RootNavigationPage}/{NavigationConstants.HomePage}");
                }
                else
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Something went wrong. Failed to send complaint"));
                }
            }
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
