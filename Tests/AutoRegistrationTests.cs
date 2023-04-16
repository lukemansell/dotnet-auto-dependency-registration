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
        const string expected = @"ReferenceSingleton, Tests.Services.Interfaces.IReferenceSingleton has been registered as Singleton. 
ScopedService, Tests.Services.Interfaces.IScopedService has been registered as Scoped. 
ScopedServiceIgnoreInterface has been registered as Scoped. 
SingletonService, Tests.Services.Interfaces.ISingletonService has been registered as Singleton. 
SingletonServiceIgnoreInterface has been registered as Singleton. 
TransientClass has been registered as Transient. 
TransientService, Tests.Services.Interfaces.ITransientService has been registered as Transient. 
TransientServiceIgnoreInterface has been registered as Transient. 
";

        // Act
        var results = collection.AutoRegisterDependencies();
        
        // Assert
        results.Should().BeEquivalentTo(expected);
    }
}