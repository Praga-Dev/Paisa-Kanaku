using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataAccess.Repositories;
using Praga.PaisaKanaku.Core.DataAccess.Repositories.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using Praga.PaisaKanaku.Core.Operations.Services;
using Praga.PaisaKanaku.Core.Operations.Services.Setup;

namespace Praga.PaisaKanaku.API.IoC
{
    public static class DependencyRegistrations
    {
        public static void InjectDependencies(this IServiceCollection services, string connectionString) 
        {
            services.AddTransient<IDataBaseConnection, DataBaseConnection>(con => new DataBaseConnection(connectionString));

            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<ILookupsRepository, LookupsRepository>();

            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IBrandRepository, BrandRepository>();

            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IMemberRepository, MemberRepository>();

            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
