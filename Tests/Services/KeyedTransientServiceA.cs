using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsTransientKeyed("ServiceA")]
public class KeyedTransientServiceA : ITransientService
{
    public string DemoService()
    {
        return "Hi from keyed ServiceA!";
    }
}

