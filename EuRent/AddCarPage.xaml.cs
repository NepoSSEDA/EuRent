using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EuRent
{
    public partial class AddCarPage : ContentPage
    {
        public AddCarPage(Picker depPicker)
        {
            InitializeComponent();
            foreach (string item in depPicker.Items)
                this.depPicker.Items.Add(item);
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
