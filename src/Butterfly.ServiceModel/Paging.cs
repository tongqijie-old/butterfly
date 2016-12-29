using Petecat.Formatter.Attribute;

namespace Butterfly.ServiceModel
{
    public class Paging
    {
        [JsonProperty(Alias = "pn")]
        public int PageNumber { get; set; }

        [JsonProperty(Alias = "tp")]
        public int TotalPages { get; set; }

        [JsonProperty(Alias = "ps")]
        public int PageSize { get; set; }
    }
}