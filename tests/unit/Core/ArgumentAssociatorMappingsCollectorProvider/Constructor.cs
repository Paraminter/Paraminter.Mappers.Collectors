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

public sealed class Constructor
{
    [Fact]
    public void NullMappingsProvider_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<IParameter, IArgumentData>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsProvider()
    {
        var result = Target(Mock.Of<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>>>());

        Assert.NotNull(result);
    }

    private static ArgumentAssociatorMappingsCollectorProvider<TParameter, TArgumentData> Target<TParameter, TArgumentData>(
        IQueryHandler<IGetArgumentAssociatorMappingsQuery, IArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mappingsProvider)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return new ArgumentAssociatorMappingsCollectorProvider<TParameter, TArgumentData>(mappingsProvider);
    }
}
