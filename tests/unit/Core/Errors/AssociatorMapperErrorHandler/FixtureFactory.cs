namespace Paraminter.Mappers.Collectors.Errors;

using Moq;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

internal static class FixtureFactory
{
    public static IFixture<TParameter> Create<TParameter>()
        where TParameter : IParameter
    {
        var unmappedParameterMock = new Mock<ICommandHandler<IHandleUnmappedParameterCommand<TParameter>>>();

        var sut = new AssociatorMapperErrorHandler<TParameter>(unmappedParameterMock.Object);

        return new Fixture<TParameter>(sut, unmappedParameterMock);
    }

    private sealed class Fixture<TParameter>
        : IFixture<TParameter>
        where TParameter : IParameter
    {
        private readonly IAssociatorMapperErrorHandler<TParameter> Sut;

        private readonly Mock<ICommandHandler<IHandleUnmappedParameterCommand<TParameter>>> UnmappedParameterMock;

        public Fixture(
            IAssociatorMapperErrorHandler<TParameter> sut,
            Mock<ICommandHandler<IHandleUnmappedParameterCommand<TParameter>>> unmappedParameterMock)
        {
            Sut = sut;

            UnmappedParameterMock = unmappedParameterMock;
        }

        IAssociatorMapperErrorHandler<TParameter> IFixture<TParameter>.Sut => Sut;

        Mock<ICommandHandler<IHandleUnmappedParameterCommand<TParameter>>> IFixture<TParameter>.UnmappedParameterMock => UnmappedParameterMock;
    }
}
