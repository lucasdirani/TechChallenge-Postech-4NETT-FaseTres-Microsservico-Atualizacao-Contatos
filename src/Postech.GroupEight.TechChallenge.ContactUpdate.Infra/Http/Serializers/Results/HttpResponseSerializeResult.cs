namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Serializers.Results
{
    public record HttpResponseSerializeResult
    {
        public required string Data { get; init; }
        public required string ContentType { get; init; }
    }
}