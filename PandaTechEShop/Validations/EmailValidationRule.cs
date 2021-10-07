using System;
using System.Text.RegularExpressions;

namespace PandaTechEShop.Validations
{
    public class EmailValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        protected string DefaultRegexPattern
            => @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        protected RegexOptions DefaultRegexOptions => RegexOptions.IgnoreCase;

        // TODO add DecoractionFlags (e.g. Trim)

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            var regex = new Regex(DefaultRegexPattern, DefaultRegexOptions);

            return regex.IsMatch(str);
        }
    }
}
