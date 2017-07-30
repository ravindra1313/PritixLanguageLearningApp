using PritixDictionary.Models;
using PritixDictionary.Services;
using PritixDictionary.Utilities;
using PritixDictionary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PritixDictionary.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        public ICommand SignInCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    Notify("UserName");
                }

            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                }

            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    Notify("Password");
                }
            }
        }

        private bool isValidEmail;
        public bool IsValidEmail
        {
            get { return isValidEmail; }
            set
            {
                if (isValidEmail != value)
                {
                    isValidEmail = value;
                    Notify("IsValidEmail");
                }
            }
        }

        public SignInViewModel()
        {
            SignInCommand = new Command(SignIn);
            SignUpCommand = new Command(SignUp);
        }

        private void SignUp(object obj)
        {
            App.Current.MainPage = new SignUpView();
        }

        void SignIn()
        {
            if (IsValidEmail)
            {
                if (string.IsNullOrEmpty(Password))
                {
                    new DialogService().ShowError("Error !!", AppConstants.ERROR_MESSAGE_ALL_FIELDS_REQUIRED, "Ok", null);
                }
                else
                {
                    var userInfo = App.PritixDB.GetUserByMailIdAndPassword(Email, Password).Result;
                    if (userInfo != null)
                    {
                        App.PritixDB.CurrentLoggedInUser = userInfo;
                        var allprogress = App.PritixDB.GetCurrentUserProgress().Result;
                        UserProgress latestProgress = App.PritixDB.GetUserLatestProgress().Result;
                        if(latestProgress!=null)
                        {
                            Dictionary lastViewedDictionary = App.PritixDB.GetDictionaryById(latestProgress.DictionaryId).Result;
                            App.PritixDB.CurrentDictionaryInUse = lastViewedDictionary;
                            App.Current.MainPage = new NavigationPage(new DictionaryDetailsView());
                        }
                        else
                        {
                            App.Current.MainPage = new NavigationPage(new HomeView());
                        }
                        
                    }
                    else
                    {
                        new DialogService().ShowError("Error !!", AppConstants.ERROR_MESSAGE_USER_SIGN_IN_FAILED, "Ok", null);
                    }
                }
            }
            else
            {
                new DialogService().ShowError("Error!", AppConstants.ERROR_MESSAGE_EMAIL_VALIDATION, "Ok", null);
            }
        }
    }
}
