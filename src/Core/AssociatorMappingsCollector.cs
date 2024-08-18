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
public sealed class AssociatorMappingsCollector<TParameter, TArgumentData>
    : ICommandHandler<IAddMappedArgumentAssociatorCommand<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    private readonly IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> MappingsProvider;
    private readonly IAssociatorMappingsCollectorErrorHandler<TParameter> ErrorHandler;

    /// <summary>Instantiates a collector of mappings from parameters to associators of arguments and that parameter.</summary>
    /// <param name="mappingsProvider">Provides the set of mappings from parameters to associators of arguments and that parameter.</param>
    /// <param name="errorHandler">Handles encountered errors.</param>
    public AssociatorMappingsCollector(
        IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> mappingsProvider,
        IAssociatorMappingsCollectorErrorHandler<TParameter> errorHandler)
    {
        MappingsProvider = mappingsProvider ?? throw new ArgumentNullException(nameof(mappingsProvider));
        ErrorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }

    void ICommandHandler<IAddMappedArgumentAssociatorCommand<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>.Handle(
        IAddMappedArgumentAssociatorCommand<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>> command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var mappings = MappingsProvider.Handle(GetArgumentAssociatorMappingsQuery.Instance);

        if (mappings.TryAddMapping(command.Parameter, command.Associator) is false)
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
