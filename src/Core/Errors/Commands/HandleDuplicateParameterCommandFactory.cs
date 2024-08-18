namespace Paraminter.Mappers.Collectors.Errors.Commands;

using Paraminter.Parameters.Models;

internal static class HandleDuplicateParameterCommandFactory
{
    public static IHandleDuplicateParameterCommand<TParameter> Create<TParameter>(
        TParameter parameter)
        where TParameter : IParameter
    {
        return new HandleDuplicateParameterCommand<TParameter>(parameter);
    }

    private sealed class HandleDuplicateParameterCommand<TParameter>
        : IHandleDuplicateParameterCommand<TParameter>
        where TParameter : IParameter
    {
        private readonly TParameter Parameter;

        public HandleDuplicateParameterCommand(
            TParameter parameter)
        {
            Parameter = parameter;
        }

        TParameter IHandleDuplicateParameterCommand<TParameter>.Parameter => Parameter;
    }
}
