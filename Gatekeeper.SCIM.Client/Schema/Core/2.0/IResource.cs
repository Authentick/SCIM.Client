using System.Text.Json.Serialization;

namespace Gatekeeper.SCIM.Client.Schema.Core20
{
    public interface IResource
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("externalId")]
        public string? ExternalId { get; set; }

        [JsonPropertyName("meta")]
        public MetaResourceData? Meta { get; set; }

        public class MetaResourceData
        {
            [JsonPropertyName("resourceType")]
            public string ResourceType { get; set; }

            [JsonPropertyName("created")]
            public string Created { get; set; }

            [JsonPropertyName("lastModified")]
            public string LastModified { get; set; }

            [JsonPropertyName("location")]
            public string Location { get; set; }

            [JsonPropertyName("version")]
            public string Version { get; set; }
        }
    }
}
