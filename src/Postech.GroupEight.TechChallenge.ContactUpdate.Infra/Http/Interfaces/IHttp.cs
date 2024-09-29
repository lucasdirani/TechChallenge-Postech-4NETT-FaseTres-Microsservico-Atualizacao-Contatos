using System.Net;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.OpenApi.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Interfaces
{
    public interface IHttp
    {
        void On<TRequest, TResponse>(string method, string url, Func<TRequest?, IDictionary<string, object?>, IServiceProvider, Task<GenericResponseCommand<TResponse>>> callback, IEndpointOpenApiDocumentation openApiDocumentation, int successfulStatusCode = (int) HttpStatusCode.Accepted, int failureStatusCode = (int) HttpStatusCode.BadRequest);
        void Run();
    }
}