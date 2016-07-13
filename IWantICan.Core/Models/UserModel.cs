namespace IWantICan.Core.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string vkLink { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }

        public UserModelWithToken ToTokenedModel(string token)
        {
            return new UserModelWithToken
            {
                id = id,
                login = login,
                password = password,
                email = email,
                name = name,
                surname = surname,
                vkLink = vkLink,
                avatar = avatar,
                phone = phone,

                token = token
            };
        }
    }
}
