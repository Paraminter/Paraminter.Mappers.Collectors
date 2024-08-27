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
        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock = new();

        ArgumentAssociatorMapperProvider<TParameter, TArgumentData> sut = new(mappingsProviderMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mappingsProviderMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> Sut;

        private readonly Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> MappingsProviderMock;

        public Fixture(
            IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> sut,
            Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock)
        {
            Sut = sut;

            MappingsProviderMock = mappingsProviderMock;
        }

        IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> IFixture<TParameter, TArgumentData>.MappingsProviderMock => MappingsProviderMock;
    }
}
