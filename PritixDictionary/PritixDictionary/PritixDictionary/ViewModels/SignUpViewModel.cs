using System.Windows.Input;
using Xamarin.Forms;
using PritixDictionary.Utilities;
using PritixDictionary.Models;
using System;
using PritixDictionary.Views;
using PritixDictionary.Services;

namespace PritixDictionary.ViewModels
{
    public class SignUpViewModel :ViewModelBase
    {
        public ICommand SignUpCommand { get; private set; }
        public ICommand SignInCommand { get; private set; }
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
                    Notify("Email");
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
        public SignUpViewModel()
        {
            SignUpCommand = new Command(SignUp);
            SignInCommand = new Command(SignIn);
        }

        private void SignIn(object obj)
        {
            App.Current.MainPage = new SignInView();
        }

        void SignUp()
        {
            if(IsValidEmail)
            {
                if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    new DialogService().ShowError("Error !!", AppConstants.ERROR_MESSAGE_ALL_FIELDS_REQUIRED, "Ok", null);
                }
                else
                {
                   string user =  App.PritixDB.GetUserNameAsync(Email, Password);
                    if(string.IsNullOrEmpty(user))
                    {
                        User newUser = new User()
                        {
                            Name = UserName,
                            Email = Email,
                            Password = Password //We can encrypt it if required
                        };
                        App.PritixDB.SaveUserAsync(newUser).Wait();
                        var recentUser = App.PritixDB.GetUserByMailIdAndPassword(newUser.Email, newUser.Password).Result;
                        App.PritixDB.CurrentLoggedInUser = recentUser;
                        App.PritixDB.AddDefaultDictionary();
                        App.Current.MainPage = new NavigationPage(new HomeView());
                    }
                    else
                    {
                        new DialogService().ShowError("Error !!", AppConstants.ERROR_MESSAGE_USER_ALREADY_EXIST, "Ok", null);
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
