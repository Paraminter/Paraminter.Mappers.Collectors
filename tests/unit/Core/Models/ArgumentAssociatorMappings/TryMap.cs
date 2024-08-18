namespace Paraminter.Mappers.Collectors.Models;

using Moq;

using Paraminter.Parameters.Models;

using System;

using Xunit;

public sealed class TryMap
{
    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_UnsuccessfullyMapped_ReturnsUnsuccessfulResult()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        fixture.ParameterComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<IParameter>(), It.IsAny<IParameter>())).Returns(false);

        fixture.Sut.TryAddMapping(Mock.Of<IParameter>(), Mock.Of<object>());

        var result = Target(fixture, Mock.Of<IParameter>());

        Assert.False(result.WasSuccessful);
    }

    [Fact]
    public void ValidArguments_SuccessfullyMapped_ReturnsSuccessfulResult()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        var associator = Mock.Of<object>();

        fixture.ParameterComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<IParameter>(), It.IsAny<IParameter>())).Returns(true);

        fixture.Sut.TryAddMapping(Mock.Of<IParameter>(), associator);

        var result = Target(fixture, Mock.Of<IParameter>());

        Assert.True(result.WasSuccessful);
        Assert.Same(associator, result.GetResult());
    }

    private static IArgumentAssociatorMapAttemptResult<TAssociator> Target<TParameter, TAssociator>(
        IFixture<TParameter, TAssociator> fixture,
        TParameter parameter)
        where TParameter : IParameter
    {
        return fixture.Sut.TryMap(parameter);
    }
}
