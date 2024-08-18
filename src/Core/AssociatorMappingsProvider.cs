namespace Paraminter.Mappers.Collectors;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

using System;

/// <summary>Provides a set of mappings from parameters to associators of arguments and that parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public sealed class AssociatorMappingsProvider<TParameter, TArgumentData>
    : IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    private readonly IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>> Mappings;

    /// <summary>Instantiates a provider of a set of mappings from parameters to associators of arguments and that parameter.</summary>
    /// <param name="mappings">The set of mappings from parameters to associators of arguments and that parameter.</param>
    public AssociatorMappingsProvider(
        IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>> mappings)
    {
        Mappings = mappings ?? throw new ArgumentNullException(nameof(mappings));
    }

    IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>> IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>.Handle(
        IGetArgumentAssociatorMappingsQuery query)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        return Mappings;
    }
}
