namespace Paraminter.Mappers.Collectors.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

using Xunit;

public sealed class DuplicateParameter
{
    [Fact]
    public void ReturnsHandler()
    {
        var fixture = FixtureFactory.Create<IParameter>();

        var result = Target(fixture);

        Assert.Same(fixture.DuplicateParameterMock.Object, result);
    }

    private static ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> Target<TParameter>(
        IFixture<TParameter> fixture)
        where TParameter : IParameter
    {
        return fixture.Sut.DuplicateParameter;
    }
}
