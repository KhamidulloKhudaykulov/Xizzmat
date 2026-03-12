using System;
using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.RestoreCustomer;

public class RestoreCustomerValidator : AbstractValidator<RestoreCustomerCommand>
{
    public RestoreCustomerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Customer ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Customer ID must be a valid GUID.");
    }
}
