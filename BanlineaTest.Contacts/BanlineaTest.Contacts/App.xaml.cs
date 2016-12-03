using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BanlineaTest.Contacts.Models;
using BanlineaTest.Contacts.Net;
using Xamarin.Forms;

namespace BanlineaTest.Contacts
{
    public partial class App : Application
    {
        public static List<Country> Countries { get; set; }

        public App()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                // load Countries list from the server
                await LoadCountries();
            });

            MainPage = new NavigationPage(new BanlineaTest.Contacts.MainPage());
        }

        private async Task LoadCountries() {
            // download the list 
            Countries = await ServiceDataProvider.GetCountriesList();
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
