using CoreGraphics;
using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;

namespace PandaTechEShop.iOS.Effects
{
    public class ShadowEffect : PlatformEffect
    {
        private CGSize _originalOffset;

        private CGColor _originalColor;

        private float _originalOpacity;

        private nfloat _originalRadius;

        protected override void OnAttached()
        {
            if (base.Container != null)
            {
                _originalOffset = base.Container.Layer.ShadowOffset;
                _originalColor = base.Container.Layer.ShadowColor;
                _originalOpacity = base.Container.Layer.ShadowOpacity;
                _originalRadius = base.Container.Layer.ShadowRadius;
                UpdateShadow();
            }
        }

        protected override void OnDetached()
        {
            if (base.Container?.Layer != null)
            {
                base.Container.Layer.ShadowColor = _originalColor;
                base.Container.Layer.ShadowOffset = _originalOffset;
                base.Container.Layer.ShadowOpacity = _originalOpacity;
                base.Container.Layer.ShadowRadius = _originalRadius;
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == PandaTechEShop.Effects.Effects.ShadowProperty.PropertyName || args.PropertyName == PandaTechEShop.Effects.Effects.ShadowIOSColorProperty.PropertyName || args.PropertyName == PandaTechEShop.Effects.Effects.ShadowOpacityProperty.PropertyName || args.PropertyName == PandaTechEShop.Effects.Effects.ShadowSizeProperty.PropertyName)
            {
                UpdateShadow();
            }
        }

        private void UpdateShadow()
        {
            base.Container.Layer.ShadowColor = PandaTechEShop.Effects.Effects.GetShadowIOSColor(base.Element).ToCGColor();
            nfloat nfloat = PandaTechEShop.Effects.Effects.GetShadowSize(base.Element);
            base.Container.Layer.ShadowOffset = new CGSize(0f, (nfloat > 0) ? 1 : 0);
            base.Container.Layer.ShadowRadius = ((nfloat > 0) ? nfloat : ((nfloat)0));
            base.Container.Layer.ShadowOpacity = PandaTechEShop.Effects.Effects.GetShadowOpacity(base.Element);
        }
    }
}
