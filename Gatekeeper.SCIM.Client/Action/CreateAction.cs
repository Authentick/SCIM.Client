using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Action
{
    public class CreateAction<T> : IAction where T : IResource
    {
        internal readonly T Resource;

        public CreateAction(T resource)
        {
            Resource = resource;
        }
    }
}
