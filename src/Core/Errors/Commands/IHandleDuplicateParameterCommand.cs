namespace Paraminter.Mappers.Collectors.Errors.Commands;

using Paraminter.Cqs;
using Paraminter.Parameters.Models;

/// <summary>Represents a command to handle an error encountered when collecting mappings, caused by multiple mappings for the same parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
public interface IHandleDuplicateParameterCommand<out TParameter>
    : ICommand
    where TParameter : IParameter
{
    /// <summary>The duplicate parameter.</summary>
    public abstract TParameter Parameter { get; }
}
