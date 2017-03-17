using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using App095.Resources;
using AppDatabase;
using OneSignalSDK_WP80;
using Classes;
using App095.Src.Pages;
using Microsoft.Phone.Tasks;

namespace App095
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            HtmlViewerPage.selectedPage = new AppPage();
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            while (NavigationService.BackStack.Any())
                NavigationService.RemoveBackEntry();
            base.OnNavigatedTo(e);
            Database.initializeMainPage(this);
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            OneSignal.Init("3b6b7aca-3aa3-447b-9a27-e5d16d5dd3ec", ReceivedNotification);
        }
        private static void ReceivedNotification(string message, IDictionary<string, string> additionalData, bool isActive)
        {
            System.Diagnostics.Debug.WriteLine("message: " + message);
            if (additionalData != null)
                System.Diagnostics.Debug.WriteLine("\nadditionalData:\n" + string.Join(";", additionalData.Select(x => x.Key + "=" + x.Value).ToArray()));
        }

        

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Database.isVoted)
            {
                if (MessageBox.Show("Your feedback means a lot to us", "Rate this app?", MessageBoxButton.OKCancel)
                    == MessageBoxResult.OK)
                {
                    var wbt = new WebBrowserTask();
                    wbt.Uri = new Uri("https://www.microsoft.com/store/apps/9nblggh52q43", UriKind.RelativeOrAbsolute);
                    wbt.Show();
                    App.writeUserSetting("1");
                }
                else
                {
                    App.writeUserSetting("0");
                }
            }
            else App.writeUserSetting("1");
        }
        private void img_a1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.a1;
            NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
        }

        private void img_a2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.a2;
            NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
        }

        private void img_a3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.a3;
            NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
        }

        private void img_a4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.a4;
            NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
        }

        private void img_a5_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.a5;
            NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
        }

        private void img_a6_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.a6;
            NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
        }

        private void longListSelector_BONUS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            MainPageItem selectedItem;
            if (parent.SelectedItem != null)
            {
                selectedItem = (MainPageItem)parent.SelectedItem;
            }
            else return;

            if (selectedItem.imageCaption == "\nBONUS 1")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.b1;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "BONUS 2")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.b2;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "BONUS 3")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.b3;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "BONUS 4")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.b4;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "\nBONUS 5")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.b5;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "\nBONUS 6")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.b6;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "BONUS 7 - Get DISCOUNT for FRENCH  DICTIONARY here")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.b7;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "If you don't like our suggestion, search for more HOW TO LEARN FRENCH material here")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.b8;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
        }

        private void longListSelector_MOREFUN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            MainPageItem selectedItem;
            if (parent.SelectedItem != null)
            {
                selectedItem = (MainPageItem)parent.SelectedItem;
            }
            else return;

            if (selectedItem.imageCaption == "\nLike Us? Rate for Us!")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.c1;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "\nGet more quality apps")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.c2;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "\nGet more FREE games here!")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.c3;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
            else if (selectedItem.imageCaption == "Contact Us")
            {
                HtmlViewerPage.selectedPage.type = AppPage.PAGE_TYPE.c4;
                NavigationService.Navigate(new Uri("/Src/Pages/HtmlViewerPage.xaml", UriKind.Relative));
            }
        }
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}