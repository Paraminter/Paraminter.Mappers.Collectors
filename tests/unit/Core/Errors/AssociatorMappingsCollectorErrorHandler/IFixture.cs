namespace Paraminter.Mappers.Collectors.Errors;

using Moq;

using Paraminter.Cqs.Handlers;
using Paraminter.Mappers.Collectors.Errors.Commands;
using Paraminter.Parameters.Models;

internal interface IFixture<TParameter>
    where TParameter : IParameter
{
    public abstract IAssociatorMappingsCollectorErrorHandler<TParameter> Sut { get; }

    public abstract Mock<ICommandHandler<IHandleDuplicateParameterCommand<TParameter>>> DuplicateParameterMock { get; }
}
