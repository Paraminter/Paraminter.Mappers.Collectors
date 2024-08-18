namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

internal static class FixtureFactory
{
    public static IFixture<TParameter, TArgumentData> Create<TParameter, TArgumentData>()
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        Mock<IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> mappingsMock = new();

        AssociatorMappingsProvider<TParameter, TArgumentData> sut = new(mappingsMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mappingsMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> Sut;

        private readonly Mock<IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> MappingsMock;

        public Fixture(
            IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> sut,
            Mock<IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> mappingsMock)
        {
            Sut = sut;

            MappingsMock = mappingsMock;
        }

        IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.MappingsMock => MappingsMock;
    }
}
