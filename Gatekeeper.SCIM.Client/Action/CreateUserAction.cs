using Gatekeeper.SCIM.Client.Schema.Core20;

namespace Gatekeeper.SCIM.Client.Action
{
    public class CreateUserAction : IAction
    {
        internal readonly User User;

        public CreateUserAction(User user)
        {
            User = user;
        }
    }
}
