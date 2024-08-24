namespace Paraminter.Mappers.Collectors.Models;

using Paraminter.Parameters.Models;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IArgumentAssociatorMappings{TParameter, TAssociator}"/>
public sealed class ArgumentAssociatorMappings<TParameter, TAssociator>
    : IArgumentAssociatorMappings<TParameter, TAssociator>
    where TParameter : IParameter
{
    private readonly IArgumentAssociatorMappingsCollector<TParameter, TAssociator> Collector;
    private readonly IArgumentAssociatorMapper<TParameter, TAssociator> Mapper;

    /// <summary>Instantiates a representation of an appendable set of mappings from parameters to associators of arguments and that parameter.</summary>
    /// <param name="parameterComparer">Determines equality when comparing parameters.</param>
    public ArgumentAssociatorMappings(
        IEqualityComparer<TParameter> parameterComparer)
    {
        if (parameterComparer is null)
        {
            throw new ArgumentNullException(nameof(parameterComparer));
        }

        var mappings = new Dictionary<TParameter, TAssociator>(parameterComparer);

        Collector = new ArgumentAssociatorMappingsCollector(mappings);
        Mapper = new ArgumentAssociatorMapper(mappings);
    }

    IArgumentAssociatorMappingsCollector<TParameter, TAssociator> IArgumentAssociatorMappings<TParameter, TAssociator>.Collector => Collector;
    IArgumentAssociatorMapper<TParameter, TAssociator> IArgumentAssociatorMappings<TParameter, TAssociator>.Mapper => Mapper;

    private sealed class ArgumentAssociatorMappingsCollector
        : IArgumentAssociatorMappingsCollector<TParameter, TAssociator>
    {
        private readonly IDictionary<TParameter, TAssociator> Mappings;

        public ArgumentAssociatorMappingsCollector(
            IDictionary<TParameter, TAssociator> mappings)
        {
            Mappings = mappings;
        }

        bool IArgumentAssociatorMappingsCollector<TParameter, TAssociator>.TryAddMapping(
            TParameter parameter, TAssociator associator)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (associator is null)
            {
                throw new ArgumentNullException(nameof(associator));
            }

            if (Mappings.ContainsKey(parameter))
            {
                return false;
            }

            Mappings.Add(parameter, associator);

            return true;
        }
    }

    private sealed class ArgumentAssociatorMapper
        : IArgumentAssociatorMapper<TParameter, TAssociator>
    {
        private readonly IDictionary<TParameter, TAssociator> Mappings;

        public ArgumentAssociatorMapper(
            IDictionary<TParameter, TAssociator> mappings)
        {
            Mappings = mappings;
        }

        IArgumentAssociatorMapAttemptResult<TAssociator> IArgumentAssociatorMapper<TParameter, TAssociator>.TryMap(
            TParameter parameter)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (Mappings.TryGetValue(parameter, out var associator) is false)
            {
                return UnsuccessfulArgumentAssociatorMapAttemptResult<TAssociator>.Instance;
            }

            return new SuccessfulArgumentAssociatorMapAttemptResult<TAssociator>(associator);
        }
    }
}
