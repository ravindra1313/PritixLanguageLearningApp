using PritixDictionary.Models;
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
    public class TestReportViewModel : ViewModelBase
    {
        public ICommand BackToHome { get; private set; }

        private string score;
        public string Score
        {
            get { return score; }
            set
            {
                if (score != value)
                {
                    score = value;
                    Notify("Score");
                }
            }
        }

        public TestReportViewModel()
        {
            BackToHome = new Command(GoBackToHome);
            UserProgress progress = App.PritixDB.GetUserProgressForCurrentDictionary().Result;
            Score = string.Format("{0}/{1}", progress.UserMarks, progress.TotalMarks);
        }

        private void GoBackToHome(object obj)
        {
            //I know i am not using navigation here, because i want to remove all stack and make user to start fresh.
            App.Current.MainPage = new NavigationPage(new HomeView());
        }
    }
}
