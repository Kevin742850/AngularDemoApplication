using PharmacyManagementSystem.Core.Repositories;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }
        public DataContext? DataContext
        {
            get { return Context as DataContext; }
        }

    }
}
