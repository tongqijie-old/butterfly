using Petecat.Configuring.Attribute;
using Petecat.Formatter.Attribute;

namespace Butterfly.Configuration
{
    [StaticFile(
        Key = "ApiConfiguration",
        Path = "./configuration/api.json",
        FileFormat = "json",
        Inference = typeof(IApiConfiguration))]
    public class ApiConfiguration : IApiConfiguration
    {
        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }
    }
}