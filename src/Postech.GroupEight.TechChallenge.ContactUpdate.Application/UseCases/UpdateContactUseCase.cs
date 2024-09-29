using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Extensions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Exceptions;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Inputs;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Interfaces;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Outputs;
using Postech.GroupEight.TechChallenge.ContactUpdate.Core.Entities;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases
{
    public class UpdateContactUseCase(IEventPublisher<ContactUpdatedEvent> eventPublisher) : IUpdateContactUseCase
    {
        private readonly IEventPublisher<ContactUpdatedEvent> _eventPublisher = eventPublisher;

        public async Task<UpdateContactOutput> ExecuteAsync(UpdateContactInput input)
        {
            UpdateContactInputException.ThrowWhenCurrentAndUpdatedContactDataAreEqual(input);
            ContactEntity contactEntity = input.AsContactEntity();
            contactEntity.UpdateContactEmail(input.UpdatedContactData.ContactEmail);
            contactEntity.UpdateContactName(input.UpdatedContactData.ContactFirstName, input.UpdatedContactData.ContactLastName);
            contactEntity.UpdateContactPhone(input.UpdatedContactData.AsContactPhoneValueObject());
            PublishedEventResult eventResult = await _eventPublisher.PublishEventAsync(contactEntity.AsContactUpdatedEvent());
            return UpdateContactOutput.CreateUsing(contactEntity, eventResult);
        }
    }
}