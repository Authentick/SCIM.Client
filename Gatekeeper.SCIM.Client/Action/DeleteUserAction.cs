using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Action
{
    public class DeleteUserAction : IAction
    {
        internal readonly User User;

        public DeleteUserAction(User user)
        {
            User = user;
        }
    }
}
