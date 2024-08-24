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
        Mock<IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mapperMock = new();

        ArgumentAssociatorMapperProvider<TParameter, TArgumentData> sut = new(mapperMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mapperMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> Sut;

        private readonly Mock<IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> MapperMock;

        public Fixture(
            IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> sut,
            Mock<IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mapperMock)
        {
            Sut = sut;

            MapperMock = mapperMock;
        }

        IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.MapperMock => MapperMock;
    }
}
