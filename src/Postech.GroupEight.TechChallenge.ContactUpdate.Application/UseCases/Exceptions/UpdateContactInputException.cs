using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Inputs;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UpdateContactInputException(string? message, CurrentContactDataInput currentContactData, UpdatedContactDataInput updatedContactData) : Exception(message)
    {
        public CurrentContactDataInput CurrentContactData { get; private set; } = currentContactData;
        public UpdatedContactDataInput UpdatedContactData { get; private set; } = updatedContactData;

        public static void ThrowWhenCurrentAndUpdatedContactDataAreEqual(
            UpdateContactInput input, 
            string message = "The current contact data cannot be the same as the update")
        {
            if (input.CheckIfCurrentAndUpdatedContactDataAreTheSame())
            {
                throw new UpdateContactInputException(message, input.CurrentContactData, input.UpdatedContactData);
            }
        }
    }
}