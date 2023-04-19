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
        
        // Assert
        results.Should()
            .Contain(
                "ReferenceSingleton, Tests.Services.Interfaces.IReferenceSingleton has been registered as Singleton.");
        results.Should()
            .Contain("ScopedService, Tests.Services.Interfaces.IScopedService has been registered as Scoped.");
        results.Should().Contain("ScopedServiceIgnoreInterface has been registered as Scoped.");
        results.Should()
            .Contain("SingletonService, Tests.Services.Interfaces.ISingletonService has been registered as Singleton.");
        results.Should().Contain("SingletonServiceIgnoreInterface has been registered as Singleton.");
        results.Should().Contain("TransientClass has been registered as Transient.");
        results.Should()
            .Contain("TransientService, Tests.Services.Interfaces.ITransientService has been registered as Transient.");
        results.Should().Contain("TransientServiceIgnoreInterface has been registered as Transient.");
    }
}