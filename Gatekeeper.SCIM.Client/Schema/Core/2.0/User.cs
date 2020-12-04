using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gatekeeper.SCIM.Client.Schema.Core20
{
    public class User : ISchema, IResource
    {
        public string SchemaIdentifier => "urn:ietf:params:scim:schemas:core:2.0:User";

        [JsonPropertyName("userName")]
        public string UserName { get; set; } = null!;

        [JsonPropertyName("name")]
        public INameComponent? Name { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("nickName")]
        public string? NickName { get; set; }

        [JsonPropertyName("profileUrl")]
        public string? ProfileUrl { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("userType")]
        public string? UserType { get; set; }

        // TODO: Use enum
        [JsonPropertyName("preferredLanguage")]
        public string? PreferredLanguage { get; set; }
        // TODO: Use enum

        [JsonPropertyName("locale")]
        public string? Locale { get; set; }
        // TODO: USe enum

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("groups")]
        public IEnumerable<GroupMembership>? Groups { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("externalId")]
        public string? ExternalId { get; set; }

        [JsonPropertyName("meta")]
        public IResource.MetaResourceData? Meta { get; set; }

        public class GroupMembership
        {
            [JsonPropertyName("value")]
            public string? Value { get; set; }
        }

        public interface INameComponent
        {
            string value { get; set; }
        }

        class FormattedName : INameComponent
        {
            public string value { get; set; }
        }

        class FamilyName : INameComponent
        {
            public string value { get; set; }
        }

        class GivenName : INameComponent
        {
            public string value { get; set; }
        }

        class HonoricPrefix : INameComponent
        {
            public string value { get; set; }
        }

        class HonoricSuffix : INameComponent
        {
            public string value { get; set; }
        }
    }
}
