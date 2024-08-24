namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

using Xunit;

public sealed class Mapper
{
    [Fact]
    public void ReturnsMapper()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        var result = Target(fixture);

        Assert.NotNull(result);
    }

    private static IArgumentAssociatorMapper<TParameter, TAssociator> Target<TParameter, TAssociator>(
        IFixture<TParameter, TAssociator> fixture)
        where TParameter : IParameter
    {
        return fixture.Sut.Mapper;
    }
}
