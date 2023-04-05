using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsTransientIgnoreInterface]
public class TransientServiceIgnoreInterface : IDummyInterface
{
    public string DemoService()
    {
        return "Hi!";
    }
}