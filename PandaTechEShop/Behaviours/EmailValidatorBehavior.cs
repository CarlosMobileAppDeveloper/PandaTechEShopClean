using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PandaTechEShop.Behaviours
{
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        private const string _emailRegex =
            "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

        private static readonly BindablePropertyKey _isValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), true);

        public static readonly BindableProperty IsValidProperty = _isValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get
            {
                return (bool)GetValue(IsValidProperty);
            }
            private set
            {
                SetValue(_isValidPropertyKey, value);
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (!string.IsNullOrEmpty(e.NewTextValue) && Regex.IsMatch(e.NewTextValue, _emailRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250.0)));
        }
    }
}