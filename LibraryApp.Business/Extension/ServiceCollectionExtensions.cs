using LibraryApp.Business.Abstract;
using LibraryApp.Business.Concrete;
using LibraryApp.DataAccess.Abstract.Repository;
using LibraryApp.DataAccess.Concrete;
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
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IBookManager, BookManager>();
        }
    }
}