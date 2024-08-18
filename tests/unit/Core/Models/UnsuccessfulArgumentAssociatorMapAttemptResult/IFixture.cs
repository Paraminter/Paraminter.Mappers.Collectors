namespace Paraminter.Mappers.Collectors.Models;

internal interface IFixture<TAssociator>
    where TAssociator : class
{
    public abstract IArgumentAssociatorMapAttemptResult<TAssociator> Sut { get; }
}
