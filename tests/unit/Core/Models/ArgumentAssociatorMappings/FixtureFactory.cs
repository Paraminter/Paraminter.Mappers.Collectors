namespace Paraminter.Mappers.Collectors.Models;

using Moq;

using Paraminter.Parameters.Models;

using System.Collections.Generic;

internal static class FixtureFactory
{
    public static IFixture<TParameter, TAssociator> Create<TParameter, TAssociator>()
        where TParameter : IParameter
    {
        Mock<IEqualityComparer<TParameter>> parameterComparerMock = new();

        ArgumentAssociatorMappings<TParameter, TAssociator> sut = new(parameterComparerMock.Object);

        return new Fixture<TParameter, TAssociator>(sut, parameterComparerMock);
    }

    private sealed class Fixture<TParameter, TAssociator>
        : IFixture<TParameter, TAssociator>
        where TParameter : IParameter
    {
        private readonly IArgumentAssociatorMappings<TParameter, TAssociator> Sut;

        private readonly Mock<IEqualityComparer<TParameter>> ParameterComparerMock;

        public Fixture(
            IArgumentAssociatorMappings<TParameter, TAssociator> sut,
            Mock<IEqualityComparer<TParameter>> parameterComparerMock)
        {
            Sut = sut;

            ParameterComparerMock = parameterComparerMock;
        }

        IArgumentAssociatorMappings<TParameter, TAssociator> IFixture<TParameter, TAssociator>.Sut => Sut;

        Mock<IEqualityComparer<TParameter>> IFixture<TParameter, TAssociator>.ParameterComparerMock => ParameterComparerMock;
    }
}
