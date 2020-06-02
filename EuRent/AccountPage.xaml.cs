using System;
using Mono.Data.Sqlite;
using Xamarin.Forms;
using System.IO;
using Mono.Data.Sqlite;
using MapKit;
using UIKit;

namespace EuRent
{
    public partial class AccountPage : ContentPage
    {
        SqliteConnection conn;
        long ID;
        public AccountPage(SqliteConnection conn, long ID)
        {
            this.conn = conn;
            this.ID = ID;
            InitializeComponent();

            SqliteCommand get_name = new SqliteCommand("SELECT client_lname, client_name FROM clients WHERE client_ID = @ID", conn);
            get_name.Parameters.AddWithValue("@ID", this.ID);
            SqliteDataReader dataReader = get_name.ExecuteReader();
            label.Text = (string)dataReader[0] + " " + (string)dataReader[1];
        }

        void Frame_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Content = new Label() { BackgroundColor = Color.CornflowerBlue };
        }

        void quit_Clicked(object sender, EventArgs e)
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IsToggled.txt");
            File.WriteAllText(filepath, "");
            Navigation.PopModalAsync();
        }
    }
}
