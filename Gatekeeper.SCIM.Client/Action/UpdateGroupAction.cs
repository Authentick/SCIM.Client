using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Action
{
    public class UpdateGroupAction : IAction
    {
        internal readonly Group Group;

        public UpdateGroupAction(Group group)
        {
            Group = group;
        }
    }
}
