using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Framework.Repository
{
    public abstract class TypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
