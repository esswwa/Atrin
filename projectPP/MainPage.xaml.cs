using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Policy;
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
using static System.Net.Mime.MediaTypeNames;

namespace projectPP
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private int KategoryCount = 0;
        string loginT;
        public MainPage(string nameT)
        {
            InitializeComponent(); 
            if (File.Exists("users.json"))
                jsonUser = File.ReadAllText("users.json");
            else
            {
                List<Person> obya1111 = new List<Person>();
                chel = new Person("esswwa", "Десанский", "Артём", "Юлианович", "qazwsxedca", "89991226595");
                obya1111.Add(chel);
                string jsonUser = JsonConvert.SerializeObject(obya1111, Formatting.Indented);
                File.WriteAllText("users.json", jsonUser);
            }
            if (nameT != null)
            {
                List<Person> list = JsonConvert.DeserializeObject<List<Person>>(jsonUser);
                foreach (var item in list)
                {
                    if (nameT == item.Login1.ToString())
                    {
                        profileText.Text = item.Login1.ToString();
                    }
                    if (nameT == "esswwa")
                    {
                        leftClick1.Visibility = Visibility.Visible;
                        UploadObya.Visibility = Visibility.Hidden;
                    }
                }
            }
            if (File.Exists("objavlenieModer.json"))
                jsonObya = File.ReadAllText("objavlenieModer.json");
            else
            {
                List <string> obya111 = new List<string>();
                string jsonObya = JsonConvert.SerializeObject(obya111, Formatting.Indented);
                File.WriteAllText("objavlenieModer.json", jsonObya);
            }

            list11 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
            listForStatusCheck = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
            listForStatusCheck.Clear();
            foreach (var item in list11)
            {
                if (item.Status == "checked")
                { 
                        Photo = item.Photo.Substring(item.Photo.IndexOf("Resources"));
                        if (item.Photo.Contains(Photo))
                        {
                            Photo = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(Photo)}";
                        }
                        else
                        {
                        }
                        item.Photo = Photo;
                        listForStatusCheck.Add(item);
                    
                    
                }
            }
            if (kategory == "")
                objavleniyaListView.ItemsSource = listForStatusCheck;
           
        }
        string jsonObya; List<Obya> list11; List<Obya> listForStatusCheck;
        public string kategory = ""; string Photo; string jsonUser; Person chel;
        List<String> listObya;
        private void KategoryClick1(object sender, MouseButtonEventArgs e)
        {
            if (((TextBlock)sender).Name == "KategoryTransport")
                kategory = "Транспорт";
            if (((TextBlock)sender).Name == "KategoryElectronika")
                kategory = "Электроника";
            if (((TextBlock)sender).Name == "KategoryNedvizhimost")
                kategory = "Недвижимость";
            if (((TextBlock)sender).Name == "KategoryLichnieVeshi")
                kategory = "Личные вещи";
            if (((TextBlock)sender).Name == "KategoryUslugi")
                kategory = "Услуги";
            if (kategory.Length > 0)
            {
                List<Obya> list2 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                list2.Clear();
                foreach (var item in list11)
                {
                    if (item.Kategory == kategory)
                    {
                        if (item.Status == "checked")
                        {
                            Photo = item.Photo.Substring(item.Photo.IndexOf("Resources"));
                            if (item.Photo.Contains(Photo))
                            {
                                Photo = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(Photo)}";
                            }
                            else
                            {
                            }
                            item.Photo = Photo;
                            list2.Add(item);
                        }
                     
                    }
                    
                }
                objavleniyaListView.ItemsSource = list2;
            }
            
            if (kategory.Length == 0)
            {
                var jsonRun = File.ReadAllText("objavlenieModer.json");
                List<Obya> list = JsonConvert.DeserializeObject<List<Obya>>(jsonRun);
                foreach (var item in list)
                {
                    if (item.Status == "checked")
                    {
                        Photo = item.Photo.Substring(item.Photo.IndexOf("Resources"));
                        if (item.Photo.Contains(Photo))
                        {
                            Photo = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(Photo)}";
                        }
                        else
                        {
                        }
                        item.Photo = Photo;
                        list.Add(item);
                    }
                    if (item.Status == "blocked")
                    {
                    }
                    if (item.Status == "notChecked")
                    {

                    }
                }
                objavleniyaListView.ItemsSource = list;
                objavleniyaListView.UpdateLayout();
            }
        }

        private void KategoryClick(object sender, MouseButtonEventArgs e)
        {
            if (KategoryCount == 0)
            {
                KategoryK.Visibility = Visibility.Visible;
                KategoryCount = 1;
            }

            else
            {
                KategoryK.Visibility = Visibility.Hidden;
                KategoryCount = 0;

                objavleniyaListView.ItemsSource = listForStatusCheck;
                objavleniyaListView.UpdateLayout();
                kategory = "";
            }
        }
        private void SearchClick(object sender, MouseButtonEventArgs e)
        {
            if (kategory.Length == 0)
            {
                if (Search.Text.Length != 0 && Search.Text != "" && Search.Text != " ")
                {
                    List<Obya> list2 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                    list2.Clear();
                    foreach (var item in list11)
                    {
                        if (item.Status == "checked")
                        {
                            if (item.Name.ToLower().Contains(Search.Text.ToLower()) || item.Country.ToLower().Contains(Search.Text.ToLower()))
                            {
                                Photo = item.Photo.Substring(item.Photo.IndexOf("Resources"));
                                if (item.Photo.Contains(Photo))
                                {
                                    Photo = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(Photo)}";
                                }
                                else
                                {
                                }
                                item.Photo = Photo;
                                list2.Add(item);
                            }
                        }
                    }
                    objavleniyaListView.ItemsSource = list2;
                }
                else if (Search.Text.Length == 0 || Search.Text == "" || Search.Text == " ")
                {
                    objavleniyaListView.ItemsSource = listForStatusCheck;
                    objavleniyaListView.UpdateLayout();
                }
            }
           
            if (kategory.Length > 0)
            {
                if (Search.Text.Length != 0 && Search.Text != "" && Search.Text != " ")
                {
                    List<Obya> list2 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                    list2.Clear();
                    foreach (var item in list11)
                    {
                        if (item.Kategory == kategory)
                        {
                            if (item.Status == "checked")
                            {
                                if (item.Name.ToLower().Contains(Search.Text.ToLower()) || item.Country.ToLower().Contains(Search.Text.ToLower()))
                                {
                                    Photo = item.Photo.Substring(item.Photo.IndexOf("Resources"));
                                    if (item.Photo.Contains(Photo))
                                    {
                                        Photo = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(Photo)}";
                                    }
                                    else
                                    {
                                    }
                                    item.Photo = Photo;
                                    list2.Add(item);
                                }
                            }

                        }

                    }
                    objavleniyaListView.ItemsSource = list2;
                }
                else
                {
                    List<Obya> list2 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                    list2.Clear();
                    foreach (var item in list11)
                    {
                        if (item.Kategory == kategory)
                        {
                            if (item.Status == "checked")
                            {
                                    Photo = item.Photo.Substring(item.Photo.IndexOf("Resources"));
                                    if (item.Photo.Contains(Photo))
                                    {
                                        Photo = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(Photo)}";
                                    }
                                    else
                                    {
                                    }
                                    item.Photo = Photo;
                                    list2.Add(item);
                            }

                        }

                    }
                    objavleniyaListView.ItemsSource = list2;

                }
            }
        }
        private void LoginClick(object sender, MouseButtonEventArgs e)
        {
            if (profileText.Text == "Войти")
            {
                this.NavigationService.Navigate(new LoginPage());
            }
            
            else
            {
                this.NavigationService.Navigate(new Registration(profileText.Text));
            }
        }
        private void elementClick(object sender, MouseButtonEventArgs e)
        {if(kategory.Length == 0)
            {
                var jsonObya = File.ReadAllText("objavlenieModer.json");
                List<Obya> list1 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                int i = 0;
                foreach (var item1 in list1)
                {
                        if (item1.Status == "checked")
                        {

                            if (objavleniyaListView.SelectedIndex == i)
                            {
                                if (profileText.Text != "Войти")
                                {
                                    this.NavigationService.Navigate(new Objavlenie(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, profileText.Text, item1.NameProfile));
                                }
                                else 
                                {
                                    this.NavigationService.Navigate(new Objavlenie(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, loginT, item1.NameProfile));
                                }

                                
                            }
                            i++;
                        }
                    
                }
            }
            else
            {
                var jsonObya = File.ReadAllText("objavlenieModer.json");
                List<Obya> list2 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                List<Obya> list3 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                list2.Clear();
                foreach (var item in list11)
                {
                    if (item.Kategory == kategory)
                    {
                        if (item.Status == "checked")
                        { 
                            list2.Add(item);
                        }
                   
                    }
                }

                int i = 0;
                foreach (var item1 in list2)
                {
                    if (item1.Status == "checked")
                    {
                        if (objavleniyaListView.SelectedIndex == i)
                        {
                            if (profileText.Text != "Войти")
                            {
                                this.NavigationService.Navigate(new Objavlenie(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, profileText.Text, item1.NameProfile));
                            }
                            else
                            {
                                this.NavigationService.Navigate(new Objavlenie(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, loginT, item1.NameProfile));
                            }
                        }
                        i++;
                    }
                }

            }
           
        }

        private void uploadClick(object sender, MouseButtonEventArgs e)
        {
            if (profileText.Text == "Войти")
            {
                this.NavigationService.Navigate(new LoginPage());
            }
            else
            {
                this.NavigationService.Navigate(new PlaceAnAd(profileText.Text));
            }
        }

        private void leftClick(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new AdminPage("esswwa"));
        }

        private void AtrinClick(object sender, MouseButtonEventArgs e)
        {
            KategoryCount = 1;
            KategoryK.Visibility = Visibility.Hidden;
            KategoryCount = 0;
            Search.Text = "";
            objavleniyaListView.ItemsSource = listForStatusCheck;
            objavleniyaListView.UpdateLayout();
            kategory = "";

        }
        public string lastKey = "Space";
        private void prviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && lastKey != "Space")
            {
                e.Handled = false;
                lastKey = e.Key.ToString();
            }
            else if (e.Key == Key.Space && lastKey == "Space")
            {
                e.Handled = true;
                lastKey = e.Key.ToString();
            }
            else
            {
                e.Handled = false;
                lastKey = e.Key.ToString();
            }
        }
    }

}
        
    
        
    

