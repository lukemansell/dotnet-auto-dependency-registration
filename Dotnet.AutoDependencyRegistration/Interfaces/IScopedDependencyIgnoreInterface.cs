namespace AutoDependencyRegistration.Interfaces;

/// <summary>
/// Marker interface for classes that should be registered as Scoped dependencies
/// without registering their implemented interfaces.
/// Classes implementing this interface will be automatically registered with ServiceLifetime.Scoped
/// and will ignore any interfaces they implement.
/// </summary>
public interface IScopedDependencyIgnoreInterface
{
}

