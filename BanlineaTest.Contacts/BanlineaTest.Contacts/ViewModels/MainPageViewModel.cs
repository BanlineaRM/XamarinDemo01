using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using BanlineaTest.Contacts.Models;
using BanlineaTest.Contacts.Pages;
using Xamarin.Forms;
using System.Linq;

namespace BanlineaTest.Contacts.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields

        private Command onNewItemClicked;
        private Command onRegisterItemsClicked;
        private Command onAboutUsCommand;

        #endregion

        #region Constructors

        public MainPageViewModel() : base() {
            this.ContactsList = new ObservableCollection<Contact>();
        }

        #endregion

        #region Properties

        public ObservableCollection<Contact> ContactsList { get; set; }

        public bool ShowEmptyListMessage => this.ContactsList.Count == 0;

        public bool ShowDataList => this.ContactsList.Count > 0;
        
        #endregion

        #region Commands

        public ICommand OnNewItemClicked {
            get {
                return this.onNewItemClicked ?? (this.onNewItemClicked = new Command(
                           () => {
                               this.UpdateCanExecute(false, this.onNewItemClicked);

                               Page contactPage = new AddContactPage();
                               AddContactViewModel viewModel = contactPage.BindingContext as AddContactViewModel;
                               if (viewModel == null) {
                                   return;
                               }

                               viewModel.OnSaveContact += (sender, contact) => {
                                   this.ContactsList.Add(contact);

                                   OnPropertyChanged("ShowDataList");
                                   OnPropertyChanged("ShowEmptyListMessage");
                               };

                               NavigationManager.NavigateTo(contactPage);

                               this.UpdateCanExecute(true, this.onNewItemClicked);
                           }, () => this.CanExecute
                       ));
            }
        }

        public ICommand OnRegisterItemsClicked {
            get {
                return this.onRegisterItemsClicked ?? (this.onRegisterItemsClicked = new Command(
                    async () => {
                        this.UpdateCanExecute(false, this.onRegisterItemsClicked);

                        if (this.ContactsList.Count == 0)
                        {
                            await NavigationManager.CurrentPage.DisplayAlert("Error", TextResources.NoHayRegistro, TextResources.OkButtonText);
                            this.UpdateCanExecute(true, this.onRegisterItemsClicked);
                            return;
                        }

                        bool result = await Net.ServiceDataProvider.RegisterContact(new Net.RegisterRequest()
                        {
                            Contacts = this.ContactsList.ToList(),
                            Location = new Location(15, 15),
                            RegisteredBy = new Author()
                        });

                        if (result)
                        {
                            await
                                NavigationManager.CurrentPage.DisplayAlert("Succeeded!", "The contacts has been registered in the server.", TextResources.OkButtonText);
                        }
                        else
                        {
                            await NavigationManager.CurrentPage.DisplayAlert("Failed!", "There was an error registering contacts in the server.", TextResources.OkButtonText);
                        }

                        this.UpdateCanExecute(true, this.onRegisterItemsClicked);
                    }, () => this.CanExecute));
            }
        }
        
        public ICommand OnAboutUsCommand {
            get {
                return this.onAboutUsCommand ?? (this.onAboutUsCommand = new Command(
                           () => {
                               this.UpdateCanExecute(false, this.onAboutUsCommand);

                               Page aboutUsPage = new AboutUsPage();
                               NavigationManager.NavigateTo(aboutUsPage);

                               this.UpdateCanExecute(true, this.onAboutUsCommand);
                           }, () => this.CanExecute
                       ));
            }
        }

        #endregion
    }
}