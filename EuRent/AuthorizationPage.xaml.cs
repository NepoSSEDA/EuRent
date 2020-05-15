using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace EuRent
{
    public partial class AuthorizationPage : ContentPage
    {
        static string SingInXaml, RegistrationXaml;

        Entry pib, adress, tel, email, pass;
        Switch sw;

        public AuthorizationPage()
        {
            //InitializeComponent();
            using (StreamReader sr = new StreamReader("SingInXaml.txt"))
            {
                SingInXaml = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("RegistrationXaml.txt"))
            {
                RegistrationXaml = sr.ReadToEnd();
            }

            HaveAnAccount_Clicked(this, new EventArgs());
        }

        void NoAccount_Clicked(object sender, EventArgs e)
        {
            sw = null;
            this.LoadFromXaml(RegistrationXaml);
            pib = this.FindByName<Entry>("pib");
            adress = this.FindByName<Entry>("adress");
            tel = this.FindByName<Entry>("tel");
            email = this.FindByName<Entry>("email");
            pass = this.FindByName<Entry>("pass");
        }

        void SingIn_Clicked(object sender, EventArgs e)
        {
            email = pass = null;
            sw = null;
            Content = new Label
            {
                BackgroundColor = Color.CornflowerBlue
            };
            //this.LoadFromXaml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><ContentPage xmlns=\"http://xamarin.com/schemas/2014/forms\" xmlns:x=\"http://schemas.microsoft.com/winfx/2009/xaml\" x:Class=\"EuRent.AuthorizationPage\" BackgroundColor=\"LightSkyBlue\"> </ContentPage >"); // To load empty loading page
            Navigation.PushModalAsync(new MainPage());
        }

        void Registration_Clicked(object sender, EventArgs e)
        {
            pib = adress = tel = null;
            SingIn_Clicked(this, new EventArgs());
        }

        void HaveAnAccount_Clicked(object sender, EventArgs e)
        {
            this.LoadFromXaml(SingInXaml);
            email = this.FindByName<Entry>("email");
            pass = this.FindByName<Entry>("pass");
            sw = this.FindByName<Switch>("sw");
            pib = adress = tel = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            HaveAnAccount_Clicked(this, new EventArgs());
        }
    }
}
