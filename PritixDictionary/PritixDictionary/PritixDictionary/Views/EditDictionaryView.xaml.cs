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
    public partial class EditDictionaryView : ContentPage
    {
        EditDictionaryViewModel editDictionaryViewModel;
        public EditDictionaryView(Dictionary selectedDictionary)
        {
            InitializeComponent();
            this.Title = "Edit Dictionary";
            NavigationPage.SetHasBackButton(this, true);
            editDictionaryViewModel = new EditDictionaryViewModel(this.Navigation, selectedDictionary);
            this.BindingContext = editDictionaryViewModel;
        }
    }
}