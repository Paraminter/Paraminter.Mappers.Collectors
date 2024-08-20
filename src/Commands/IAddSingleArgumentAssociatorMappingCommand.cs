namespace Paraminter.Mappers.Collectors.Commands;

using Paraminter.Cqs;
using Paraminter.Parameters.Models;

/// <summary>Represents a command to add a mapping from a parameter to an associator of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TAssociator">The type representing the associators.</typeparam>
public interface IAddSingleArgumentAssociatorMappingCommand<out TParameter, out TAssociator>
    : ICommand
    where TParameter : IParameter
{
    /// <summary>The parameter.</summary>
    public abstract TParameter Parameter { get; }

    /// <summary>Associates arguments with the parameter.</summary>
    public abstract TAssociator Associator { get; }
}
