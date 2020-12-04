using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gatekeeper.SCIM.Client.Schema.Core20
{
    public class Group : ISchema, IResource
    {
        public string SchemaIdentifier => "urn:ietf:params:scim:schemas:core:2.0:User";

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = null!;

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("externalId")]
        public string? ExternalId { get; set; }

        [JsonPropertyName("members")]
        public IEnumerable<GroupMembership>? Members { get; set; }
        
        [JsonPropertyName("meta")]
        public IResource.MetaResourceData? Meta { get; set; }

        public class GroupMembership
        {
            [JsonPropertyName("value")]
            public string? Value { get; set; }

            [JsonPropertyName("$ref")]
            public string? Ref { get; set; }
        }
    }
}
