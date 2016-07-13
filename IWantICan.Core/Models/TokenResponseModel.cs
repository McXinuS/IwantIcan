namespace IWantICan.Core.Models
{
    public class TokenResponseModel
    {
        public string token { get; set; }
        public string expires { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}
