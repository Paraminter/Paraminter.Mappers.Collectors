namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

using Xunit;

public sealed class WasSuccessful
{
    [Fact]
    public void ReturnsTrue()
    {
        var fixture = FixtureFactory.Create<IParameter>();

        var result = Target(fixture);

        Assert.True(result);
    }

    private static bool Target<TAssociator>(
        IFixture<TAssociator> fixture)
        where TAssociator : class
    {
        return fixture.Sut.WasSuccessful;
    }
}
