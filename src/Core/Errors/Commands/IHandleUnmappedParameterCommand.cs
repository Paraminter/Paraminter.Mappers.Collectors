namespace Paraminter.Mappers.Collectors.Errors.Commands;

using Paraminter.Cqs;
using Paraminter.Parameters.Models;

/// <summary>Represents a command to handle an error encountered when attempting to map a parameter, caused by not being successful.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
public interface IHandleUnmappedParameterCommand<out TParameter>
    : ICommand
    where TParameter : IParameter
{
    /// <summary>The parameter.</summary>
    public abstract TParameter Parameter { get; }
}
