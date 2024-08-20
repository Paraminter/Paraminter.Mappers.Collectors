namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Commands;
using Paraminter.Mappers.Collectors.Errors;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

internal interface IFixture<TParameter, TArgumentData>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    public abstract ICommandHandler<IAddSingleArgumentAssociatorMappingCommand<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> Sut { get; }

    public abstract Mock<IQueryHandler<IGetArgumentAssociatorMappingsQuery, IWriteOnlyArgumentAssociatorMappings<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>>> MappingsProviderMock { get; }
    public abstract Mock<IArgumentAssociatorMappingsCollectorErrorHandler<TParameter>> ErrorHandlerMock { get; }
}
