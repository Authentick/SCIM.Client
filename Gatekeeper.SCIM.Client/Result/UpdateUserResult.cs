using Gatekeeper.SCIM.Client.Result;
using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Action
{
    public class UpdateUserResult : IResult
    {
        public StateEnum ResultStatus { get; set; }

        public User? User { get; set; }
    }
}
