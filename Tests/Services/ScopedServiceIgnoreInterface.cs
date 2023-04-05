using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsScopedIgnoreInterface]
public class ScopedServiceIgnoreInterface : IDummyInterface
{
    public string DemoService()
    {
        return "Hi!";
    }
}