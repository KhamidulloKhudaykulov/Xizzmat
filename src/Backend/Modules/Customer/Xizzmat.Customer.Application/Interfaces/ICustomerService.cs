namespace Xizzmat.Customer.Application.Interfaces;

public interface ICustomerService
{
    Task<bool> ExistsAsync(Guid id);
}
