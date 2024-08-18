namespace Paraminter.Mappers.Collectors.Models;

using Moq;

using Paraminter.Parameters.Models;

using System.Collections.Generic;

internal static class FixtureFactory
{
    public static IFixture<TAssociator> Create<TAssociator>()
        where TAssociator : class
    {
        IArgumentAssociatorMappings<IParameter, TAssociator> mappings = new ArgumentAssociatorMappings<IParameter, TAssociator>(Mock.Of<IEqualityComparer<IParameter>>());

        var sut = mappings.TryMap(Mock.Of<IParameter>());

        return new Fixture<TAssociator>(sut);
    }

    private sealed class Fixture<TAssociator>
        : IFixture<TAssociator>
        where TAssociator : class
    {
        private readonly IArgumentAssociatorMapAttemptResult<TAssociator> Sut;

        public Fixture(
            IArgumentAssociatorMapAttemptResult<TAssociator> sut)
        {
            Sut = sut;
        }

        IArgumentAssociatorMapAttemptResult<TAssociator> IFixture<TAssociator>.Sut => Sut;
    }
}
