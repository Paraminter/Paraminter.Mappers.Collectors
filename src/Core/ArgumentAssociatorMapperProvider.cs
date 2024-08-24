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
    private readonly IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> Mapper;

    /// <summary>Instantiates a provider of a mapper from parameters to associators of arguments and that parameter.</summary>
    /// <param name="mapper">Maps parameters to associators of arguments and that parameter.</param>
    public ArgumentAssociatorMapperProvider(
        IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>.Handle(
        IGetArgumentAssociatorMapperQuery query)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        return Mapper;
    }
}
