using Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Framework.Repository
{
    public class HistoryDataMapping : TypeConfiguration<HistoryData>
    {
        public override void Map(EntityTypeBuilder<HistoryData> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("h_history_tables");
        }
    }
}
