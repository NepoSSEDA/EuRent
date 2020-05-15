using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EuRent
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        void Frame_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Content = new Label() { BackgroundColor = Color.CornflowerBlue };
        }
    }
}
