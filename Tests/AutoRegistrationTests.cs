using AutoDependencyRegistration;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tests;

public class AutoRegistrationTests
{
    [Fact]
    public void Given_Services_Have_Correct_Attribute_They_Should_Register()
    {
        // Arrange
        var collection = new ServiceCollection();
        
        // Act
        var results = collection.AutoRegisterDependencies();
        
        // Assert - Verify attribute-based services are registered
        results.Should().Contain("ReferenceSingleton, Tests.Services.Interfaces.IReferenceSingleton has been registered as Singleton");
        results.Should().Contain("ScopedService, Tests.Services.Interfaces.IScopedService has been registered as Scoped");
        results.Should().Contain("ScopedServiceIgnoreInterface has been registered as Scoped");
        results.Should().Contain("SingletonService, Tests.Services.Interfaces.ISingletonService has been registered as Singleton");
        results.Should().Contain("SingletonServiceIgnoreInterface has been registered as Singleton");
        results.Should().Contain("TransientClass has been registered as Transient");
        results.Should().Contain("TransientService, Tests.Services.Interfaces.ITransientService has been registered as Transient");
        results.Should().Contain("TransientServiceIgnoreInterface has been registered as Transient");
    }

    [Fact]
    public void Given_Services_Implement_Dependency_Interfaces_They_Should_Register()
    {
        // Arrange
        var collection = new ServiceCollection();
        
        // Act
        var results = collection.AutoRegisterDependencies();
        
        // Assert - Verify interface-based services are registered
        results.Should().Contain("InterfaceBasedTransientService");
        results.Should().Contain("InterfaceBasedScopedService");
        results.Should().Contain("InterfaceBasedSingletonService");
        results.Should().Contain("InterfaceBasedTransientIgnoreInterface");
        
        // Verify they're registered with correct lifetimes
        results.Should().Contain("InterfaceBasedTransientService").And.Contain("Transient");
        results.Should().Contain("InterfaceBasedScopedService").And.Contain("Scoped");
        results.Should().Contain("InterfaceBasedSingletonService").And.Contain("Singleton");
        results.Should().Contain("InterfaceBasedTransientIgnoreInterface").And.Contain("Transient");
        
        // Verify interface-based services with interfaces are registered with their interfaces
        results.Should().Contain("InterfaceBasedTransientService, Tests.Services.Interfaces.ITransientService");
        results.Should().Contain("InterfaceBasedScopedService, Tests.Services.Interfaces.IScopedService");
        results.Should().Contain("InterfaceBasedSingletonService, Tests.Services.Interfaces.ISingletonService");
        
        // Verify ignore interface variant doesn't register the interface
        results.Should().Contain("InterfaceBasedTransientIgnoreInterface has been registered as Transient");
        results.Should().NotContain("InterfaceBasedTransientIgnoreInterface, Tests.Services.Interfaces.ITransientService");
    }

#if NET8_0_OR_GREATER
    [Fact]
    public void Given_Services_Have_Keyed_Attributes_They_Should_Register_As_Keyed_Services()
    {
        // Arrange
        var collection = new ServiceCollection();
        
        // Act
        var results = collection.AutoRegisterDependencies();
        
        // Assert - Verify keyed services are registered
        results.Should().Contain("KeyedTransientServiceA");
        results.Should().Contain("KeyedTransientServiceB");
        results.Should().Contain("KeyedScopedService");
        results.Should().Contain("KeyedSingletonService");
        results.Should().Contain("KeyedTransientIgnoreInterface");
        
        // Verify they're registered with keys
        results.Should().Contain("KeyedTransientServiceA").And.Contain("ServiceA");
        results.Should().Contain("KeyedTransientServiceB").And.Contain("ServiceB");
        results.Should().Contain("KeyedScopedService").And.Contain("ScopedKey");
        results.Should().Contain("KeyedSingletonService").And.Contain("SingletonKey");
        results.Should().Contain("KeyedTransientIgnoreInterface").And.Contain("NoInterfaceKey");
        
        // Verify keyed services with interfaces are registered with their interfaces
        results.Should().Contain("KeyedTransientServiceA, Tests.Services.Interfaces.ITransientService");
        results.Should().Contain("KeyedTransientServiceB, Tests.Services.Interfaces.ITransientService");
        results.Should().Contain("KeyedScopedService, Tests.Services.Interfaces.IScopedService");
        results.Should().Contain("KeyedSingletonService, Tests.Services.Interfaces.ISingletonService");
        
        // Verify keyed services can be resolved by key
        var serviceProvider = collection.BuildServiceProvider();
        var keyedServiceProvider = (Microsoft.Extensions.DependencyInjection.IKeyedServiceProvider)serviceProvider;
        
        var serviceA = keyedServiceProvider.GetRequiredKeyedService<Tests.Services.Interfaces.ITransientService>("ServiceA");
        var serviceB = keyedServiceProvider.GetRequiredKeyedService<Tests.Services.Interfaces.ITransientService>("ServiceB");
        
        serviceA.Should().NotBeNull();
        serviceB.Should().NotBeNull();
        serviceA.Should().BeOfType<Tests.Services.KeyedTransientServiceA>();
        serviceB.Should().BeOfType<Tests.Services.KeyedTransientServiceB>();
        serviceA.Should().NotBeSameAs(serviceB);
    }
#endif
}