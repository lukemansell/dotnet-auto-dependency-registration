using AutoDependencyRegistration.Interfaces;
using Tests.Services.Interfaces;

namespace Tests.Services;

public class InterfaceBasedTransientService : ITransientService, ITransientDependency
{
    public string DemoService()
    {
        return "Hi from interface-based transient!";
    }
}

