using FluentValidation.Results;
using NerdStore.Core.Data.Interfaces;
using System.Threading.Tasks;

namespace NerdStore.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow)
        {
            if (!await uow.CommitAsync())
            {
                AddError("Houve um erro ao persistir os dados");
            }

            return ValidationResult;
        }
    }
}
