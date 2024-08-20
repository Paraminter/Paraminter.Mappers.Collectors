namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors;
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
        var result = Record.Exception(() => Target<IParameter, IArgumentData>(
            null!,
            Mock.Of<IArgumentAssociatorMappingsCollectorErrorHandler<IParameter>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullErrorHandler_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(
            Mock.Of<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>>>(),
            null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsCollector()
    {
        var result = Target(
            Mock.Of<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>>>(),
            Mock.Of<IArgumentAssociatorMappingsCollectorErrorHandler<IParameter>>());

        Assert.NotNull(result);
    }

    private static ArgumentAssociatorMappingsCollector<TParameter, TArgumentData> Target<TParameter, TArgumentData>(
        IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mappingsProvider,
        IArgumentAssociatorMappingsCollectorErrorHandler<TParameter> errorHandler)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return new ArgumentAssociatorMappingsCollector<TParameter, TArgumentData>(mappingsProvider, errorHandler);
    }
}
