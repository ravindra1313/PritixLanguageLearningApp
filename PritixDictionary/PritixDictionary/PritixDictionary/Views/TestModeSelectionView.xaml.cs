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
    public partial class TestModeSelectionView : ContentPage
    {
        TestModeSelectionViewModel testModeSelectionViewModel;
        public TestModeSelectionView(IEnumerable<DictionaryDetail> dictionaryInfo)
        {
            InitializeComponent();
            this.Title = "Choose Test Mode";
            NavigationPage.SetHasBackButton(this,true);
            testModeSelectionViewModel = new TestModeSelectionViewModel(this.Navigation, new List<DictionaryDetail>(dictionaryInfo));
            this.BindingContext = testModeSelectionViewModel;
        }
    }
}