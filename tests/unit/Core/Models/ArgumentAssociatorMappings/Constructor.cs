namespace Paraminter.Mappers.Collectors.Models;

using Moq;

using Paraminter.Parameters.Models;

using System;
using System.Collections.Generic;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullParameterComparer_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<IParameter, object>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsMappings()
    {
        var result = Target<IParameter, object>(Mock.Of<IEqualityComparer<IParameter>>());

        Assert.NotNull(result);
    }

    private static ArgumentAssociatorMappings<TParameter, TAssociator> Target<TParameter, TAssociator>(
        IEqualityComparer<TParameter> parameterComparer)
        where TParameter : IParameter
    {
        return new ArgumentAssociatorMappings<TParameter, TAssociator>(parameterComparer);
    }
}
