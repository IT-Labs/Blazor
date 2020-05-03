namespace Core.Shared.Repository
{
    public interface IMap<TEntity, TRequest> : IMap
        where TEntity : AuditableEntity
      //  where TRequest : IRequest
    {
        void Map(TEntity entity, TRequest request);
    }
    public interface IMap { }
}