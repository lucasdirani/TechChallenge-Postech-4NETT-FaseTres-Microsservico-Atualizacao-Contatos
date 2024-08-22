using System.ComponentModel;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Messaging.Endpoints
{
    public enum QueueEndpoint
    {
        [Description("contact.update")]
        ContactUpdated,
    }
}