using AutoDependencyRegistration.Attributes;

namespace Tests.Services;

[RegisterClassAsTransientKeyedIgnoreInterface("NoInterfaceKey")]
public class KeyedTransientIgnoreInterface
{
    public string DemoService()
    {
        return "Hi from keyed transient without interface!";
    }
}

