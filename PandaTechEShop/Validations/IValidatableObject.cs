using System.Collections.Generic;

namespace PandaTechEShop.Validations
{
    public interface IValidatableObject<T>
    {
        bool IsValid { get; set; }
        List<string> Errors { get; set; }
        List<IValidationRule<T>> Validations { get; }
        T Value { get; set; }
        bool Validate();
    }
}
