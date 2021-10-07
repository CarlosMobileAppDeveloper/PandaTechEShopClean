using System.Collections.Generic;
using PropertyChanged;
using System.Linq;

namespace PandaTechEShop.Validations
{
    // Taken from Microsoft eshop-mobile-client
    // https://github.com/dotnet-architecture/eshop-mobile-client/blob/main/eShopOnContainers/eShopOnContainers.Core/Validations/ValidatableObject.cs

    [AddINotifyPropertyChangedInterface]
    public class ValidatableObject<T> : IValidity
    {
        private readonly List<IValidationRule<T>> _validations;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get;
            set;
        }

        public T Value
        {
            get;
            set;
        }

        public bool IsValid
        {
            get;
            set;
        }

        public ValidatableObject()
        {
            IsValid = true;
            Errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
