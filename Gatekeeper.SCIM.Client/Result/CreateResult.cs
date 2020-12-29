using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Result
{
    public class CreateResult<T> : IResult, IError where T : IResource
    {
        public StateEnum ResultStatus { get; set; }
        public T? Resource { get; set; }
        public string? ErrorDetail { get; set; }
        public ErrorResult.ScimTypeEnum? ScimType { get; set; }
    }
}
