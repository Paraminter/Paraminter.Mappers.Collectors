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
            Mock.Of<IArgumentAssociatorMapperErrorHandler<IParameter>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullErrorHandler_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(
            Mock.Of<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>>>(),
            null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsMapper()
    {
        var result = Target(
            Mock.Of<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<IParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<IArgumentData>>>>>(),
            Mock.Of<IArgumentAssociatorMapperErrorHandler<IParameter>>());

        Assert.NotNull(result);
    }

    private static ArgumentAssociatorMapper<TParameter, TArgumentData> Target<TParameter, TArgumentData>(
        IQueryHandler<IGetArgumentAssociatorMappingsQuery, IReadOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> mappingsProvider,
        IArgumentAssociatorMapperErrorHandler<TParameter> errorHandler)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return new ArgumentAssociatorMapper<TParameter, TArgumentData>(mappingsProvider, errorHandler);
    }
}
