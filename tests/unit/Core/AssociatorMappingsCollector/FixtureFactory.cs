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
        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock = new();
        Mock<IAssociatorMappingsCollectorErrorHandler<TParameter>> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        AssociatorMappingsCollector<TParameter, TArgumentData> sut = new(mappingsProviderMock.Object, errorHandlerMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mappingsProviderMock, errorHandlerMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly ICommandHandler<IAddMappedArgumentAssociatorCommand<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> Sut;

        private readonly Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> MappingsProviderMock;
        private readonly Mock<IAssociatorMappingsCollectorErrorHandler<TParameter>> ErrorHandlerMock;

        public Fixture(
            ICommandHandler<IAddMappedArgumentAssociatorCommand<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> sut,
            Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock,
            Mock<IAssociatorMappingsCollectorErrorHandler<TParameter>> errorHandlerMock)
        {
            Sut = sut;

            MappingsProviderMock = mappingsProviderMock;
            ErrorHandlerMock = errorHandlerMock;
        }

        ICommandHandler<IAddMappedArgumentAssociatorCommand<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> IFixture<TParameter, TArgumentData>.MappingsProviderMock => MappingsProviderMock;
        Mock<IAssociatorMappingsCollectorErrorHandler<TParameter>> IFixture<TParameter, TArgumentData>.ErrorHandlerMock => ErrorHandlerMock;
    }
}
