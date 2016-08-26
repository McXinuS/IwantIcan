namespace IWantICan.Core.Models
{
	public class ContactsEntry
	{
		public string Image { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }

		public ContactsEntry(string image, string value)
		{
			Image = image;
			Value = value;
		}

		public ContactsEntry(string image, string value, string description)
		{
			Image = image;
			Value = value;
			Description = description;
		}
	}
}
