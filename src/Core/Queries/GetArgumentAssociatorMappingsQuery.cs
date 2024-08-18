namespace Paraminter.Mappers.Collectors.Queries;

internal sealed class GetArgumentAssociatorMappingsQuery
    : IGetArgumentAssociatorMappingsQuery
{
    public static IGetArgumentAssociatorMappingsQuery Instance { get; } = new GetArgumentAssociatorMappingsQuery();

    private GetArgumentAssociatorMappingsQuery() { }
}
