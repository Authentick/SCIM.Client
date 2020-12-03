using System.Collections.Generic;
using System.Text.Json.Serialization;
using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Schema.Common20 {
    class FilterResponse : ISchema
    {
        public string SchemaIdentifier => "urn:ietf:params:scim:api:messages:2.0:ListResponse";
        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }
        [JsonPropertyName("Resources")]
        public List<User>? Resources { get; set; }
    }
}
