using Postech.GroupEight.TechChallenge.ContactUpdate.Api.Setup;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Adapters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyFluentValidation();
builder.Services.AddDependencyRequestCorrelationId();
builder.Services.AddDependencyNotifier();
builder.Services.AddDependencyRabbitMQ(configuration);
builder.Services.AddDependencyEventPublisher();
builder.Services.AddDependencyUseCase();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

AspNetCoreAdapter http = new(app);
_ = new ContactsController(http);
http.Run();