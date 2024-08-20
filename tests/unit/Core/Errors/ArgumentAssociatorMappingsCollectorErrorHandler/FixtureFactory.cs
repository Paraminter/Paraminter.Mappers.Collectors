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

        var sut = new ArgumentAssociatorMappingsCollectorErrorHandler<TParameter>(duplicateParameterMock.Object);

        return new Fixture<TParameter>(sut, duplicateParameterMock);
    }

    private sealed class Fixture<TParameter>
        : IFixture<TParameter>
        where TParameter : IParameter
    {
        private readonly IArgumentAssociatorMappingsCollectorErrorHandler<TParameter> Sut;

        private readonly Mock<ICommandHandler<IHandleDuplicateParameterCommand<TParameter>>> DuplicateParameterMock;

        public Fixture(
            IArgumentAssociatorMappingsCollectorErrorHandler<TParameter> sut,
            Mock<ICommandHandler<IHandleDuplicateParameterCommand<TParameter>>> duplicateParameterMock)
        {
            Sut = sut;

            DuplicateParameterMock = duplicateParameterMock;
        }

        IArgumentAssociatorMappingsCollectorErrorHandler<TParameter> IFixture<TParameter>.Sut => Sut;

        Mock<ICommandHandler<IHandleDuplicateParameterCommand<TParameter>>> IFixture<TParameter>.DuplicateParameterMock => DuplicateParameterMock;
    }
}
