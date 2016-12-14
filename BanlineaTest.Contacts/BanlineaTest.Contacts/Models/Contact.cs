using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanlineaTest.Contacts.ViewModels;
using Newtonsoft.Json;

namespace BanlineaTest.Contacts.Models
{
    public class Contact : ObservableObject
    {
        #region Fields

        private string company;

        private ObservableCollection<string> emailsAddress;

        private string lastName;

        private string name;

        private List<PhoneNumber> phoneNumbers;

        private string photo;

        #endregion

        #region Constructors

        public Contact()
        {
            this.EmailsAddress = new ObservableCollection<string>();
            this.PhoneNumbers = new List<PhoneNumber>();
        }

        #endregion

        #region Properties

        public string Company
        {
            get { return this.company; }

            set
            {
                if (this.company == value) {
                    return;
                }

                this.company = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<string> EmailsAddress {
            get { return this.emailsAddress; }

            set {
                if (this.emailsAddress == value) {
                    return;
                }

                this.emailsAddress = value;
                OnPropertyChanged();
            }
        }

        public string LastName {
            get { return this.lastName; }

            set {
                if (this.lastName == value) {
                    return;
                }

                this.lastName = value;
                OnPropertyChanged();
            }
        }

        public string Name {
            get { return this.name; }

            set {
                if (this.name == value) {
                    return;
                }

                this.name = value;
                OnPropertyChanged();
            }
        }

        public List<PhoneNumber> PhoneNumbers {
            get { return this.phoneNumbers; }

            set {
                if (this.phoneNumbers == value) {
                    return;
                }

                this.phoneNumbers = value;
                OnPropertyChanged();
            }
        }
        
        public string Photo {
            get { return this.photo; }

            set {
                if (this.photo == value) {
                    return;
                }

                this.photo = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string FullName => $"{this.Name} {this.LastName}";

        [JsonIgnore]
        public string EmailsAsString => this.EmailsAddress == null
            ? string.Empty
            : string.Join(", ", this.EmailsAddress);

        [JsonIgnore]
        public string PhoneNumbersAsString => this.PhoneNumbers == null
            ? string.Empty
            : string.Join(" | ", this.PhoneNumbers.Select(w => $"+{w.Country.Code} {w.Number}"));

        #endregion
    }
}
