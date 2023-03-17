using Microsoft.EntityFrameworkCore;
using ContactManag.Infra.Context;

namespace ContactManag.Test
{
    public class ContextFactory
    {
        public readonly AppDbContext Context;

        public ContextFactory()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseNpgsql("Server=contactmanag.ce6vmgidwgna.us-east-1.rds.amazonaws.com;Database=ContactManagDB;Uid=postgres;Pwd=postgres1234")
           .Options;

            // Insert seed data into the database using one instance of the context
            Context = new AppDbContext(options);
        }
    }
}