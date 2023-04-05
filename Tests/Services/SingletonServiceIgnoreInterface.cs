using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsSingletonIgnoreInterface]
public class SingletonServiceIgnoreInterface : IDummyInterface
{
    public string DemoService()
    {
        return "Hi!";
    }
}