using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Action
{
    public class GetAction<T> : IAction where T : IResource
    {
        internal readonly string Id;

        public GetAction(string id)
        {
            Id = id;
        }
    }
}
