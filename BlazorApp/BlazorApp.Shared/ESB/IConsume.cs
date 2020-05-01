namespace BlazorApp.Shared.ESB
{
    public interface IConsume
    {

    }

    public interface IConsume<T> : IConsume
        where T : IMessage
    {
        bool Handle(T message);
    }
}