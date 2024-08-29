namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

using System;

using Xunit;

public sealed class Handle
{
    [Fact]
    public void NullQuery_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidQuery_ReturnsCollector()
    {
        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        var collector = Mock.Of<IArgumentAssociatorMappingsCollector<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>>();

        Mock<IArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> mappingsMock = new();

        mappingsMock.Setup(static (mappings) => mappings.Collector).Returns(collector);

        fixture.MappingsProviderMock.Setup(static (provider) => provider.Handle(It.IsAny<IGetArgumentAssociatorMappingsQuery>())).Returns(mappingsMock.Object);

        var result = Target(fixture, Mock.Of<IGetArgumentAssociatorMappingsCollectorQuery>());

        Assert.Same(collector, result);
    }

    private static IArgumentAssociatorMappingsCollector<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> Target<TParameter, TArgumentData>(
        IFixture<TParameter, TArgumentData> fixture,
        IGetArgumentAssociatorMappingsCollectorQuery query)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return fixture.Sut.Handle(query);
    }
}
