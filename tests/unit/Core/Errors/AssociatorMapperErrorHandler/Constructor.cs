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
    public void NullUnmappedParameter_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<IParameter>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsHandler()
    {
        var result = Target(Mock.Of<ICommandHandler<IHandleUnmappedParameterCommand<IParameter>>>());

        Assert.NotNull(result);
    }

    private static AssociatorMapperErrorHandler<TParameter> Target<TParameter>(
        ICommandHandler<IHandleUnmappedParameterCommand<TParameter>> unmappedParameter)
        where TParameter : IParameter
    {
        return new AssociatorMapperErrorHandler<TParameter>(unmappedParameter);
    }
}
