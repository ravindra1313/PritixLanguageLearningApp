using PritixDictionary.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PritixDictionary.Services
{
    public class DialogService : IDialogService
    {
        private readonly IPlatformDialogService _platformDialogService;

        public DialogService()
        {
            _platformDialogService = DependencyService.Get<IPlatformDialogService>();
        }

        public void CloseAllDialogs()
        {
            _platformDialogService.CloseAllDialogs();
        }

        public async Task ShowError(string title, string message, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() => _platformDialogService.ShowAlert(title, message, buttonText, afterHideCallback));
        }

        public async Task ShowError(string title, Exception error, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() => _platformDialogService.ShowAlert(title, error.Message, buttonText, afterHideCallback));
        }

        public async Task ShowMessage(string title, string message)
        {
            await Task.Run(() => _platformDialogService.ShowAlert(title, message, "OK", null));
        }

        public async Task ShowMessage(string title, string message, string buttonText, Action afterHideCallback)
        {
            var result = await Task.Run(() => _platformDialogService.ShowAlert(title, message, buttonText, afterHideCallback));
        }

        public async Task<bool> ShowMessage(string title, string message, string buttonConfirmText, string buttonCancelText,
                                            Action<bool> afterHideCallback)
        {
            var result = await Task.Run(() =>
                                _platformDialogService.ShowAlertConfirm(title, message, buttonConfirmText, buttonCancelText, afterHideCallback));
            return result;
        }
    }
}
