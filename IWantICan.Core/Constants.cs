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
        public static string VkApiGetAvatarUrl  = "http://api.vkontakte.ru/method/users.get?uids={0}&fields=photo_max_orig";

        // Placeholders
	    public static string ImagePlaceholderUrl = "https://unsplash.it/800/480/?image=";
	    public static string TextPlaceholderUrl = "https://vesna.yandex.ru/referats/?t=";

        // Login / Registration
        public const int PasswordMinLength = 8;
        public const string LoginWarning = "Имя или пароль введен неверно";
        public const string LoginRequired = "Логин является обязательным";
        public const string LoginExists = "Данный логин уже существует";
        public const string LoginNotValid = "Допустимые символы: \'A-Z\', \'0-9\' и \'_\'";
        public const string PasswordRequired = "Пароль является обязательным";
        public static string PasswordShort = $"Пароль должен содержать не менее {PasswordMinLength} символов";
        public const string LoginOrRegisterFailed = "Произошла ошибка. Проверьте введенные данные и попробуйте еще раз.";

        // Categories
        public const string UnknownCategory = "Неизвестно";

        // Dialogs
        public const string DialogSaveOk = "Изменения успешно сохранены.";
        public const string DialogSaveFailed = "Произошла ошибка, проверьте введенные данные.";
        public const string DialogFilterFailed = "Не удалось получить предложения. Выберите категории";
        public const string DialogTitleOk = "Успешно";
        public const string DialogTitleError = "Ошибка";
    }
}