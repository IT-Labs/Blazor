namespace BlazorApp.Shared.Repository
{
    public interface ITransform<TEntity, TResult> where TEntity : class where TResult : class
    {
        TResult Transform(TEntity query);
    }
}