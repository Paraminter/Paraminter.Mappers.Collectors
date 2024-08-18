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
    public static IFixture<TParameter, TArgumentData> Create<TParameter, TArgumentData>()
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock = new();
        Mock<IAssociatorMapperErrorHandler<TParameter>> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        AssociatorMapper<TParameter, TArgumentData> sut = new(mappingsProviderMock.Object, errorHandlerMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mappingsProviderMock, errorHandlerMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly IQueryHandler<IGetMappedIndividualArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>> Sut;

        private readonly Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> MappingsProviderMock;
        private readonly Mock<IAssociatorMapperErrorHandler<TParameter>> ErrorHandlerMock;

        public Fixture(
            IQueryHandler<IGetMappedIndividualArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>> sut,
            Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock,
            Mock<IAssociatorMapperErrorHandler<TParameter>> errorHandlerMock)
        {
            Sut = sut;

            MappingsProviderMock = mappingsProviderMock;
            ErrorHandlerMock = errorHandlerMock;
        }

        IQueryHandler<IGetMappedIndividualArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateIndividualMappedArgumentCommand<TArgumentData>>>>> IFixture<TParameter, TArgumentData>.MappingsProviderMock => MappingsProviderMock;
        Mock<IAssociatorMapperErrorHandler<TParameter>> IFixture<TParameter, TArgumentData>.ErrorHandlerMock => ErrorHandlerMock;
    }
}
