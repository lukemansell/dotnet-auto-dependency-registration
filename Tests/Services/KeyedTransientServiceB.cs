using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsTransientKeyed("ServiceB")]
public class KeyedTransientServiceB : ITransientService
{
    public string DemoService()
    {
        return "Hi from keyed ServiceB!";
    }
}

