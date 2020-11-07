namespace Ntrada.Extensions.RabbitMq.Contexts
{
    internal sealed class NullContextBuilder : IContextBuilder
    {
        public object Build(ExecutionData executionData) => null;
    }
}