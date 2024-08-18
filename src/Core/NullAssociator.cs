namespace Paraminter.Mappers.Collectors;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Commands;

using System;

internal sealed class NullAssociator<TArgumentData>
    : ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>
    where TArgumentData : IArgumentData
{
    public static ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>> Instance { get; } = new NullAssociator<TArgumentData>();

    private NullAssociator() { }

    void ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>.Handle(IAssociateIndividualMappedArgumentCommand<TArgumentData> command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }
    }
}
