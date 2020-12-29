using static Gatekeeper.SCIM.Client.Result.ErrorResult;

namespace Gatekeeper.SCIM.Client.Result
{
    interface IError
    {
        public string? ErrorDetail { get; set; }
        public ScimTypeEnum? ScimType { get; set; }
    }
}
