using ContactManag.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManag.Infra.Context
{
    public class AppDbContextIndex
    {
        public static void Configure(ModelBuilder builder)
        {
            #region Contact
            builder.Entity<Contact>()
                .HasIndex(x => x.Id);
            #endregion

            #region Person
            builder.Entity<Person>()
                .HasIndex(x => x.Id);
            #endregion
        }
    }
}