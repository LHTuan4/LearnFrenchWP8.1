using System;

namespace Classes
{
    public class AppPage
    {
        public enum PAGE_TYPE
        {
            a1, a2, a3, a4, a5, a6,
            b1, b2, b3, b4, b5, b6, b7, b8,
            c1, c2, c3, c4
        }

        public AppPage() { }

        public Uri link { get; set; }
        public PAGE_TYPE type { get; set; }
    }
}