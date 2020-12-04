using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Result
{
    public class GetResult<T> : IResult where T : IResource
    {
        public StateEnum ResultStatus { get; set; }
        public T? Resource { get; set; }
    }
}
