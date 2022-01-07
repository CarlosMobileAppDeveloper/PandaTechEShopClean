using System.Collections.Generic;
using PandaTechEShop.Models.Product;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PandaTechEShop.Helpers
{
    public class AlternateMarginDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate UnevenTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // TODO: Maybe some more error handling here
            return ((ObservableRangeCollection<TrendingProduct>)((CollectionView)container).ItemsSource).IndexOf(item as TrendingProduct) % 2 == 0 ? EvenTemplate : UnevenTemplate;
        }
    }
}