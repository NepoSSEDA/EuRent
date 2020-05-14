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
            this.LoadFromXaml(RegistrationXaml);
            pib = this.FindByName<Entry>("pib");
            adress = this.FindByName<Entry>("adress");
            tel = this.FindByName<Entry>("tel");
            email = this.FindByName<Entry>("email");
            pass = this.FindByName<Entry>("pass");
        }

        void SingIn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        void Registration_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        void HaveAnAccount_Clicked(object sender, EventArgs e)
        {
            this.LoadFromXaml(SingInXaml);
            email = this.FindByName<Entry>("email");
            pass = this.FindByName<Entry>("pass");
            pib = adress = tel = null;
        }
    }
}
