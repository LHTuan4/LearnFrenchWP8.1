using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Classes;
using Microsoft.Phone.Tasks;
using AppDatabase;

namespace App095.Src.Pages
{
    public partial class HtmlViewerPage : PhoneApplicationPage
    {
        public static AppPage selectedPage { get; set; }

        public HtmlViewerPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.webBrowser_Browser.IsScriptEnabled = true;

            if (AppDatabase.Database.relatedUrl[selectedPage.type].IndexOf("external:", 0) != -1)
            {
                String url = AppDatabase.Database.relatedUrl[selectedPage.type];
                if (url.IndexOf("external:") != -1)
                {
                    url = url.Replace("external:", "");
                    var wbt = new WebBrowserTask();
                    wbt.Uri = new Uri(url, UriKind.RelativeOrAbsolute);
                    wbt.Show();
                    NavigationService.GoBack();
                }
            }
            else
                if (AppDatabase.Database.relatedUrl[selectedPage.type].IndexOf("Assets", 0) != -1)
                {
                    this.webBrowser_Browser.Navigate(
                        new Uri(AppDatabase.Database.relatedUrl[selectedPage.type], UriKind.Relative));
                }
                else
                {
                    this.webBrowser_Browser.Source =
                        new Uri(AppDatabase.Database.relatedUrl[selectedPage.type]);
                }

        }

        private void webBrowser_Browser_Navigating(object sender, NavigatingEventArgs e)
        {
            String url = e.Uri.ToString();
            if (url.IndexOf("-external") != -1)
            {
                url = url.Replace("-external", "");
                var wbt = new WebBrowserTask();
                wbt.Uri = new Uri(url, UriKind.RelativeOrAbsolute);
                wbt.Show();
            }
        }


    }
}