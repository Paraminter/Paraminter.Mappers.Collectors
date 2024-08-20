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
        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock = new();
        Mock<IArgumentAssociatorMapperErrorHandler<TParameter>> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        ArgumentAssociatorMapper<TParameter, TArgumentData> sut = new(mappingsProviderMock.Object, errorHandlerMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mappingsProviderMock, errorHandlerMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly IQueryHandler<IMapParameterToSingleArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> Sut;

        private readonly Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> MappingsProviderMock;
        private readonly Mock<IArgumentAssociatorMapperErrorHandler<TParameter>> ErrorHandlerMock;

        public Fixture(
            IQueryHandler<IMapParameterToSingleArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> sut,
            Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> mappingsProviderMock,
            Mock<IArgumentAssociatorMapperErrorHandler<TParameter>> errorHandlerMock)
        {
            Sut = sut;

            MappingsProviderMock = mappingsProviderMock;
            ErrorHandlerMock = errorHandlerMock;
        }

        IQueryHandler<IMapParameterToSingleArgumentAssociatorQuery<TParameter>, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> IFixture<TParameter, TArgumentData>.MappingsProviderMock => MappingsProviderMock;
        Mock<IArgumentAssociatorMapperErrorHandler<TParameter>> IFixture<TParameter, TArgumentData>.ErrorHandlerMock => ErrorHandlerMock;
    }
}
