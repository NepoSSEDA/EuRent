using System;
using System.IO;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Mono.Data.Sqlite;

namespace EuRent
{
    public partial class AuthorizationPage : ContentPage
    {
        //static string SingInXaml, RegistrationXaml;
        SqliteConnection conn; //= new SqliteConnection(@"Data Source = /Users/familylipovetskiy/Documents/car_rent.db; Version=3;");

        public AuthorizationPage()
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "car_rent1.db");
            if (!File.Exists(filepath))
                File.Copy("car_rent1.db", filepath);
            conn = new SqliteConnection(@"Data Source = " + filepath + "; Version = 3;");
            filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "IsToggled.txt");
            if (!File.Exists(filepath))
            {
                FileStream fs = File.Create(filepath);
                fs.Close();
            }
            string line = File.ReadAllText(filepath);
            if (!string.IsNullOrWhiteSpace(line))
            {
                conn.Open();
                Navigation.PushModalAsync(new MainPage(conn, long.Parse(line)));
            }

            InitializeComponent();
            //using (StreamReader sr = new StreamReader("/Users/familylipovetskiy/Library/Developer/CoreSimulator/Devices/9D1B4BFA-8660-4EEF-90FE-E8DD11046377/data/Containers/Bundle/Application/FFA11B6C-7B54-4E26-B744-1500D7EA8BD5/EuRent.iOS.app/IsToggled.txt"))

            //HaveAnAccount_Clicked(this, new EventArgs());
        }

        void NoAccount_Clicked(object sender, EventArgs e)
        {
            /*sw = null;
            this.LoadFromXaml(RegistrationXaml);
            pib = this.FindByName<Entry>("pib");
            adress = this.FindByName<Entry>("adress");
            tel = this.FindByName<Entry>("tel");
            email = this.FindByName<Entry>("email");
            pass = this.FindByName<Entry>("pass");*/
            email.Text = null;
            pass.Text = null;
            sw.IsToggled = false;
            Navigation.PushModalAsync(new RegistrationPage(conn), false);
        }

        void SingIn_Clicked(object sender, EventArgs e)
        {
            if (!(conn.State == System.Data.ConnectionState.Open))
                conn.Open();

            SqliteCommand get_users_by_email = new SqliteCommand("SELECT [client_ID] FROM clients WHERE [client_email] = @email;", conn);
            get_users_by_email.Parameters.AddWithValue("@email", email.Text);
            SqliteDataReader dataReader = get_users_by_email.ExecuteReader();
            if (!dataReader.Read()) // If here is no rows
            {
                DisplayAlert("Користувача не знайдено", "Користувача з поштою " + email.Text + " не було знайдено", "Ок");
                return; // To exit singin process
            }   
            long ID = (long)dataReader["client_ID"];
            SqliteCommand check_pass = new SqliteCommand("SELECT client_lname, client_name, client_pass FROM clients WHERE client_ID = @ID", conn);
            check_pass.Parameters.AddWithValue("@ID", ID);
            dataReader = check_pass.ExecuteReader();
            dataReader.Read();
            if (pass.Text != (string)dataReader["client_pass"]) // If user pass isn`t like in database
            {
                DisplayAlert("Невірний пароль", "Пароль, що був введений не є дійсним для " + email.Text, "Спробувати ще раз");
                return; 
            } 
             
            //string name = (string)dataReader["client_lname"] + " " + (string)dataReader["client_name"];
            //bool IsAdmin = false;
            //if (email.Text == "admin")
            //    IsAdmin = true;

            //using (StreamWriter writer = new StreamWriter("/Users/familylipovetskiy/Library/Developer/CoreSimulator/Devices/9D1B4BFA-8660-4EEF-90FE-E8DD11046377/data/Containers/Bundle/Application/FFA11B6C-7B54-4E26-B744-1500D7EA8BD5/EuRent.iOS.app/IsToggled.txt", false))
            if (sw.IsToggled)
            {
                /*using (StreamWriter writer = new StreamWriter("/Users/familylipovetskiy/Documents/IsToggled.txt"))
                {
                    writer.WriteLine(ID);
                }*/

                string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Console.WriteLine(folderpath);
                File.WriteAllText(Path.Combine(folderpath, "IsToggled.txt"), ID.ToString());
            }

            //Loading a next page
            stack.IsVisible = false;
            email.Text = null;
            pass.Text = null;
            sw.IsToggled = false;
            Navigation.PushModalAsync(new MainPage(conn, ID));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!stack.IsVisible) // If Content is just empty blue screen then draw UI
                stack.IsVisible = true;
        }
    }
}
