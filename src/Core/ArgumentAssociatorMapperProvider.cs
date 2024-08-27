namespace Paraminter.Mappers.Collectors;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

using System;

/// <summary>Provides a mapper of parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public sealed class ArgumentAssociatorMapperProvider<TParameter, TArgumentData>
    : IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    private readonly IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> MappingsProvider;

    /// <summary>Instantiates a provider of a mapper from parameters to associators of arguments and that parameter.</summary>
    /// <param name="mappingsProvider">Provides the mappings from parameters to associators of arguments and that parameter.</param>
    public ArgumentAssociatorMapperProvider(
        IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mappingsProvider)
    {
        MappingsProvider = mappingsProvider ?? throw new ArgumentNullException(nameof(mappingsProvider));
    }

    IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>.Handle(
        IGetArgumentAssociatorMapperQuery query)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        return MappingsProvider.Handle(GetArgumentAssociatorMappingsQuery.Instance).Mapper;
    }
}
