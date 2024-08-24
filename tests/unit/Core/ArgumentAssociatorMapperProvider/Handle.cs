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
    public void ValidQuery_ReturnsMappings()
    {
        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        var result = Target(fixture, Mock.Of<IGetArgumentAssociatorMapperQuery>());

        Assert.Same(fixture.MapperMock.Object, result);
    }

    private static IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> Target<TParameter, TArgumentData>(
        IFixture<TParameter, TArgumentData> fixture,
        IGetArgumentAssociatorMapperQuery query)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return fixture.Sut.Handle(query);
    }
}
