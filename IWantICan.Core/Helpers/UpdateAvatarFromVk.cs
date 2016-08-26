using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IWantICan.Core.Models;
using Java.IO;
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
            if (!ValidatorHelper.IsValid(user.vkLink, ValidationType.Vk))
                return user;

			// TODO clone the object properly
            var userNew = user;

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
                        var result = resp.Value<JToken>("response")[0].Value<string>(Constants.VkApiAvatarQuality);

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
