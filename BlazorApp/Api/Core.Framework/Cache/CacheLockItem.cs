namespace Core.Framework.Cache
{
    internal class CacheLockItem
    {
        public object Lock { get; } = new object();
    }
}