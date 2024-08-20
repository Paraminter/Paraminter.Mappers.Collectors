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
        Mock<IArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mappingsMock = new();

        Mock<IArgumentAssociatorMapAttemptResult<ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mappingResultMock = new();

        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock = new();
        Mock<IArgumentAssociatorMapperErrorHandler<IParameter>> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        mappingsMock.Setup(static (mappings) => mappings.TryMap(It.IsAny<IParameter>())).Returns(mappingResultMock.Object);

        mappingResultMock.Setup(static (result) => result.WasSuccessful).Returns(false);

        mappingsProviderMock.Setup(static (provider) => provider.Handle(It.IsAny<IGetArgumentAssociatorMappingsQuery>())).Returns(mappingsMock.Object);

        Mock<IMapParameterToSingleArgumentAssociatorQuery<IParameter>> queryMock = new() { DefaultValue = DefaultValue.Mock };

        IQueryHandler<IMapParameterToSingleArgumentAssociatorQuery<IParameter>, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> mapper = new ArgumentAssociatorMapper<IParameter, TArgumentData>(mappingsProviderMock.Object, errorHandlerMock.Object);

        var sut = mapper.Handle(queryMock.Object);

        return new Fixture<TArgumentData>(sut);
    }

    private sealed class Fixture<TArgumentData>
        : IFixture<TArgumentData>
        where TArgumentData : IArgumentData
    {
        private readonly ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>> Sut;

        public Fixture(
            ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>> sut)
        {
            Sut = sut;
        }

        ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>> IFixture<TArgumentData>.Sut => Sut;
    }
}
