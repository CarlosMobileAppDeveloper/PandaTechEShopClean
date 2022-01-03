using System.Threading.Tasks;
using PandaTechEShop.Helpers;
using XF.Material.Forms.UI.Dialogs;

namespace PandaTechEShop.Utilities.Dialog
{
    public class DialogService : IDialogService
    {
        private readonly IMaterialDialog _materialDialog;

        public DialogService()
        {
            _materialDialog = MaterialDialog.Instance;
        }
        
        public Task<IMaterialModalPage> ShowLoadingDialogAsync(string message)
        {
            return _materialDialog.LoadingDialogAsync(message: message, configuration: MaterialStylesConfigurations.LoadingDialogConfiguration);
        }

        public Task ShowSnackbarAsync(string message, int duration = MaterialSnackbar.DurationLong)
        {
            return _materialDialog.SnackbarAsync(message: message, msDuration: duration, configuration: MaterialStylesConfigurations.SnackbarConfiguration);
        }
    }
}