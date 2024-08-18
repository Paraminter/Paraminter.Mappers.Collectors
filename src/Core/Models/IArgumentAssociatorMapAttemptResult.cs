namespace Paraminter.Mappers.Collectors.Models;

using System;

/// <summary>Represents the result of an attempt to map a parameter to an associator of arguments and that parameter.</summary>
/// <typeparam name="TAssociator">The type representing the associators.</typeparam>
public interface IArgumentAssociatorMapAttemptResult<out TAssociator>
{
    /// <summary>Indicates whether the attempt was successful.</summary>
    public abstract bool WasSuccessful { get; }

    /// <summary>Retrieves the resulting associator of arguments and the parameter, or throws an <see cref="InvalidOperationException"/> if the attempt was unsuccessful.</summary>
    /// <returns>The resulting associator of arguments and the parameter, if the attempt was successful.</returns>
    public abstract TAssociator GetResult();
}
