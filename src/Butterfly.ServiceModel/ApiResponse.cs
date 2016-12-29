using Petecat.Formatter.Attribute;

namespace Butterfly.ServiceModel
{
    public class ApiResponse
    {
        [JsonProperty(Alias = "error")]
        public bool Error { get; set; }

        [JsonProperty(Alias = "msg")]
        public string Message { get; set; }

        [JsonProperty(Alias = "data")]
        public object Data { get; set; }
    }
}
