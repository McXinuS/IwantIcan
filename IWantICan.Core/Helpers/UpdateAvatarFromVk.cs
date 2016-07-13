using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IWantICan.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IWantICan.Core.Helpers
{
    public static class UpdateAvatarFromVk
    {
        /// <summary>
        /// Parses avatar from user's Vk profile.
        /// </summary>
        public static async Task<UserModel> UpdateAvatar(this UserModel user)
        {
            var userNew = user;
            if (!userNew.vkLink.IsValidVkLink())
                return userNew;

            try
            {
                var vkId = userNew.vkLink?.Split('/')?.LastOrDefault();
                var uri = new Uri(string.Format(Constants.VkApiGetAvatarUrl, vkId));

                using (var client = NewHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentResp = await response.Content.ReadAsStringAsync();
                        JToken resp = (JToken)JsonConvert.DeserializeObject(contentResp);
                        var result = resp.Value<JToken>("response")[0].Value<string>("photo_200");

                        if (result != null && !result.Contains("images/camera"))
                            userNew.avatar = result;

                        return userNew;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return user;
        }

        /// <summary>
        /// Shows whether the link is a valid Vk profile link.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsValidVkLink(this string source)
        {
            var link = source.ToLower();
            return !string.IsNullOrWhiteSpace(link)
                   && (link.StartsWith("http://vk.com/")
                   || link.StartsWith("https://vk.com/")
                   || link.StartsWith("vk.com/")
                   || link.Split('/').Length==1);
        }

        private static HttpClient NewHttpClient()
        {
            var c = new HttpClient
            {
                MaxResponseContentBufferSize = 256000,
                Timeout = TimeSpan.FromSeconds(10)
            };
            c.DefaultRequestHeaders.IfModifiedSince = DateTime.Now;
            return c;
        }
    }
}
