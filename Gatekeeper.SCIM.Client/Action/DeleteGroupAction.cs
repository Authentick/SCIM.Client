namespace Gatekeeper.SCIM.Client.Action
{
    public class DeleteGroupAction : IAction
    {
        internal readonly string Id;

        public DeleteGroupAction(string id)
        {
            Id = id;
        }
    }
}
