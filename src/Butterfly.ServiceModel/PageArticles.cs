using Petecat.Formatter.Attribute;

namespace Butterfly.ServiceModel
{
    public class PageArticles
    {
        [JsonProperty("pg")]
        public Paging Paging { get; set; }

        [JsonProperty("a")]
        public ArticleInfo[] Articles { get; set; }
    }
}
