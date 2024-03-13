using ASP_Dapper.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ASP_Dapper.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        #region DbSet
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        #endregion

        #region Fluent Relation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Ignore(t=>t.Employees);

            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Company)
                .WithMany(t => t.Employees)
                .HasForeignKey(t => t.CompanyId);


            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
