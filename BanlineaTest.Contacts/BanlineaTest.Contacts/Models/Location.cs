using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanlineaTest.Contacts.Models
{
    public class Location
    {
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }

        public Location(Decimal latitude, Decimal longitude) {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
