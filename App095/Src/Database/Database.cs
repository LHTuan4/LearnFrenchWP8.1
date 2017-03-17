using Classes;
using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using System.Windows;
using App095;

namespace AppDatabase
{
    public class Database
    {
        public static Dictionary<AppPage.PAGE_TYPE, String> relatedUrl;
        public static bool isVoted { get; set; }
        public static void initializeMainPage(MainPage page)
        {
            /* Item for FUN Page */

            List<MainPageItem> itemsForBonuses = new List<MainPageItem>();
            itemsForBonuses.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/31.png",
                imageCaption = "\nBONUS 1",
                imageMessage = "Get the FREE app to start learning Spanish NOW!"
            });
            itemsForBonuses.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/ConversationConfidence.jpg",
                imageCaption = "BONUS 2",
                imageMessage = "Get your FREE 10 Groundbreaking Persuasion Secrets Course here"
            });
            itemsForBonuses.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/ConversationConfidence2.png",
                imageCaption = "BONUS 3",
                imageMessage = "A Social Confidence Coach Reveals the Secrets to Making Effortless, Confident and Captivating Conversation with Anyone"
            });

            itemsForBonuses.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/34.png",
                imageCaption = "BONUS 4",
                imageMessage = "FREE App which teaches how to get rid of Fear of Public Speaking"
            });
            itemsForBonuses.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/icon96relax.png",
                imageCaption = "\nBONUS 5",
                imageMessage = "Keep your mind fresh to learn even faster..."
            });

            itemsForBonuses.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/icon96weightloss.png",
                imageCaption = "\nBONUS 6",
                imageMessage = "A sound mind in a sound body"
            });
            itemsForBonuses.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/37.png",
                imageCaption = "BONUS 7 - Get DISCOUNT for FRENCH  DICTIONARY here",
                imageMessage = "Don't miss the HOLIDAY season deal…"
            });
            itemsForBonuses.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/38.png",
                imageCaption = "If you don't like our suggestion, search for more HOW TO LEARN FRENCH material here",
                imageMessage = ""
            });
            page.longListSelector_BONUS.ItemsSource = itemsForBonuses;

            /* Item for FUN Page */
            List<MainPageItem> itemsForFun = new List<MainPageItem>();
            itemsForFun.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/Rating.png",
                imageCaption = "\nLike Us? Rate for Us!",
                imageMessage = "Your feedback means a lot to us..."
            });
            
            itemsForFun.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/getmoreicon2.png",
                imageCaption = "\nGet more quality apps",
                imageMessage = "Enjoy all great Window Phone apps here!"
            });
            itemsForFun.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/icon96x96contra.png",
                imageCaption = "\nGet more FREE games here!",
                imageMessage = ""
            });
            itemsForFun.Add(new MainPageItem()
            {
                imageSource = "/Assets/Html/Logo96.png",
                imageCaption = "Contact Us",
                imageMessage = "Visit our website, fan page or send us email about how you feel about this app and our service here."
            });

            page.longListSelector_MOREFUN.ItemsSource = itemsForFun;

       
        }
        public static void initializeRelatedUrl()
        {
            relatedUrl = new Dictionary<AppPage.PAGE_TYPE, String>();

            relatedUrl.Add(AppPage.PAGE_TYPE.a1, "/Assets/Html/11AboutApp.html");
            relatedUrl.Add(AppPage.PAGE_TYPE.a2, "/Assets/Html/12TIPSTAB.html");
            relatedUrl.Add(AppPage.PAGE_TYPE.a3, "/Assets/Html/13TAB.html");
            relatedUrl.Add(AppPage.PAGE_TYPE.a4, "/Assets/Html/14TAB.html");
            relatedUrl.Add(AppPage.PAGE_TYPE.a5, "/Assets/Html/15TAB.html");
            relatedUrl.Add(AppPage.PAGE_TYPE.a6, "/Assets/Html/16Book.html");

            relatedUrl.Add(AppPage.PAGE_TYPE.b1, "http://www.amazon.com/gp/mas/dl/android?p=com.bestappsforphone.bestwaytolearnspanishfasthowtospeakspanishfreeonlineapp");
            relatedUrl.Add(AppPage.PAGE_TYPE.b2, "/Assets/Html/32bonus.html");
            relatedUrl.Add(AppPage.PAGE_TYPE.b3, "/Assets/Html/33bonus.html");
            relatedUrl.Add(AppPage.PAGE_TYPE.b4, "http://www.bestappsforphone.com/windowsphone2");
            relatedUrl.Add(AppPage.PAGE_TYPE.b5, "https://www.microsoft.com/store/apps/9nblggh525bm");
            relatedUrl.Add(AppPage.PAGE_TYPE.b6, "http://www.bestappsforphone.com/windowsphone2");
            relatedUrl.Add(AppPage.PAGE_TYPE.b7, "http://www.amazon.com/gp/product/0198614225/ref=as_li_ss_tl?ie=UTF8&tag=myhomkin-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=0198614225");
            relatedUrl.Add(AppPage.PAGE_TYPE.b8, "http://www.amazon.com/mn/search/?_encoding=UTF8&tag=myhomkin-20&linkCode=ur2&camp=1789&creative=390957&field-keywords=learn%20french&url=search-alias&ajr=0");

            relatedUrl.Add(AppPage.PAGE_TYPE.c1, "https://www.microsoft.com/store/apps/9nblggh52q43");
            relatedUrl.Add(AppPage.PAGE_TYPE.c2, "http://www.bestappsforphone.com/windowsphone");
            relatedUrl.Add(AppPage.PAGE_TYPE.c3, "http://www.bestappsforphone.com/windowsphone2");
            relatedUrl.Add(AppPage.PAGE_TYPE.c4, "/Assets/Html/47AboutUs.html");

        }

    }
}