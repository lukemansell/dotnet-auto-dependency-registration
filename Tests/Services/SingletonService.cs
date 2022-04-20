using Dotnet.AutoDependencyRegistration.Extensions.Attributes;

namespace Tests.Services;

[RegisterClassAsSingleton]
public class SingletonService : ISingletonService
{
    public string DemoService()
    {
        return "Hi!";
    }
}