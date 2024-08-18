namespace Paraminter.Mappers.Collectors.Models;

internal sealed class SuccessfulArgumentAssociatorMapAttemptResult<TAssociator>
    : IArgumentAssociatorMapAttemptResult<TAssociator>
{
    private readonly TAssociator Associator;

    public SuccessfulArgumentAssociatorMapAttemptResult(TAssociator associator)
    {
        Associator = associator;
    }

    bool IArgumentAssociatorMapAttemptResult<TAssociator>.WasSuccessful => true;

    TAssociator IArgumentAssociatorMapAttemptResult<TAssociator>.GetResult() => Associator;
}
