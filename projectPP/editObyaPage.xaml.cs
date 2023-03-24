using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для editObyaPage.xaml
    /// </summary>
    public partial class editObyaPage : Page
    {
        bool z = false;
        OpenFileDialog dialog = new OpenFileDialog();
        public string kategory = "";
        public string condition = "";
        private string status = "notChecked";
        bool priceZ = true;
        public editObyaPage(string Name, string Price, string Country, string Photo, string Kategory, string Condition, string Opisanie, string Status, string nameProfile, string nameT)
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
                    }

                }
                ll = Name;
                stat = Status;
                nameT1 = nameProfile;
                Photo12 = Photo;
                kategoryOLDD = Kategory;
                yourKategory.Text = "Ваша категория: " + Kategory;
                textName.Text = Name; 
                OpisanieObya = Opisanie; 
                nameObya = Name;
                if (Condition == "Б/У")
                {
                    condition = oldCondition.Text;
                    newCondition.TextDecorations = TextDecorations.Strikethrough;
                    oldCondition.TextDecorations = TextDecorations.Underline;
                }
                else
                {
                    condition = newCondition.Text;
                    newCondition.TextDecorations = TextDecorations.Underline;
                    oldCondition.TextDecorations = TextDecorations.Strikethrough;
                    
                }
                textOpisanie.Text = Opisanie;
                iMage.Source = new BitmapImage(new Uri(Photo, UriKind.RelativeOrAbsolute));
                priceText.Text = Price;
                if (priceText.Text.Contains(" "))
                {
                    priceText.Text = priceText.Text.Replace(" ", "");
                    if (priceText.Text.Contains("₽"))
                    {
                        priceText.Text = priceText.Text.Replace("₽", ""); 
                    }
                }
                
                
                AdresText.Text = Country;
            }
            textOpisanie.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            priceText.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            textName.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            AdresText.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));

        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
        }

        string loginT; string nameT1; string kategoryOLDD; int count = 0; string Photo12; string stat;
        string nameObya; string OpisanieObya; string priceObya; string ll;
        private void AtrinClick(object sender, MouseButtonEventArgs e)
        {
            if (loginT == null)
                this.NavigationService.Navigate(new MainPage(loginT));
            else
            {
                this.NavigationService.Navigate(new MainPage(loginT));
            }
        }

        private void KategoryClick(object sender, MouseButtonEventArgs e)
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
            if (((TextBlock)sender).Name == "yourKategory")
                kategory = kategoryOLDD;
            if (kategory.Length > 0)
            {
                Border1.Visibility = Visibility.Hidden;
                Border3.Visibility = Visibility.Visible;
                imgClick1.Visibility = Visibility.Visible;
            }

        } 
        private void NewClick(object sender, MouseButtonEventArgs e)
        {
            condition = newCondition.Text;
            newCondition.TextDecorations = TextDecorations.Underline;
            oldCondition.TextDecorations = TextDecorations.Strikethrough;
        }

        private void oldClick(object sender, MouseButtonEventArgs e)
        {
            condition = oldCondition.Text;
            oldCondition.TextDecorations = TextDecorations.Underline;
            newCondition.TextDecorations = TextDecorations.Strikethrough;
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

        private void adCLick(object sender, RoutedEventArgs e)
        {
            if (textName.Text.Length > 0)
            {

                if (condition.Length > 0 )
                {
                    if (textOpisanie.Text.Length > 0)
                    {
                        Border4.Visibility = Visibility.Visible;
                        Border3.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void AddPhoto(object sender, RoutedEventArgs e)
        {

            count = 1;
            dialog = new OpenFileDialog();
            dialog.Title = "Выбор логотипа";
            dialog.Filter = "Все форматы |*.jpg*;*.jpeg*;*.jpe*;*.png|PNG (*.png*)|*.png|JPEG (*.jpg*,*.jpeg*,*.jpe*)|*.jpg*;*.jpeg*;*.jpe";
            if (dialog.ShowDialog() == true)
            {
                if (!File.Exists($"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(dialog.FileName)}"))
                    File.Copy(dialog.FileName, $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(dialog.FileName)}");
                iMage.Source = new BitmapImage(new Uri($"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(dialog.FileName)}"));
                Border3.Visibility = Visibility.Hidden;
                Border2.Visibility = Visibility.Visible;
                z = true;
            }
        }

        private void againPhoto(object sender, RoutedEventArgs e)
        {
            Border3.Visibility = Visibility.Hidden;
            Border2.Visibility = Visibility.Visible;
            z = true;
            count = 0;
        }
        int priceLength = 0;
        int indexName = 0;
        int indexOpisanie = 0;
        int indexAdress = 0;
        static bool nameFlag = false;
        static bool opisanieFlag = false;
        static bool adressFlag = false;

        int indexName1, indexOpisanie1, indexAdress1 = 0;


        private void adCLick1(object sender, RoutedEventArgs e)
        {
            z = true;
            nameFlag = false;
            opisanieFlag = false;
            adressFlag = false;
            textOpisanie.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            textOpisanie.ToolTip = null;
            textName.ToolTip = null;
            priceText.ToolTip = null;
            AdresText.ToolTip = null;
            textName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            priceText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));
            AdresText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b7aff"));

            if (priceText.Text.Length > 0)
            {
                if (priceText.Text[0].ToString() == "0" || priceText.Text[0].ToString() == " ")
                {
                    z = false;
                    priceText.Foreground = Brushes.Red;
                    priceText.ToolTip = "Цена не должна быть и пустой, и должна начинаться хотя бы с одного рубля!";
                }
            }

            Customer2 customer = new Customer2();
            CustomerValidator3 validator = new CustomerValidator3();

            customer.Opisanie = textOpisanie.Text;
            customer.Price = priceText.Text;
            customer.Name = textName.Text;
            customer.Adress = AdresText.Text;
            var results = validator.Validate(customer);


            indexName1 = 0; indexOpisanie1 = 0; indexAdress1 = 0;

            foreach (var i111 in textName.Text)
            {
                if (Char.IsLetter(i111))
                {
                    indexName1++;
                    if (indexName1 >= 2)
                    {
                        nameFlag = true;
                        break;
                    }
                }
            }

            foreach (var i111 in textOpisanie.Text)
            {
                if (Char.IsLetter(i111))
                {
                    indexOpisanie1++;
                    if (indexOpisanie1 >= 5)
                    {
                        opisanieFlag = true;
                        break;
                    }
                }
            }
            foreach (var i111 in AdresText.Text)
            {
                if (Char.IsLetter(i111))
                {
                    indexAdress1++;
                    if (indexAdress1 >= 5)
                    {
                        adressFlag = true;
                        break;
                    }
                }
            }

            if (z && results.IsValid && nameFlag && opisanieFlag && adressFlag)
            {
                string Photo1 = string.Empty;
                if (count == 1)
                    Photo1 = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(dialog.FileName)}";
                else { Photo1 = Photo12; }
                var jsonRun = File.ReadAllText("objavlenieModer.json");
                List<Obya> list = JsonConvert.DeserializeObject<List<Obya>>(jsonRun);
                foreach (char c in priceText.Text)
                {
                    if (!Char.IsDigit(c))
                    {
                        priceZ = false;
                        break;
                    }
                }
                if (priceZ == true)
                {
                    foreach (var item in list)
                    {
                        if (item.Name == ll)
                        {
                            indexOpisanie = 0;
                            indexAdress = 0;
                            indexName = 0;
                            if (textName.Text.StartsWith(" "))
                            {
                                foreach (var d in textName.Text)
                                {
                                    if (Char.IsWhiteSpace(d))
                                    {
                                        indexName++;
                                    }
                                    else
                                        break;
                                }

                                textName.Text = textName.Text.Substring(indexName);
                            }

                            if (AdresText.Text.StartsWith(" "))
                            {
                                foreach (var d in AdresText.Text)
                                {
                                    if (Char.IsWhiteSpace(d))
                                    {
                                        indexAdress++;
                                    }
                                    else
                                        break;
                                }

                                AdresText.Text = AdresText.Text.Substring(indexAdress);
                            }

                            if (textOpisanie.Text.StartsWith(" "))
                            {
                                foreach (var d in textOpisanie.Text)
                                {
                                    if (Char.IsWhiteSpace(d))
                                    {
                                        indexOpisanie++;
                                    }
                                    else
                                        break;
                                }

                                textOpisanie.Text = textOpisanie.Text.Substring(indexOpisanie);
                            }

                            priceLength = priceText.Text.Length;
                            switch (priceLength)
                            {
                                case 5:
                                    priceText.Text = priceText.Text.Insert(2, " ");
                                    break;
                                case 6:
                                    priceText.Text = priceText.Text.Insert(3, " ");
                                    break;
                                case 7:
                                    priceText.Text = priceText.Text.Insert(1, " ");
                                    priceText.Text = priceText.Text.Insert(5, " ");
                                    break;
                            }

                            item.Name = textName.Text;
                            item.Price = priceText.Text + " ₽";
                            item.Country = AdresText.Text;
                            item.Status = "notChecked";
                            item.Kategory = kategory;
                            item.Condition = condition;
                            item.Opisanie = textOpisanie.Text;
                            item.Photo = Photo1;
                            item.NameProfile = nameT1;
                            break;
                        }
                    }
                    var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
                    File.WriteAllText("objavlenieModer.json", convertedJson);
                    this.NavigationService.Navigate(new MainPage(loginT));
                }

            }
            foreach (var failure in results.Errors)
            {
                if (failure.ToString().Contains("Opisanie"))
                {
                    textOpisanie.Foreground = Brushes.Red;
                    textOpisanie.ToolTip = failure.ToString();
                }
                if (failure.ToString().Contains("Price"))
                {
                    priceText.Foreground = Brushes.Red;
                    priceText.ToolTip = failure.ToString();
                }
                if (failure.ToString().Contains("Name"))
                {
                    textName.Foreground = Brushes.Red;
                    textName.ToolTip = failure.ToString();
                }
                if (failure.ToString().Contains("Adress"))
                {
                    AdresText.Foreground = Brushes.Red;
                    AdresText.ToolTip = failure.ToString();
                }
            }
            if (!nameFlag)
            {
                textName.Foreground = Brushes.Red;
                textName.ToolTip = "Название должно содержать только буквы, либо буквы и цифры, либо буквы и символы, либо буквы, цифры и символы!\nБукв должно быть больше 1!";

            }

            if (!opisanieFlag)
            {
                textOpisanie.Foreground = Brushes.Red;
                textOpisanie.ToolTip = "Описание должно содержать только буквы, либо буквы и цифры, либо буквы и символы, либо буквы, цифры и символы!\nБукв должно быть больше 4!";

            }
            if (!adressFlag)
            {
                AdresText.Foreground = Brushes.Red;
                AdresText.ToolTip = "Адрес должен содержать только буквы, либо буквы и цифры, либо буквы и символы, либо буквы, цифры и символы!\nБукв должно быть больше 4!";
            }

        }
                            



        private void prviewKeyDownNumber(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
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

        private void leftClick(object sender, MouseButtonEventArgs e)
        {
            if(Border3.Visibility == Visibility.Visible)
            {
                Border1.Visibility = Visibility.Visible;
                Border3.Visibility = Visibility.Hidden;
            }
            if(Border2.Visibility == Visibility.Visible)
            {
                Border3.Visibility = Visibility.Visible; 
                Border2.Visibility = Visibility.Hidden;
            }
            if(Border4.Visibility == Visibility.Visible)
            {
                Border2.Visibility = Visibility.Visible;
                Border4.Visibility = Visibility.Hidden;
                Border3.Visibility = Visibility.Hidden;
            }
        }
    }
}

