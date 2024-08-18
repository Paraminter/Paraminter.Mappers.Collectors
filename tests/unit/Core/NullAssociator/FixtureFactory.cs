namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Mappers.Queries;
using Paraminter.Parameters.Models;

internal static class FixtureFactory
{
    public static IFixture<TArgumentData> Create<TArgumentData>()
        where TArgumentData : IArgumentData
    {
        Mock<IArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> mappingsMock = new();

        Mock<IArgumentAssociatorMapAttemptResult<ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> mappingResultMock = new();

        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock = new();
        Mock<IAssociatorMapperErrorHandler<IParameter>> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        mappingsMock.Setup(static (mappings) => mappings.TryMap(It.IsAny<IParameter>())).Returns(mappingResultMock.Object);

        mappingResultMock.Setup(static (result) => result.WasSuccessful).Returns(false);

        mappingsProviderMock.Setup(static (provider) => provider.Handle(It.IsAny<IGetArgumentAssociatorMappingsQuery>())).Returns(mappingsMock.Object);

        Mock<IGetMappedIndividualArgumentAssociatorQuery<IParameter>> queryMock = new() { DefaultValue = DefaultValue.Mock };

        IQueryHandler<IGetMappedIndividualArgumentAssociatorQuery<IParameter>, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>> mapper = new AssociatorMapper<IParameter, TArgumentData>(mappingsProviderMock.Object, errorHandlerMock.Object);

        var sut = mapper.Handle(queryMock.Object);

        return new Fixture<TArgumentData>(sut);
    }

    private sealed class Fixture<TArgumentData>
        : IFixture<TArgumentData>
        where TArgumentData : IArgumentData
    {
        private readonly ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>> Sut;

        public Fixture(
            ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>> sut)
        {
            Sut = sut;
        }

        ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>> IFixture<TArgumentData>.Sut => Sut;
    }
}
