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

        var sut = new ArgumentAssociatorMapperErrorHandler<TParameter>(unmappedParameterMock.Object);

        return new Fixture<TParameter>(sut, unmappedParameterMock);
    }

    private sealed class Fixture<TParameter>
        : IFixture<TParameter>
        where TParameter : IParameter
    {
        private readonly IArgumentAssociatorMapperErrorHandler<TParameter> Sut;

        private readonly Mock<ICommandHandler<IHandleUnmappedParameterCommand<TParameter>>> UnmappedParameterMock;

        public Fixture(
            IArgumentAssociatorMapperErrorHandler<TParameter> sut,
            Mock<ICommandHandler<IHandleUnmappedParameterCommand<TParameter>>> unmappedParameterMock)
        {
            Sut = sut;

            UnmappedParameterMock = unmappedParameterMock;
        }

        IArgumentAssociatorMapperErrorHandler<TParameter> IFixture<TParameter>.Sut => Sut;

        Mock<ICommandHandler<IHandleUnmappedParameterCommand<TParameter>>> IFixture<TParameter>.UnmappedParameterMock => UnmappedParameterMock;
    }
}
