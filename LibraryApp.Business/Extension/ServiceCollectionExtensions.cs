using LibraryApp.DataAccess.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace LibraryApp.Business.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessModule(this IServiceCollection services, IConfiguration configurationContext)
        {
            services.AddDataAccessModule(configurationContext);
        }
    }
}
