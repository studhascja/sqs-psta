using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;

namespace MovieRating.ArchitectureTests;

public class ArchitectureTest
{
    private const string Core = "MovieRating.Core";
    private const string Infrastructure = "MovieRating.Infrastructure";
    private const string Web = "MovieRating.Web";

    [Fact]
    public void TestDependencys()
    {
        // Prep
        var core = Types.InAssembly(Assembly.Load(Core));
        var infrastructure = Types.InAssembly(Assembly.Load(Infrastructure));

        // Act
        // Core have no Dependency's
        var coreDependencyResult = core
            .Should()
            .NotHaveDependencyOnAny()
            .GetResult();

        // Infrastructure have no Dependency's on Web
        var infrastructureDependencyResult = infrastructure
            .ShouldNot()
            .HaveDependencyOn(Web)
            .GetResult();

        // Assert
        Assert.True(coreDependencyResult.IsSuccessful);
        Assert.True(infrastructureDependencyResult.IsSuccessful);
    }

    [Fact]
    public void TestClassTypeLocation()
    {
        // Prep
        var core = Types.InAssembly(Assembly.Load(Core));
        var infrastructure = Types.InAssembly(Assembly.Load(Infrastructure));
        var web = Types.InAssembly(Assembly.Load(Web));

        // Act
        // Exception are only in Core
        var exceptionsInCore = core
            .That()
            .Inherit(typeof(Exception))
            .Should()
            .ResideInNamespaceStartingWith(Core)
            .GetResult();
        
        var exceptionsNotInInfrastructure = infrastructure
            .That()
            .Inherit(typeof(Exception))
            .Should()
            .ResideInNamespaceStartingWith(Core)
            .GetResult();
        
        var exceptionsNotInWeb = web
            .That()
            .Inherit(typeof(Exception))
            .Should()
            .ResideInNamespaceStartingWith(Core)
            .GetResult();
        
        // Interfaces are only in Core
        var interfacesInCore = core
            .That()
            .AreInterfaces()
            .Should()
            .ResideInNamespaceStartingWith(Core)
            .GetResult();
        
        var interfacesNotInInfrastructure = infrastructure
            .That()
            .AreInterfaces()
            .Should()
            .ResideInNamespaceStartingWith(Core)
            .GetResult();
        
        var interfacesNotInWeb = web
            .That()
            .AreInterfaces()
            .Should()
            .ResideInNamespaceStartingWith(Core)
            .GetResult();
        
        // Services are only in Infrastructure
        var servicesNotInCore = core
            .That()
            .HaveNameEndingWith("Service")
            .And()
            .DoNotHaveNameStartingWith("I")
            .Should()
            .ResideInNamespaceStartingWith(Infrastructure)
            .GetResult();
        
        var servicesInInfrastructure= infrastructure
            .That()
            .HaveNameEndingWith("Service")
            .Should()
            .ResideInNamespaceStartingWith(Infrastructure)
            .GetResult();
        
        var servicesNotInWeb = web
            .That()
            .HaveNameEndingWith("Service")
            .Should()
            .ResideInNamespaceStartingWith(Infrastructure)
            .GetResult();
        
        // Controllers are only in Web
        var controllerNotInCore = core
            .That()
            .Inherit(typeof(Controller))
            .Should()
            .ResideInNamespaceStartingWith(Web)
            .GetResult();
        
        var controllerNotInInfrastructure = infrastructure
            .That()
            .Inherit(typeof(Controller))
            .Should()
            .ResideInNamespaceStartingWith(Web)
            .GetResult();
        
        var controllerInWeb = web
            .That()
            .Inherit(typeof(Controller))
            .Should()
            .ResideInNamespaceStartingWith(Web)
            .GetResult();
        
        // Assert
        Assert.True(exceptionsInCore.IsSuccessful);
        Assert.True(exceptionsNotInInfrastructure.IsSuccessful);
        Assert.True(exceptionsNotInWeb.IsSuccessful);
        
        Assert.True(interfacesInCore.IsSuccessful);
        Assert.True(interfacesNotInInfrastructure.IsSuccessful);
        Assert.True(interfacesNotInWeb.IsSuccessful);
        
        Assert.True(servicesNotInCore.IsSuccessful);
        Assert.True(servicesInInfrastructure.IsSuccessful);
        Assert.True(servicesNotInWeb.IsSuccessful);
        
        Assert.True(controllerNotInCore.IsSuccessful);
        Assert.True(controllerNotInInfrastructure.IsSuccessful);
        Assert.True(controllerInWeb.IsSuccessful);
    }
    
    [Fact]
    public void TestNamingConventions()
    {
        // Prep
        var core = Types.InAssembly(Assembly.Load(Core));
        var web = Types.InAssembly(Assembly.Load(Web));

        // Act
        // Controller Classes ends with "Controller"
        var controllerNameTest = web
            .That()
            .Inherit(typeof(Controller))
            .Should()
            .HaveNameEndingWith("Controller")
            .GetResult();

        // Interfaces start with an "I"
        var interfaceNameTest = core
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        // Exceptions end with Exception
        var exceptionNameTest = core
            .That()
            .Inherit(typeof(Exception))
            .Should()
            .HaveNameEndingWith("Exception")
            .GetResult();

        // Assert
        Assert.True(controllerNameTest.IsSuccessful);
        Assert.True(interfaceNameTest.IsSuccessful);
        Assert.True(exceptionNameTest.IsSuccessful);
    }

    [Fact]
    public void TestNameSpaces()
    {
        // Prep
        var core = Types.InAssembly(Assembly.Load(Core));
        var infrastructure = Types.InAssembly(Assembly.Load(Infrastructure));
        var web = Types.InAssembly(Assembly.Load(Web));

        // Act
        // Core Members have Namespaces, starting with MovieRating.Core
        var coreNamespaces = core
            .Should()
            .ResideInNamespaceStartingWith(Core)
            .GetResult();

        // Core Members have Namespaces, starting with MovieRating.Infrastructure
        var infrastructureNamespaces = infrastructure
            .Should()
            .ResideInNamespaceStartingWith(Infrastructure)
            .GetResult();

        // Core Members have Namespaces, starting with MovieRating.Web, are in namespace AspNetCoreGeneratedDocument or have no namespace
        var webNamespaces = web
            .Should()
            .ResideInNamespaceStartingWith(Web)
            .Or()
            .ResideInNamespace("AspNetCoreGeneratedDocument")
            .Or()
            .ResideInNamespace("")
            .GetResult();
        
        // Assert
        Assert.True(coreNamespaces.IsSuccessful);
        Assert.True(infrastructureNamespaces.IsSuccessful);
        Assert.True(webNamespaces.IsSuccessful);
    }
}