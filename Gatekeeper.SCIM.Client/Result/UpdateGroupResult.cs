using Gatekeeper.SCIM.Client.Result;
using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Action
{
    public class UpdateGroupResult : IResult
    {
        public StateEnum ResultStatus { get; set; }

        public Group? Group { get; set; }
    }
}
