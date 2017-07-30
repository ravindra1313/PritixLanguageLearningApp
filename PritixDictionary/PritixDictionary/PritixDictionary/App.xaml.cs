using PritixDictionary.Database;
using PritixDictionary.Interfaces.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PritixDictionary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var users = PritixDB.GetUsersAsync().Result;
            if(users!=null && users.Count>0)
            {
                MainPage = new PritixDictionary.Views.SignInView();
            }
            else
            {
                MainPage = new PritixDictionary.Views.SignUpView();
            }
            
        }

        protected override void OnStart()
        {
            
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        static PritixDatabase pritixDB;

        public static PritixDatabase PritixDB
        {
            get
            {
                if (pritixDB == null)
                {
                    pritixDB = new PritixDatabase(DependencyService.Get<IDBFileHelper>().GetLocalDBFilePath("PritixSQLite.db3"));
                }
                return pritixDB;
            }
        }
    }
}
