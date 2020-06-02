using System;
using System.ComponentModel;
using Xamarin.Forms;
using Mono.Data.Sqlite;

namespace EuRent
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    { 
        public MainPage(SqliteConnection conn, long ID)
        {
            Children.Add(new RentPage(conn, ID) { Title = "Авто", IconImageSource = "Cars.png" });
            Children.Add(new AccountPage(conn, ID) { Title = "Кабінет", IconImageSource = "Account.png" });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            Page auth = navPage.Navigation.NavigationStack[0];
            navPage.Navigation.RemovePage(auth);*/
        }
    }
}
