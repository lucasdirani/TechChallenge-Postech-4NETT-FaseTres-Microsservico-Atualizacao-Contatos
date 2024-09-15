using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactUpdate.Application.Notifications.Models;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.Infra.Controllers.Http.Validators.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class ValidationFailureExtensions
    {
        public static IEnumerable<Notification> AsNotifications(this List<ValidationFailure> failures)
        {
            return failures.Select(failure => new Notification() {
                Message = failure.ErrorMessage,
                Type = NotificationType.Error
            });
        }
    }
}