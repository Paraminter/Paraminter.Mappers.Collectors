namespace Paraminter.Mappers.Collectors.Queries;

internal sealed class GetArgumentAssociatorMappingsCollectorQuery
    : IGetArgumentAssociatorMappingsCollectorQuery
{
    public static IGetArgumentAssociatorMappingsCollectorQuery Instance { get; } = new GetArgumentAssociatorMappingsCollectorQuery();

    private GetArgumentAssociatorMappingsCollectorQuery() { }
}
