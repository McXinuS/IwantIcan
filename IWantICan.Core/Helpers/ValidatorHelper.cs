/*
 * By McXinuS
*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IWantICan.Core.Helpers
{
    /// <summary>
    ///  Result of validation of such input field as login, text, email and vk profile link.
    /// </summary>
    [Flags]
    public enum ValidationStatus
    {
        // common
        Ok = 0,
        Empty = 1,
        // login
        LoginInvalid = 4,
        // password
        PasswordShort = 16,
        // phone
        PhoneInvalid = 32,
        // email
        EmailInvalid = 64,
        // vk link
        VkLinkInvalid = 128
    }

    /// <summary>
    ///  Type of input field.
    /// </summary>
    public enum ValidationType
    {
        Login,
        Password,
        Phone,
        Email,
        Vk,
        Common  // not null or empty
    }

    /// <summary>
    /// Check input fields according to the following rules:
    /// Login:
    /// - not empty/whitespaces;
    /// - only has latin/numeric/'_'.
    /// Password:
    /// - not empty/whitespaces;
    /// - has not less than Constants.PasswordMinLength length.
    /// Phone:
    /// - 9 to 15 numbers.
    /// Email:
    /// - just see RegEx.
    /// VkLink:
    /// - starts from [http[s]://]vk.com;
    /// - has 0 or 1 slash symbol.
    /// </summary>
    public static class ValidatorHelper
    {
        public static ValidationStatus Validate(string source, ValidationType type)
        {
            if (string.IsNullOrWhiteSpace(source))
                return ValidationStatus.Empty;

            switch (type)
            {
                case ValidationType.Login:
                    return ValidateLogin(source);
                case ValidationType.Password:
                    return ValidatePassword(source);
                case ValidationType.Phone:
                    return ValidatePhone(source);
                case ValidationType.Email:
                    return ValidateEmail(source);
                case ValidationType.Vk:
                    return ValidateVkLink(source);
                case ValidationType.Common:
                    return ValidateCommon(source);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static bool Validate(List<Tuple<string, string, ValidationType>> input,
            ref ObservableDictionary<string, string> errors)
        {
            errors.Clear();

            bool ok = true;

            foreach (var pair in input)
            {
                var res = Validate(pair.Item1, pair.Item3);
                if (!res.Equals(ValidationStatus.Ok))
                {
                    FillErrors(res, pair.Item2, pair.Item3, ref errors);
                    ok = false;
                }
            }

            return ok;
        }

        public static bool IsValid(string source, ValidationType type)
        {
            return Validate(source, type).Equals(ValidationStatus.Ok);
        }

        /// <summary>
        /// Sets error messages into observable dictionary.
        /// </summary>
        /// <param name="status">Results of validation</param>
        /// <param name="errors">Dictionary containing error messages.</param>
        public static void FillErrors(ValidationStatus status,
            string key,
            ValidationType type,
            ref ObservableDictionary<string, string> errors)
        {
            if (status.Equals(ValidationStatus.Ok))
                return;

            if (status.HasFlag(ValidationStatus.Empty))
                errors[key] = Constants.FieldIsRequired;

            switch (type)
            {
                case ValidationType.Login:
                    if (status.HasFlag(ValidationStatus.LoginInvalid))
                        errors[key] = Constants.LoginInvalid;
                    break;
                case ValidationType.Password:
                    if (status.HasFlag(ValidationStatus.PasswordShort))
                        errors[key] = Constants.PasswordShort;
                    break;
                case ValidationType.Phone:
                    if (status.HasFlag(ValidationStatus.PhoneInvalid))
                        errors[key] = Constants.PhoneInvalid;
                    break;
                case ValidationType.Email:
                    if (status.HasFlag(ValidationStatus.EmailInvalid))
                        errors[key] = Constants.EmailInvalid;
                    break;
                case ValidationType.Vk:
                    if (status.HasFlag(ValidationStatus.VkLinkInvalid))
                        errors[key] = Constants.VkLinkInvalid;
                    break;
                case ValidationType.Common:
                    if (status.HasFlag(ValidationStatus.Empty))
                        errors[key] = Constants.FieldIsRequired;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        /// Shows whether the string is a valid Vk profile link.
        /// </summary>
        private static ValidationStatus ValidateLogin(string source)
        {
            foreach (var c in source)
            {
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_' || (c >= '0' && c <= '9')))
                {
                    return ValidationStatus.LoginInvalid;
                }
            }

            return ValidationStatus.Ok;
        }

        /// <summary>
        /// Shows whether the string is a valid Vk profile link.
        /// </summary>
        private static ValidationStatus ValidatePassword(string source)
        {
            if (source.Length < Constants.PasswordMinLength)
                return ValidationStatus.PasswordShort;

            return ValidationStatus.Ok;
        }

        private static ValidationStatus ValidatePhone(string source)
        {
            var regex = new Regex(@"^(\+[1-9]{1})?[0-9]{10,11}$");
            var match = regex.Match(source);

            if (!match.Success)
                return ValidationStatus.PhoneInvalid;
            return ValidationStatus.Ok;
        }

        private static ValidationStatus ValidateEmail(string source)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(source);

            if (!match.Success)
                return ValidationStatus.EmailInvalid;
            return ValidationStatus.Ok;
        }

        /// <summary>
        /// Shows whether the link is a valid Vk profile link.
        /// </summary>
        private static ValidationStatus ValidateVkLink(string source)
        {
            var linkLower = source.ToLower();

            if (string.IsNullOrWhiteSpace(linkLower)
                || !(linkLower.StartsWith("http://vk.com/")
                     || linkLower.StartsWith("https://vk.com/")
                     || linkLower.StartsWith("vk.com/")
                     || linkLower.Split('/').Length == 1))
            {
                return ValidationStatus.VkLinkInvalid;
            }

            return ValidationStatus.Ok;
        }

        private static ValidationStatus ValidateCommon(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return ValidationStatus.Empty;

            return ValidationStatus.Ok;
        }
    }
}
