namespace Paraminter.Mappers.Collectors.Models;

using System;

internal sealed class UnsuccessfulArgumentAssociatorMapAttemptResult<TAssociator>
    : IArgumentAssociatorMapAttemptResult<TAssociator>
{
    public static IArgumentAssociatorMapAttemptResult<TAssociator> Instance { get; } = new UnsuccessfulArgumentAssociatorMapAttemptResult<TAssociator>();

    private UnsuccessfulArgumentAssociatorMapAttemptResult() { }

    bool IArgumentAssociatorMapAttemptResult<TAssociator>.WasSuccessful => false;

    TAssociator IArgumentAssociatorMapAttemptResult<TAssociator>.GetResult() => throw new InvalidOperationException("Cannot retrieve the mapped associator, as the attempt was unsuccessful.");
}
