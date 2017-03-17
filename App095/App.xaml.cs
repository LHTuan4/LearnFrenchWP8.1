using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using App095.Resources;
using App095.ViewModels;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using System.IO;
using AppDatabase;
using Classes;
using System.Collections.Generic;

namespace App095
{
    public partial class App : Application
    {
        private static MainViewModel viewModel = null;

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static MainViewModel ViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (viewModel == null)
                    viewModel = new MainViewModel();

                return viewModel;
            }
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            Database.isVoted = loadUserSetting() == "1";
            Database.initializeRelatedUrl();
            SaveFilesInHTMLFolderToIsoStore();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            // Ensure that application state is restored appropriately
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            // Ensure that required application state is persisted here.
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
        private static void SaveFilesInHTMLFolderToIsoStore()
        {
#if DEBUG
            // This deletes all existing files in IsolatedStorage - Useful in testing
            // In live should not do this, but only load files once - this speeds subsequent loading of the app
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                isoStore.Remove();
            }
#endif
            string[] files = AllFilesInHTMLFolder();

            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Debug.WriteLine("check for exist " + files[0]);
                if (!isoStore.FileExists(files[0]))
                {
                    foreach (string f in files)
                    {
                        Debug.WriteLine("copy to isolated storage " + f.ToString());
                        StreamResourceInfo sr = Application.GetResourceStream(new Uri(f, UriKind.Relative));

                        // T4 Template includes all files in source folder(s). This may include some which are not in the project
                        if (sr != null)
                        {
                            using (BinaryReader br = new BinaryReader(sr.Stream))
                            {
                                byte[] data = br.ReadBytes((int)sr.Stream.Length);
                                SaveFileToIsoStore(f, data);
                            }
                        }
                    }
                }
            }
        }

        private static void SaveFileToIsoStore(string fileName, byte[] data)
        {
            string strBaseDir = string.Empty;
            const string DelimStr = "/";
            char[] delimiter = DelimStr.ToCharArray();
            string[] dirsPath = fileName.Split(delimiter);

            // Get the IsoStore
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Recreate the directory structure
                for (int i = 0; i < dirsPath.Length - 1; i++)
                {
                    strBaseDir = System.IO.Path.Combine(strBaseDir, dirsPath[i]);
                    isoStore.CreateDirectory(strBaseDir);
                }

                // Create the file if not exists
                // or override if exist
                using (BinaryWriter bw = new BinaryWriter(new IsolatedStorageFileStream(fileName,
                        FileMode.Create, FileAccess.Write, FileShare.Write, isoStore)))
                {
                    bw.Write(data);
                }
            }
        }
        private static string[] AllFilesInHTMLFolder()
        {
            return new[] {
                           "Assets/Html/11AboutApp.html",
                           "Assets/Html/1210tiptab.html",
                           "Assets/Html/121tiptab.html",
                           "Assets/Html/122tiptab.html",
                           "Assets/Html/123tiptab.html",
                           "Assets/Html/124tiptab.html",
                           "Assets/Html/125tiptab.html",
                           "Assets/Html/126tiptab.html",
                           "Assets/Html/127tiptab.html",
                           "Assets/Html/128tiptab.html",
                           "Assets/Html/129tiptab.html",
                           "Assets/Html/12TIPSTAB.html",
                           "Assets/Html/135_chat.png",
                           "Assets/Html/13TAB.html",
                           "Assets/Html/14TAB.html",
                           "Assets/Html/15TAB.html",
                           "Assets/Html/16Book.html",
                           "Assets/Html/24-gift.png",
                           "Assets/Html/28-star.png",
                           "Assets/Html/31.png",
                           "Assets/Html/32bonus.html",
                           "Assets/Html/33bonus.html",
                           "Assets/Html/34.png",
                           "Assets/Html/37.png",
                           "Assets/Html/38.png",
                           "Assets/Html/42ShareThisApp.html",
                           "Assets/Html/45Puzzle1.html",
                           "Assets/Html/46MEMOGame.html",
                           "Assets/Html/46Puzzle2.html",
                           "Assets/Html/47AboutUs.html",
                           "Assets/Html/96x96.png",
                           "Assets/Html/AboutUs.css",
                           "Assets/Html/app.xml",
                           "Assets/Html/apple.png",
                           "Assets/Html/background13.jpg",
                           "Assets/Html/best.png",
                           "Assets/Html/BestAppsForPhone.jpg",
                           "Assets/Html/colormenupagearrow.png",
                           "Assets/Html/colormenupageitem1.png",
                           "Assets/Html/contentmenupagearrow.png",
                           "Assets/Html/ConversationConfidence.jpg",
                           "Assets/Html/ConversationConfidence2.png",
                           "Assets/Html/cordova.js",
                           "Assets/Html/Default.png",
                           "Assets/Html/getmoreicon2.png",
                           "Assets/Html/icon-96.png",
                           "Assets/Html/icon.png",
                           "Assets/Html/icon512coreboard.png",
                           "Assets/Html/icon96piano.png",
                           "Assets/Html/icon96relax.png",
                           "Assets/Html/icon96weightloss.png",
                           "Assets/Html/icon96x96bollywood.png",
                           "Assets/Html/icon96x96caloncabe.png",
                           "Assets/Html/icon96x96candy.png",
                           "Assets/Html/icon96x96carrace.png",
                           "Assets/Html/icon96x96catagory.png",
                           "Assets/Html/icon96x96celebrity.png",
                           "Assets/Html/icon96x96chess.png",
                           "Assets/Html/icon96x96contra.png",
                           "Assets/Html/icon96x96dapchuot.png",
                           "Assets/Html/icon96x96feeddragon.png",
                           "Assets/Html/icon96x96gkkid.png",
                           "Assets/Html/icon96x96gkquiz.png",
                           "Assets/Html/icon96x96gta.png",
                           "Assets/Html/icon96x96hoangtuech.png",
                           "Assets/Html/icon96x96lyrics.png",
                           "Assets/Html/icon96x96mileycyrus.png",
                           "Assets/Html/icon96x96minionxmas.png",
                           "Assets/Html/icon96x96phimtrang.png",
                           "Assets/Html/icon96x96pokemon.png",
                           "Assets/Html/icon96x96scarysound.png",
                           "Assets/Html/icon96x96scoreboard.png",
                           "Assets/Html/icon96x96spca.png",
                           "Assets/Html/icon96x96superhero.png",
                           "Assets/Html/icon96x96wcquiz.png",
                           "Assets/Html/icon96x96xanimal.png",
                           "Assets/Html/icon96x96zombiebird.png",
                           "Assets/Html/ImageList.html",
                           "Assets/Html/iPadrssheader.png",
                           "Assets/Html/listArrow.png",
                           "Assets/Html/listArrowSel.png",
                           "Assets/Html/loading.gif",
                           "Assets/Html/Localizable.strings",
                           "Assets/Html/Logo96.png",
                           "Assets/Html/moregame.html",
                           "Assets/Html/notificationpageicon.png",
                           "Assets/Html/PushNoti.html",
                           "Assets/Html/puzzle1.jpg",
                           "Assets/Html/puzzle2.jpg",
                           "Assets/Html/Rating.png",
                           "Assets/Html/RECORD.html",
                           "Assets/Html/Reservation.png",
                           "Assets/Html/rss-noimage.png",
                           "Assets/Html/rssheader.png",
                           "Assets/Html/rssreader.js",
                           "Assets/Html/SamplelistArrow.png",
                           "Assets/Html/samplemenuicon.png",
                           "Assets/Html/sidebarmenuicon.png",
                           "Assets/Html/stayupdate.html",
                           "Assets/Html/style.css",
                           "Assets/Html/tab1.html",
                           "Assets/Html/tab1.png",
                           "Assets/Html/tab2.html",
                           "Assets/Html/tab2.png",
                           "Assets/Html/tab3.html",
                           "Assets/Html/tab3.png",
                           "Assets/Html/tab4.html",
                           "Assets/Html/tab4.png",
                           "Assets/Html/tab5.png",
                           "Assets/Html/tab6.png",
                           "Assets/Html/wood_ipad.jpg",
                        };
        }
        public static void writeUserSetting(String data)
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var stream = new IsolatedStorageFileStream("Setting.txt",
                    FileMode.Create, FileAccess.Write, myIsolatedStorage);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                    writer.Close();
                }
                stream.Close();
            }
        }


        public static String loadUserSetting()
        {
            // Obtain a virtual store for the application.
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            // Specify the file path and options.
            if (!local.FileExists("Setting.txt"))
                return "0";

            using (var isoFileStream =
                    new System.IO.IsolatedStorage.IsolatedStorageFileStream
                        ("Setting.txt", System.IO.FileMode.Open, local))
            {
                // Read the data.
                using (var isoFileReader = new System.IO.StreamReader(isoFileStream))
                {
                    return isoFileReader.ReadToEnd();
                }
            }
        }
    }
}