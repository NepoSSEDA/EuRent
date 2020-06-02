using System;
using Mono.Data.Sqlite;
using Xamarin.Forms;
using Mono.Data.Sqlite;

namespace EuRent
{
    public partial class RentPage : ContentPage
    {
        SqliteConnection conn;

        public RentPage(SqliteConnection conn, long ID)
        {
            this.conn = conn;
            InitializeComponent();

            SqliteCommand isAdmin = new SqliteCommand("SELECT client_email FROM clients WHERE client_id = @ID;", conn);
            isAdmin.Parameters.AddWithValue("@ID", ID);
            SqliteDataReader dataReader = isAdmin.ExecuteReader();
            if ((string)dataReader["client_email"] == "admin")
            {
                Button b = new Button
                {
                    Text = "Додати авто",
                    BackgroundColor = Color.LightGreen,
                    BorderWidth = 5,
                    BorderColor = Color.LightGreen,
                    Margin = new Thickness(20, 0, 20, 0),
                    FontFamily = "Apple",
                    FontSize = 30
                };
                b.Clicked += addCars_Clicked;
                s.Children.Add(b);
            }

            SqliteCommand getDeps = new SqliteCommand("SELECT * FROM department;", conn);
            dataReader = getDeps.ExecuteReader();
            while (dataReader.Read())
                depPicker.Items.Add((string)dataReader[1] + " " + (string)dataReader[2] + " " + (string)dataReader[3] + " " + ((dataReader[4].ToString() == "0") ? "" : dataReader[4].ToString()));
            startDatePicker.MinimumDate = DateTime.Now;
            startDatePicker.MaximumDate = DateTime.Now.AddMonths(5);
            startDatePicker_DateSelected(this);
            sliderMin.Maximum = 300;
            sliderMax.Maximum = 300;
            sliderMin.Minimum = 5;
            sliderMax.Minimum = 5;
            sliderMin.Value = 150;
            sliderMax.Value = 150;
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
            if (sliderMax.Value < sliderMin.Value)
                sliderMax.Value = sliderMin.Value;
            else max_price.Text = ((int)sliderMax.Value).ToString();
        }

        void sliderMin_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (sliderMax.Value < sliderMin.Value)
                sliderMax.Value = sliderMin.Value;
            min_price.Text = ((int)sliderMin.Value).ToString();
        }

        void startDatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e = null)
        {
            try
            {
                endDatePicker.MinimumDate = startDatePicker.Date;
                endDatePicker.MaximumDate = startDatePicker.Date.AddMonths(1);
            }
            catch (ArgumentException ex)
            {
                startDatePicker.Date = e.OldDate;
            }
        }

        void addCars_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new AddCarPage(depPicker));
        }
    }
}
