namespace Paraminter.Mappers.Collectors.Models;

using Moq;

using Paraminter.Parameters.Models;

using System.Collections.Generic;

internal static class FixtureFactory
{
    public static IFixture<TAssociator> Create<TAssociator>()
        where TAssociator : class
    {
        Mock<TAssociator> associatorMock = new();

        Mock<IEqualityComparer<IParameter>> parameterComparerMock = new();

        parameterComparerMock.Setup(static (comparer) => comparer.Equals(It.IsAny<IParameter>(), It.IsAny<IParameter>())).Returns(true);

        IArgumentAssociatorMappings<IParameter, TAssociator> mappings = new ArgumentAssociatorMappings<IParameter, TAssociator>(parameterComparerMock.Object);

        mappings.Collector.TryAddMapping(Mock.Of<IParameter>(), associatorMock.Object);

        var sut = mappings.Mapper.TryMap(Mock.Of<IParameter>());

        return new Fixture<TAssociator>(sut, associatorMock);
    }

    private sealed class Fixture<TAssociator>
        : IFixture<TAssociator>
        where TAssociator : class
    {
        private readonly IArgumentAssociatorMapAttemptResult<TAssociator> Sut;

        private readonly Mock<TAssociator> AssociatorMock;

        public Fixture(
            IArgumentAssociatorMapAttemptResult<TAssociator> sut,
            Mock<TAssociator> associatorMock)
        {
            Sut = sut;

            AssociatorMock = associatorMock;
        }

        IArgumentAssociatorMapAttemptResult<TAssociator> IFixture<TAssociator>.Sut => Sut;

        Mock<TAssociator> IFixture<TAssociator>.AssociatorMock => AssociatorMock;
    }
}
