namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Commands;
using Paraminter.Mappers.Collectors.Errors;
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
        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock = new();
        Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        ArgumentAssociatorMappingsCollector<TParameter, TArgumentData> sut = new(mappingsProviderMock.Object, errorHandlerMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mappingsProviderMock, errorHandlerMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> Sut;

        private readonly Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> MappingsProviderMock;
        private readonly Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> ErrorHandlerMock;

        public Fixture(
            ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> sut,
            Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock,
            Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> errorHandlerMock)
        {
            Sut = sut;

            MappingsProviderMock = mappingsProviderMock;
            ErrorHandlerMock = errorHandlerMock;
        }

        ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> IFixture<TParameter, TArgumentData>.MappingsProviderMock => MappingsProviderMock;
        Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> IFixture<TParameter, TArgumentData>.ErrorHandlerMock => ErrorHandlerMock;
    }
}
