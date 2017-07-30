using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Interfaces.ServiceInterface
{
    public interface IDialogService
    {
        void CloseAllDialogs();

        Task ShowError(string title, string message, string buttonText, Action afterHideCallback);
        Task ShowError(string title, Exception error, string buttonText, Action afterHideCallback);
        Task ShowMessage(string title, string message);
        Task ShowMessage(string title, string message, string buttonText, Action afterHideCallback);
        Task<bool> ShowMessage(string title, string message, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback);
    }

    public interface IPlatformDialogService
    {
        void CloseAllDialogs();

        Task<bool> ShowAlert(string title, string content, string okButton, Action callback);
        Task<bool> ShowAlertConfirm(string title, string content, string confirmButton, string cancelButton,
                                    Action<bool> callback);
    }
}
