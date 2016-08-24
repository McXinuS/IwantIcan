using System;
using Newtonsoft.Json;

namespace IWantICan.Core.Models
{
    public class OfferModel
    {
        public int id { get; set; }
        public int UserModelId { get; set; }
        public int subCategoryModelId { get; set; }
        public string name { get; set; }
        public string shown { get; set; }
        public string imgurl { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
		[JsonIgnore]
		public OfferType type { get; set; }

        public OfferModelWithToken ToTokenedModel(string token)
        {
            return new OfferModelWithToken
            {
                id = id,
                UserModelId = UserModelId,
                subCategoryModelId = subCategoryModelId,
                name = name,
                shown = shown,
                imgurl = imgurl,
                description = description,
                createdAt = createdAt,
                updatedAt = updatedAt,
				type = type,

                token = token
            };
        }
    }
}
