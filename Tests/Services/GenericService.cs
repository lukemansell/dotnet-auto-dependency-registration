using AutoDependencyRegistration.Attributes;

namespace Tests.Services;

[RegisterClassAsTransient]
public class GenericService<T> : IGenericService<T>
{
    public string DemoService()
    {
        return "Hi!";
    }
}