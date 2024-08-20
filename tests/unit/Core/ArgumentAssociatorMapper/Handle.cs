namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Mappers.Queries;
using Paraminter.Parameters.Models;

using System;
using System.Linq.Expressions;

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
    public void ValidQuery_SuccessfulMapping_ReturnsAssociator()
    {
        var parameter = Mock.Of<IParameter>();

        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        var associator = Mock.Of<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>();
        Mock<IArgumentAssociatorMapAttemptResult<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> attemptResultMock = new();

        Mock<IReadOnlyArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> mappingsMock = new();
        Mock<IMapParameterToSingleArgumentAssociatorQuery<IParameter>> queryMock = new();

        attemptResultMock.Setup(static (result) => result.WasSuccessful).Returns(true);
        attemptResultMock.Setup(static (result) => result.GetResult()).Returns(associator);

        mappingsMock.Setup((mappings) => mappings.TryMap(parameter)).Returns(attemptResultMock.Object);

        queryMock.Setup(static (query) => query.Parameter).Returns(parameter);

        fixture.MappingsProviderMock.Setup(static (provider) => provider.Handle(It.IsAny<IGetArgumentAssociatorMappingsQuery>())).Returns(mappingsMock.Object);

        var result = Target(fixture, queryMock.Object);

        Assert.Same(associator, result);
    }

    [Fact]
    public void ValidQuery_UnsuccessfulMapping_ReturnsAssociatorAndHandlesError()
    {
        var parameter = Mock.Of<IParameter>();

        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        Mock<IArgumentAssociatorMapAttemptResult<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> attemptResultMock = new();

        Mock<IReadOnlyArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> mappingsMock = new();
        Mock<IMapParameterToSingleArgumentAssociatorQuery<IParameter>> queryMock = new();

        attemptResultMock.Setup(static (result) => result.WasSuccessful).Returns(false);
        attemptResultMock.Setup(static (result) => result.GetResult()).Throws<InvalidOperationException>();

        mappingsMock.Setup((mappings) => mappings.TryMap(parameter)).Returns(attemptResultMock.Object);

        queryMock.Setup(static (query) => query.Parameter).Returns(parameter);

        fixture.MappingsProviderMock.Setup(static (provider) => provider.Handle(It.IsAny<IGetArgumentAssociatorMappingsQuery>())).Returns(mappingsMock.Object);

        var result = Target(fixture, queryMock.Object);

        Assert.NotNull(result);

        fixture.ErrorHandlerMock.Verify(static (handler) => handler.UnmappedParameter.Handle(It.IsAny<IHandleUnmappedParameterCommand<IParameter>>()), Times.Once());
        fixture.ErrorHandlerMock.Verify(HandleUnmappedParameterExpression(parameter), Times.Once());
    }

    private static Expression<Action<IArgumentAssociatorMapperErrorHandler<TParameter>>> HandleUnmappedParameterExpression<TParameter>(
        TParameter parameter)
        where TParameter : IParameter
    {
        return (handler) => handler.UnmappedParameter.Handle(It.Is(MatchHandleUnmappedParameterCommand(parameter)));
    }

    private static Expression<Func<IHandleUnmappedParameterCommand<TParameter>, bool>> MatchHandleUnmappedParameterCommand<TParameter>(
        TParameter parameter)
        where TParameter : IParameter
    {
        return (command) => ReferenceEquals(command.Parameter, parameter);
    }

    private static ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>> Target<TParameter, TArgumentData>(
        IFixture<TParameter, TArgumentData> fixture,
        IMapParameterToSingleArgumentAssociatorQuery<TParameter> query)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return fixture.Sut.Handle(query);
    }
}
