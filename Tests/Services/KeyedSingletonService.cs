using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsSingletonKeyed("SingletonKey")]
public class KeyedSingletonService : ISingletonService
{
    public string DemoService()
    {
        return "Hi from keyed singleton service!";
    }
}

