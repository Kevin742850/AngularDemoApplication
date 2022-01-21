using PharmacyManagementSystem.Core;
using PharmacyManagementSystem.Core.Repositories;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Persistence.Repositories;

namespace PharmacyManagementSystem.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
        }

        public ICustomerRepository Customers { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}