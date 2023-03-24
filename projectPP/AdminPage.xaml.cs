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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private int KategoryCount = 0;
        public AdminPage(string nameT)
        {
            InitializeComponent();
            nameText.Text = nameT;
            jsonObya = File.ReadAllText("objavlenieModer.json");
            list11 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
            listForStatusCheck = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
            listForStatusCheck.Clear();
            foreach (var item in list11)
            {
                if (item.Status == "notChecked")
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
        public string kategory = ""; string Photo;

        List<String> listObya;  
        private void SearchClick(object sender, MouseButtonEventArgs e)
        {
            if (Search.Text.Length != 0 && Search.Text != "" && Search.Text != " ")
            {
                List<Obya> list2 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                list2.Clear();
                foreach (var item in list11)
                {
                    if (item.Status == "notChecked")
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
        private void LoginClick(object sender, MouseButtonEventArgs e)
        {
                this.NavigationService.Navigate(new Registration("esswwa"));
        }
        private void elementClick(object sender, MouseButtonEventArgs e)
        {
            if (kategory.Length == 0)
            {
                var jsonObya = File.ReadAllText("objavlenieModer.json");
                List<Obya> list1 = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);
                int i = 0;
                foreach (var item1 in list1)
                {
                    if (item1.Status == "notChecked")
                    {

                        if (objavleniyaListView.SelectedIndex == i)
                            this.NavigationService.Navigate(new adminObjavlenie(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, "esswwa", item1.NameProfile));
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
                        if (item.Status == "notChecked")
                        {
                            list2.Add(item);
                        }

                    }
                }

                int i = 0;
                foreach (var item1 in list2)
                {
                    if (item1.Status == "notChecked")
                    {
                        if (objavleniyaListView.SelectedIndex == i)
                            this.NavigationService.Navigate(new adminObjavlenie(item1.Name, item1.Price, item1.Country, item1.Photo, item1.Kategory, item1.Condition, item1.Opisanie, "esswwa", item1.NameProfile));
                        i++;
                    }

                }

            }

        }
        private void leftClick(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage("esswwa"));
            Search.Text = "";
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

        private void AtrinClick(object sender, MouseButtonEventArgs e)
        {
            Search.Text = "";
            objavleniyaListView.ItemsSource = listForStatusCheck;
            objavleniyaListView.UpdateLayout();
            kategory = "";
        }
    }
}
