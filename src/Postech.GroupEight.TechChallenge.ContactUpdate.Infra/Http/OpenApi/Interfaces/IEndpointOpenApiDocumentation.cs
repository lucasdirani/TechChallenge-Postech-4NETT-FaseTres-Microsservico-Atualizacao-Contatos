using Swashbuckle.AspNetCore.Annotations;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.OpenApi.Interfaces;

public interface IEndpointOpenApiDocumentation
{
    public string EndpointName { get; }
    public string OperationSummary { get; }
    public string OperationDescription { get; }
    public string ParameterDescription { get; }
    public Tuple<int, string>[] ResponsesDetails { get; }
    SwaggerResponseAttribute[] GetResponsesDetails();
}