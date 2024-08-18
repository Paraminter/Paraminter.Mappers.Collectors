namespace Paraminter.Mappers.Collectors;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Commands;

internal interface IFixture<TArgumentData>
    where TArgumentData : IArgumentData
{
    public abstract ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>> Sut { get; }
}
