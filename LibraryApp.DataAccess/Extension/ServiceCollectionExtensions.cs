using LibraryApp.DataAccess.Abstract;
using LibraryApp.DataAccess.Abstract.Profile;
using LibraryApp.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.DataAccess.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessModule(this IServiceCollection services, IConfiguration configurationContext)
        {
            services.AddDbContext<LibraryContext>(options => options.UseSqlServer(configurationContext.GetConnectionString("SQLServerConnection")), ServiceLifetime.Scoped);
            services.AddAutoMapper(typeof(ProfileBase));
        }
    }
}