using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Globalization;
using System.Xml.Linq;
using projectPP.Properties;
using System.Windows.Documents;

namespace projectPP
{
    /// <summary>
    /// Логика взаимодействия для PlaceAnAd.xaml
    /// </summary>
    public partial class PlaceAnAd : Page
    {
        bool z = false;
        OpenFileDialog dialog = new OpenFileDialog();
        public string kategory = "";
        public string condition = "";
        private string status = "notChecked";
        int count = 0;
        string kategoryOLDD;
        public PlaceAnAd(string nameT)
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
            }
            textOpisanie.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            priceText.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            textName.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            AdresText.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));

        }


        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
        }

        string loginT; bool priceZ = true;
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
            if(kategoryOLDD != "")
            {
                if (((TextBlock)sender).Name == "yourKategory")
                    kategory = kategoryOLDD;
            }
           
            if (kategory.Length > 0)
            {
                Border1.Visibility = Visibility.Hidden;
                Border3.Visibility = Visibility.Visible;
                imgClick1.Visibility = Visibility.Visible;
            }

        }

        private void adCLick(object sender, RoutedEventArgs e)
        {
            if (textName.Text.Length > 0 && textName.Text.Length < 25 && textName.Text != "Укажите название")
            {

                if (condition.Length > 0)
                {
                    if (textOpisanie.Text.Length > 0 && textOpisanie.Text.Length < 151 && textOpisanie.Text != "Описание")
                    {
                        Border4.Visibility = Visibility.Visible;
                        Border3.Visibility = Visibility.Hidden;
                    }
                }
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

        string Photo12 = string.Empty;
        private void AddPhoto(object sender, RoutedEventArgs e)
        {
           
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
                count = 1;
                Photo12 = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(dialog.FileName)}";
            }
        }

        private void againPhoto(object sender, RoutedEventArgs e)
        {
            Border3.Visibility = Visibility.Hidden;
            Border2.Visibility = Visibility.Visible;
            z = true;
            count = 0;
        }
        int indexName = 0;
        int indexOpisanie = 0;
        int indexAdress = 0;
        int priceLength = 0;
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
                    Customer2 customer = new Customer2();
                CustomerValidator3 validator = new CustomerValidator3();

                    customer.Opisanie = textOpisanie.Text;
                    customer.Price = priceText.Text;
                    customer.Name = textName.Text;
                    customer.Adress = AdresText.Text;
                    var results = validator.Validate(customer);

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

                    priceZ = true;
                    string jsonObya = File.ReadAllText("objavlenieModer.json");
                    List<Obya> obya = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);


                    indexName1 = 0; indexOpisanie1 = 0; indexAdress1 = 0;

                    foreach (var i111 in textName.Text)
                    {
                        if (Char.IsLetter(i111))
                        {
                            indexName1++;
                            if(indexName1 >= 2)
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
                        string Photo = string.Empty;
                        if (count == 1)
                            Photo = $"{System.IO.Path.GetFullPath("Resources/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(dialog.FileName)}";
                        else { Photo = Photo12; }
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

                        Obya chel = new Obya(textName.Text, priceText.Text + " ₽", AdresText.Text, Photo, kategory, condition, textOpisanie.Text, status, loginT);
                        obya.Add(chel);
                        string json = JsonConvert.SerializeObject(obya, Formatting.Indented);
                        File.WriteAllText("objavlenieModer.json", json);

                        if (loginT == null)
                            this.NavigationService.Navigate(new MainPage(loginT));
                        else
                        {
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

        private void prviewKeyDownNumber(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
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

        private void leftClick(object sender, MouseButtonEventArgs e)
        {
            if (Border3.Visibility == Visibility.Visible)
            {
                Border1.Visibility = Visibility.Visible;
                Border3.Visibility = Visibility.Hidden;
                kategoryOLDD = kategory;
                yourKategory.Visibility = Visibility.Visible;
                yourKategory.Text = "Ваша категория: " + kategoryOLDD;
            }
            if (Border2.Visibility == Visibility.Visible)
            {
                renamePhoto.IsEnabled = IsEnabled;
                AddPhoto1.Content = "Изменить";
                Border3.Visibility = Visibility.Visible;
                Border2.Visibility = Visibility.Hidden;
            }
            if (Border4.Visibility == Visibility.Visible)
            {
                Border2.Visibility = Visibility.Visible;
                Border4.Visibility = Visibility.Hidden;
                Border3.Visibility = Visibility.Hidden;
            }
        }

       
    }
}
