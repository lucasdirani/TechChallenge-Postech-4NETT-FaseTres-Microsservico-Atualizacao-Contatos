using Microsoft.OpenApi.Models;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.OpenApi.Interfaces;

public interface IEndpointOpenApiDocumentation
{
    OpenApiOperation GetOpenApiDocumentation();
}