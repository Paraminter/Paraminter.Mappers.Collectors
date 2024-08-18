namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

/// <summary>Represents an appendable set of mappings from parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TAssociator">The type representing the associators.</typeparam>
public interface IArgumentAssociatorMappings<in TParameter, TAssociator>
    : IWriteOnlyArgumentAssociatorMappings<TParameter, TAssociator>,
    IReadOnlyArgumentAssociatorMappings<TParameter, TAssociator>
    where TParameter : IParameter
{ }
