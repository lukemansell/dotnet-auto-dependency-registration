using Dotnet.AutoDependencyRegistration.Extensions.Attributes;

namespace Tests.Services;

[RegisterClassAsScoped]
public class ScopedService : IScopedService
{
    public string DemoService()
    {
        return "Hi!";
    }
}