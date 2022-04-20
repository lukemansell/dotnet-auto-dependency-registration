using Dotnet.AutoDependencyRegistration.Extensions.Attributes;

namespace Tests.Services;

[RegisterClassAsTransient]
public class TransientService : ITransientService
{
    public string DemoService()
    {
        return "Hi!";
    }
}