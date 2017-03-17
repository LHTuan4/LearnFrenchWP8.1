using System.Windows;

namespace Classes
{
    public class MainPageItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalItem { get; set; }
        public DataTemplate AdsItem { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            MainPageItem pageItem = item as MainPageItem;
            if (pageItem != null)
            {
                if (pageItem.itemType == MainPageItem.TYPE.NORMAL)
                {
                    return NormalItem;
                }
                else
                {
                    return AdsItem;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
}