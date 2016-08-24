using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using IWantICan.Core.Helpers;
using IWantICan.Core.Models;
using Newtonsoft.Json;

namespace IWantICan.Core.Services.Api
{
    public class ApiService : IApiService
    {
        private HttpClient NewHttpClient()
        {
            var c = new HttpClient
            {
                MaxResponseContentBufferSize = 256000,
                Timeout = TimeSpan.FromSeconds(10)
            };
            c.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
            return c;
        }

        public async Task<TokenResponseModel> LoginAsync(string login, string pass)
        {
            var item = new { identifier = login, password = pass };
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = new Uri(Constants.UserLoginUrl);
            try
            {
                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        TokenResponseModel result = JsonConvert.DeserializeObject<TokenResponseModel>(contentResp);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = new Uri(Constants.UserCreateUrl);
            try
            {
                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        UserModel result = JsonConvert.DeserializeObject<UserModel>(contentResp);
                        await result.UpdateAvatar();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public async Task<UserModel> GetUserAsync(int id, string token)
        {
            var uri = new Uri(Constants.UserGetUrl + "/" + id);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        UserModel result = JsonConvert.DeserializeObject<UserModel>(contentResp);
                        await result.UpdateAvatar();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public async Task<bool> UpdateUserAsync(UserModelWithToken user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = new Uri(Constants.UserUpdateUrl);
            try
            {
                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync(uri, content);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public async Task<bool> AddCanAsync(OfferModelWithToken can)
        {
            var json = JsonConvert.SerializeObject(can);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = new Uri(Constants.CanUrl);
            try
            {
                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public async Task<OfferModel> GetCanAsync(int id, string token)
        {
            //return GetDummyCan(id);

            var uri = new Uri(Constants.CanUrl + "/" + id);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        OfferModel result = JsonConvert.DeserializeObject<OfferModel>(contentResp);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public async Task<List<OfferModel>> GetCanListAll(string token)
        {
            var uri = new Uri(Constants.CanUrl);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        List<OfferModel> list = JsonConvert.DeserializeObject<List<OfferModel>>(contentResp);
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<OfferModel>();
        }

        public async Task<List<OfferModel>> GetCanListByCategoryAsync(int catId, string token)
        {
            // DEBUG
            /*var l = new List<CanModel>();

            for (var i = 1; i <= 1; i++)
            {
                l.Add(GetDummyCan(i));
            }

            return l;*/

            var uri = new Uri(Constants.CanListByCatUrl + "/" + catId);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        List<OfferModel> list = JsonConvert.DeserializeObject<List<OfferModel>>(contentResp);
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<OfferModel>();
        }

        public async Task<List<OfferModel>> GetCanListByUserAsync(int userId, string token)
        {
            /*var l = new List<CanModel>();

            for (var i = 1; i <= 20; i++)
            {
                l.Add(GetDummyCan(i));
            }

            return l;*/

            var uri = new Uri(Constants.CanListByUserUrl + "/" + userId);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        List<OfferModel> list = JsonConvert.DeserializeObject<List<OfferModel>>(contentResp);
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<OfferModel>();
        }

        public async Task<bool> UpdateCanAsync(OfferModelWithToken can)
        {
            var json = JsonConvert.SerializeObject(can);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = new Uri(Constants.CanUrl + "/" + can.id);
            try
            {
                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync(uri, content);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public async Task<bool> DeleteCanAsync(int id, string token)
        {
            var uri = new Uri(Constants.CanUrl + "/" + id);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.DeleteAsync(uri);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public async Task<bool> AddWantAsync(OfferModelWithToken want)
        {
            var json = JsonConvert.SerializeObject(want);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = new Uri(Constants.WantUrl);
            try
            {
                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public async Task<OfferModel> GetWantAsync(int id, string token)
        {
            //return GetDummyWant(id);

            var uri = new Uri(Constants.WantUrl + "/" + id);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

						OfferModel result = JsonConvert.DeserializeObject<OfferModel>(contentResp);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public async Task<List<OfferModel>> GetWantListAllAsync(string token)
        {
            var uri = new Uri(Constants.WantUrl);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        List<OfferModel> list = JsonConvert.DeserializeObject<List<OfferModel>>(contentResp);
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<OfferModel>();
        }

        public async Task<List<OfferModel>> GetWantListByCategoryAsync(int id, string token)
        {
            var uri = new Uri(Constants.WantListUrl + "/" + id);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        List<OfferModel> list = JsonConvert.DeserializeObject<List<OfferModel>>(contentResp);
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<OfferModel>();
        }

        public async Task<List<OfferModel>> GetWantListByUserAsync(int userId, string token)
        {
            /*var l = new List<WantModel>();

            for (var i = 1; i <= 20; i++)
            {
                l.Add(GetDummyWant(i));
            }

            return l;*/

            var uri = new Uri(Constants.WantUrl + "/" + userId);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        List<OfferModel> list = JsonConvert.DeserializeObject<List<OfferModel>>(contentResp);
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<OfferModel>();
        }

        public async Task<bool> UpdateWantAsync(OfferModelWithToken want)
        {
            var json = JsonConvert.SerializeObject(want);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = new Uri(Constants.WantUrl + "/" + want.id);
            try
            {
                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync(uri, content);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public async Task<bool> DeleteWantAsync(int id, string token)
        {
            var uri = new Uri(Constants.WantUrl + "/" + id);
            try
            {
                using (var client = NewHttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", token);
                    HttpResponseMessage response = await client.DeleteAsync(uri);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public async Task<List<CategoryModel>> GetCategoryListAsync()
        {
            /*var l = new List<CategoryModel>();

            for (var i = 1; i <= 20; i++)
            {
                l.Add(GetDummyCategory(i));
            }

            return l;*/

            var uri = new Uri(Constants.CategoryUrl);
            try
            {
                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();

                        List<CategoryModel> list = JsonConvert.DeserializeObject<List<CategoryModel>>(contentResp);
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<CategoryModel>();
        }

        /* ---DUMMY MODELS--- */

        private UserModel GetDummyUser()
        {
            return new UserModel
            {
                id = 1,
                login = "Login",
                phone = "+79011234567",
                name = "Herp",
                surname = "Derp",
                vkLink = "https://vk.com/id0",
                avatar = Constants.ImagePlaceholderUrl + 1,
                email = "test@test.com"
            };
        }

        private TokenResponseModel GetDummyToken()
        {
            return new TokenResponseModel()
            {
                token = "simple_token",
                expires = "3600000",
                user_id = 0
            };
        }

        private OfferModel GetDummyCan(int seed, bool my = false)
        {
            var rnd = new Random(seed);
            var desc = "";
            if (my)
                desc = "[My can] ";
            desc += Task.Run(() => GetFromYandex(100)).Result;

            return new OfferModel
            {
                subCategoryModelId = seed,
                UserModelId = seed,
                description = desc,
                id = seed,
                imgurl = Constants.ImagePlaceholderUrl + seed,
                name = "Title " + seed,

                createdAt = new DateTime(2016, 07, 05,
                    rnd.Next(24), rnd.Next(60), rnd.Next(60)),
                updatedAt = new DateTime(2016, 07, 05,
                    rnd.Next(24), rnd.Next(60), rnd.Next(60))
            };
        }

        private OfferModel GetDummyWant(int seed, bool my = false)
        {
            var rnd = new Random(seed);
            var desc = "";
            if (my)
                desc = "[My want] ";
            desc += Task.Run(() => GetFromYandex(100)).Result;

            return new OfferModel
			{
                subCategoryModelId = seed,
                UserModelId = seed,
                description = desc,
                id = seed,
                imgurl = Constants.ImagePlaceholderUrl + seed,
                name = "Title " + seed,

                createdAt = new DateTime(2016, 07, 05,
                    rnd.Next(24), rnd.Next(60), rnd.Next(60)),
                updatedAt = new DateTime(2016, 07, 05,
                    rnd.Next(24), rnd.Next(60), rnd.Next(60))
            };
        }

        private CategoryModel GetDummyCategory(int seed)
        {
            return new CategoryModel
            {
                id = seed,
                name = "Категория " + seed,
                createdAt = new DateTime(2016, 07, 01, 00, 00, 00),
                updatedAt = new DateTime(2016, 07, 01, 10, 00, 00),
                shown = "true"
            };
        }

        // download referats from Yandex.Vesna until needed number of words is reached
        async Task<string> GetFromYandex(int words)
        {
            List<string> themes = new List<string>() {
                "astronomy","geology","gyroscope","literature","marketing","mathematics","music","polit",
                "agrobiologia","law","psychology","geography","physics","philosophy","chemistry","estetica"};
            // seed limit
            const int maxSeedValue = 100000;

            int wordsNeed = words;
            int wordsCount = 0;
            Random rnd = new Random();

            string result = "";

            while (wordsCount < wordsNeed)
            {
                // generate URI
                string uri = Constants.TextPlaceholderUrl;
                int rndThemesCount = rnd.Next(1, themes.Count);
                int[] usedThemes = Enumerable.Range(0, themes.Count).ToArray();
                for (int i = 0; i < rndThemesCount; i++)
                {
                    int tnum;
                    do
                        tnum = rnd.Next(themes.Count);
                    while (usedThemes[tnum] == 1);
                    usedThemes[tnum] = 1;
                    uri += themes[tnum] + '+';
                }
                int seed = rnd.Next(maxSeedValue);
                uri += "&s=" + seed.ToString();

                string responseFromServer;
                try
                {
                    HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                    request.CookieContainer = new CookieContainer();
                    CredentialCache myCache = new CredentialCache();
                    myCache.Add(new Uri(uri), "Basic", new NetworkCredential("", "", ""));
                    request.Credentials = myCache;

                    HttpWebResponse response;
                    response = await request.GetResponseAsync() as HttpWebResponse;

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    responseFromServer = reader.ReadToEnd();
                    // get source with no indents since they're interrupt parsing
                    responseFromServer = responseFromServer.Replace('\n', ' ');
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                
                // parse generated text (first three 'p' tags)
                string currentText = "";
                MatchCollection myMatch = Regex.Matches(responseFromServer, @"<p>(.*?)</p>");
                for (int i = 0; i < 3; i++)
                    currentText += myMatch[i].Value.Remove(0, 3).Remove(myMatch[i].Length - 3 - 4, 4) + Environment.NewLine;
                result += currentText;

                // get number of words
                int index = 0;
                while (index < currentText.Length)
                {
                    while (index < currentText.Length && Char.IsWhiteSpace(currentText[index]) == false)
                        index++;
                    wordsCount++;
                    while (index < currentText.Length && Char.IsWhiteSpace(currentText[index]) == true)
                        index++;
                }
            }

            return result;
        }
    }
}
