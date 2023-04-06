using AutoDependencyRegistration.Attributes;
using Tests.Services.Interfaces;

namespace Tests.Services;

[RegisterClassAsTransient]
public class TransientService : ITransientService
{
    public string DemoService()
    {
        return "Hi!";
    }
}