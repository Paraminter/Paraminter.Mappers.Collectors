namespace Paraminter.Mappers.Collectors;

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

/// <summary>Maps parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public sealed class ArgumentAssociatorMapper<TParameter, TArgumentData>
    : IQueryHandler<IMapParameterToSingleArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    private readonly IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> MapperProvider;

    private readonly IArgumentAssociatorMapperErrorHandler<TParameter> ErrorHandler;

    /// <summary>Instantiates a mapper of parameters to associators of arguments and that parameter.</summary>
    /// <param name="mapperProvider">Provides a mapper from parameters to associators of arguments and that parameter.</param>
    /// <param name="errorHandler">Handles encountered errors.</param>
    public ArgumentAssociatorMapper(
        IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mapperProvider,
        IArgumentAssociatorMapperErrorHandler<TParameter> errorHandler)
    {
        MapperProvider = mapperProvider ?? throw new ArgumentNullException(nameof(mapperProvider));

        ErrorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }

    ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>> IQueryHandler<IMapParameterToSingleArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>.Handle(
        IMapParameterToSingleArgumentAssociatorQuery<TParameter> query)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var mapper = MapperProvider.Handle(GetArgumentAssociatorMapperQuery.Instance);

        var mappingResult = mapper.TryMap(query.Parameter);

        if (mappingResult.WasSuccessful is false)
        {
            HandleUnmappedParameter(query.Parameter);

            return NullSingleArgumentAssociator<TArgumentData>.Instance;
        }

        return mappingResult.GetResult();
    }

    private void HandleUnmappedParameter(TParameter parameter)
    {
        var command = HandleUnmappedParameterCommandFactory.Create(parameter);

        ErrorHandler.UnmappedParameter.Handle(command);
    }
}
