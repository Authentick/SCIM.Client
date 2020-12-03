using System.Collections.Generic;
using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Result {
    public class GetUsersResult : IResult
    {
        public StateEnum ResultStatus { get; set; }

        public IEnumerable<User>? Users { get; set; }
    }
}
