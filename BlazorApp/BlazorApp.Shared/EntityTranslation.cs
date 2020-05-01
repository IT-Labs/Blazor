namespace BlazorApp.Shared
{
    public class EntityTranslation<TEntity, TTranslation> where TEntity : class where TTranslation : class
    {
        public TEntity Entity { get; set; }
        public TTranslation Translation { get; set; }
    }
}