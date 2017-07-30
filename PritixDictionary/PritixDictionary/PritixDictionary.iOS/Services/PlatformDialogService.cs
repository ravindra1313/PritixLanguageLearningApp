using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using PritixDictionary.iOS.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using PritixDictionary.Interfaces.ServiceInterface;

[assembly: Dependency(typeof(PlatformDialogService))]
namespace PritixDictionary.iOS.Services
{
    public class PlatformDialogService : IPlatformDialogService
    {
        readonly List<UIAlertView> _openDialogs = new List<UIAlertView>();

        public void CloseAllDialogs()
        {
            foreach (var dialog in _openDialogs)
            {
                dialog.DismissWithClickedButtonIndex(-1, false); // Cancel and don't do ANYTHING
            }
            _openDialogs.Clear();
        }

        public async Task<bool> ShowAlert(string title, string content, string okButton, Action callback)
        {
            return await Task.Run(() => Alert(title, content, okButton, callback));
        }

        public async Task<bool> ShowAlertConfirm(string title, string content, string confirmButton, string cancelButton, Action<bool> callback)
        {
            return await Task.Run(() => AlertConfirm(title, content, confirmButton, cancelButton, callback));
        }

        private bool Alert(string title, string content, string okButton, Action callback)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var alert = new UIAlertView(title, content, null, okButton);
                _openDialogs.Add(alert);
                alert.Clicked += (sender, buttonArgs) =>
                {
                    if (!Equals(callback, null))
                    {
                        callback();
                    }
                    _openDialogs.Remove(alert);
                };
                alert.Show();
            });

            return true;
        }

        private bool AlertConfirm(string title, string content, string confirmButton, string cancelButton, Action<bool> callback)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var alert = new UIAlertView(title, content, null, confirmButton, cancelButton);
                _openDialogs.Add(alert);
                alert.Clicked += (sender, buttonArgs) =>
                {
                    callback(buttonArgs.ButtonIndex == 0);
                    _openDialogs.Remove(alert);
                };
                alert.Show();
            });

            return true;
        }
    }
}