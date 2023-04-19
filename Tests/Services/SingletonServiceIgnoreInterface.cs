using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsSingleton(ExcludeInterface = true)]
public class SingletonServiceIgnoreInterface : IDummyInterface
{
    public string DemoService()
    {
        return "Hi!";
    }
}