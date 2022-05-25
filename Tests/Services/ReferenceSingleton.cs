using AutoDependencyRegistration.Attributes;

namespace Tests.Services;

[RegisterClassAsSingleton]
public class ReferenceSingleton : IReferenceSingleton
{
    private readonly ISingletonService _singletonService;

    public ReferenceSingleton(ISingletonService singletonService)
    {
        _singletonService = singletonService;
    }

    public string Demo()
    {
        return _singletonService.DemoService();
    }
}