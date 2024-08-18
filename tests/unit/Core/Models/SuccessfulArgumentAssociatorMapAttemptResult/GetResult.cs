namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

using Xunit;

public sealed class GetResult
{
    [Fact]
    public void ReturnsAssociator()
    {
        var fixture = FixtureFactory.Create<IParameter>();

        var result = Target(fixture);

        Assert.Same(fixture.AssociatorMock.Object, result);
    }

    private static TAssociator Target<TAssociator>(
        IFixture<TAssociator> fixture)
        where TAssociator : class
    {
        return fixture.Sut.GetResult();
    }
}
