namespace Paraminter.Mappers.Collectors.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

using Xunit;

public sealed class UnmappedParameter
{
    [Fact]
    public void ReturnsHandler()
    {
        var fixture = FixtureFactory.Create<IParameter>();

        var result = Target(fixture);

        Assert.Same(fixture.UnmappedParameterMock.Object, result);
    }

    private static ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> Target<TParameter>(
        IFixture<TParameter> fixture)
        where TParameter : IParameter
    {
        return fixture.Sut.UnmappedParameter;
    }
}
