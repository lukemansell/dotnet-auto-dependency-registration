using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsScopedKeyed("ScopedKey")]
public class KeyedScopedService : IScopedService
{
    public string DemoService()
    {
        return "Hi from keyed scoped service!";
    }
}

