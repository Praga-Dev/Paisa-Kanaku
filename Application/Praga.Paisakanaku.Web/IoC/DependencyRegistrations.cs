using Praga.PaisaKanaku.Core.DataAccess.ConnectionManager;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Setup;
using Praga.PaisaKanaku.Core.DataAccess.IRepositories.Transactions;
using Praga.PaisaKanaku.Core.DataAccess.Repositories;
using Praga.PaisaKanaku.Core.DataAccess.Repositories.Setup;
using Praga.PaisaKanaku.Core.DataAccess.Repositories.Transactions;
using Praga.PaisaKanaku.Core.Operations.IServices;
using Praga.PaisaKanaku.Core.Operations.IServices.Setup;
using Praga.PaisaKanaku.Core.Operations.IServices.Transactions;
using Praga.PaisaKanaku.Core.Operations.Services;
using Praga.PaisaKanaku.Core.Operations.Services.Setup;

namespace Praga.PaisaKanaku.Web.IoC
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

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IExpenseRepository, ExpenseRepository>();

            services.AddTransient<IBillTypeService, BillTypeService>();
            services.AddTransient<IBillTypeRepository, BillTypeRepository>();

            services.AddTransient<IRepairTypeService, RepairTypeService>();
            services.AddTransient<IRepairTypeRepository, RepairTypeRepository>();
        }
    }
}
