using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PandaTechEShop.Effects
{
    [ContentProperty("Colors")]
    public abstract class Gradient : BindableObject
    {
        internal static readonly BindableProperty _backgroundGradientForcedHeightProperty = BindableProperty.CreateAttached("__BackgroundGradientForcedHeight", typeof(double), typeof(Gradient), -1.0);

        public ObservableCollection<GradientColor> Colors
        {
            get;
            private set;
        }

        internal static void SetBackgroundGradientForcedHeight(BindableObject view, double value)
        {
            view.SetValue(_backgroundGradientForcedHeightProperty, value);
        }

        internal static double GetBackgroundGradientForcedHeight(BindableObject view)
        {
            return (double)view.GetValue(_backgroundGradientForcedHeightProperty);
        }

        protected Gradient()
        {
            Colors = new ObservableCollection<GradientColor>();
        }
    }
}
