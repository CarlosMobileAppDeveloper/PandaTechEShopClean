using System.Linq;
using PandaTechEShop.Enums;
using Xamarin.Forms;

namespace PandaTechEShop.Effects
{
    public static class Effects
    {
        private class ApplyIOSSafeAreaAsPaddingEffect : RoutingEffect
        {
            public ApplyIOSSafeAreaAsPaddingEffect()
                : base("PandaTechEShop.Effects.ApplyIOSSafeAreaAsPaddingEffect")
            {
            }
        }

        private class IgnoreIOSSafeAreaOnScrollViewEffect : RoutingEffect
        {
            public IgnoreIOSSafeAreaOnScrollViewEffect()
                : base("PandaTechEShop.Effects.IgnoreIOSSafeAreaOnScrollViewEffect")
            {
            }
        }

        public static readonly BindableProperty ApplyIOSSafeAreaAsPaddingProperty = BindableProperty.CreateAttached("ApplyIOSSafeAreaAsPadding", typeof(IOSSafeArea), typeof(Effects), IOSSafeArea.None, BindingMode.OneWay, null, OnChanged<ApplyIOSSafeAreaAsPaddingEffect, IOSSafeArea>);

        public static readonly BindableProperty IOSSafeAreaBottomSizeProperty = BindableProperty.CreateAttached("IOSSafeAreaBottomSize", typeof(double?), typeof(Effects), null);

        public static readonly BindableProperty IOSSafeAreaTopSizeProperty = BindableProperty.CreateAttached("IOSSafeAreaTopSize", typeof(double?), typeof(Effects), null);

        public static readonly BindableProperty IOSSafeAreaLeftSizeProperty = BindableProperty.CreateAttached("IOSSafeAreaLeftSize", typeof(double?), typeof(Effects), null);

        public static readonly BindableProperty IOSSafeAreaRightSizeProperty = BindableProperty.CreateAttached("IOSSafeAreaRightSize", typeof(double?), typeof(Effects), null);

        public static readonly BindableProperty IgnoreIOSSafeAreaOnScrollViewProperty = BindableProperty.CreateAttached("IgnoreIOSSafeAreaOnScrollView", typeof(IOSSafeArea), typeof(Effects), IOSSafeArea.None, BindingMode.OneWay, null, OnChanged<IgnoreIOSSafeAreaOnScrollViewEffect, IOSSafeArea>);

        public static void SetApplyIOSSafeAreaAsPadding(BindableObject view, IOSSafeArea value)
        {
            view.SetValue(ApplyIOSSafeAreaAsPaddingProperty, value);
        }

        public static IOSSafeArea GetApplyIOSSafeAreaAsPadding(BindableObject view)
        {
            return (IOSSafeArea)view.GetValue(ApplyIOSSafeAreaAsPaddingProperty);
        }

        public static void SetIOSSafeAreaBottomSize(BindableObject view, double? value)
        {
            view.SetValue(IOSSafeAreaBottomSizeProperty, value);
        }

        public static double? GetIOSSafeAreaBottomSize(BindableObject view)
        {
            return (double?)view.GetValue(IOSSafeAreaBottomSizeProperty);
        }

        public static void SetIOSSafeAreaTopSize(BindableObject view, double? value)
        {
            view.SetValue(IOSSafeAreaTopSizeProperty, value);
        }

        public static double? GetIOSSafeAreaTopSize(BindableObject view)
        {
            return (double?)view.GetValue(IOSSafeAreaTopSizeProperty);
        }

        public static void SetIOSSafeAreaLeftSize(BindableObject view, double? value)
        {
            view.SetValue(IOSSafeAreaLeftSizeProperty, value);
        }

        public static double? GetIOSSafeAreaLeftSize(BindableObject view)
        {
            return (double?)view.GetValue(IOSSafeAreaLeftSizeProperty);
        }

        public static void SetIOSSafeAreaRightSize(BindableObject view, double? value)
        {
            view.SetValue(IOSSafeAreaRightSizeProperty, value);
        }

        public static double? GetIOSSafeAreaRightSize(BindableObject view)
        {
            return (double?)view.GetValue(IOSSafeAreaRightSizeProperty);
        }

        public static void SetIgnoreIOSSafeAreaOnScrollView(BindableObject view, IOSSafeArea value)
        {
            view.SetValue(IgnoreIOSSafeAreaOnScrollViewProperty, value);
        }

        public static IOSSafeArea GetIgnoreIOSSafeAreaOnScrollView(BindableObject view)
        {
            return (IOSSafeArea)view.GetValue(IgnoreIOSSafeAreaOnScrollViewProperty);
        }

        private static void OnChanged<TEffect, TProp>(BindableObject bindable, object oldValue, object newValue) where TEffect : Effect, new()
        {
            VisualElement visualElement = bindable as VisualElement;
            if (visualElement != null)
            {
                Effect effect = visualElement.Effects.FirstOrDefault((Effect e) => e is TEffect);
                if (effect != null)
                {
                    visualElement.Effects.Remove(effect);
                }
                if (!object.Equals(newValue, default(TProp)))
                {
                    visualElement.Effects.Add(new TEffect());
                }
            }
        }
    }
}
