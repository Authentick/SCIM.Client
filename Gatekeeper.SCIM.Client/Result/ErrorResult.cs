using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Gatekeeper.SCIM.Client.Result
{
    public class ErrorResult
    {
        [JsonPropertyName("detail")]
        public string? Detail { get; set; }

        [JsonPropertyName("scimType")]
	    [JsonConverter(typeof(JsonStringEnumConverter))]
        public ScimTypeEnum? ScimType { get; set; }

        public enum ScimTypeEnum
        {
	        [EnumMember(Value = "invalidFilter")]
            invalidFilter,
	        [EnumMember(Value = "tooMany")]
            tooMany,
	        [EnumMember(Value = "uniqueness")]
            uniqueness,
	        [EnumMember(Value = "mutability")]
            mutability,
	        [EnumMember(Value = "invalidSyntax")]
            invalidSyntax,
	        [EnumMember(Value = "invalidPath")]
            invalidPath,
	        [EnumMember(Value = "noTarget")]
            noTarget,
	        [EnumMember(Value = "invalidValue")]
            invalidValue,
	        [EnumMember(Value = "invalidVers")]
            invalidVers,
	        [EnumMember(Value = "sensitive")]
            sensitive,
        }
    }
}
