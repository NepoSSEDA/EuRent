using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EuRent
{
    public partial class AddCarPage : ContentPage
    {
        public AddCarPage()
        {
            InitializeComponent();
        }

        void backButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void addCars_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
