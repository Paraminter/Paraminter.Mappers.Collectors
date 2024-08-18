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
        var duplicateParameterMock = new Mock<ICommandHandler<IHandleDuplicateParameterCommand<TParameter>>>();

        var sut = new AssociatorMappingsCollectorErrorHandler<TParameter>(duplicateParameterMock.Object);

        return new Fixture<TParameter>(sut, duplicateParameterMock);
    }

    private sealed class Fixture<TParameter>
        : IFixture<TParameter>
        where TParameter : IParameter
    {
        private readonly IAssociatorMappingsCollectorErrorHandler<TParameter> Sut;

        private readonly Mock<ICommandHandler<IHandleDuplicateParameterCommand<TParameter>>> DuplicateParameterMock;

        public Fixture(
            IAssociatorMappingsCollectorErrorHandler<TParameter> sut,
            Mock<ICommandHandler<IHandleDuplicateParameterCommand<TParameter>>> duplicateParameterMock)
        {
            Sut = sut;

            DuplicateParameterMock = duplicateParameterMock;
        }

        IAssociatorMappingsCollectorErrorHandler<TParameter> IFixture<TParameter>.Sut => Sut;

        Mock<ICommandHandler<IHandleDuplicateParameterCommand<TParameter>>> IFixture<TParameter>.DuplicateParameterMock => DuplicateParameterMock;
    }
}
