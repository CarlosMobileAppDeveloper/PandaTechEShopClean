using System;
namespace PandaTechEShop.Validations
{
    public class RequiredStringValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public IValidatableObject<string> RequiredValidatableObject { get; set; }

        public string RequiredString { get; set; }

        public bool Check(T value)
        {
            var str = value as string;

            if (RequiredValidatableObject != null)
            {
                return str == RequiredValidatableObject.Value;
            }

            return str == RequiredString;
        }
    }
}
