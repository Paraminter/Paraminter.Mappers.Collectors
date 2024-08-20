namespace Paraminter.Mappers.Collectors.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

/// <summary>Handles errors encountered when attempting to map parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
public interface IArgumentAssociatorMapperErrorHandler<in TParameter>
    where TParameter : IParameter
{
    /// <summary>Handles unsuccessful attempts to map parameters.</summary>
    public abstract ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> UnmappedParameter { get; }
}
