using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EuRent
{
    public partial class RentPage : ContentPage
    {
        public RentPage()
        {
            InitializeComponent();
            //sliderMin.Minimum = 5;
            //sliderMax.Minimum = 5;
            //sliderMin.Maximum = 300;
            //sliderMax.Maximum = 300;
            //sliderMin.Value = 150;
            //sliderMax.Value = 150;
        }

        void Rent_Clicked(object sender, EventArgs e)
        {
            /*NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            Page main = navPage.Navigation.NavigationStack[navPage.Navigation.NavigationStack.Count - 1];
            navPage.Navigation.InsertPageBefore(new AuthorizationPage(), main);*/ 
            Navigation.PopModalAsync();
        }

        void searchButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new CarsListPage());
        }

        void sliderMax_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
        }

        void sliderMin_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
        }

        void startDatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
        }

        void addCars_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new AddCarPage());
        }
    }
}
