using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Requests.Context.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Requests.Context
{
    public record DefaultRequestCorrelationId : IRequestCorrelationId
    {
        private readonly Guid _correlationId = Guid.NewGuid();

        public Guid GetCorrelationId()
        {
            return _correlationId;
        }
    }
}