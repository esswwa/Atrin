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

namespace projectPP
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            loginText = LoginText;
            passwordText = PasswordText;
        }
        List<Person> obya; Person chel;
        string loginT;
        static TextBox loginText = new TextBox();
        static PasswordBox passwordText = new PasswordBox();
        static bool z1 = true;
        static bool z2 = true;
        private void AtrinClick(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage(loginT));
        }
        public static bool LoginAuth(string login, string password)
        {
            bool z1 = true;
            Customer1 customer = new Customer1();
            CustomerValidator1 validator = new CustomerValidator1();
            customer.LoginV = login;
            customer.PasswordV = password;
            loginText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            loginText.ToolTip = null;
            passwordText.ToolTip = null;
            passwordText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            var results = validator.Validate(customer);
            var jsonObya = File.ReadAllText(@"C:\Users\essww\OneDrive\Рабочий стол\Основа wpf\projectPP\projectPP\bin\Debug\users.json");
            bool flag = true;
            List<Person> list1 = JsonConvert.DeserializeObject<List<Person>>(jsonObya);
            foreach (var item1 in list1)
            {
                if (results.IsValid && item1.Login1.ToString().ToLower() == login.ToLower() && item1.Password1.ToString() == password)
                {
                    flag = true;
                    break;
                }
                else
                {
                    foreach (var failure in results.Errors)
                    {
                        
                        if (item1.Login1.ToString().ToLower() == login.ToLower() && item1.Password1.ToString() == password)
                        {
                            loginText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
                            loginText.ToolTip = null;
                            passwordText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
                            passwordText.ToolTip = null;
                            z1 = false;
                        }
                        if (item1.Login1.ToString() == login.ToLower() && item1.Password1.ToString() != password && !z1)
                        {
                            loginText.Foreground = Brushes.Red;
                            loginText.ToolTip = "Логин не существует или не подходит пароль!";
                            passwordText.Foreground = Brushes.Red;
                            passwordText.ToolTip = "Пароль не подходит!";
                            z1 = false;
                        }
                        if (failure.ToString().Contains("login"))
                        {
                            loginText.Foreground = Brushes.Red;
                            loginText.ToolTip = "Логин не существует или не подходит пароль!";
                            z1 = false;
                        }
                        if (failure.ToString().Contains("password"))
                        {
                            passwordText.Foreground = Brushes.Red;
                            passwordText.ToolTip = "Пароль не подходит!";
                            z1 = false;
                        }

                    }
                    if (z1)
                    {
                        loginText.Foreground = Brushes.Red;
                        loginText.ToolTip = "Логин не существует или не подходит пароль!";
                        passwordText.Foreground = Brushes.Red;
                        passwordText.ToolTip = "Пароль не подходит!";
                    }
                    flag = false;
                }

            }
            return flag;
        }

        private void LoginClick1(object sender, RoutedEventArgs e)
        {
            if (LoginAuth(LoginText.Text.ToLower(), PasswordText.Password))
            {
                this.NavigationService.Navigate(new MainPage(LoginText.Text.ToLower()));
            }
        }

        private void RegClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Profile());
        }

        private void prviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
