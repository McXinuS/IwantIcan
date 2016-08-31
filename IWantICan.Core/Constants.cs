namespace IWantICan.Core
{
	public static class Constants
	{
        // REST
	    public const string RestUrl = "http://46.101.99.134:8080/api{0}";
        public static string CategoryUrl        = string.Format(Constants.RestUrl, "/user/subcategories");
        public static string CategorySingleUrl  = string.Format(Constants.RestUrl, "/user/subcategories/subcategory");
        public static string CanUrl             = string.Format(Constants.RestUrl, "/user/offers");
        public static string CanListByUserUrl   = string.Format(Constants.RestUrl, "/user/offers/byUser");
        public static string CanListByCatUrl    = string.Format(Constants.RestUrl, "/user/offers/bySubCategory");
        public static string UserLoginUrl   = string.Format(Constants.RestUrl, "/user/profile/logIn");
        public static string UserCreateUrl  = string.Format(Constants.RestUrl, "/user/profile/create");
        public static string UserGetUrl     = string.Format(Constants.RestUrl, "/user/profile/getInfo");
        public static string UserUpdateUrl  = string.Format(Constants.RestUrl, "/user/profile/updateInfo");
        public static string WantUrl        = string.Format(Constants.RestUrl, "/user/userwants");
        public static string WantListUrl    = string.Format(Constants.RestUrl, "/user/userwants/bySubCategory");

        // VK
        public static string VkApiAvatarQuality = "photo_max_orig";
        public static string VkApiGetAvatarUrl  = "http://api.vkontakte.ru/method/users.get?uids={0}&fields="+VkApiAvatarQuality;

        // Placeholders
	    public static string ImagePlaceholderUrl = "https://unsplash.it/800/480/?image=";
	    public static string TextPlaceholderUrl = "https://vesna.yandex.ru/referats/?t=";

        // Input
        public const int PasswordMinLength = 6;
        public const string FieldIsRequired = "Это поле является обязательным";
        public const string LoginInvalid = "Допустимые символы: \'A-Z\', \'0-9\' и \'_\'";
        public static string PasswordShort = $"Пароль должен содержать не менее {PasswordMinLength} символов";
        public const string PhoneInvalid = "Неверный формат телефона";
        public const string EmailInvalid = "Неверный формат адреса";
        public const string VkLinkInvalid = "Неверный формат ссылки";
        public const string CategoryEmpty = FieldIsRequired;


		public const string LoginOrPasswordWarning = "Имя или пароль введены неверно";
        public const string FieldWarning = "Проверьте это поле неверно";
        public const string OperationFailed = "Произошла ошибка. Проверьте введенные данные и попробуйте еще раз.";

        // Categories
        public const string UnknownCategory = "Неизвестно";

		// Dialogs
		public const string DialogTitleConfirm = "Подтвердите действие";
		public const string DialogTitleSuccess = "Успешно";
		public const string DialogTitleError = "Ошибка";
		public const string DialogButtonOk = "ОК";
		public const string DialogButtonCancel = "ОТМЕНА";

		public const string DialogSaveSuccess = "Изменения успешно сохранены.";
        public const string DialogSaveFailed = "Произошла ошибка, проверьте введенные данные.";

		public const string DialogNoNetwork = "Не удалось подключиться к серверу. Повторите попытку позже";
		public const string DialogFilterNoNetwork = "Не удалось получить предложения. Выберите категории";

		public const string DialogDeleteConfirm = "Вы уверены, что хотите удалить эту карточку?";
        public const string DialogDeleteSuccess = "Карточка успешно удалена.";
        public const string DialogDeleteFailed = "Произошла ошибка. Попробуйте удалить карточку позже.";

        // FallbackValues
        public const string FallbackPhone = "Номер не указан";
        public const string FallbackEmail = "Адрес почты не указан";
        public const string FallbackVk = "Профиль Вконтакте не указан";
        public const string FallbackContacts = "Не указано";

		// Contacts
		public const string ProfileLogin = "Логин";
		public const string ProfilePassword = "Пароль";
		public const string ProfileName = "Имя";
		public const string ProfileSurname = "Фамилия";

		public const string ProfilePhone = "Телефон";
		public const string ProfilePhoneImage = "res:ic_call_black_24dp";
		public const string ProfileEmail = "Email";
		public const string ProfileEmailImage = "res:ic_email_black_24dp";
		public const string ProfileVk = "ВКонтакте";
		public const string ProfileVkImage = "res:ic_vk_black_24dp";

		// Offer create
		public const string OfferCreateWant = "Хочу";
		public const string OfferCreateCan = "Могу";
	}
}