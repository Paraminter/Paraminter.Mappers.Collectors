namespace Paraminter.Mappers.Collectors;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Commands;
using Paraminter.Mappers.Collectors.Errors;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

using System;

/// <summary>Collects mappings from parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public sealed class ArgumentAssociatorMappingsCollector<TParameter, TArgumentData>
    : ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    private readonly IQueryHandler<IGetArgumentAssociatorMappingsCollectorQuery, IArgumentAssociatorMappingsCollector<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> MappingsProvider;
    private readonly IArgumentAssociatorMappingsCollectorErrorHandler<TParameter> ErrorHandler;

    /// <summary>Instantiates a collector of mappings from parameters to associators of arguments and that parameter.</summary>
    /// <param name="mappingsCollectorProvider">Provides a collector of mappings from parameters to associators of arguments and that parameter.</param>
    /// <param name="errorHandler">Handles encountered errors.</param>
    public ArgumentAssociatorMappingsCollector(
        IQueryHandler<IGetArgumentAssociatorMappingsCollectorQuery, IArgumentAssociatorMappingsCollector<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mappingsCollectorProvider,
        IArgumentAssociatorMappingsCollectorErrorHandler<TParameter> errorHandler)
    {
        MappingsProvider = mappingsCollectorProvider ?? throw new ArgumentNullException(nameof(mappingsCollectorProvider));
        ErrorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }

    void ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>.Handle(
        IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var mappingsCollector = MappingsProvider.Handle(GetArgumentAssociatorMappingsCollectorQuery.Instance);

        if (mappingsCollector.TryAddMapping(command.Parameter, command.Associator) is false)
        {
            HandleDuplicateParameter(command.Parameter);
        }
    }

    private void HandleDuplicateParameter(TParameter parameter)
    {
        var command = HandleDuplicateParameterCommandFactory.Create(parameter);

        ErrorHandler.DuplicateParameter.Handle(command);
    }
}
