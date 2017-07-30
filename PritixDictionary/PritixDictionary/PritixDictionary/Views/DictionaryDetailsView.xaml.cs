using PritixDictionary.Models;
using PritixDictionary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PritixDictionary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DictionaryDetailsView : ContentPage
    {
        DictionaryDetailsViewModel dictionaryDetailsViewModel;
        public DictionaryDetailsView()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
            var activeDictionary = App.PritixDB.CurrentDictionaryInUse;
            this.Title = activeDictionary.FromLanguage+" to "+activeDictionary.ToLanguage;
            dictionaryDetailsViewModel = new DictionaryDetailsViewModel(this.Navigation);
            dictionaryDetailsViewModel.UpdateDictionaryDetails();
            this.BindingContext = dictionaryDetailsViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();            
        }
        protected override bool OnBackButtonPressed()
        {
            if(Navigation.NavigationStack.Count==1)
            {
                App.Current.MainPage = new NavigationPage(new HomeView());
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
            
        }
    }
}