namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Commands;
using Paraminter.Mappers.Collectors.Errors;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

using System;
using System.Linq.Expressions;

using Xunit;

public sealed class Handle
{
    [Fact]
    public void NullCommand_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidCommand_TrueReturningModel_AddsMapping()
    {
        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        var parameter = Mock.Of<IParameter>();
        var associator = Mock.Of<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>();

        Mock<IArgumentAssociatorMappingsCollector<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> mappingsCollectorMock = new();
        Mock<IAddSingleArgumentAssociatorMappingCommand<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> commandMock = new();

        mappingsCollectorMock.Setup(static (collector) => collector.TryAddMapping(It.IsAny<IParameter>(), It.IsAny<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>())).Returns(true);

        commandMock.Setup(static (command) => command.Parameter).Returns(parameter);
        commandMock.Setup(static (command) => command.Associator).Returns(associator);

        fixture.MappingsCollectorProviderMock.Setup(static (provider) => provider.Handle(It.IsAny<IGetArgumentAssociatorMappingsCollectorQuery>())).Returns(mappingsCollectorMock.Object);

        Target(fixture, commandMock.Object);

        mappingsCollectorMock.Verify(static (collector) => collector.TryAddMapping(It.IsAny<IParameter>(), It.IsAny<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>()), Times.Once());
        mappingsCollectorMock.Verify((collector) => collector.TryAddMapping(parameter, associator), Times.Once());
    }

    [Fact]
    public void ValidCommand_FalseReturningModel_AddsMappingAndHandlesError()
    {
        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        var parameter = Mock.Of<IParameter>();
        var associator = Mock.Of<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>();

        Mock<IArgumentAssociatorMappingsCollector<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> mappingsCollectorMock = new();
        Mock<IAddSingleArgumentAssociatorMappingCommand<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>> commandMock = new();

        mappingsCollectorMock.Setup(static (collector) => collector.TryAddMapping(It.IsAny<IParameter>(), It.IsAny<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>())).Returns(false);

        commandMock.Setup(static (command) => command.Parameter).Returns(parameter);
        commandMock.Setup(static (command) => command.Associator).Returns(associator);

        fixture.MappingsCollectorProviderMock.Setup(static (provider) => provider.Handle(It.IsAny<IGetArgumentAssociatorMappingsCollectorQuery>())).Returns(mappingsCollectorMock.Object);

        Target(fixture, commandMock.Object);

        mappingsCollectorMock.Verify(static (collector) => collector.TryAddMapping(It.IsAny<IParameter>(), It.IsAny<ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>()), Times.Once());
        mappingsCollectorMock.Verify((collector) => collector.TryAddMapping(parameter, associator), Times.Once());

        fixture.ErrorHandlerMock.Verify(static (handler) => handler.DuplicateParameter.Handle(It.IsAny<IHandleDuplicateParameterCommand<IParameter>>()), Times.Once());
        fixture.ErrorHandlerMock.Verify(HandleDuplicateParameterExpression(parameter), Times.Once());
    }

    private static Expression<Action<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>>> HandleDuplicateParameterExpression<TParameter>(
        TParameter parameter)
        where TParameter : IParameter
    {
        return (handler) => handler.DuplicateParameter.Handle(It.Is(MatchHandleDuplicateParameterCommand(parameter)));
    }

    private static Expression<Func<IHandleDuplicateParameterCommand<TParameter>, bool>> MatchHandleDuplicateParameterCommand<TParameter>(
        TParameter parameter)
        where TParameter : IParameter
    {
        return (command) => ReferenceEquals(command.Parameter, parameter);
    }

    private static void Target<TParameter, TArgumentData>(
        IFixture<TParameter, TArgumentData> fixture,
        IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> command)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        fixture.Sut.Handle(command);
    }
}
