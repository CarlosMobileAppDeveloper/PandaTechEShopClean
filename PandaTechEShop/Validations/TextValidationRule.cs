using System;
namespace PandaTechEShop.Validations
{
    public class TextValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public TextValidationRuleType ValidationRuleType { get; set; }
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }
        //public string? RegexPattern { get; set; }

        // TODO add DecoractionFlags (e.g. Trim)

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            switch (ValidationRuleType)
            {
                case TextValidationRuleType.None:
                    return false;
                case TextValidationRuleType.MinimumLength:
                    return str.Length >= MinimumLength;
                case TextValidationRuleType.MaximumLength:
                    return str.Length <= MaximumLength;
                //case TextValidationRuleType.RegexPattern:
                // TODO
                default:
                    return false;
            }
        }
    }

    public enum TextValidationRuleType
    {
        None,
        MinimumLength,
        MaximumLength,
        RegexPattern,
    }
}
