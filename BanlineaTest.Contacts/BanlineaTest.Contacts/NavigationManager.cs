using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BanlineaTest.Contacts
{
    public static class NavigationManager
    {
        public static void NavigateTo(Page pageToNavigate) {
            CurrentNavigationPage.Navigation.PushAsync(pageToNavigate);
        }

        public static void NavigateToModal(Page pageToNavigate) {
            CurrentPage.Navigation.PushModalAsync(pageToNavigate);
        }

        public static void ClosePage() {
            CurrentPage.Navigation.PopAsync();
        }

        public static void CloseModalPage() {
            CurrentPage.Navigation.PopModalAsync();
        }

        public static Page CurrentPage {
            get {
                return Application.Current.MainPage;
            }
        }

        public static Page CurrentNavigationPage {
            get {
                var navPage = Application.Current.MainPage as NavigationPage;
                if (navPage != null)
                    return navPage;

                var resPage = Application.Current.MainPage as MasterDetailPage;
                if (resPage != null)
                    return resPage.Detail;

                return Application.Current.MainPage;
            }
        }

        public static MasterDetailPage MasterPage {
            get {
                return Application.Current.MainPage as MasterDetailPage;
            }
        }
    }
}
