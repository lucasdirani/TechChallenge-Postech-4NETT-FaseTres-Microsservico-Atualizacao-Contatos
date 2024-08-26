using System.Diagnostics.CodeAnalysis;
using System.Net;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http
{
    [ExcludeFromCodeCoverage]
    public class ContactsController
    {
        private readonly IHttp _http;

        public ContactsController(IHttp http)
        {
            _http = http;
            _http.On<UpdateContactRequestCommand, string?>("PATCH", "/contacts/{contactId}", async (body, routeValues) =>
            {
                await Task.Delay(1000);
                return routeValues["contactId"]?.ToString();
            }, (int) HttpStatusCode.Accepted);
        }
    }
}