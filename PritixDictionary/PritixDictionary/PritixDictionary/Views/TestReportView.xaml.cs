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
    public partial class TestReportView : ContentPage
    {
        TestReportViewModel testReportViewModel;
        public TestReportView()
        {
            InitializeComponent();
            testReportViewModel = new TestReportViewModel();
            this.BindingContext = testReportViewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}