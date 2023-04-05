using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsScoped]
public class ScopedService : IScopedService
{
    public string DemoService()
    {
        return "Hi!";
    }
}