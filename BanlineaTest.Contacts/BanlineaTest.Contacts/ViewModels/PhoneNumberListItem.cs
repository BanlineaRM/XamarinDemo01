using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanlineaTest.Contacts.Models;
using Newtonsoft.Json;

namespace BanlineaTest.Contacts.ViewModels
{
    public class PhoneNumberListItem : PhoneNumber
    {
        private int index;

        public PhoneNumberListItem(int index, Country country, string number) {
            this.Index = index;
            this.Number = number;
            this.Country = country;
        }

        [JsonIgnore]
        public int Index {
            get { return this.index; }

            set {
                if (this.index == value) {
                    return;
                }

                this.index = value;
                OnPropertyChanged();
            }
        }

    }
}