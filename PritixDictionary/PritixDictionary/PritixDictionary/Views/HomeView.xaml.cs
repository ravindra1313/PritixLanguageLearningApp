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
    public partial class HomeView : ContentPage
    {
        HomeViewModel homeViewModel;
        public HomeView()
        {
            InitializeComponent();
            this.Title = "Contents";
            homeViewModel = new HomeViewModel(this.Navigation);
            BindingContext = homeViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            homeViewModel.UpdateContentList();

        }
    }
}