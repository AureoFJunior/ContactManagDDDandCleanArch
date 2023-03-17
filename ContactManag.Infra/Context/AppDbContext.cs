using ContactManag.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManag.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppDbContextIndex.Configure(modelBuilder);
        }

        #region DbSets
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<User> User { get; set; }
        #endregion
    }
}