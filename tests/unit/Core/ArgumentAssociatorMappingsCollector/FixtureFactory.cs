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
        Mock<IQueryHandler<IGetArgumentAssociatorMappingsCollectorQuery, IArgumentAssociatorMappingsCollector<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsCollectorProviderMock = new();
        Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        ArgumentAssociatorMappingsCollector<TParameter, TArgumentData> sut = new(mappingsCollectorProviderMock.Object, errorHandlerMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mappingsCollectorProviderMock, errorHandlerMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> Sut;

        private readonly Mock<IQueryHandler<IGetArgumentAssociatorMappingsCollectorQuery, IArgumentAssociatorMappingsCollector<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> MappingsCollectorProviderMock;
        private readonly Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> ErrorHandlerMock;

        public Fixture(
            ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> sut,
            Mock<IQueryHandler<IGetArgumentAssociatorMappingsCollectorQuery, IArgumentAssociatorMappingsCollector<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsCollectorProviderMock,
            Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> errorHandlerMock)
        {
            Sut = sut;

            MappingsCollectorProviderMock = mappingsCollectorProviderMock;
            ErrorHandlerMock = errorHandlerMock;
        }

        ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IQueryHandler<IGetArgumentAssociatorMappingsCollectorQuery, IArgumentAssociatorMappingsCollector<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> IFixture<TParameter, TArgumentData>.MappingsCollectorProviderMock => MappingsCollectorProviderMock;
        Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> IFixture<TParameter, TArgumentData>.ErrorHandlerMock => ErrorHandlerMock;
    }
}
