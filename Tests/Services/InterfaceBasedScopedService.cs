using AutoDependencyRegistration.Interfaces;
using Tests.Services.Interfaces;

namespace Tests.Services;

public class InterfaceBasedScopedService : IScopedService, IScopedDependency
{
    public string DemoService()
    {
        return "Hi from interface-based scoped!";
    }
}

