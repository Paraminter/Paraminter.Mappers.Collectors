namespace Paraminter.Mappers.Collectors.Errors.Commands;

using Paraminter.Parameters.Models;

internal static class HandleUnmappedParameterCommandFactory
{
    public static IHandleUnmappedParameterCommand<TParameter> Create<TParameter>(
        TParameter parameter)
        where TParameter : IParameter
    {
        return new HandleUnmappedParameterCommand<TParameter>(parameter);
    }

    private sealed class HandleUnmappedParameterCommand<TParameter>
        : IHandleUnmappedParameterCommand<TParameter>
        where TParameter : IParameter
    {
        private readonly TParameter Parameter;

        public HandleUnmappedParameterCommand(
            TParameter parameter)
        {
            Parameter = parameter;
        }

        TParameter IHandleUnmappedParameterCommand<TParameter>.Parameter => Parameter;
    }
}
