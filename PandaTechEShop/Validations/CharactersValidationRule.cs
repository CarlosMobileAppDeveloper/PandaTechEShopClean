using System;
using System.Collections.Generic;
using System.Linq;

namespace PandaTechEShop.Validations
{
    public class CharactersValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public CharacterType CharacterType { get; set; }
        public int MinimumCharacterCount { get; set; }
        public int MaximumCharacterCount { get; set; } = int.MaxValue;

        public bool Check(T value)
        {
            var str = value as string;

            var characterPredicates = GetCharacterPredicates(CharacterType).ToList();
            var count = str?.ToCharArray().Count(character => characterPredicates.Any(predicate => predicate.Invoke(character))) ?? 0;
            return count >= MinimumCharacterCount
                && count <= MaximumCharacterCount;
        }

        private IEnumerable<Predicate<char>> GetCharacterPredicates(CharacterType characterType)
        {
            if (characterType.HasFlag(CharacterType.LowercaseLetter))
            {
                yield return char.IsLower;
            }

            if (characterType.HasFlag(CharacterType.UppercaseLetter))
            {
                yield return char.IsUpper;
            }

            if (characterType.HasFlag(CharacterType.Digit))
            {
                yield return char.IsDigit;
            }

            if (characterType.HasFlag(CharacterType.Whitespace))
            {
                yield return char.IsWhiteSpace;
            }

            if (characterType.HasFlag(CharacterType.NonAlphanumericSymbol))
            {
                yield return c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c);
            }

            if (characterType.HasFlag(CharacterType.LowercaseLatinLetter))
            {
                yield return c => c >= 'a' && c <= 'z';
            }

            if (characterType.HasFlag(CharacterType.UppercaseLatinLetter))
            {
                yield return c => c >= 'A' && c <= 'Z';
            }
        }
    }

    /// <summary>
    /// The allowed character types used to determine if a value is valid in the <see cref="CharactersValidationBehavior"/>. Since this is a flag, multiple flags cane be combined.
    /// </summary>
    [Flags]
    public enum CharacterType
    {
        /// <summary>Lowercase characters are allowed.</summary>
        LowercaseLetter = 1,

        /// <summary>Uppercase characters are allowed.</summary>
        UppercaseLetter = 2,

        /// <summary>Either lowercase characters or uppercase characters are allowed.</summary>
        Letter = LowercaseLetter | UppercaseLetter,

        /// <summary>Digits are allowed.</summary>
        Digit = 4,

        /// <summary>Characters and digits are allowed.</summary>
        Alphanumeric = Letter | Digit,

        /// <summary>Whitespace is allowed.</summary>
        Whitespace = 8,

        /// <summary>Non-alphanumeric symbols are allowed.</summary>
        NonAlphanumericSymbol = 16,

        /// <summary>Lowercase latin characters are allowed.</summary>
        LowercaseLatinLetter = 32,

        /// <summary>Uppercase latin characters are allowed.</summary>
        UppercaseLatinLetter = 64,

        /// <summary>Either latin lowercase characters or latin uppercase characters are allowed.</summary>
        LatinLetter = LowercaseLatinLetter | UppercaseLatinLetter,

        /// <summary>Any type of character or digit either lowercase or uppercase, latin or non-latin is allowed.</summary>
        Any = Alphanumeric | NonAlphanumericSymbol | Whitespace,
    }
}
