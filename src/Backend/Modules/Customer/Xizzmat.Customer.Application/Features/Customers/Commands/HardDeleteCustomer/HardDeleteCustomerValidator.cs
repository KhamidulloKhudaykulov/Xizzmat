using System;
using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.HardDeleteCustomer;

public class HardDeleteCustomerValidator : AbstractValidator<HardDeleteCustomerCommand>
{
    public HardDeleteCustomerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Customer ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Customer ID must be a valid GUID.");
    }
}
