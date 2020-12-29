using System.Text.Json.Serialization;

namespace Gatekeeper.SCIM.Client.Result
{
    public class ErrorResult
    {
        [JsonPropertyName("detail")]
        public string? Detail { get; set; }

        [JsonPropertyName("scimType")]
        public ScimTypeEnum? ScimType { get; set; }

        public enum ScimTypeEnum
        {
            invalidFilter = 1,
            tooMany = 2,
            uniqueness = 3,
            mutability = 4,
            invalidSyntax = 5,
            invalidPath = 6,
            noTarget = 7,
            invalidValue = 8,
            invalidVers = 9,
            sensitive = 10,
        }
    }
}
