﻿namespace Paraminter.Mappers.Collectors;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Mappers.Queries;
using Paraminter.Parameters.Models;

using System;

/// <summary>Maps parameters to associators arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public sealed class AssociatorMapper<TParameter, TArgumentData>
    : IQueryHandler<IGetMappedIndividualArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    private readonly IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> MappingsProvider;

    private readonly IAssociatorMapperErrorHandler<TParameter> ErrorHandler;

    /// <summary>Instantiates a mapper of parameters to associators of arguments and that parameter.</summary>
    /// <param name="mappingsProvider">Provides the set of mappings from parameters to associators of arguments and that parameter.</param>
    /// <param name="errorHandler">Handles encountered errors.</param>
    public AssociatorMapper(
        IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> mappingsProvider,
        IAssociatorMapperErrorHandler<TParameter> errorHandler)
    {
        MappingsProvider = mappingsProvider ?? throw new ArgumentNullException(nameof(mappingsProvider));

        ErrorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }

    ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>> IQueryHandler<IGetMappedIndividualArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>.Handle(
        IGetMappedIndividualArgumentAssociatorQuery<TParameter> query)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var mappings = MappingsProvider.Handle(GetArgumentAssociatorMappingsQuery.Instance);

        var mappingResult = mappings.TryMap(query.Parameter);

        if (mappingResult.WasSuccessful is false)
        {
            HandleUnmappedParameter(query.Parameter);

            return NullAssociator<TArgumentData>.Instance;
        }

        return mappingResult.GetResult();
    }

    private void HandleUnmappedParameter(TParameter parameter)
    {
        var command = HandleUnmappedParameterCommandFactory.Create(parameter);

        ErrorHandler.UnmappedParameter.Handle(command);
    }
}
