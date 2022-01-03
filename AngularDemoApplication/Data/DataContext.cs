using AngularDemoApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularDemoApplication.Data
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
    }
}
