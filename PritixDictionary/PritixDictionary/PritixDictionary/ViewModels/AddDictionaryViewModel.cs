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
using static PritixDictionary.Utilities.AppConstants;

namespace PritixDictionary.ViewModels
{
    public class AddDictionaryViewModel:ViewModelBase
    {
        INavigation Navigation { set; get; }
        public ICommand SaveDictionary { get; private set; }
        public ICommand GoHome { get; private set; }
        private ObservableCollection<String> languages = new ObservableCollection<String>();
        public ObservableCollection<String> Languages
        {
            get { return languages; }
            set
            {
                if (value == languages) return;
                languages = value;
                Notify("Languages");
            }
        }

        private int primaryLangSelectedIndex = -1;
        public int PrimaryLangSelectedIndex
        {
            get { return primaryLangSelectedIndex; }
            set
            {
                if (value == primaryLangSelectedIndex) return;
                primaryLangSelectedIndex = value;
                Notify("PrimaryLangSelectedIndex");
            }
        }
        private int translateLangSelectedIndex = -1;
        public int TranslateLangSelectedIndex
        {
            get { return translateLangSelectedIndex; }
            set
            {
                if (value == translateLangSelectedIndex) return;
                translateLangSelectedIndex = value;
                Notify("TranslateLangSelectedIndex");
            }
        }
        private ObservableCollection<String> translateLanguages = new ObservableCollection<String>();
        public ObservableCollection<String> TranslateLanguages
        {
            get { return translateLanguages; }
            set
            {
                if (value == translateLanguages) return;
                translateLanguages = value;
                Notify("TranslateLanguages");
            }
        }

        public AddDictionaryViewModel(INavigation Navigation)
        {
           SaveDictionary = new Command(SaveNewDictionaryToDB);
            GoHome = new Command(GoBackToHome);
            this.Navigation = Navigation;

        }

        public void UpdateLanguagesList()
        {
            Languages = new ObservableCollection<string>(Enum.GetNames(typeof(Languages)));
        }

        public void SaveNewDictionaryToDB()
        {
            //Get primary and translated language selected by User
            string fromLanguage = Languages.ElementAt(PrimaryLangSelectedIndex);
            string toLanguage = Languages.ElementAt(translateLangSelectedIndex);
            Dictionary dict = App.PritixDB.GetDictionaryByLanguage(fromLanguage, toLanguage).Result;
            if(dict==null)
            {
                //Save dictionary to DB
               App.PritixDB.SaveDictionaryItemAsync(new Dictionary() {FromLanguage = fromLanguage,ToLanguage = toLanguage }).Wait();
                Dictionary lastSavedDictionary = App.PritixDB.GetDictionaryByLanguage(fromLanguage, toLanguage).Result;
                var xxxx = App.PritixDB.GetAllDictionariesByUserId(App.PritixDB.CurrentLoggedInUser.UserId).Result;

                App.PritixDB.SaveUserDictionaryMapping(new UserDictionaryMapping() { DictionaryId = lastSavedDictionary.DictionaryId, UserId = App.PritixDB.CurrentLoggedInUser.UserId}).Wait();
                
                new DialogService().ShowMessage("Success!!", "Your dictionary is successfully saved.", "OK", GoBackToHome);
            }
            else
            {
                //Check wether User mapping with this dictionary present or not
                var userDictMapping = App.PritixDB.GetUserDictMappingByDictId(dict.DictionaryId).Result;
                //Means mapping not exist but dictionary exist(Which is added by other user)
                if (userDictMapping == null)
                {
                    try
                    {
                        var xx = App.PritixDB.GetAllDictionariesByUserId(App.PritixDB.CurrentLoggedInUser.UserId).Result;
                        var mapping = new UserDictionaryMapping() {DictionaryId = dict.DictionaryId, UserId = App.PritixDB.CurrentLoggedInUser.UserId };
                        int mapperIndex = App.PritixDB.SaveUserDictionaryMapping(mapping).Result;
                        if (mapperIndex != 0)
                        {
                            new DialogService().ShowMessage("Success!!", "Your dictionary is successfully saved.", "OK", GoBackToHome);
                        }
                        else
                        {
                            new DialogService().ShowMessage("Error!!", "Something went wrong while mapping user with dictionary.", "OK", null);
                        }
                    }
                    catch (Exception e)
                    {

                    }                    
                }
                else
                {
                    new DialogService().ShowMessage("Error!!", "This Dictionary already exist.Please choose another.","OK",null);
                }
               
            }

        }
         
        void GoBackToHome()
        {
            this.Navigation.PopModalAsync();
        }

    }
}
