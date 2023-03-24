using BespokeFusion;
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
using System.Xml.Linq;

namespace projectPP
{
    /// <summary>
    /// Логика взаимодействия для Objavlenie.xaml
    /// </summary>
    public partial class Objavlenie : Page
    {
        public Objavlenie(string Name, string Price, string Country, string Photo, string Kategory, string Condition, string Opisanie, string nameT, string nameProfile)
        {
            
            InitializeComponent();
            if (nameT != null)
            {
                var jsonUser = File.ReadAllText("users.json");
                var jsonObya = File.ReadAllText("objavlenieModer.json");
                List<Person> list = JsonConvert.DeserializeObject<List<Person>>(jsonUser);
                List<Obya> list11 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                foreach (var item in list)
                {
                    if (item.Login1.ToString() == nameProfile)
                    {
                        item.Number1.ToString();
                        numberPhone.Content = item.Number1.ToString();

                    }
                    if (nameT == item.Login1.ToString())
                    { 
                        loginT = item.Login1.ToString();
                        if (loginT == nameProfile)
                        {
                            editObya.Visibility = Visibility.Visible;
                            sellClick.Visibility = Visibility.Visible;  
                        }
                        foreach (var item111111 in list11)
                        {

                          if (item111111.Status == "selled" && Name.Contains(item111111.Name.ToString()) && Opisanie.Contains(item111111.Opisanie.ToString()))
                          {
                             editObya.Visibility = Visibility.Hidden;
                             sellClick.Visibility = Visibility.Hidden;
                          }
                        }
                    }

                }
            }
            var jsonUser1 = File.ReadAllText("users.json");
            List<Person> list1 = JsonConvert.DeserializeObject<List<Person>>(jsonUser1);

            foreach (var item in list1)
            {
                if (item.Login1.ToString() == nameProfile)
                {
                    parese = long.Parse(item.Number1.ToString());
                    numberPhone.Content = parese.ToString("# (###) ### ## ##");

                }
            }

            nameText.Text = Name;
            if (Price.Contains("₽"))
                priceText.Text = Price;
            else
            {
                priceText.Text = Price + " ₽";
            }
            Adres.Text = "Адрес: " + Country;
            if (!Photo.Contains("pack://application:,,,/Marathon registration;component/Resources/Photo.png"))
            {
                image.Source = new BitmapImage(new Uri($"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(Photo)}", UriKind.RelativeOrAbsolute));
            }
            else
            {
                image.Source = new BitmapImage(new Uri(Photo, UriKind.RelativeOrAbsolute));
            }
            Kategory1.Text = Kategory;
            Condition1.Text = "Состояние\n" + Condition;
            Opisanie1.Text = "Описание\n" + Opisanie;
            profileText.Text = nameProfile;
           


        }
        string loginT = ""; string Photo; CustomMaterialMessageBox msg; long parese;
        private void AtrinClick(object sender, MouseButtonEventArgs e)
        {
            if (loginT == null)
                this.NavigationService.Navigate(new MainPage(loginT));
            else
            {
                this.NavigationService.Navigate(new MainPage(loginT));
            }
        }
        private void editClick(object sender, RoutedEventArgs e)
        {
            var jsonObya = File.ReadAllText("objavlenieModer.json");
            List<Obya> list1 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
            foreach (var item1 in list1)
            {
                if (item1.Name.ToString() != null)
                {
                    if (item1.Name.ToString() == nameText.Text)
                    {
                        Photo = item1.Photo.Substring(item1.Photo.IndexOf("Resources"));
                        if (item1.Photo.Contains(Photo))
                        {
                            Photo = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(Photo)}";
                        }
                        else
                        {
                        }

                        item1.Photo = Photo;
                        this.NavigationService.Navigate(new editObyaPage(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, item1.Status, item1.NameProfile, profileText.Text));
                    }
                }
            }
        }

      

        private void numberClick(object sender, RoutedEventArgs e)
        {
            msg = new CustomMaterialMessageBox
            {
                TxtMessage = { Text = "The phone number was copied.", Foreground = Brushes.White },
                TxtTitle = { Text = "Call a phone number", Foreground = Brushes.White },
                BtnOk = { Content = "Ok, thank you", Background = Brushes.BlueViolet, MinWidth = 200},
                BtnCancel = {Visibility = Visibility.Hidden},
                MainContentControl = { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff")) },
                TitleBackgroundPanel = { Background = Brushes.BlueViolet },
                BorderBrush = Brushes.BlueViolet,
                BtnCopyMessage = {Visibility = Visibility.Hidden}
            };
            msg.Show();
            Clipboard.Clear();
            Clipboard.SetText(numberPhone.Content.ToString());

        }
        string status = "";
        private void selled1(object sender, RoutedEventArgs e)
        {
            status = "selled";
            var jsonRun = File.ReadAllText("objavlenieModer.json");
            List<Obya> list = JsonConvert.DeserializeObject<List<Obya>>(jsonRun);
            foreach (var item in list)
            {
                if (item.Name == nameText.Text && Opisanie1.Text.Contains(item.Opisanie))
                {
                    item.Status = status;
                    break;
                }

            }
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText("objavlenieModer.json", convertedJson);
            this.NavigationService.Navigate(new Registration(loginT));
        }
    }
}
