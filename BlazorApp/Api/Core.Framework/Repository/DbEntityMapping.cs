using BlazorApp.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Framework.Repository
{
    public class DbEntityMapping<T> : TypeConfiguration<T>
        where T : Entity
    {
        public sealed override void Map(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            AdditionalMap(builder);
        }

        public virtual void AdditionalMap(EntityTypeBuilder<T> builder)
        {

        }
    }
}
