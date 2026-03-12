using Microsoft.EntityFrameworkCore;
using Xizzmat.Customer.Application.Interfaces;
using Xizzmat.Customer.Infrastructure.Database;

namespace Xizzmat.Customer.Infrastructure.Implementations;

public class CustomerService : ICustomerService
{
    private readonly CustomerDbContext _context;

    public CustomerService(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context
            .Customers
            .FirstOrDefaultAsync(c => c.Id == id) != null;
    }
}
