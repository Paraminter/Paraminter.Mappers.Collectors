namespace Paraminter.Mappers.Collectors;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Commands;

using System;

internal sealed class NullSingleArgumentAssociator<TArgumentData>
    : ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>
    where TArgumentData : IArgumentData
{
    public static ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>> Instance { get; } = new NullSingleArgumentAssociator<TArgumentData>();

    private NullSingleArgumentAssociator() { }

    void ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>.Handle(
        IAssociateSingleMappedArgumentCommand<TArgumentData> command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }
    }
}
