namespace Paraminter.Mappers.Collectors.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

using System;

/// <inheritdoc cref="IArgumentAssociatorMapperErrorHandler{TParameter}"/>
public sealed class ArgumentAssociatorMapperErrorHandler<TParameter>
    : IArgumentAssociatorMapperErrorHandler<TParameter>
    where TParameter : IParameter
{
    private readonly ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> UnmappedParameter;

    /// <summary>Instantiates a handler of errors encountered when attempting to map parameters to associators of arguments and that parameter.</summary>
    /// <param name="unmappedParameter">Handles unsuccessful attempts to map parameters.</param>
    public ArgumentAssociatorMapperErrorHandler(
        ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> unmappedParameter)
    {
        UnmappedParameter = unmappedParameter ?? throw new ArgumentNullException(nameof(unmappedParameter));
    }

    ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> IArgumentAssociatorMapperErrorHandler<TParameter>.UnmappedParameter => UnmappedParameter;
}
