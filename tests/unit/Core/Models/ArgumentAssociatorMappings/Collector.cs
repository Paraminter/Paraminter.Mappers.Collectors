namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

using Xunit;

public sealed class Collector
{
    [Fact]
    public void ReturnsCollector()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        var result = Target(fixture);

        Assert.NotNull(result);
    }

    private static IArgumentAssociatorMappingsCollector<TParameter, TAssociator> Target<TParameter, TAssociator>(
        IFixture<TParameter, TAssociator> fixture)
        where TParameter : IParameter
    {
        return fixture.Sut.Collector;
    }
}
