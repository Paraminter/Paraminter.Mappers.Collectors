namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

/// <summary>Represents a set of mappings from parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TAssociator">The type representing the associators.</typeparam>
public interface IReadOnlyArgumentAssociatorMappings<in TParameter, out TAssociator>
    where TParameter : IParameter
{
    /// <summary>Attempts to map a parameter to an associator of arguments and that parameter.</summary>
    /// <param name="parameter">The parameter.</param>
    /// <returns>The associator of arguments and the parameter, if the attempt was successful.</returns>
    public abstract IArgumentAssociatorMapAttemptResult<TAssociator> TryMap(TParameter parameter);
}
