using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsTransient(ExcludeInterface = true)]
public class TransientServiceIgnoreInterface : IDummyInterface
{
    public string DemoService()
    {
        return "Hi!";
    }
}