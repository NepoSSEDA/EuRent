using System;
using System.Text;
using Mono.Data.Sqlite;
using Xamarin.Forms;

namespace EuRent
{
    public partial class RegistrationPage : ContentPage
    {
        SqliteConnection conn;
        Entry[] entries;
 
        protected enum AlertType { EMAIL, PIB, TEL, ADRESS };

        public RegistrationPage(SqliteConnection conn)
            : this()
        {
            this.conn = conn;
        }

        public RegistrationPage()
        {
            InitializeComponent();
            entries = new Entry[5] { pib, adress, tel, email, pass };
        }

        void Registration_Clicked(object sender, EventArgs e)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            bool exit_flag = false;
            for (int i = 0; i < 5; i++)
            {
                Label target_label = null;
                switch (i)
                {
                    case 0:
                        target_label = pib_label;
                        break;
                    case 1:
                        target_label = adress_label;
                        break;
                    case 2:
                        target_label = tel_label;
                        break;
                    case 3:
                        target_label = email_label;
                        break;
                    case 4:
                        target_label = pass_label;
                        break;
                }
                if (String.IsNullOrWhiteSpace(entries[i].Text))
                {
                    target_label.TextColor = Color.Red;
                    exit_flag = true;
                }
                else target_label.TextColor = Color.CornflowerBlue;
            }
            if (exit_flag)
                return;

            string text = email.Text.ToLower();
            bool @was = false, dot = false;
            for (int i = 0; i < email.Text.Length; i++)
            {
                if ((char.IsLetter(text[i]) && (text[i] >= 97)  && (text[i] <= 122)) || char.IsDigit(text[i]) ||
                    (text[i] == '_') || (text[i] == '.') || (text[i] == '@'))
                {
                    if (text[i] == '@')
                        if ((i == 0) || @was || ((i + 1) == text.Length))
                        {
                            Alert(AlertType.EMAIL);
                            return;
                        }
                        else @was = true;
                    else if (@was)
                    {
                        if ((text[i] == '_') || ((text[i] == '.') && ((text[i - 1] == '.') || (text[i - 1] == '@') || ((i + 1) == text.Length))
                            || char.IsDigit(text[i])))
                        {
                            Alert(AlertType.EMAIL);
                            return;
                        }
                        else if (text[i] == '.') dot = true;
                    }
                    else if ((text[i] == '.') || (char.IsDigit(text[i]) && (i == 0)))
                    {
                        Alert(AlertType.EMAIL);
                        return;
                    }
                }
                else
                {
                    DisplayAlert("Недопустимий символ", "Поле електронної адреси може містити тільки символи латинської абетки, цифри та символи \"_\", \".\", а також \"@\".", "Добре");
                    return;
                }
                     
            }
            if (!@was || !dot)
            {
                Alert(AlertType.EMAIL);
                return;
            }

            SqliteCommand get_user_email = new SqliteCommand("SELECT client_ID FROM clients WHERE client_email = @email;", conn);
            get_user_email.Parameters.AddWithValue("@email", email.Text);
            SqliteDataReader dataReader = get_user_email.ExecuteReader();
            if (dataReader.Read())
            {
                DisplayAlert("Помилка реєстрації", "Користувач з поштою " + email.Text + " вже існує. Скористайтеся іншою поштою", "Добре");
                return;
            }

            text = pib.Text;
            StringBuilder l_name = new StringBuilder(), name = new StringBuilder(), patr = new StringBuilder();
            int count = 0;
            for (int i = 0; i < pib.Text.Length; i++)
            {
                if (!char.IsLetter(text[i]) && (text[i] != ' '))
                {
                    Alert(AlertType.PIB);
                    return;
                }
                else
                {
                    if ((text[i] != ' ') && ((i == 0) || (text[i - 1] == ' ')))
                        count++;
                    if (char.IsLetter(text[i]))
                        switch (count)
                        {
                            case 1:
                                l_name.Append(text[i]);
                                break;
                            case 2:
                                name.Append(text[i]);
                                break;
                            case 3:
                                patr.Append(text[i]);
                                break;
                        }
                }

            }
            if (count != 3)
            {
                DisplayAlert("Невірний формат ПІБ", "ПІБ повинно складатися з трьох позицій (Прізвище Ім'я По-батькові).", "Добре");
                return;
            }

            text = adress.Text;
            StringBuilder country = new StringBuilder(), city = new StringBuilder(), street = new StringBuilder(), house = new StringBuilder();
            count = 0;
            for (int i = 0; i < adress.Text.Length; i++)
            {
                if (char.IsLetter(text[i]) || (text[i] == ' ') || ((count == 4) && ((text[i] == '/') || char.IsDigit(text[i]))) || ((count == 3) &&
                    char.IsDigit(text[i]) && (text[i - 1] == ' ')))
                {
                    if (text[i] != ' ')
                    {
                        if ((i == 0) || (text[i - 1] == ' '))
                            if (((count == 3) && char.IsLetter(text[i])))
                                street.Append(' ');
                            else count++;
                        switch (count)
                        {
                            case 1:
                                country.Append(text[i]);
                                break;
                            case 2:
                                city.Append(text[i]);
                                break;
                            case 3:
                                street.Append(text[i]);
                                break;
                            case 4:
                                house.Append(text[i]);
                                break;
                        }
                    }
                }
                else
                {
                    Alert(AlertType.ADRESS);
                    return;
                }

            }
            if (count != 4)
            {
                Alert(AlertType.ADRESS);
                return;
            }

            text = tel.Text;
            for (int i = 0; i < tel.Text.Length; i++)
            {
                if (i == 0)
                {
                    if (text[i] != '+')
                    {
                        DisplayAlert("Невірний формат телефоного номеру", "Поле \"Телефон\" повинне містити код країни", "Добре");
                        return;
                    }
                }
                else if (!char.IsDigit(text[i]))
                {
                    DisplayAlert("Невірний формат телефонного номеру", "Номер не повинен містити жодних символів, окрім цифр та символу \"+\" на початку.", "Добре");
                    return;
                }
            }

            SqliteCommand registrate_user = new SqliteCommand("INSERT INTO clients " +
               "VALUES (NULL, '" + l_name.ToString() + "', '" + name.ToString() + "', '" + patr.ToString() + "', '" + country.ToString() + "', '" +
               city.ToString() + "', '" + street.ToString() + "', '" + house.ToString() + "', '" + tel.Text + "', '" + email.Text + "', '" +
               pass.Text + "');", conn);
            registrate_user.ExecuteNonQuery();
            SqliteCommand get_new_ID = new SqliteCommand("SELECT client_ID FROM clients WHERE client_email = '" + email.Text + "';", conn);
            dataReader = get_new_ID.ExecuteReader();
            //Loading next page
            stack.IsVisible = false;
            Navigation.PushModalAsync(new MainPage(conn, (long)dataReader["client_ID"]));
            //SingIn_Clicked(this, new EventArgs());
        }

        void HaveAnAccount_Clicked(object sender, EventArgs e)
        {
            /*this.LoadFromXaml(SingInXaml);
            email = (Entry)this.FindByName("email");
            pass = (Entry)this.FindByName("pass");
            sw = (Switch)this.FindByName("sw");
            pib = adress = tel = null;*/
            Navigation.PopModalAsync(false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!stack.IsVisible)
                Navigation.PopModalAsync();
        }

        protected void Alert(AlertType type)
        {
            switch (type)
            {
                case AlertType.EMAIL:
                    DisplayAlert("Невірний формат пошти", "Формат електронної адреси повинен відповідати формату \"mailname@example.com\".", "Добре");
                    break;
                case AlertType.PIB:
                    DisplayAlert("Невірний формат ПІБ", "У полі \"ПІБ\" можуть використувуватися тільки букви та символи \" \".", "Добре");
                    break;
                case AlertType.ADRESS:
                    DisplayAlert("Невірний формат Адреси", "Адреса повинна відповідати формату \"Країна Місто Вулиця [допоміжне слово, наприклад (проспект, av, passage)] №дому", "Добре");
                    break;
            }
        }
    }
}
