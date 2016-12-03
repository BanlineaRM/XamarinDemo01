using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using BanlineaTest.Contacts.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace BanlineaTest.Contacts.ViewModels
{
    public class AddContactViewModel : ViewModelBase
    {
        #region Fields

        public ObservableCollection<ListItem> EmailsList { get; set; }

        public ObservableCollection<PhoneNumberListItem> PhoneNumbersList { get; set; }

        public event EventHandler<Contact> OnSaveContact;

        private Contact model;

        private byte[] userImageBytes;

        private Command onSaveCommand;

        private Command onCancelCommand;

        private Command onAddEmailCommand;

        private Command onDeleteEmailCommand;

        private Command onAddPhoneNumberCommand;

        private Command onDeletePhoneNumberCommand;

        private Command onChangePictureCommand;

        private Command onShowCountriesCommand;

        #endregion

        #region Constructors

        public AddContactViewModel() {
            this.Model = new Contact();

            this.EmailsList = new ObservableCollection<ListItem>();
            this.EmailsList.Add(new ListItem(this.EmailsList.Count, String.Empty));

            this.PhoneNumbersList = new ObservableCollection<PhoneNumberListItem>();
            this.PhoneNumbersList.Add(new PhoneNumberListItem(this.PhoneNumbersList.Count, Country.Colombia, string.Empty));


            // ToDo: dummy 
            this.Model.Name = "Mohammad Taghi";
            this.Model.LastName = "Jahed";
            this.Model.Company= "Processa S.A.S";

            this.EmailsList.Clear();
            this.EmailsList.Add(new ListItem(this.EmailsList.Count, "taghi.jahed@yahoo.com"));

            this.PhoneNumbersList.Clear();
            this.PhoneNumbersList.Add(new PhoneNumberListItem(this.PhoneNumbersList.Count, Country.Colombia, "3044337163"));
        }

        #endregion

        #region Properties

        public Contact Model {
            get { return this.model; }

            set {
                if (this.model == value) {
                    return;
                }

                this.model = value;
                OnPropertyChanged();
            }
        }

        public byte[] UserImageBytes {
            get { return this.userImageBytes; }

            set {
                if (this.userImageBytes == value) {
                    return;
                }

                this.userImageBytes = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand OnSaveCommand {
            get {
                return this.onSaveCommand ?? (this.onSaveCommand = new Command(
                           () => {
                               this.UpdateCanExecute(false, this.onSaveCommand);

                               if (!ValidateForm()) {
                                   this.UpdateCanExecute(true, this.onSaveCommand);
                                   return;
                               }

                               // save emails to the model
                               this.Model.EmailsAddress = new ObservableCollection<string>(this.EmailsList.Select(w => w.Text));

                               // save phone numbers to the model
                               this.Model.PhoneNumbers = this.PhoneNumbersList.Cast<PhoneNumber>().ToList();

                               // send data back to the main page (callback event)
                               this.OnSaveContact?.Invoke(this, this.Model);

                               // close current page
                               NavigationManager.ClosePage();

                               this.UpdateCanExecute(true, this.onSaveCommand);
                           }, () => this.CanExecute
                       ));
            }
        }

        public ICommand OnCancelCommand {
            get {
                return this.onCancelCommand ?? (this.onCancelCommand = new Command(
                           () => {
                               this.UpdateCanExecute(false, this.onCancelCommand);

                               // Command's code goes here

                               this.UpdateCanExecute(true, this.onCancelCommand);
                           }, () => this.CanExecute
                       ));
            }
        }

        public ICommand OnAddEmailCommand {
            get {
                return this.onAddEmailCommand ?? (this.onAddEmailCommand = new Command(
                           () => {
                               this.UpdateCanExecute(false, this.onAddEmailCommand);

                               this.EmailsList.Add(new ListItem(this.EmailsList.Count, String.Empty));
                               this.OnPropertyChanged("EmailsList");

                               this.UpdateCanExecute(true, this.onAddEmailCommand);
                           }, () => this.CanExecute
                       ));
            }
        }

        public ICommand OnDeleteEmailCommand {
            get {
                return this.onDeleteEmailCommand ?? (this.onDeleteEmailCommand = new Command(
                           data => {
                               var item = data as ListItem;
                               if (item != null) {
                                   int index = item.Index;
                                   this.EmailsList.RemoveAt(index);

                                   for (int i = 0; i < this.EmailsList.Count; i++) {
                                       this.EmailsList[i].Index = i;
                                   }
                               }
                           }
                       ));
            }
        }

        public ICommand OnAddPhoneNumberCommand {
            get {
                return this.onAddPhoneNumberCommand ?? (this.onAddPhoneNumberCommand = new Command(
                           () => {
                               this.UpdateCanExecute(false, this.onAddPhoneNumberCommand);

                               this.PhoneNumbersList.Add(new PhoneNumberListItem(this.PhoneNumbersList.Count, Country.Colombia, string.Empty));
                               this.OnPropertyChanged("PhoneNumbersList");
                               this.UpdateCanExecute(true, this.onShowCountriesCommand);

                               this.UpdateCanExecute(true, this.onAddPhoneNumberCommand);
                           }, () => this.CanExecute
                       ));
            }
        }

        public ICommand OnDeletePhoneNumberCommand {
            get {
                return this.onDeletePhoneNumberCommand ?? (this.onDeletePhoneNumberCommand = new Command(
                           data => {
                               var item = data as PhoneNumberListItem;
                               if (item != null) {
                                   int index = item.Index;
                                   this.PhoneNumbersList.RemoveAt(index);

                                   for (int i = 0; i < this.PhoneNumbersList.Count; i++) {
                                       this.PhoneNumbersList[i].Index = i;
                                   }
                               }
                           }
                       ));
            }
        }

        public ICommand OnChangePictureCommand
        {
            get
            {
                return this.onChangePictureCommand ?? (this.onChangePictureCommand = new Command(
                           async () =>
                           {
                               this.UpdateCanExecute(false, this.onChangePictureCommand);

                               this.UserImageBytes = await TakePicture();
                               this.Model.Photo = Convert.ToBase64String(this.UserImageBytes);

                               this.UpdateCanExecute(true, this.onChangePictureCommand);
                           }, () => this.CanExecute
                       ));
            }
        }

        public ICommand OnShowCountriesCommand
        {
            get
            {
                return this.onShowCountriesCommand ?? (this.onShowCountriesCommand = new Command(
                           async data =>
                           {
                               this.UpdateCanExecute(false, this.onShowCountriesCommand);

                               var item = data as PhoneNumberListItem;
                               if (item != null)
                               {
                                   // show actionsheet 
                                   var selectedItem = await NavigationManager.CurrentPage.DisplayActionSheet(
                                       TextResources.SelectCountryActionSheetTitle,
                                       null,
                                       TextResources.CancelButtonText,
                                       App.Countries.Select(w => w.Name).ToArray());

                                   // if the user didn't click on the Cancel, set the user's selection to the Country property [Item]
                                   if (selectedItem == TextResources.CancelButtonText) {
                                       this.UpdateCanExecute(true, this.onShowCountriesCommand);
                                       return;
                                   }

                                   // find the country according to the selected item
                                   item.Country = App.Countries.FirstOrDefault(w => w.Name == selectedItem) ??
                                                  Country.Colombia;
                               }

                               this.UpdateCanExecute(true, this.onShowCountriesCommand);
                           }, data => this.CanExecute
                       ));
            }
        }

        #endregion

        #region Methods

        private bool ValidateForm()
        {
            if (this.EmailsList.Count == 0) {
                NavigationManager.CurrentPage.DisplayAlert(TextResources.Error, TextResources.DebeDigitarUnCorreo, TextResources.OkButtonText);
                return false;
            }

            if (this.PhoneNumbersList.Count == 0) {
                NavigationManager.CurrentPage.DisplayAlert(TextResources.Error, TextResources.DebeDigitarUnNumeroDeCelular, TextResources.OkButtonText);
                return false;
            }

            // validate emails
            foreach (var emailItem in this.EmailsList) {
                if (!this.IsValidEmail(emailItem.Text)) {
                    NavigationManager.CurrentPage.DisplayAlert(TextResources.Error, TextResources.ElCorreoNoEsValido, TextResources.OkButtonText);
                    return false;
                }
            }

            // validate phone numbers
            foreach (var phoneItem in this.PhoneNumbersList) {
                if (!this.IsValidPhoneNumber(phoneItem.Number)) {
                    NavigationManager.CurrentPage.DisplayAlert(TextResources.Error, TextResources.ElNumeroDeCelularNoEsValido, TextResources.OkButtonText);
                    return false;
                }
            }

            // validate other fields
            if (string.IsNullOrWhiteSpace(this.Model.Name) || string.IsNullOrWhiteSpace(this.Model.LastName) || string.IsNullOrWhiteSpace(this.Model.Company))
            {
                NavigationManager.CurrentPage.DisplayAlert(TextResources.Error, TextResources.TodosLosCamposSonRequeridos, TextResources.OkButtonText);
                return false;
            }

            return true;
        }

        private bool IsValidPhoneNumber(string number)
        {
            if (number.Length < 10)
                return false;

            if (!number.StartsWith("3"))
                return false;

            return true;
        }

        private bool IsValidEmail(string email)
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var validEmailRegex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);
            return validEmailRegex.IsMatch(email);
        }


        public static byte[] StreamToByteArray(Stream input) {
            using (MemoryStream ms = new MemoryStream()) {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private async Task<byte[]> TakePicture() {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                await NavigationManager.CurrentPage.DisplayAlert(TextResources.Camera, TextResources.NoCameraAvailable, TextResources.OkButtonText);
                return null;
            }

            MediaFile pictureMediaFile = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    AllowCropping = true,
                    SaveToAlbum = false,
                    CompressionQuality = 10,
                    PhotoSize = PhotoSize.Small
                });

            if (pictureMediaFile == null) {
                return null;
            }

            Stream resStream = pictureMediaFile.GetStream();
            pictureMediaFile.Dispose();

            return StreamToByteArray(resStream);
        }

        #endregion
    }
}