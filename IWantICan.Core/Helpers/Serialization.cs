using Newtonsoft.Json;

namespace IWantICan.Core.Helpers
{
    public static class Serialization
    {
        /// <summary>
        /// Serializes can/want offer into JSON string to pass MvvmCross parameter restriction.
        /// </summary>
        /// <returns>JSON representation of the object.</returns>
        public static string Serialize<T>(this T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        /// <summary>
        /// Deserializes JSON string into can/want offer.
        /// </summary>
        /// <returns>Offer.</returns>
        public static T Deserialize<T>(this string item)
        {
            return JsonConvert.DeserializeObject<T>(item);
        }
    }
}
