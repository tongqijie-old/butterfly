﻿using Petecat.Formatter.Attribute;

namespace Butterfly.ServiceModel
{
    public class ArticleInfo
    {
        [JsonProperty(Alias = "id")]
        public string Id { get; set; }

        [JsonProperty(Alias = "creationDate")]
        public string CreationDate { get; set; }

        [JsonProperty(Alias = "modifiedDate")]
        public string ModifiedDate { get; set; }

        [JsonProperty(Alias = "title")]
        public string Title { get; set; }

        [JsonProperty(Alias = "abstract")]
        public string Abstract { get; set; }

        [JsonProperty(Alias = "content")]
        public string Content { get; set; }

        [JsonProperty(Alias = "signature")]
        public string Signature { get; set; }
    }
}