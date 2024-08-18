namespace Paraminter.Mappers.Collectors.Models;

using Moq;

internal interface IFixture<TAssociator>
    where TAssociator : class
{
    public abstract IArgumentAssociatorMapAttemptResult<TAssociator> Sut { get; }

    public abstract Mock<TAssociator> AssociatorMock { get; }
}
