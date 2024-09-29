using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands
{
    [ExcludeFromCodeCoverage]
    public record UpdateContactResponseCommand
    {
        [JsonPropertyName("contactId")]
        public Guid ContactId { get; init; }

        [JsonPropertyName("isContactNotifiedForUpdate")]
        public bool IsContactNotifiedForUpdate { get; init; }

        [JsonPropertyName("contactNotifiedForUpdateAt")]
        public DateTime? ContactNotifiedForUpdateAt { get; init; }
    }
}