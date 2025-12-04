namespace AutoDependencyRegistration.Interfaces;

/// <summary>
/// Marker interface for classes that should be registered as Scoped dependencies.
/// Classes implementing this interface will be automatically registered with ServiceLifetime.Scoped.
/// </summary>
public interface IScopedDependency
{
}

