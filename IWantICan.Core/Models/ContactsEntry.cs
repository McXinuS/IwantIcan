using System;

namespace IWantICan.Core.Models
{
	public enum ContactType
	{
		Phone,
		Email,
		VkLink
	}

	public class ContactsEntry
	{
		public ContactType Type { get; set; }
		public string Image { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }

		public ContactsEntry(ContactType type, string value)
		{
			Type = type;

			switch (type)
			{
				case ContactType.Phone:
					Image = Constants.ProfilePhoneImage;
					Description = Constants.ProfilePhone;
					break;
				case ContactType.Email:
					Image = Constants.ProfileEmailImage;
					Description = Constants.ProfileEmail;
					break;
				case ContactType.VkLink:
					Image = Constants.ProfileVkImage;
					Description = Constants.ProfileVk;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}

			Value = value;
		}
	}
}
