using PritixDictionary.Models;
using PritixDictionary.Services;
using PritixDictionary.Utilities;
using PritixDictionary.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PritixDictionary.ViewModels
{
    public class DictionaryDetailsViewModel : ViewModelBase
    {
        INavigation Navigation { set; get; }
        public ICommand StartTest { get; private set; }
        private ObservableCollection<DictionaryDetail> dictionayDetails = new ObservableCollection<DictionaryDetail>();
        public ObservableCollection<DictionaryDetail> DictionaryDetails
        {
            get { return dictionayDetails; }
            set
            {
                if (value == dictionayDetails) return;
                dictionayDetails = value;
                Notify("DictionaryDetails");
            }
        }
        private string score;
        public string Score
        {
            get { return score; }
            set
            {
                if (value == score) return;
                score = value;
                Notify("Score");
            }
        }
        private bool scoreVisibility;
        public bool ScoreVisibility
        {
            get { return scoreVisibility; }
            set
            {
                if (value == scoreVisibility) return;
                scoreVisibility = value;
                Notify("ScoreVisibility");
            }
        }

        public DictionaryDetailsViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            StartTest = new Command(StartPracticeTest);
        }

       

        public void UpdateDictionaryDetails()
        {
            var mapping = App.PritixDB.GetUserDictMappingByDictId(App.PritixDB.CurrentDictionaryInUse.DictionaryId).Result;
            List<DictionaryDetail> dictDetails = App.PritixDB.GetDictionaryDetailsByMappingIndex(mapping.MappingIndex).Result;
            DictionaryDetails = new ObservableCollection<DictionaryDetail>(dictDetails);
            try
            {
               
                UserProgress progress = App.PritixDB.GetUserProgressForCurrentDictionary().Result;
                if (progress != null)
                {
                    ScoreVisibility = true;
                    Score = string.Format("Your Latest Score is {0}/{1}", progress.UserMarks, progress.TotalMarks);
                }
            }
            catch (Exception e)
            {

            }
            
        }
        private void StartPracticeTest(object obj)
        {
            if (DictionaryDetails.Count >= AppConstants.MINIMUM_QUESTIONS_REQUIRED_FOR_TEST)
            {
                Navigation.PushAsync(new TestModeSelectionView(DictionaryDetails));
            }
            else
            {
                new DialogService().ShowMessage("Sorry !!", "You should have atleast 5 words in your dictionary to start the test.");
            }
        }


    }
}
