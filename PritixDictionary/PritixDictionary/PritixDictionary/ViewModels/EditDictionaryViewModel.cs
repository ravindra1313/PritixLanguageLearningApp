using PritixDictionary.Models;
using PritixDictionary.Services;
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
    public class EditDictionaryViewModel:ViewModelBase
    {
        public ICommand AddKeyValue { get; private set; }
        public ICommand Save { get; private set; }
        
        INavigation Navigation { set; get; }

        private string primaryLanguage;
        public string PrimaryLanguage
        {
            get { return primaryLanguage; }
            set
            {
                if (primaryLanguage != value)
                {
                    primaryLanguage = value;
                    Notify("PrimaryLanguage");
                }
            }
        }
        private string translateLanguage;
        public string TranslateLanguage
        {
            get { return translateLanguage; }
            set
            {
                if (translateLanguage != value)
                {
                    translateLanguage = value;
                    Notify("TranslateLanguage");
                }
            }
        }
        private ObservableCollection<DictionaryDetail> dictionaryDetails = new ObservableCollection<DictionaryDetail>();
        public ObservableCollection<DictionaryDetail> DictionaryDetails
        {
            get { return dictionaryDetails; }
            set
            {
                if (value == dictionaryDetails) return;
                dictionaryDetails = value;
                Notify("DictionaryDetails");
            }
        }

        private Dictionary SelectedDictionary { get; set; }
        private UserDictionaryMapping UserSelectedDictionaryMapping { get; set; }


        public EditDictionaryViewModel(INavigation navigation,Dictionary selectedDictionary)
        {
            this.Navigation = navigation;
            AddKeyValue = new Command(AddBlankKeyValuePair);
            Save = new Command(SaveAndGoBack);
            SelectedDictionary = selectedDictionary;
            PrimaryLanguage = selectedDictionary.FromLanguage;
            TranslateLanguage = selectedDictionary.ToLanguage;
            var mapping = App.PritixDB.GetUserDictMappingByDictId(selectedDictionary.DictionaryId).Result;
            UserSelectedDictionaryMapping = mapping;
            List<DictionaryDetail> dictDetails = App.PritixDB.GetDictionaryDetailsByMappingIndex(mapping.MappingIndex).Result;
            DictionaryDetails = new ObservableCollection<DictionaryDetail>(dictDetails);
        }

        private void SaveAndGoBack(object obj)
        {
            var filteredList = DictionaryDetails.Where(x => x.Key != null && x.Value != null).ToList();
            //avoid case sensitivity
            //Convert all keys to to lower then check duplicacy
            var duplicateKeys = filteredList.Select(x=>x.Key.ToUpper()).ToList().GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            if(duplicateKeys !=null && duplicateKeys.Count!=0)
            {
                new DialogService().ShowError("Error!!", "Please remove duplicate keys from dictionary.","Ok",null);
            }
            else
            {
                var result = App.PritixDB.DeleteDetailsByMappingIndex(UserSelectedDictionaryMapping.MappingIndex).Result;                
                App.PritixDB.SaveAllDictionaryDetails(filteredList).Wait();
                var xxxx = App.PritixDB.GetDictionaryDetailsByMappingIndex(UserSelectedDictionaryMapping.MappingIndex);
                Navigation.PopModalAsync();
            }            
        }

        private void AddBlankKeyValuePair(object obj)
        {
            DictionaryDetails.Add(new DictionaryDetail() { MappingKey = UserSelectedDictionaryMapping.MappingIndex });
        }
    }
}
