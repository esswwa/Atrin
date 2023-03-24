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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {

        public Registration(string nameT)
        {
            InitializeComponent();
            if (nameT != null)
            {
                var jsonUser = File.ReadAllText("users.json");
                List<Person> list = JsonConvert.DeserializeObject<List<Person>>(jsonUser);
                
                foreach (var item in list)
                {
                    if (nameT == item.Login1.ToString())
                    {
                        loginT = item.Login1.ToString();
                        loginText.Text = item.Login1.ToString();
                        numberText.Text = item.Number1.ToString();
                        fioText.Text = $"{item.Surname1.ToString()} {item.Name1.ToString()} {item.Otchestvo1.ToString()}";
                        exitClick.Visibility = Visibility.Visible;

                    }
                    
                    var jsonObya = File.ReadAllText("objavlenieModer.json"); 
                    List<Obya> list22 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                    List<Obya> listChecked = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                    List<Obya> listSelled = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                    listChecked.Clear();
                    listSelled.Clear();
                    foreach (var item1 in list22)
                    {
                            if (item1.NameProfile == loginT && (item1.Status == "checked" || item1.Status == "selled"))
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
                                if(item1.Status == "checked")
                                    listChecked.Add(item1);
                                if(item1.Status == "selled")
                                    listSelled.Add(item1);
                            }

                        
                    }
                    if (listChecked.Count > 0)
                    {
                        objavleniyaListView.ItemsSource = listChecked;
                        RectVisible.Visibility = Visibility.Visible;
                        textObya.Visibility = Visibility.Visible;
                        objavleniyaListView.Visibility = Visibility.Visible;
                    }
                    if (listSelled.Count > 0)
                    {
                        objavleniyaListView1.ItemsSource = listSelled;
                        RectVisible.Visibility = Visibility.Visible;
                        sellObya.Visibility = Visibility.Visible;
                        objavleniyaListView1.Visibility = Visibility.Visible;
                    }
                    if (listChecked.Count == 0 && listSelled.Count == 0)
                    {
                        RectVisible.Visibility = Visibility.Hidden;
                        sellObya.Visibility = Visibility.Hidden;
                        textObya.Visibility = Visibility.Hidden;
                        objavleniyaListView1.Visibility = Visibility.Hidden;
                        objavleniyaListView.Visibility = Visibility.Hidden;
                    }
                }
                var jsonUser1 = File.ReadAllText("users.json");
                List<Person> list1 = JsonConvert.DeserializeObject<List<Person>>(jsonUser1);

                foreach (var item in list1)
                {
                    if (item.Login1.ToString() == loginT)
                    {
                        parese = long.Parse(item.Number1.ToString());
                        numberText.Text = parese.ToString("# (###) ### ## ##");

                    }
                }
            }
        }
        string loginT; string loginT1; string Photo;  long parese;
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (loginT == null)
                this.NavigationService.Navigate(new MainPage(loginT));
            else
            {
                this.NavigationService.Navigate(new MainPage(loginT));
            }
        }
       

        private void elementClick(object sender, MouseButtonEventArgs e)
        {
            var jsonObya = File.ReadAllText("objavlenieModer.json");
            List<Obya> listChecked = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
            List<Obya> listSelled = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
            List<Obya> list3 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
            listChecked.Clear();
            listSelled.Clear();
            foreach (var item in list3)
            {
                    if (item.NameProfile == loginT && item.Status == "checked")
                    {
                        listChecked.Add(item);
                    }
                    if (item.NameProfile == loginT && item.Status == "selled")
                    {
                        listSelled.Add(item);
                    }

            }

            int i = 0;
            foreach (var item1 in listChecked)
            {
                    if (objavleniyaListView.SelectedIndex == i)
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
                        this.NavigationService.Navigate(new Objavlenie(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, loginT, item1.NameProfile));
                      
                    }
                    i++;
                
            }
            i = 0;
            foreach (var item1 in listSelled)
            {
                if (objavleniyaListView1.SelectedIndex == i)
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
                    this.NavigationService.Navigate(new Objavlenie(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, loginT, item1.NameProfile));

                }
                i++;

            }
        }

        private void ExitToMainPage(object sender, RoutedEventArgs e)
        {

            loginT = null;
            this.NavigationService.Navigate(new MainPage(loginT));
        }
    }
}
