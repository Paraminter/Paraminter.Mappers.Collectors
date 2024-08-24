namespace Paraminter.Mappers.Collectors.Models;

using Moq;

using Paraminter.Parameters.Models;

using System;

using Xunit;

public sealed class Collector_TryAddMapping
{
    [Fact]
    public void NullParameter_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        var result = Record.Exception(() => Target(fixture, null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullAssociator_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        var result = Record.Exception(() => Target(fixture, Mock.Of<IParameter>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_NotAlreadyMapped_ReturnsTrue()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        fixture.ParameterComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<IParameter>(), It.IsAny<IParameter>())).Returns(false);

        fixture.Sut.Collector.TryAddMapping(Mock.Of<IParameter>(), Mock.Of<object>());

        var result = Target(fixture, Mock.Of<IParameter>(), Mock.Of<object>());

        Assert.True(result);
    }

    [Fact]
    public void ValidArguments_AlreadyMapped_ReturnsFalse()
    {
        var fixture = FixtureFactory.Create<IParameter, object>();

        fixture.ParameterComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<IParameter>(), It.IsAny<IParameter>())).Returns(true);

        fixture.Sut.Collector.TryAddMapping(Mock.Of<IParameter>(), Mock.Of<object>());

        var result = Target(fixture, Mock.Of<IParameter>(), Mock.Of<object>());

        Assert.False(result);
    }

    private static bool Target<TParameter, TAssociator>(
        IFixture<TParameter, TAssociator> fixture,
        TParameter parameter,
        TAssociator associator)
        where TParameter : IParameter
    {
        return fixture.Sut.Collector.TryAddMapping(parameter, associator);
    }
}
