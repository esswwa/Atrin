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
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace projectPP
{
    /// <summary>
    /// Логика взаимодействия для adminObjavlenie.xaml
    /// </summary>
    public partial class adminObjavlenie : Page
    {
        public adminObjavlenie(string Name, string Price, string Country, string Photo, string Kategory, string Condition, string Opisanie, string nameT, string profileText1)
        {
            InitializeComponent();

            var jsonUser1 = File.ReadAllText("users.json");
            List<Person> list1 = JsonConvert.DeserializeObject<List<Person>>(jsonUser1);

            foreach (var item in list1)
            {
                if (item.Login1.ToString() == profileText1)
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
                priceText.Text = Price + "₽";
            }
            Adres.Text = "Адрес: " + Country;
            photo1 = Photo;
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
            profileText.Text = profileText1;
            opisanie1 = Opisanie;
            condition1 = Condition;
            adres1 = Country;
            num = numberPhone.Content.ToString();
            
        }

        long parese;
        public string opisanie1 = "";
        public string condition1 = "";
        public string adres1 = "";
        public string status1 = "";
        public string photo1 = "";
        string Photo, num;
        bool pr = false;
        private void AtrinClick(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new AdminPage("esswwa"));
        }

        private void blockClick(object sender, RoutedEventArgs e)
        {
            //
            var jsonObya = File.ReadAllText("objavlenieModer.json");
            List<Obya> listOtchet = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);

            foreach (var item1 in listOtchet)
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
                }
            }
            //
            try
            {
                GetCheck(Photo, "Заблокирован");
            }
            catch (Exception ex) { }
            if (pr)
            {
                status1 = "blocked";
                var jsonRun = File.ReadAllText("objavlenieModer.json");
                List<Obya> list = JsonConvert.DeserializeObject<List<Obya>>(jsonRun);

                foreach (var item in list)
                {
                    if (item.Name.Contains(nameText.Text) && item.Opisanie.Contains(opisanie1))
                    {
                        item.Status = status1;
                        break;
                    }
                }
                var convertedJson = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("objavlenieModer.json", convertedJson);
                this.NavigationService.Navigate(new MainPage("esswwa"));
            }
           
        }

        private void uploadClick(object sender, RoutedEventArgs e)
        {
            //
            var jsonObya = File.ReadAllText("objavlenieModer.json");
            List<Obya> listOtchet = JsonConvert.DeserializeObject<List<Obya>>(jsonObya);

            foreach (var item1 in listOtchet)
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
                }
            }
            try
            {
                GetCheck(Photo, "Одобрен");
            }
            catch (Exception ex) { }
            //
            if (pr)
            {
                status1 = "checked";
                var jsonRun = File.ReadAllText("objavlenieModer.json");
                List<Obya> list = JsonConvert.DeserializeObject<List<Obya>>(jsonRun);

                foreach (var item in list)
                {

                    if (item.Name.Contains(nameText.Text) && item.Opisanie.Contains(opisanie1))
                    {
                        item.Status = status1;
                        break;
                    }
                }
                var convertedJson = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("objavlenieModer.json", convertedJson);
                this.NavigationService.Navigate(new AdminPage("esswwa"));
            }
        }
        
        private static Dictionary<string, string> _replacePatterns;
        private void GetCheck(string IMAGE, string STATUS)
        {
            try
            {
                _replacePatterns = new Dictionary<string, string>()
                {
                    { "LOGIN", "esswwa"},
                    { "NAME", nameText.Text},
                    { "NUMBER", num},
                    { "PRICE", priceText.Text},
                    { "CONDITION", condition1},
                    { "LOGINOBYA", profileText.Text},
                    { "OPISANIE", opisanie1},
                    { "ADRES", adres1},
                    { "KATEGORY", Kategory1.Text},
                    { "STATUS", STATUS},
                };

                using (var document = DocX.Load(System.IO.Path.GetFullPath(@"Resources\asd.docx").Replace(@"\bin\Debug\", @"\")))
                {
                    document.ReplaceText("<(.*?)>", ReplaceFunc);

                    var image = document.AddImage(IMAGE);
                    var picture = image.CreatePicture(150, 150);
                    var p = document.InsertParagraph("").InsertPicture(picture);
                    p.Alignment = Alignment.right;
                    var p1 = document.InsertParagraph($"{DateTime.Now:dd/MM/yyyy}");
                    p1.Alignment = Alignment.right;
                    SaveFileDialog sfd = new SaveFileDialog();

                    sfd.FileName = "Отчет";
                    sfd.DefaultExt = ".docx";
                    sfd.AddExtension = true;
                    sfd.Filter = "Документ Word (*.docx)|*.docx";
                    sfd.Title = "Сохранение фига";

                    if (sfd.ShowDialog() is true)
                    {
                        document.SaveAs(sfd.FileName);
                        pr = true;
                    }
                    else
                        pr = false;
                }
            }
            catch (Exception ex) { }
        }

        private static string ReplaceFunc(string findStr)
        {
            if (_replacePatterns.ContainsKey(findStr))
            {
                return _replacePatterns[findStr];
            }
            return findStr;
        }

    }
}
