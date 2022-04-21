using Dotnet.AutoDependencyRegistration.Extensions;
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
        var expected = @"ScopedService, IScopedService has been registered as Scoped. 
SingletonService, ISingletonService has been registered as Singleton. 
TransientService, ITransientService has been registered as Transient. 
";

        // Act
        var results = collection.AutoRegister();
        
        // Assert
        results.Should().BeEquivalentTo(expected);
    }
}