namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Mappers.Commands;

using System;

using Xunit;

public sealed class Handle
{
    [Fact]
    public void NullCommand_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<IArgumentData>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_NullAction()
    {
        var fixture = FixtureFactory.Create<IArgumentData>();

        var result = Record.Exception(() => Target(fixture, Mock.Of<IAssociateSingleMappedArgumentCommand<IArgumentData>>()));

        Assert.Null(result);
    }

    private static void Target<TArgumentData>(
        IFixture<TArgumentData> fixture,
        IAssociateSingleMappedArgumentCommand<TArgumentData> command)
        where TArgumentData : IArgumentData
    {
        fixture.Sut.Handle(command);
    }
}
