namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

/// <summary>Collects mappings from parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TAssociator">The type representing the associators.</typeparam>
public interface IArgumentAssociatorMappingsCollector<in TParameter, in TAssociator>
    where TParameter : IParameter
{
    /// <summary>Attempts to add a mapping from a parameter to an associator of arguments and that parameter.</summary>
    /// <param name="parameter">The parameter.</param>
    /// <param name="associator">Associates arguments and the parameter.</param>
    /// <returns>A <see cref="bool"/> indicating whether the attempt was successful.</returns>
    public abstract bool TryAddMapping(TParameter parameter, TAssociator associator);
}
