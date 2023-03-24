using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectPP
{
    internal class Obya
    {

        public string Name { get; set; }
        public string Price { get; set; }
        public string Country { get; set; }
        public string Photo { get; set; }
        public string Kategory {get; set; }
        public string Condition { get; set; }
        public string Opisanie { get; set; }
        public string Status { get; set; }
        public string NameProfile { get; set; }
        public Obya(string Name, string Price, string Country, string photo, string Kategory, string Condition, string Opisanie, string Status, string nameProfile)
        {

            this.Name = Name;
            this.Price = Price;
            this.Country = Country;
            this.Photo = photo;
            this.Kategory = Kategory;
            this.Condition = Condition;
            this.Opisanie = Opisanie;
            this.Status = Status;
            this.NameProfile = nameProfile;
        }
    }
}
