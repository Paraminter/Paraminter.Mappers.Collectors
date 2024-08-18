﻿namespace Paraminter.Mappers.Collectors.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

/// <summary>Handles errors encountered when creating mappers.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
public interface IAssociatorMappingsCollectorErrorHandler<in TParameter>
    where TParameter : IParameter
{
    /// <summary>Handles multiple mappings for the same parameters.</summary>
    public abstract ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> DuplicateParameter { get; }
}
