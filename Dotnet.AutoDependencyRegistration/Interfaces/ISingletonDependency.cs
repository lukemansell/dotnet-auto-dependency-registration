namespace AutoDependencyRegistration.Interfaces;

/// <summary>
/// Marker interface for classes that should be registered as Singleton dependencies.
/// Classes implementing this interface will be automatically registered with ServiceLifetime.Singleton.
/// </summary>
public interface ISingletonDependency
{
}

