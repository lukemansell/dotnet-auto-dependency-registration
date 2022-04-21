using Dotnet.AutoDependencyRegistration.Attributes;

namespace Tests.Services;

[RegisterClassAsTransient]
public class TransientService : ITransientService
{
    public string DemoService()
    {
        return "Hi!";
    }
}