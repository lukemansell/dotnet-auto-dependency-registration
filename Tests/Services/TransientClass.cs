using Dotnet.AutoDependencyRegistration.Attributes;

namespace Tests.Services;

[RegisterClassAsTransient]
public class TransientClass
{
    public string DemoService()
    {
        return "Hi!";
    }
}