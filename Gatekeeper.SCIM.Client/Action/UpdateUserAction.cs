using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Action
{
    public class UpdateUserAction : IAction
    {
        internal readonly User User;

        public UpdateUserAction(User user)
        {
            User = user;
        }
    }
}
