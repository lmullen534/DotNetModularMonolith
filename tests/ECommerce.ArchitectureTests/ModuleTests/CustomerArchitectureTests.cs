using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using ECommerce.Common.Domain;

namespace ECommerce.ArchitectureTests.ModuleTests;

public class CustomerArchitectureTests : BaseArchitecture
{
    private const string CustomersDomainNamespace = $"{CustomersModuleNamespace}.Domain";

    [Fact]
    public void Modules_Should_Not_Depend_On_Each_Other()
    {
        IObjectProvider<IType> ordersClasses =
            ArchRuleDefinition.Types().That().ResideInNamespace(OrdersModuleNamespace, true);
        IObjectProvider<IType> productsClasses =
            ArchRuleDefinition.Types().That().ResideInNamespace(ProductsModuleNamespace, true);
        IObjectProvider<IType> customersClasses =
            ArchRuleDefinition.Types().That().ResideInNamespace(CustomersModuleNamespace, true);

        var ordersRule = ArchRuleDefinition.Types().That().Are(ordersClasses)
            .Should().NotDependOnAny(productsClasses).AndShould().NotDependOnAny(customersClasses);

        var productsRule = ArchRuleDefinition.Types().That().Are(productsClasses)
            .Should().NotDependOnAny(ordersClasses).AndShould().NotDependOnAny(customersClasses);

        var customersRule = ArchRuleDefinition.Types().That().Are(customersClasses)
            .Should().NotDependOnAny(ordersClasses).AndShould().NotDependOnAny(productsClasses);

        ordersRule.Check(_architecture);
        productsRule.Check(_architecture);
        customersRule.Check(_architecture);
    }

    [Fact]
    public void Module_Namespaces_Should_Be_Prefixed_With_Modules()
    {
        // Arrange
        IObjectProvider<IType> moduleClasses = CreateModuleClassesProvider();

        // Act
        var rule = CreateModuleNamespaceRule(moduleClasses);

        // Assert
        rule.Check(_architecture);
    }

    [Fact]
    public void Domain_Classes_Should_Inherit_From_Entity()
    {
        // Arrange & Act
        var rule = CreateDomainEntityInheritanceRule();

        // Assert
        rule.Check(_architecture);
    }

    private static IObjectProvider<IType> CreateModuleClassesProvider()
    {
        return ArchRuleDefinition.Types()
            .That()
            .ResideInNamespace(ModulesNamespace, true);
    }

    private static IArchRule CreateModuleNamespaceRule(IObjectProvider<IType> moduleClasses)
    {
        return ArchRuleDefinition.Types()
            .That()
            .Are(moduleClasses)
            .Should()
            .ResideInNamespace(ModulesNamespace, true);
    }

    private static IArchRule CreateDomainEntityInheritanceRule()
    {
        return ArchRuleDefinition.Classes()
            .That()
            .ResideInNamespace(CustomersDomainNamespace)
            .Should()
            .BeAssignableTo(typeof(Entity));
    }
}
