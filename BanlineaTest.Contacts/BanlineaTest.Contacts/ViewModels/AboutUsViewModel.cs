using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BanlineaTest.Contacts.ViewModels
{
    public class AboutUsViewModel : ViewModelBase
    {
        #region Fields

        private Command onSocialItemCommand;


        #endregion

        public ICommand OnSocialItemCommand {
            get {
                return this.onSocialItemCommand ?? (this.onSocialItemCommand = new Command(
                           paramValue => {
                               this.UpdateCanExecute(false, this.onSocialItemCommand);

                               string socialType = paramValue.ToString();

                               switch (socialType)
                               {
                                   case "LinkedIn":
                                       Device.OpenUri(new Uri(NetResources.LinkedInProfileUrl));
                                       break;

                                   case "Stackoverflow":
                                       Device.OpenUri(new Uri(NetResources.StackoverflowProfileUrl));
                                       break;

                                   case "Twitter":
                                       Device.OpenUri(new Uri(NetResources.TwitterProfileUrl));
                                       break;

                                   case "Facebook":
                                       Device.OpenUri(new Uri(NetResources.FacebookProfileUrl));
                                       break;

                                   case "Instagram":
                                       Device.OpenUri(new Uri(NetResources.InstagramProfileUrl));
                                       break;
                               }

                               this.UpdateCanExecute(true, this.onSocialItemCommand);
                           }, paramValue => this.CanExecute
                       ));
            }
        }
    }
}
