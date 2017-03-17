using System;

namespace Classes
{
    public class MainPageItem
    {
        public enum TYPE { NORMAL, ADS }
        public String imageSource { get; set; }
        public String imageCaption { get; set; }
        public String imageMessage { get; set; }

        public TYPE itemType { get; set; }
    }
}