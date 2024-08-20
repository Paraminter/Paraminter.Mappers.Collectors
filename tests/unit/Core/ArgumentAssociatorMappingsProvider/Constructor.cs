namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullMappings_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<IParameter, IArgumentData>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsProvider()
    {
        var result = Target(Mock.Of<IArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>>());

        Assert.NotNull(result);
    }

    private static ArgumentAssociatorMappingsProvider<TParameter, TArgumentData> Target<TParameter, TArgumentData>(
        IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> mappings)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return new ArgumentAssociatorMappingsProvider<TParameter, TArgumentData>(mappings);
    }
}
