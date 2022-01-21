using PharmacyManagementSystem.Core.Repositories;

namespace PharmacyManagementSystem.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        int Complete();
    }
}
