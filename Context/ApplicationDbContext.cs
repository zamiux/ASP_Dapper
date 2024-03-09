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
        #endregion
    }
}
