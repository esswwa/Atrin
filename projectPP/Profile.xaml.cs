using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentValidation;
using FluentValidation.Results;
using System.Threading;

namespace projectPP
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            loginText = LoginText;
            passwordText = PasswordText;
            surnameText = SurnameText;
            nameText = NameText;
            otchestvoText = OtchestvoText;
            numberText = NumberPhoneText;
            passwordText.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            NumberPhoneText.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));

        }


        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
        }

        List<Person> obya; Person chel;
        string loginT; bool z = true;
        static TextBox loginText = new TextBox();
        static TextBox surnameText = new TextBox();
        static TextBox nameText = new TextBox();
        static TextBox otchestvoText = new TextBox();
        static PasswordBox passwordText = new PasswordBox();
        static TextBox numberText = new TextBox();
        static bool log = false;
        static bool surname = false;
        static bool name = false;
        static bool otch = false;
        static bool pass = false;
        static bool num = false;
        static bool good = false;
        static bool flagNum = false;
        static bool passwordFlag = false;
        static bool loginFlag = false;


        public static bool RegCreate(string login, string password, string name, string otchestvo, string number, string surname)
        {
            good = false;
            passwordFlag = false;
            loginFlag = false;
            Customer customer = new Customer();
            CustomerValidator validator = new CustomerValidator();

            loginText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            loginText.ToolTip = null;
            surnameText.ToolTip = null;
            nameText.ToolTip = null;
            otchestvoText.ToolTip = null;
            passwordText.ToolTip = null;
            numberText.ToolTip = null;
            surnameText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            nameText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            otchestvoText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            passwordText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            numberText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));

            customer.LoginV = login;
            customer.PasswordV = password;
            customer.NameV = name;
            customer.OtchestvoV = otchestvo;
            customer.SurnameV = surname;
            customer.NumberV = number;
            var results = validator.Validate(customer);
            
            var jsonObya = File.ReadAllText(@"C:\Users\essww\OneDrive\Рабочий стол\Основа wpf\projectPP\projectPP\bin\Debug\users.json");
            bool flag = true;
            List<Person> list1 = JsonConvert.DeserializeObject<List<Person>>(jsonObya);
            foreach (var item1 in list1)
            {
                if (item1.Login1.ToString().ToLower() == login.ToLower())
                {
                    flag = false;
                    break;
                }
            }

            int indexName1 = 0; int indexPassword1 = 0;

            foreach (var i111 in password)
            {
                if (Char.IsLetter(i111))
                {
                    indexPassword1++;
                    if (indexPassword1 >= 4)
                    {
                        passwordFlag = true;
                        break;
                    }
                }
            }

            foreach (var i111 in login)
            {
                if (Char.IsLetter(i111))
                {
                    indexName1++;
                    if (indexName1 >= 4)
                    {
                        loginFlag = true;
                        break;
                    }
                }
            }

            if (flag && results.IsValid && (number[0].ToString() == "8" || number[0].ToString() == "7") && number[1].ToString() == "9" && passwordFlag && loginFlag)
                flag = true;
            else
            {
                foreach (var failure in results.Errors)
                {
                    if (failure.ToString().Contains("Login"))
                    {
                        loginText.Foreground = Brushes.Red;
                        loginText.ToolTip = failure.ToString();
                        good = true;
                    }
                    if (failure.ToString().Contains("Surname"))
                    {
                        surnameText.Foreground = Brushes.Red;
                        surnameText.ToolTip = failure.ToString();
                        good = true;
                    }
                    if (failure.ToString().Contains("Name"))
                    {
                        nameText.Foreground = Brushes.Red;
                        nameText.ToolTip = failure.ToString();
                        good = true;
                    }
                    if (failure.ToString().Contains("Otchestvo"))
                    {
                        otchestvoText.Foreground = Brushes.Red;
                        otchestvoText.ToolTip = failure.ToString();
                        good = true;
                    }
                    if (failure.ToString().Contains("Password"))
                    {
                        passwordText.Foreground = Brushes.Red;
                        passwordText.ToolTip = failure.ToString();
                        good = true;
                    }
                    if (failure.ToString().Contains("Number"))
                    {
                        numberText.Foreground = Brushes.Red;
                        numberText.ToolTip = failure.ToString();
                        good = true;
                    }
                }

                if(!passwordFlag)
                {
                    passwordText.Foreground = Brushes.Red;
                    passwordText.ToolTip = "Пароль должен содержать только буквы, либо буквы и цифры, либо буквы и символы, либо буквы, цифры и символы!\nБукв должно быть больше 3!";
                    good = true;
                }

                if (!loginFlag)
                {
                    loginText.Foreground = Brushes.Red;
                    loginText.ToolTip = "Логин должен содержать только буквы, либо буквы и цифры!\nБукв должно быть больше 3!";
                    good = true;
                }

                if (number.Length > 1)
                {
                    if ((number[0].ToString() != "8" && number[0].ToString() != "7") || number[1].ToString() != "9")
                    {
                        numberText.Foreground = Brushes.Red;
                        numberText.ToolTip = "Номер телефона должен начинать с 89 или 79";
                        good = true;
                    }
                }
                    
                if (!good)
                {
                    loginText.Foreground = Brushes.Red;
                    loginText.ToolTip = "Логин уже сществует";
                }
                flag = false;
            }
            return flag;
        }

        private void RegClick1(object sender, RoutedEventArgs e)
        {
            if (RegCreate(LoginText.Text, PasswordText.Password, NameText.Text, OtchestvoText.Text, NumberPhoneText.Text, SurnameText.Text))
            {
                string jsonObya = File.ReadAllText("users.json");
                obya = JsonConvert.DeserializeObject<List<Person>>(jsonObya);
                chel = new Person(LoginText.Text.ToLower(), SurnameText.Text, NameText.Text, OtchestvoText.Text, PasswordText.Password, NumberPhoneText.Text);
                obya.Add(chel);
                string json = JsonConvert.SerializeObject(obya, Formatting.Indented);
                File.WriteAllText("users.json", json);
                this.NavigationService.Navigate(new LoginPage());
            }
        }


        private void AtrinClick(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage(loginT));
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
        }

        
        private void priceText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int i = 0;
            bool result = int.TryParse(e.Text.ToString(), out i);
            if (!result)
            {
                e.Handled = true;
            }
            
        }
        private void numberChanged(object sender, TextChangedEventArgs e)
        {
            foreach (char c in NumberPhoneText.Text)
            {
                if (!Char.IsDigit(c) && NumberPhoneText.Text.Length > 0)
                {
                    NumberPhoneText.Text = "";
                }
            }
        }
        public string lastKey = "Space";


        private void prviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
