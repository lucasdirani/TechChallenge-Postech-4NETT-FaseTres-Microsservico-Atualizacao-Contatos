using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Adapters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

AspNetCoreAdapter http = new(app);
_ = new ContactsController(http);
http.Run();