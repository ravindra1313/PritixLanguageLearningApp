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
    public partial class TestView : ContentPage
    {
        TestViewModel testViewModel;
        public TestView()
        {
            InitializeComponent();
            testViewModel = new TestViewModel(this.Navigation);
            this.BindingContext = testViewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            return true; 
        }
    }
}