using Praga.Paisakanaku.Web.Middleware;
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
using Praga.PaisaKanaku.Core.Operations.Services.Transactions;

namespace Praga.PaisaKanaku.Web.IoC
{
    public static class DependencyRegistrations
    {
        public static void InjectDependencies(this IServiceCollection services, string connectionString) 
        {
            // Middlewares
            services.AddTransient<AuthenticationMiddleware>();

            services.AddTransient<IDataBaseConnection, DataBaseConnection>(con => new DataBaseConnection(connectionString));

            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<ILookupsRepository, LookupsRepository>();

            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IBrandRepository, BrandRepository>();

            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IMemberRepository, MemberRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IGroceryService, GroceryService>();
            services.AddTransient<IGroceryRepository, GroceryRepository>();

            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<ICommonRepository, CommonRepository>();

            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IExpenseRepository, ExpenseRepository>();

            services.AddTransient<IExpenseGroceryService, ExpenseGroceryService>();
            services.AddTransient<IExpenseGroceryRepository, ExpenseGroceryRepository>();

            services.AddTransient<IExpenseProductService, ExpenseProductService>();
            services.AddTransient<IExpenseProductRepository, ExpenseProductRepository>();

            services.AddTransient<IExpenseFamilyWellbeingService, ExpenseFamilyWellbeingService>();
            services.AddTransient<IExpenseFamilyWellbeingRepository, ExpenseFamilyWellbeingRepository>();

            services.AddTransient<IBillTypeService, BillTypeService>();
            services.AddTransient<IBillTypeRepository, BillTypeRepository>();

            services.AddTransient<IRepairTypeService, RepairTypeService>();
            services.AddTransient<IRepairTypeRepository, RepairTypeRepository>();

            services.AddTransient<IOutdoorFoodVendorService, OutdoorFoodVendorService>();
            services.AddTransient<IOutdoorFoodVendorRepository, OutdoorFoodVendorRepository>();

            services.AddTransient<IExpenseOutdoorFoodService, ExpenseOutdoorFoodService>();
            services.AddTransient<IExpenseOutdoorFoodRepository, ExpenseOutdoorFoodRepository>();

            services.AddTransient<IExpenseTravelService, ExpenseTravelService>();
            services.AddTransient<IExpenseTravelRepository, ExpenseTravelRepository>();
        }
    }
}
