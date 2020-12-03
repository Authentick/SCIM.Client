using System.Text.Json.Serialization;

namespace Gatekeeper.SCIM.Client.Schema.Common20
{
    public class Common
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("externalId")]
        public string? ExternalId { get; set; }
    }
}
