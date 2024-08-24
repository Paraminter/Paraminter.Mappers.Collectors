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
    public void NullMappingsCollector_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<IParameter, IArgumentData>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsProvider()
    {
        var result = Target(Mock.Of<IArgumentAssociatorMappingsCollector<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>>());

        Assert.NotNull(result);
    }

    private static ArgumentAssociatorMappingsCollectorProvider<TParameter, TArgumentData> Target<TParameter, TArgumentData>(
        IArgumentAssociatorMappingsCollector<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>> mappingsCollector)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return new ArgumentAssociatorMappingsCollectorProvider<TParameter, TArgumentData>(mappingsCollector);
    }
}
