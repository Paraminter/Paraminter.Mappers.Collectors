namespace Paraminter.Mappers.Collectors.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

using System;

/// <inheritdoc cref="IAssociatorMapperErrorHandler{TParameter}"/>
public sealed class AssociatorMapperErrorHandler<TParameter>
    : IAssociatorMapperErrorHandler<TParameter>
    where TParameter : IParameter
{
    private readonly ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> UnmappedParameter;

    /// <summary>Instantiates a handler of errors encountered when attempting to map parameters to associators of arguments and that parameter.</summary>
    /// <param name="unmappedParameter">Handles unsuccessful attempts to map parameters.</param>
    public AssociatorMapperErrorHandler(
        ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> unmappedParameter)
    {
        UnmappedParameter = unmappedParameter ?? throw new ArgumentNullException(nameof(unmappedParameter));
    }

    ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> IAssociatorMapperErrorHandler<TParameter>.UnmappedParameter => UnmappedParameter;
}
