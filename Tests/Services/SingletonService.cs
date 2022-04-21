using Dotnet.AutoDependencyRegistration.Attributes;

namespace Tests.Services;

[RegisterClassAsSingleton]
public class SingletonService : ISingletonService
{
    public string DemoService()
    {
        return "Hi!";
    }
}