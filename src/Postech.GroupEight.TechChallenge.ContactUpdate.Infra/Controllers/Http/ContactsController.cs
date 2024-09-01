using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Models;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Outputs;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Commands.Extensions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Validators.Extensions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Http.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http
{
    [ExcludeFromCodeCoverage]
    public class ContactsController
    {
        public ContactsController(IHttp http)
        {
            http.On<UpdateContactRequestCommand, UpdateContactResponseCommand>("PATCH", "/contacts/{contactId}", async (body, routeValues, serviceProvider) =>
            {     
                INotifier notifier = serviceProvider.GetRequiredService<INotifier>();
                if (body is null)
                {
                    notifier.Handle(new Notification() { Message = "Unable to read request body", Type = NotificationType.Error });
                    return new() { Messages = notifier.GetNotifications() };
                }
                _ = Guid.TryParse(routeValues["contactId"]?.ToString(), out Guid contactId);
                if (!body.HasSameContactId(contactId))  
                {
                    notifier.Handle(new Notification() { Message = "Contact identifier is different between route and body parameter", Type = NotificationType.Error });
                    return new() { Messages = notifier.GetNotifications() };
                }       
                IValidator<UpdateContactRequestCommand> validator = serviceProvider.GetRequiredService<IValidator<UpdateContactRequestCommand>>();
                ValidationResult validationResult = await validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    notifier.Handle(validationResult.Errors.AsNotifications());
                    return new() { Messages = notifier.GetNotifications() };
                }
                IUpdateContactUseCase useCase = serviceProvider.GetRequiredService<IUpdateContactUseCase>();
                UpdateContactOutput updateContactOutput = await useCase.ExecuteAsync(body.AsUpdateContactInput());
                if (!updateContactOutput.IsContactNotifiedForUpdate)
                {
                    notifier.Handle(new Notification() { Message = "Unable to request contact update. Please try again.", Type = NotificationType.Error });
                    return new() { Messages = notifier.GetNotifications() };
                }
                return new() { Data = updateContactOutput.AsUpdateContactResponseCommand() };
            });
        }
    }
}