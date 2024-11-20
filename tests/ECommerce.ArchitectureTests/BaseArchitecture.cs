using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using Mono.Cecil.Cil;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerce.ArchitectureTests
{
    public abstract class BaseArchitecture
    {
        private const string BaseNamespace = "ECommerce";

        protected const string ModulesNamespace = $"{BaseNamespace}.Modules";
        protected const string CustomersModuleNamespace = $"{ModulesNamespace}.Customers";
        protected const string OrdersModuleNamespace = $"{ModulesNamespace}.Orders";
        protected const string ProductsModuleNamespace = $"{ModulesNamespace}.Products";

        protected static readonly Architecture _architecture = new ArchLoader().LoadAssemblies(
            typeof(Contracts.Interfaces.ICustomerCatalogService).Assembly,
            typeof(Contracts.Interfaces.IProductCatalogService).Assembly,
            typeof(Common.Domain.Entity).Assembly,
            typeof(Modules.Orders.Services.OrderService).Assembly,
            typeof(Modules.Products.Services.ProductService).Assembly,
            typeof(Modules.Customers.Services.CustomerService).Assembly,
            typeof(Modules.Customers.Services.CustomerCatalogService).Assembly).Build();

        protected readonly IObjectProvider<IType> _catalogLayer =
            ArchRuleDefinition.Types().That().ResideInAssembly(typeof(Modules.Customers.Services.CustomerCatalogService).Assembly).As("Catalog Layer");
    }
}
