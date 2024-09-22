using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.OpenApi.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.OpenApi;

[ExcludeFromCodeCoverage]
public record ContactUpdateEndpointOpenApiDocumentation : IEndpointOpenApiDocumentation
{
    public string EndpointName => "Update Contact";

    public string OperationSummary => "Modify an existing contact";

    public string OperationDescription => "Modifies an existing contact according to the provided data";

    public string ParameterDescription => "Data for updating the contact";

    public Tuple<int, string>[] ResponsesDetails => [new Tuple<int, string>(202, "Contact updated"), new Tuple<int, string>(400, "The data provided to update the contact is invalid"), new Tuple<int, string>(500, "Unexpected error while updating contact")];

    public SwaggerResponseAttribute[] GetResponsesDetails()
    {
        return ResponsesDetails.Select(responseDetail => new SwaggerResponseAttribute(responseDetail.Item1, responseDetail.Item2)).ToArray();
    }
}