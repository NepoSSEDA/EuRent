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
        }

        void Rent_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
