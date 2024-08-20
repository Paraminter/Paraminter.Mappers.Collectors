namespace Paraminter.Mappers.Collectors.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

using System;

/// <inheritdoc cref="IArgumentAssociatorMappingsCollectorErrorHandler{TParameter}"/>
public sealed class ArgumentAssociatorMappingsCollectorErrorHandler<TParameter>
    : IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>
    where TParameter : IParameter
{
    private readonly ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> DuplicateParameter;

    /// <summary>Instantiates a handler of errors encountered when collecting mappings from parameters to associators of arguments and that parameter.</summary>
    /// <param name="duplicateParameter">Handles multiple mappings for the same parameters.</param>
    public ArgumentAssociatorMappingsCollectorErrorHandler(
        ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> duplicateParameter)
    {
        DuplicateParameter = duplicateParameter ?? throw new ArgumentNullException(nameof(duplicateParameter));
    }

    ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>.DuplicateParameter => DuplicateParameter;
}
