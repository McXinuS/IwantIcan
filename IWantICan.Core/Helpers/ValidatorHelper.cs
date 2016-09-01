/*
 * By McXinuS
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IWantICan.Core.Helpers
{
	/// <summary>
	///  Result of validation of such input field as login, text, email and vk profile link.
	/// </summary>
	[Flags]
	public enum ValidationStatus
	{
		Ok = 0,
		Empty = 1,
		LoginInvalid = 4,
		PasswordShort = 16,
		PhoneInvalid = 32,
		EmailInvalid = 64,
		VkLinkInvalid = 128,
		CategoryInvalid = 256
	}

	/// <summary>
	///  Type of input field.
	/// </summary>
	public enum ValidationType
	{
		Common,  // not null or empty
		Login,
		Password,
		Phone,
		Email,
		Vk,
		Category
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
	/// Category:
	/// - not '-1'
	/// </summary>
	public static class ValidatorHelper
	{
		/// <summary>
		/// Check whether the field of specified type is valid.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsValid(string source, ValidationType type)
		{
			return Validate(source, type).Equals(ValidationStatus.Ok);
		}

		public static ValidationStatus Validate(string source, ValidationType type)
		{
			switch (type)
			{
				case ValidationType.Common:
					return ValidateCommon(source);
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
				case ValidationType.Category:
					return ValidateCategory(source);
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

		/// <summary>
		/// Sets error messages into observable dictionary.
		/// </summary>
		/// <param name="status">Result of validation</param>
		/// <param name="errors">Dictionary containing error messages.</param>
		private static void FillErrors(ValidationStatus status,
			string key,
			ValidationType type,
			ref ObservableDictionary<string, string> errors)
		{
			if (status.Equals(ValidationStatus.Ok))
				return;

			if (status.HasFlag(ValidationStatus.Empty))
				errors[key] = Constants.FieldIsRequired;
			else if (status.HasFlag(ValidationStatus.LoginInvalid))
				errors[key] = Constants.LoginInvalid;
			else if (status.HasFlag(ValidationStatus.PasswordShort))
				errors[key] = Constants.PasswordShort;
			else if (status.HasFlag(ValidationStatus.PhoneInvalid))
				errors[key] = Constants.PhoneInvalid;
			else if (status.HasFlag(ValidationStatus.EmailInvalid))
				errors[key] = Constants.EmailInvalid;
			else if (status.HasFlag(ValidationStatus.VkLinkInvalid))
				errors[key] = Constants.VkLinkInvalid;
			else if (status.HasFlag(ValidationStatus.CategoryInvalid))
				errors[key] = Constants.CategoryEmpty;
		}

		#region VALIDATION_RULES

		private static ValidationStatus ValidateCommon(string source)
		{
			if (string.IsNullOrWhiteSpace(source))
				return ValidationStatus.Empty;

			return ValidationStatus.Ok;
		}

		private static ValidationStatus ValidateLogin(string source)
		{
			if (string.IsNullOrWhiteSpace(source))
				return ValidationStatus.Empty;

			return source.Cast<char>().Any(
				c => !((c >= 'a' && c <= 'z')
				|| (c >= 'A' && c <= 'Z')
				|| c == '_'
				|| (c >= '0' && c <= '9'))
				) ? ValidationStatus.LoginInvalid : ValidationStatus.Ok;
		}

		private static ValidationStatus ValidatePassword(string source)
		{
			if (string.IsNullOrWhiteSpace(source))
				return ValidationStatus.Empty;

			if (source.Length < Constants.PasswordMinLength)
				return ValidationStatus.PasswordShort;

			return ValidationStatus.Ok;
		}

		private static ValidationStatus ValidatePhone(string source)
		{
			if (string.IsNullOrWhiteSpace(source))
				return ValidationStatus.Ok;
			
			var regex = new Regex(@"^(\+[1-9]{1})?[0-9]{10,11}$");
			var match = regex.Match(source);

			if (!match.Success)
				return ValidationStatus.PhoneInvalid;

			return ValidationStatus.Ok;
		}

		private static ValidationStatus ValidateEmail(string source)
		{
			if (string.IsNullOrWhiteSpace(source))
				return ValidationStatus.Ok;
			
			var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			var match = regex.Match(source);

			if (!match.Success)
				return ValidationStatus.EmailInvalid;

			return ValidationStatus.Ok;
		}

		private static ValidationStatus ValidateVkLink(string source)
		{
			if (string.IsNullOrWhiteSpace(source))
				return ValidationStatus.Ok;

			var linkLower = source.ToLower();

			if (linkLower.StartsWith("http://vk.com/")
			    || linkLower.StartsWith("https://vk.com/")
			    || linkLower.StartsWith("vk.com/")
			    || linkLower.Split('/').Length == 2)
			{
				return ValidationStatus.Ok;
			}

			return ValidationStatus.VkLinkInvalid;
		}

		private static ValidationStatus ValidateCategory(string source)
		{
			int catId;

			if (int.TryParse(source, out catId))
			{
				if (catId == -1)
					return ValidationStatus.CategoryInvalid;

				return ValidationStatus.Ok;
			}

			return ValidationStatus.CategoryInvalid;
		}

		#endregion VALIDATION_RULES
	}
}
