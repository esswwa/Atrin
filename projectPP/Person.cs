using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace projectPP
{
    internal class Person
    {
        public string Login1 { get; set; }
        public string Name1 { get; set; }
        public string Surname1 { get; set; }
        public string Otchestvo1 { get; set; }
        public string Password1 { get; set; }
        public string Number1 { get; set; }


        public Person(string Login1, string Name1, string Surname1, string Otchestvo1, string Password1, string Number1)
        {
            this.Login1 = Login1;
            this.Password1 = Password1;
            this.Name1 = Name1;
            this.Surname1 = Surname1;
            this.Otchestvo1 = Otchestvo1;
            this.Number1 = Number1;
        }
    }
}
