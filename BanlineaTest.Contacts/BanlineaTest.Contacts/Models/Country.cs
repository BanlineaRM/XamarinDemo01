using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanlineaTest.Contacts.ViewModels;

namespace BanlineaTest.Contacts.Models
{
    public class Country : ObservableObject
    {
        #region Fields

        private int code;

        private string name;

        #endregion

        #region Properties
        
        public int Code {
            get { return this.code; }

            set {
                if (this.code == value) {
                    return;
                }

                this.code = value;
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

        public static Country Colombia { get; set; } = new Country {Code = 57, Name = "Colombia"};

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"Code: {this.Code}, Name: {this.name}";
        }

        #endregion
    }
}
