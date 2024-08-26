using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
            Func<TRequest?, IDictionary<string, object?>, Task<TResponse>> callback,
            int successfulStatusCode)
        {
            _app.MapMethods(url, [method.ToUpper()], async context =>
            {
                string? requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                TRequest? body = HttpRequestDeserializer.Deserialize<TRequest>(requestBody, context.Request.Headers.ContentType);
                IDictionary<string, object?> routeValues = context.Request.GetRouteValues();
                TResponse? output = await callback(body, routeValues);
                HttpResponseSerializeResult serializeResult = HttpResponseSerializer.Serialize(output, context.Request.Headers.Accept);
                context.Response.ContentType = serializeResult.ContentType;
                context.Response.StatusCode = successfulStatusCode;
                await context.Response.WriteAsync(serializeResult.Data);
            });
        }

        public void Run()
        {
            _app.Run();
        }
    }
}