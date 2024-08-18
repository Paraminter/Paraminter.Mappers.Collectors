namespace Paraminter.Mappers.Collectors.Models;

using Moq;

using Paraminter.Parameters.Models;

using System.Collections.Generic;

internal interface IFixture<TParameter, TAssociator>
    where TParameter : IParameter
{
    public abstract IArgumentAssociatorMappings<TParameter, TAssociator> Sut { get; }

    public abstract Mock<IEqualityComparer<TParameter>> ParameterComparerMock { get; }
}
