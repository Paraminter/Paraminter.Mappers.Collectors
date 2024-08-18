namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

using System;

using Xunit;

public sealed class GetResult
{
    [Fact]
    public void ThrowsInvalidOperationException()
    {
        var fixture = FixtureFactory.Create<IParameter>();

        var result = Record.Exception(() => Target(fixture));

        Assert.IsType<InvalidOperationException>(result);
    }

    private static TAssociator Target<TAssociator>(
        IFixture<TAssociator> fixture)
        where TAssociator : class
    {
        return fixture.Sut.GetResult();
    }
}
