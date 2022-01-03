using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace PandaTechEShop.Utilities.Dialog
{
    public interface IDialogService
    {
        Task<IMaterialModalPage> ShowLoadingDialogAsync(string message);
        Task ShowSnackbarAsync(string message, int duration = MaterialSnackbar.DurationLong);
    }
}