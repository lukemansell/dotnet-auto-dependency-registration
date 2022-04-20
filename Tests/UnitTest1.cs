using Dotnet.AutoDependencyRegistration.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var collection = new ServiceCollection();

        // Act
        var results = collection.RegisterServices();
        
        // Assert
    }
}