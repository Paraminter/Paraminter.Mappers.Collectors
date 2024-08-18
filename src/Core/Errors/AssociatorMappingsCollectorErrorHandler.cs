namespace Paraminter.Mappers.Collectors.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

using System;

/// <inheritdoc cref="IAssociatorMappingsCollectorErrorHandler{TParameter}"/>
public sealed class AssociatorMappingsCollectorErrorHandler<TParameter>
    : IAssociatorMappingsCollectorErrorHandler<TParameter>
    where TParameter : IParameter
{
    private readonly ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> DuplicateParameter;

    /// <summary>Instantiates a handler of errors encountered when creating mappers.</summary>
    /// <param name="duplicateParameter">Handles multiple mappings for the same parameters.</param>
    public AssociatorMappingsCollectorErrorHandler(
        ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> duplicateParameter)
    {
        DuplicateParameter = duplicateParameter ?? throw new ArgumentNullException(nameof(duplicateParameter));
    }

    ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> IAssociatorMappingsCollectorErrorHandler<TParameter>.DuplicateParameter => DuplicateParameter;
}
