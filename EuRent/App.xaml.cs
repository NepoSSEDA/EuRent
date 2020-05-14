﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuRent
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AuthorizationPage();
            Console.WriteLine(MainPage.GetHashCode());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
