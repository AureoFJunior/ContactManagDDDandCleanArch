using ContactManag.Domain.Interfaces;
using ContactManag.Domain.Models;
using ContactManag.Infra.Context;
using ContactManag.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContactManag.Domain.Interfaces.Repositories;
using ContactManag.Domain.Interfaces.Services;
using ContactManag.Web.Config;

namespace ContactManag.Application.DI
{
    public class Initializer
    {
        public static void Configure(IServiceCollection services, string conection)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conection));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var mapper = MapperConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped(typeof(IRepository<Contact>), typeof(ContactRepository));
            services.AddScoped(typeof(IRepository<Person>), typeof(PersonRepository));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(IContactService<Contact>), typeof(ContactService));
            services.AddScoped(typeof(IPersonService<Person>), typeof(PersonService));
            services.AddScoped(typeof(TokenService));
            services.AddScoped(typeof(IUserService<User>), typeof(UserService));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IValidator), typeof(Validator));
        }
    }
}