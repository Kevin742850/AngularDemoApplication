using PharmacyManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Inspection> Inspections { get; set; }

        public DbSet<InspectionType> InspectionTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Authenticate> Users { get; set; }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Strength> Strength { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Pharmacy> Pharmacy { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
