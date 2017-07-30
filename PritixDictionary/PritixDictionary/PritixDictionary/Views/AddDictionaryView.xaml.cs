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
    public partial class AddDictionaryView : ContentPage
    {
        AddDictionaryViewModel addDictionaryViewModel;
        public AddDictionaryView()
        {
            InitializeComponent();
            addDictionaryViewModel = new AddDictionaryViewModel(this.Navigation);
            this.BindingContext = addDictionaryViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            addDictionaryViewModel.UpdateLanguagesList();
            addDictionaryViewModel.PrimaryLangSelectedIndex = 0;
            addDictionaryViewModel.TranslateLangSelectedIndex = 0;
        }
    }
}