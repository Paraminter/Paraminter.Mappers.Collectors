namespace Paraminter.Mappers.Collectors;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Models;
using Paraminter.Mappers.Collectors.Queries;
using Paraminter.Mappers.Commands;
using Paraminter.Parameters.Models;

internal interface IFixture<TParameter, TArgumentData>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    public abstract IQueryHandler<IGetArgumentAssociatorMapperQuery, IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> Sut { get; }

    public abstract Mock<IArgumentAssociatorMapper<TParameter, ICommandHandler<IAssociateSingleMappedArgumentCommand<TArgumentData>>>> MapperMock { get; }
}
