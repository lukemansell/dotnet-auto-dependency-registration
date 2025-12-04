using AutoDependencyRegistration.Attributes;

namespace Tests.Services;

[RegisterClassAsScopedIgnoreInterface]
public class ScopedServiceIgnoreInterface
{
    public string DemoService()
    {
        return "Hi!";
    }
}