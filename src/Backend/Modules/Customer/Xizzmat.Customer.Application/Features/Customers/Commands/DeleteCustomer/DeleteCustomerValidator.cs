using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Customer ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Customer ID must be a valid GUID.");
    }
}
