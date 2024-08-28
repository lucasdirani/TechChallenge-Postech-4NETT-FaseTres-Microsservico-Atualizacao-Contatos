using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Deserializers;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Serializers;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Serializers.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Requests.Extensions;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Adapters
{
    [ExcludeFromCodeCoverage]
    public class AspNetCoreAdapter(WebApplication app) : IHttp
    {
        private readonly WebApplication _app = app;

        public void On<TRequest, TResponse>(
            string method, 
            string url, 
            Func<TRequest?, IDictionary<string, object?>, IServiceProvider, Task<GenericResponseCommand<TResponse>>> callback,
            int successfulStatusCode,
            int failureStatusCode)
        {
            _app.MapMethods(url, [method.ToUpper()], async context =>
            {
                string? requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                TRequest? body = HttpRequestDeserializer.Deserialize<TRequest>(requestBody, context.Request.Headers.ContentType);
                IDictionary<string, object?> routeValues = context.Request.GetRouteValues();
                GenericResponseCommand<TResponse> responseContent = await callback(body, routeValues, context.RequestServices);
                HttpResponseSerializeResult serializeResult = HttpResponseSerializer.Serialize(responseContent, context.Request.Headers.Accept);
                context.Response.ContentType = serializeResult.ContentType;
                context.Response.StatusCode = responseContent.IsSuccessfullyProcessed ? successfulStatusCode : failureStatusCode;
                await context.Response.WriteAsync(serializeResult.Data);
            });
        }

        public void Run()
        {
            _app.Run();
        }
    }
}