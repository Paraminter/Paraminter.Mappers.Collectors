namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

/// <summary>Represents mappings from parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TAssociator">The type representing the associators.</typeparam>
public interface IArgumentAssociatorMappings<in TParameter, TAssociator>
    where TParameter : IParameter
{
    /// <summary>Maps parameters to associators of arguments and that parameter.</summary>
    public abstract IArgumentAssociatorMapper<TParameter, TAssociator> Mapper { get; }

    /// <summary>Collects mappings from parameters to associators of arguments and that parameter.</summary>
    public abstract IArgumentAssociatorMappingsCollector<TParameter, TAssociator> Collector { get; }
}
