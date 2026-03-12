using Xizzmat.Customer.Domain.Shared;

namespace Xizzmat.Customer.Domain.Entities;

public class CustomerEntity : IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

    public string Name { get; private set; } = default!;
    public string Surname { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    private CustomerEntity(Guid id, string name, string surname, string phoneNumber)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;

        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
    }

    public static Result<CustomerEntity> Create(Guid id, string name, string surname, string phoneNumber)
    {
        var customer = new CustomerEntity(id, name, surname, phoneNumber);
        return Result.Success(customer);
    }

    public Result Delete()
    {
        IsDeleted = true;
        return Result.Success();
    }

    public Result Restore()
    {
        IsDeleted = false;
        return Result.Success();
    }

    public Result UpdateProfile(string name, string surname, string phoneNumber)
    {
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        return Result.Success();
    }
}
