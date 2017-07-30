using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PritixDictionary.Droid.Services;
using PritixDictionary.Interfaces.ServiceInterface;

[assembly: Dependency(typeof(PlatformDialogService))]
namespace PritixDictionary.Droid.Services
{
    public class PlatformDialogService : IPlatformDialogService
    {
        List<AlertDialog> _openDialogs = new List<AlertDialog>();
        public void CloseAllDialogs()
        {
            foreach (var dialog in _openDialogs)
            {
                dialog.Dismiss();
            }
            _openDialogs.Clear();
        }

        public async Task<bool> ShowAlert(string title, string content, string okButton, Action callback)
        {
            return await Task.Run(() => Alert(title, content, okButton, callback));
        }

        public async Task<bool> ShowAlertConfirm(string title, string content, string confirmButton, string cancelButton,
           Action<bool> callback)
        {
            return await Task.Run(() => AlertConfirm(title, content, confirmButton, cancelButton, callback));
        }
        //  (string title, string content, string okButton, Action callback)
        bool Alert(string title, string content, string okButton, Action callback)
        {
            var alert = new AlertDialog.Builder(Forms.Context);
            alert.SetTitle(title);
            alert.SetMessage(content);
            alert.SetNegativeButton(okButton, (sender, e) =>
            {
                if (!Equals(callback, null))
                {
                    callback();
                }
                _openDialogs.Remove((AlertDialog)sender);
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                var dialog = alert.Show();
                _openDialogs.Add(dialog);
                dialog.SetCanceledOnTouchOutside(false);
                dialog.SetCancelable(false);
            });

            return true;
        }

        private bool AlertConfirm(string title, string content, string confirmButton, string cancelButton,
            Action<bool> callback)
        {
            var alert = new AlertDialog.Builder(Forms.Context);
            alert.SetTitle(title);
            alert.SetMessage(content);
            alert.SetPositiveButton(confirmButton, (sender, e) =>
            {
                callback(true);
                _openDialogs.Remove((AlertDialog)sender);
            });
            alert.SetNegativeButton(cancelButton, (sender, e) =>
            {
                callback(false);
                _openDialogs.Remove((AlertDialog)sender);
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                var dialog = alert.Show();
                _openDialogs.Add(dialog);
                dialog.SetCanceledOnTouchOutside(false);
                dialog.SetCancelable(false);
            });

            return true;
        }
    }

}