using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Internal;
using PhoneBook.Dal;
using PhoneBook.Dal.Migrations;
using Shared.Dal;
using Npgsql;

namespace PhoneBookDbSeeder
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(HostBuilderContext builderContext)
        {
            if (builderContext == null) throw new ArgumentNullException(nameof(builderContext));

            Configuration = builderContext.Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISystemClock, SystemClock>();
            services.AddSingleton(NpgsqlConnection.GlobalTypeMapper.DefaultNameTranslator);

            services.AddDbContext<PhoneBookDbContext, IPhoneBookMigrationMarker>(Configuration.GetConnectionString("PhoneBookConnectionString"));

            services.AddScoped<PhoneBookDbSeeder>();
        }
    }
}