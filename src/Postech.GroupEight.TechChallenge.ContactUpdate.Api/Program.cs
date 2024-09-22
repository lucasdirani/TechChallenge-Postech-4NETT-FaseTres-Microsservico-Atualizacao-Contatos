using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Api.Setup;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Adapters;
using Prometheus;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5010");
builder.Configuration.AddJsonFileByEnvironment(builder.Environment.EnvironmentName);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddDependencyFluentValidation();
builder.Services.AddDependencyRequestCorrelationId();
builder.Services.AddDependencyNotifier();
builder.Services.AddDependencyRabbitMQ(builder.Configuration);
builder.Services.AddDependencyEventPublisher();
builder.Services.AddDependencyUseCase();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1")); 
}

app.UseMetricServer();
app.UseHttpMetrics();
app.UseHttpsRedirection();

AspNetCoreAdapter http = new(app);
_ = new ContactsController(http);
http.Run();

[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program() { }
}