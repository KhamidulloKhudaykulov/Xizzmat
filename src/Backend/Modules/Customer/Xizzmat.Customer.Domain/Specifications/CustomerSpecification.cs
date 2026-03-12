using System.Linq;
using System.Linq.Expressions;
using Xizzmat.Customer.Domain.Entities;

namespace Xizzmat.Customer.Domain.Specifications;

public static class CustomerSpecification
{
    public static ISpecification<CustomerEntity> ById(Guid id) => new ByIdSpecification(id);

    public static ISpecification<CustomerEntity> Paged(int page, int pageSize) => new PagedSpecification(page, pageSize);

    public static ISpecification<CustomerEntity> ByPhone(string phone) => new ByPhoneSpecification(phone);

    public static ISpecification<CustomerEntity> DeletedPaged(int page, int pageSize) => new DeletedPagedSpecification(page, pageSize);

    private sealed class ByIdSpecification : Specification<CustomerEntity>
    {
        public ByIdSpecification(Guid id)
        {
            Criteria = c => c.Id == id;
            AsNoTracking = true;
        }
    }

    public static ISpecification<CustomerEntity> RecentlyRegisteredPaged(int days, int page, int pageSize) => new RecentlyRegisteredPagedSpecification(days, page, pageSize);

    private sealed class RecentlyRegisteredPagedSpecification : Specification<CustomerEntity>
    {
        public RecentlyRegisteredPagedSpecification(int days, int page, int pageSize)
        {
            var from = DateTime.UtcNow.AddDays(-days);
            Criteria = c => c.CreatedAt >= from;
            Skip = (page - 1) * pageSize;
            Take = pageSize;
            OrderBy = q => q.OrderByDescending(c => c.CreatedAt);
            AsNoTracking = true;
        }
    }

    public static ISpecification<CustomerEntity> SearchPaged(string term, int page, int pageSize) => new SearchPagedSpecification(term, page, pageSize);

    private sealed class SearchPagedSpecification : Specification<CustomerEntity>
    {
        public SearchPagedSpecification(string term, int page, int pageSize)
        {
            var t = term?.ToLower() ?? string.Empty;
            Criteria = c => (c.Name.ToLower().Contains(t) || c.Surname.ToLower().Contains(t) || c.PhoneNumber.ToLower().Contains(t));
            Skip = (page - 1) * pageSize;
            Take = pageSize;
            OrderBy = q => q.OrderByDescending(c => c.CreatedAt);
            AsNoTracking = true;
        }
    }

    private sealed class ByPhoneSpecification : Specification<CustomerEntity>
    {
        public ByPhoneSpecification(string phone)
        {
            Criteria = c => c.PhoneNumber == phone;
            AsNoTracking = true;
        }
    }

    private sealed class DeletedPagedSpecification : Specification<CustomerEntity>
    {
        public DeletedPagedSpecification(int page, int pageSize)
        {
            Criteria = c => c.IsDeleted;
            Skip = (page - 1) * pageSize;
            Take = pageSize;
            OrderBy = q => q.OrderByDescending(c => c.CreatedAt);
            AsNoTracking = true;
        }
    }

    private sealed class PagedSpecification : Specification<CustomerEntity>
    {
        public PagedSpecification(int page, int pageSize)
        {
            Skip = (page - 1) * pageSize;
            Take = pageSize;
            OrderBy = q => q.OrderByDescending(c => c.CreatedAt);
            AsNoTracking = true;
        }
    }
}
