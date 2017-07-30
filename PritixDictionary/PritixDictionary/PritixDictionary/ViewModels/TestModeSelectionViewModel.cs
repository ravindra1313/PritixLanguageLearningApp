using PritixDictionary.Managers;
using PritixDictionary.Models;
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
    public class TestModeSelectionViewModel : ViewModelBase
    {
        INavigation Navigation { set; get; }
        public ICommand Start { get; private set; }
        List<DictionaryDetail> dictDetails { set; get; }
        private string testQuestionCount;
        public string SelectedTestQuestionCount
        {
            get { return testQuestionCount; }
            set
            {
                if (value == testQuestionCount) return;
                testQuestionCount = value;
                Notify("SelectedTestQuestionCount");
            }
        }
        private string testType;
        public string SelectedTestType
        {
            get { return testType; }
            set
            {
                if (value == testType) return;
                testType = value;
                Notify("SelectedTestType");
            }
        }
        private List<int> countSelectionList;
        public List<int> CountSelectionList
        {
            get { return countSelectionList; }
            set
            {
                if (value == countSelectionList) return;
                countSelectionList = value;
                Notify("CountSelectionList");
            }
        }


        private List<string> typeSelectionList;
        public List<string> TypeSelectionList
        {
            get { return typeSelectionList; }
            set
            {
                if (value == typeSelectionList) return;
                typeSelectionList = value;
                Notify("TypeSelectionList");
            }
        }
        public TestModeSelectionViewModel(INavigation navigation, List<DictionaryDetail> dictionaryInfo)
        {
            this.Navigation = navigation;
            Start = new Command(StartTest);
            dictDetails = dictionaryInfo;
            CountSelectionList = Enumerable.Range(1, dictDetails.Count).ToList();
            SelectedTestQuestionCount = CountSelectionList.ElementAt(0).ToString();            
            TypeSelectionList = AppConstants.TEST_TYPES;
            SelectedTestType = TypeSelectionList.ElementAt(0).ToString();
        }

        private void StartTest(object obj)
        {
            TestManager.GetInstance().ConfigureTest(dictDetails, Convert.ToInt32(SelectedTestQuestionCount), SelectedTestType);
            Navigation.PushModalAsync(new TestView());
        }
    }
}
