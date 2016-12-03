using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BanlineaTest.Contacts.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        #region Fields

        protected bool CanExecute;

        #endregion

        #region Constructors

        public ViewModelBase() 
        {
            this.CanExecute = true;
        }

        #endregion


        #region Methods

        public void UpdateCanExecute(bool status, Command command) {
            this.CanExecute = status;
            command.ChangeCanExecute();
        }

        #endregion
    }
}