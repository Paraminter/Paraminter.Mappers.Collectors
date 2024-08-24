namespace Paraminter.Mappers.Collectors.Queries;

internal sealed class GetArgumentAssociatorMapperQuery
    : IGetArgumentAssociatorMapperQuery
{
    public static IGetArgumentAssociatorMapperQuery Instance { get; } = new GetArgumentAssociatorMapperQuery();

    private GetArgumentAssociatorMapperQuery() { }
}
