namespace Paraminter.Mappers.Collectors.Errors;

using Moq;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullDuplicateParameter_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<IParameter>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsHandler()
    {
        var result = Target(Mock.Of<ICommandHandler<IHandleDuplicateParameterCommand<IParameter>>>());

        Assert.NotNull(result);
    }

    private static AssociatorMappingsCollectorErrorHandler<TParameter> Target<TParameter>(
        ICommandHandler<IHandleDuplicateParameterCommand<TParameter>> duplicateParameter)
        where TParameter : IParameter
    {
        return new AssociatorMappingsCollectorErrorHandler<TParameter>(duplicateParameter);
    }
}
