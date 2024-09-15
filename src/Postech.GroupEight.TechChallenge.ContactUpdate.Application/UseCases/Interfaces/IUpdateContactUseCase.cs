using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Inputs;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Outputs;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Application.UseCases.Interfaces
{
    public interface IUpdateContactUseCase
    {
        Task<UpdateContactOutput> ExecuteAsync(UpdateContactInput input);
    }
}