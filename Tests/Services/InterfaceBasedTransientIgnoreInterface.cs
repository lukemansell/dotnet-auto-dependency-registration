using AutoDependencyRegistration.Interfaces;
using Tests.Services.Interfaces;

namespace Tests.Services;

public class InterfaceBasedTransientIgnoreInterface : ITransientService, ITransientDependencyIgnoreInterface
{
    public string DemoService()
    {
        return "Hi from interface-based transient ignore interface!";
    }
}

