using PritixDictionary.Models;
using PritixDictionary.Services;
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
    public class HomeViewModel:ViewModelBase
    {
        INavigation Navigation { set; get; }
        public ICommand AddDictionary { get; private set; }
        public ICommand EditDictionary { get; private set; }
        public ICommand DeleteDictionary { get; private set; }
        private ObservableCollection<Dictionary> dictionaryList = new ObservableCollection<Dictionary>();
        public ObservableCollection<Dictionary> DictionaryList
        {
            get { return dictionaryList; }
            set
            {
                if (value == dictionaryList) return;
                dictionaryList = value;
                Notify("DictionaryList");
            }
        }

        private Dictionary selectedItem = new Dictionary();
        public Dictionary SelectedDictionaryItem
        {
            get { return selectedItem; }
            set
            {
                if (value == selectedItem) return;
                selectedItem = value;
                Notify("SelectedDictionaryItem");
                OnItemSelectionChanged();
            }
        }


        public HomeViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            AddDictionary = new Command(ShowDictionaryAdditionView);
            EditDictionary = new Command(ShowDictionaryEditView);
            DeleteDictionary = new Command(DeleteDictionaryAndRefreshList);
        }

        //private void DisplayUserRecentProgress()
        //{
        //   List<UserProgress> userProgress = App.PritixDB.GetCurrentUserProgress().Result;
        //    if(userProgress!=null && userProgress.Count>0)
        //    {
        //       UserProgress userRecentProgress = userProgress.OrderByDescending(x => x.ScoreTime).First();
        //        Dictionary dictionaryInfo = App.PritixDB.GetDictionaryById(userRecentProgress.DictionaryId).Result;
        //        new DialogService().ShowMessage()
        //    }


        //}

        public void UpdateContentList()
        {
            var userDictionaryIds = App.PritixDB.GetAllDictionariesByUserId(App.PritixDB.CurrentLoggedInUser.UserId).Result.Select(x=>x.DictionaryId).ToList();
            var userDictionaries = App.PritixDB.GetDictionaries().Result.Where(t => userDictionaryIds.Contains(t.DictionaryId));
            DictionaryList = new ObservableCollection<Dictionary>(userDictionaries); 
        }

        void ShowDictionaryAdditionView()
        {
            Navigation.PushModalAsync(new NavigationPage(new AddDictionaryView()));
        }
        void ShowDictionaryEditView(object args)
        {
            Navigation.PushModalAsync(new NavigationPage(new EditDictionaryView((Dictionary)args)));
        }
        void DeleteDictionaryAndRefreshList(object args)
        {
            //To Do

        }

        void OnItemSelectionChanged()
        {
            App.PritixDB.CurrentDictionaryInUse = SelectedDictionaryItem;
            Navigation.PushAsync(new DictionaryDetailsView());
        }
        
    }
}
