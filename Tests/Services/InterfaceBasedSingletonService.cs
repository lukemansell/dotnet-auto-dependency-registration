using AutoDependencyRegistration.Interfaces;
using Tests.Services.Interfaces;

namespace Tests.Services;

public class InterfaceBasedSingletonService : ISingletonService, ISingletonDependency
{
    public string DemoService()
    {
        return "Hi from interface-based singleton!";
    }
}

