using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanlineaTest.Contacts.ViewModels;

namespace BanlineaTest.Contacts.Models
{
    public class PhoneNumber : ObservableObject
    {
        #region Fields

        private Country country;

        private string number;

        #endregion

        #region Constructor

        public PhoneNumber()
        {
            this.Country = new Country();
            this.Number = string.Empty;
        }

        #endregion

        #region Properties

        public Country Country 
        {
            get { return this.country; }

            set {
                if (this.country == value) {
                    return;
                }

                this.country = value;
                OnPropertyChanged();
            }
        }

        public string Number {
            get { return this.number; }

            set {
                if (this.number == value) {
                    return;
                }

                this.number = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
